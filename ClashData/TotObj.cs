using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.DocumentParts;
using Autodesk.Navisworks.Api.Clash;
using Autodesk.Navisworks.Internal.ApiImplementation;
using Autodesk.Navisworks.Api.Automation;
using Autodesk.Navisworks.Api.Plugins;
using UserInput_Form;
using ClashData;

//-----For Navisworks 2019-----//
namespace TotalObjects //Created by Carlo Caparas
{
    public class TotObj : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {

            //Obtain user Input for Focus Zone (calls UserInput Form)
            UserInput UIReturn = new UserInput();
            UIReturn.ShowDialog();

            string fz = UIReturn.Returnfz;
            Dictionary<string, string> modDiscipline = UIReturn.Returnpd;

            if (fz == "" || modDiscipline == null)
            {
                //MessageBox.Show("Cancelled Operation");
                return 0;
            }

            Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            DocumentClash documentClash = document.GetClash();
            DocumentClashTests allTests = documentClash.TestsData;
            DocumentModels docModel = document.Models;

            List<string> discipline = new List<string>();
            List<int> objTot = new List<int>();
            List<string> tradeAll = new List<string>();
            List<string> focusZone = new List<string>();
            List<string> testDate = new List<string>();
            List<string> fileName = new List<string>();

            Dictionary<string, int> objCnt = new Dictionary<string, int>();
            foreach (string disValue in modDiscipline.Values)
            {
                objCnt.Add(disValue, 0);
                //Add discipline to list by object
                tradeAll.Add(disValue);
            }

            objCnt.Add("Misc", 0);
            tradeAll.Add("Misc");

            try
            {
                string date = "";

                if (allTests.Tests.Count != 0)
                {
                    //Record last date of clash test run
                    ClashTest test = allTests.Tests[0] as ClashTest;

                    if (test.LastRun == null)
                        date = "No Test Runs";
                    else
                        date = test.LastRun.Value.ToShortDateString();
                }
                else
                {
                    date = "No Test Runs";
                }

                //Record file name
                string file = document.CurrentFileName.ToString();

                if (file == "")
                {
                    file = "File not yet saved.";
                }

                if (docModel.Count != 0)
                {
                    //Count total objects in project
                    foreach (Model model in docModel)
                    {
                        ModelItem root = model.RootItem as ModelItem;

                        foreach (ModelItem item in root.Children)
                        {
                            List<ModelItem> dList = item.DescendantsAndSelf.ToList();

                            foreach (ModelItem subItem in dList)
                            {
                                if (subItem.IsComposite == true || subItem.ClassDisplayName == "Block")
                                {
                                    List<ModelItem> aList = subItem.Ancestors.ToList();

                                    string tradeName = Discipline_Search(aList, modDiscipline);

                                    if (objCnt.ContainsKey(tradeName))
                                    {
                                        objCnt[tradeName] += 1;
                                    }
                                    else
                                    {
                                        objCnt["Misc"] += 1;
                                    }
                                }
                                else if (subItem.IsLayer == true)
                                {
                                    foreach (ModelItem obj in subItem.Children)
                                    {
                                        if (obj.IsInsert == false && obj.IsComposite == false && obj.IsCollection == false && obj.ClassDisplayName != "Block")
                                        {
                                            List<ModelItem> aList = subItem.Ancestors.ToList();

                                            string tradeName = Discipline_Search(aList, modDiscipline);

                                            if (objCnt.ContainsKey(tradeName))
                                            {
                                                objCnt[tradeName] += 1;
                                            }
                                            else
                                            {
                                                objCnt["Misc"] += 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No models currently appended in project." + "\n"
                        + "Load models first.");
                    return 0;
                }

                //Create Discipline list from each clash by discipline involvement 
                foreach (string name in tradeAll)
                {
                    if (!discipline.Contains(name))
                    {
                        discipline.Add(name);
                    }
                }

                //Record user inputted focus zone to total rows
                int fzIdx = 0;
                while (fzIdx < discipline.Count)
                {
                    focusZone.Add(fz);
                    fzIdx++;
                }

                //Record last Test run date to match #rows
                int idxDate = 0;
                while (idxDate < discipline.Count)
                {
                    testDate.Add(date);
                    idxDate++;
                }

                //Record file name to match # of rows
                int idxFile = 0;
                while (idxFile < discipline.Count)
                {
                    fileName.Add(file);
                    idxFile++;
                }

                //Add total trade object counts to objTot List in order of discipline List
                foreach (string name in discipline)
                {
                    if (objCnt.ContainsKey(name))
                    {
                        objTot.Add(objCnt[name]);
                    }
                }

                //Launch or access Excel via COM Interop:
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook;

                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!");
                }

                //Create New Workbook & Worksheets
                xlWorkbook = xlApp.Workbooks.Add(Missing.Value);
                Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets.get_Item(1);
                xlWorksheet.Name = "Total Objects by Trade";

                //Label Column Headers - Total Objects Worksheet
                xlWorksheet.Cells[1, 1] = "Date";
                xlWorksheet.Cells[1, 2] = "Discipline";
                xlWorksheet.Cells[1, 3] = "Total Objects";
                xlWorksheet.Cells[1, 4] = "Focus Zone";
                xlWorksheet.Cells[1, 5] = "File";

                int counterDate = 2;
                foreach (string day in testDate)
                {
                    string cellName = "A" + counterDate.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = day;
                    counterDate++;
                }

                int counterDis = 2;
                foreach (string valueDis in discipline)
                {
                    string cellName = "B" + counterDis.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueDis;
                    counterDis++;
                }

                int counterObj = 2;
                foreach (int valueObj in objTot)
                {
                    string cellName = "C" + counterObj.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueObj;
                    counterObj++;
                }

                int counterFz = 2;
                foreach (string valueFz in focusZone)
                {
                    string cellName = "D" + counterFz.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueFz;
                    counterFz++;
                }

                int fileNameCount = 2;
                foreach (string fName in fileName)
                {
                    string cellName = "E" + fileNameCount.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = fName;
                    fileNameCount++;
                }

                //Locate Excel file save location
                string modDate = "";

                if (allTests.Tests.Count != 0)
                {
                    string[] clashDate = testDate[0].Split('/');

                    if (clashDate[0].Length == 1)
                    {
                        clashDate[0] = "0" + clashDate[0];
                    }

                    if (clashDate[1].Length == 1)
                    {
                        clashDate[1] = "0" + clashDate[1];
                    }
                    modDate = clashDate[2] + clashDate[0] + clashDate[1];
                }
                else
                {
                    modDate = "YYYYMMDD";
                }

                System.Windows.Forms.SaveFileDialog saveClashData = new System.Windows.Forms.SaveFileDialog();

                saveClashData.Title = "Save to...";
                saveClashData.Filter = "Excel Workbook | *.xlsx|Excel 97-2003 Workbook | *.xls";
                saveClashData.FileName = modDate + "-Total_Objects_Data-" + focusZone[0].ToString();

                if (saveClashData.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = saveClashData.FileName;
                    xlWorkbook.SaveCopyAs(path);
                    xlWorkbook.Saved = true;
                    xlWorkbook.Close(true, Missing.Value, Missing.Value);
                    xlApp.Quit();
                }

                xlApp.Visible = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error! Original Message: " + exception.Message);
            }

            return 0;
        }

        //Iterates through models to find match for discipline/trade clash involvement
        public string Discipline_Search(List<ModelItem> itemList, Dictionary<string, string> trade)
        {
            string iTradeValue = "";

            foreach (ModelItem lItem in itemList)
            {
                string[] valName = lItem.DisplayName.Split('_', '-', '.', ' ');

                if (valName.Last() == "nwd" || valName.Last() == "rvt" || valName.Last() == "nwc" || valName.Last() == "dwg" || valName.Last() == "ifc" ||
                    valName.Last() == "NWD" || valName.Last() == "RVT" || valName.Last() == "NWC" || valName.Last() == "DWG" || valName.Last() == "IFC")
                {
                    foreach (string strItem in valName)
                    {
                        if (trade.ContainsKey(strItem))
                        {
                            iTradeValue = trade[strItem];
                            //MessageBox.Show(iTradeValue);
                        }
                    }
                }
            }
            return (iTradeValue);
        }
    }
}
