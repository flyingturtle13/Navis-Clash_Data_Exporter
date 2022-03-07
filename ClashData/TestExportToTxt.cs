using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
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
using GridIntersectionCoord;
using ClashData;

//-----For Navisworks 2019-----//
namespace ClashTxtExport //Created by Carlo Caparas
{

    public class TestExportToTxt : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            //Obtain user Input for Focus Zone (calls UserInput Form)
            UserInput UIReturn = new UserInput();
            UIReturn.ShowDialog();

            string fz = UIReturn.Returnfz;
            Dictionary<string, string> trade = UIReturn.Returnpd;

            if (fz == "" || trade == null)
            {
                MessageBox.Show("Cancelled Operation");
                return 0;
            }

            //Initialize Objects
            List<string> testName = new List<string>();
            List<int> resultNew = new List<int>();
            List<int> resultActive = new List<int>();
            List<int> resultReviewed = new List<int>();
            List<int> resultApproved = new List<int>();
            List<int> resultResolved = new List<int>();
            List<string> testDate = new List<string>();
            List<string> fileName = new List<string>();
            List<string> sumTestDate = new List<string>();

            List<string> tradeStatus = new List<string>();
            List<string> tradeClash = new List<string>();
            List<string> tradeDiscipline1 = new List<string>();
            List<string> tradeDiscipline2 = new List<string>();
            List<string> tradeDate = new List<string>();
            List<string> tradeFile = new List<string>();
            List<string> tradeAll = new List<string>();
            List<string> clashAssignTo = new List<string>();
            List<string> clashApprovedBy = new List<string>();
            List<string> clashApproveTime = new List<string>();
            List<string> clashDescription = new List<string>();
            List<string> discipline = new List<string>();
            List<string> indiTest = new List<string>();
            List<string> itemAGUID = new List<string>();
            List<string> itemBGUID = new List<string>();
            List<double> indiCoordX = new List<double>();
            List<double> indiCoordY = new List<double>();
            List<double> indiCoordZ = new List<double>();
            List<string> focusZone = new List<string>();
            List<string> level = new List<string>();
            List<double> lvlElev = new List<double>();
            List<string> clashLevel = new List<string>();
            List<double> gridXMinCoord = new List<double>();
            List<double> gridXMaxCoord = new List<double>();
            List<double> gridYMinCoord = new List<double>();
            List<double> gridYMaxCoord = new List<double>();

            string tradeName1 = "";
            string tradeName2 = "";

            int countNew = 0;
            int countActive = 0;
            int countReviewed = 0;
            int countApproved = 0;
            int countResolved = 0;

            try
            {
                Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
                DocumentClash documentClash = document.GetClash();
                DocumentClashTests allTests = documentClash.TestsData;
                DocumentModels docModel = document.Models;

                DocumentGrids docGrids = document.Grids;
                GridSystem docGridSys = docGrids.ActiveSystem;

                foreach (GridLevel lvl in docGridSys.Levels)
                {
                    level.Add(lvl.DisplayName);
                    lvlElev.Add(lvl.Elevation);
                }

                //-----------------------------------------------------------------------------------------//
                //Check if Clash Test have even been created
                //If no clash tests created, exit program
                int check = allTests.Tests.Count;
                if (check == 0)
                {
                    MessageBox.Show("No clash tests currently exist!");
                    return 0;
                }
                //-----------------------------------------------------------------------------------------//

                //-----------------------------------------------------------------------------------------//
                //Begin storing clash data by created tests
                foreach (ClashTest test in allTests.Tests)
                {
                    testName.Add(test.DisplayName);

                    //Reset Clash Status counts per test in Clash Detective Summary.  Matches Results tab in Clash Detective (ungrouped clashes)
                    countNew = 0;
                    countActive = 0;
                    countReviewed = 0;
                    countApproved = 0;
                    countResolved = 0;

                    if (test.LastRun == null)
                        sumTestDate.Add("No Test Runs");
                    else
                        sumTestDate.Add(test.LastRun.Value.ToShortDateString());

                    //Count number of instances per Clash Status 
                    //(based on how user grouped clashes)
                    foreach (SavedItem issue in test.Children)
                    {
                        ClashResultGroup group = issue as ClashResultGroup;

                        //Check if clash groups exist.  If null, clash result is not grouped
                        if (null != group)
                        {
                            foreach (SavedItem subissue in group.Children)
                            {
                                ClashResult item = subissue as ClashResult;

                                //Checking if Item1 is null (due to resolved) and need to use Selection-A
                                if (item.Item1 != null)
                                {
                                    List<ModelItem> lItem1 = item.Item1.Ancestors.ToList();
                                    itemAGUID.Add(item.Item1.Ancestors.First.InstanceGuid.ToString());

                                    tradeName1 = ClashDiscipline_Search(lItem1, trade); //go to line 516 - searches for appropriate discipline by discipline code
                                }
                                else
                                {

                                    ModelItemCollection oSelA = test.SelectionA.Selection.GetSelectedItems();
                                    List<ModelItem> lItemA = new List<ModelItem>();

                                    if (oSelA.First.HasModel == true)
                                    {
                                        lItemA.Add(oSelA.First);
                                        itemAGUID.Add(oSelA.First.InstanceGuid.ToString());
                                    }
                                    else
                                    {
                                        lItemA = oSelA.First.Ancestors.ToList();
                                        itemAGUID.Add(oSelA.First.Ancestors.First.InstanceGuid.ToString());
                                    }

                                    tradeName1 = ClashDiscipline_Search(lItemA, trade);//go to line 808 - searches for appropriate discipline by discipline code
                                }

                                //Checking if Item2 is null (due to resolved) and need to use Selection-B
                                if (item.Item2 != null)
                                {
                                    List<ModelItem> lItem2 = item.Item2.Ancestors.ToList();
                                    itemBGUID.Add(item.Item2.Ancestors.First.InstanceGuid.ToString());

                                    tradeName2 = ClashDiscipline_Search(lItem2, trade);//go to line 808 - searches for appropriate discipline by discipline code
                                }
                                else
                                {

                                    ModelItemCollection oSelB = test.SelectionB.Selection.GetSelectedItems();
                                    List<ModelItem> lItemB = new List<ModelItem>();

                                    if (oSelB.First.HasModel == true)
                                    {
                                        lItemB.Add(oSelB.First);
                                        itemBGUID.Add(oSelB.First.InstanceGuid.ToString());
                                    }
                                    else
                                    {
                                        lItemB = oSelB.First.Ancestors.ToList();
                                        itemBGUID.Add(oSelB.First.Ancestors.First.InstanceGuid.ToString());
                                    }

                                    tradeName2 = ClashDiscipline_Search(lItemB, trade);
                                }

                                //Prompt User when no Discipline match found
                                //User may be missing a discipline/trade in initial input
                                //if (tradeName1 == "" || tradeName2 == "")
                                //{
                                //    MessageBox.Show("Discipline Missing.  Check Project Disciplines Input File (.txt)." + "\n"
                                //        + "Clash Test: " + test.DisplayName + "\n"
                                //        + "Clash Name: " + item.DisplayName + "\n"
                                //        + "Discipline 1: " + tradeName1 + "\n"
                                //        + "Discipline 2: " + tradeName2);

                                //    return 0;
                                //}

                                //Store Individual Clash Data
                                testDate.Add(test.LastRun.Value.ToShortDateString());
                                indiTest.Add(test.DisplayName);
                                focusZone.Add(fz);
                                tradeDiscipline1.Add(tradeName1);
                                tradeDiscipline2.Add(tradeName2);
                                tradeClash.Add(item.DisplayName.ToString());
                                tradeStatus.Add(item.Status.ToString());
                                indiCoordX.Add(item.Center.X);
                                indiCoordY.Add(item.Center.Y);
                                indiCoordZ.Add(item.Center.Z);
                                fileName.Add(document.CurrentFileName.ToString());
                                clashAssignTo.Add(item.AssignedTo);
                                clashApprovedBy.Add(item.ApprovedBy);
                                clashApproveTime.Add(item.ApprovedTime.ToString());
                                clashDescription.Add(item.Description);

                                if (test.LastRun == null)
                                {
                                    testDate.Add("Test Not Run");
                                }
                                else
                                {
                                    tradeDate.Add(test.LastRun.Value.ToShortDateString());
                                }

                                tradeFile.Add(document.CurrentFileName.ToString());
                            }
                        }
                        else
                        {
                            ClashResult rawItem = issue as ClashResult;

                            //Checking if Item1 is null (due to resolved) and need to use Selection-A
                            if (rawItem.Item1 != null)
                            {
                                List<ModelItem> lItem1 = rawItem.Item1.Ancestors.ToList();
                                itemAGUID.Add(rawItem.Item1.Ancestors.First.InstanceGuid.ToString());

                                tradeName1 = ClashDiscipline_Search(lItem1, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }
                            else
                            {
                                ModelItemCollection oSelA = test.SelectionA.Selection.GetSelectedItems();
                                List<ModelItem> lItemA = new List<ModelItem>();

                                if (oSelA.First.HasModel == true)
                                {
                                    lItemA.Add(oSelA.First);
                                    itemAGUID.Add(oSelA.First.InstanceGuid.ToString());
                                }
                                else
                                {
                                    lItemA = oSelA.First.Ancestors.ToList();
                                    itemAGUID.Add(oSelA.First.Ancestors.First.InstanceGuid.ToString());
                                }

                                tradeName1 = ClashDiscipline_Search(lItemA, trade);  //go to line 516 - searches for appropriate discipline by discipline code
                            }

                            //Checking if Item1 is null (due to resolved) and need to use Selection-B
                            if (rawItem.Item2 != null)
                            {
                                List<ModelItem> lItem2 = rawItem.Item2.Ancestors.ToList();
                                itemBGUID.Add(rawItem.Item2.Ancestors.First.InstanceGuid.ToString());
                                tradeName2 = ClashDiscipline_Search(lItem2, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }
                            else
                            {
                                ModelItemCollection oSelB = test.SelectionB.Selection.GetSelectedItems();
                                List<ModelItem> lItemB = new List<ModelItem>();

                                if (oSelB.First.HasModel == true)
                                {
                                    lItemB.Add(oSelB.First);
                                    itemBGUID.Add(oSelB.First.InstanceGuid.ToString());
                                }
                                else
                                {
                                    lItemB = oSelB.First.Ancestors.ToList();
                                    itemBGUID.Add(oSelB.First.Ancestors.First.InstanceGuid.ToString());
                                }

                                tradeName2 = ClashDiscipline_Search(lItemB, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }

                            //if (tradeName1 == "" || tradeName2 == "")
                            //{
                            //    MessageBox.Show("Discipline Missing.  Check Project Disciplines Input File (.txt)." + "\n"
                            //        + "Clash Test: " + test.DisplayName + "\n"
                            //        + "Clash Name: " + rawItem.DisplayName + "\n"
                            //        + "Discipline 1: " + tradeName1 + "\n"
                            //        + "Discipline 2: " + tradeName2);

                            //    return 0;
                            //}

                            //write to second sheet by discipline involvement
                            testDate.Add(test.LastRun.Value.ToShortDateString());
                            indiTest.Add(test.DisplayName);
                            focusZone.Add(fz);
                            tradeDiscipline1.Add(tradeName1);
                            tradeDiscipline2.Add(tradeName2);
                            tradeClash.Add(rawItem.DisplayName.ToString());
                            tradeStatus.Add(rawItem.Status.ToString());
                            tradeFile.Add(document.CurrentFileName.ToString());
                            indiCoordX.Add(rawItem.Center.X);
                            indiCoordY.Add(rawItem.Center.Y);
                            indiCoordZ.Add(rawItem.Center.Z);
                            fileName.Add(document.CurrentFileName.ToString());
                            clashAssignTo.Add(rawItem.AssignedTo);
                            clashApprovedBy.Add(rawItem.ApprovedBy);
                            clashApproveTime.Add(rawItem.ApprovedTime.ToString());
                            clashDescription.Add(rawItem.Description);
                        }
                    }

                    //inputs values into Clash Status List by Test
                    resultNew.Add(countNew);
                    resultActive.Add(countActive);
                    resultReviewed.Add(countReviewed);
                    resultApproved.Add(countApproved);
                    resultResolved.Add(countResolved);
                }
                //-----------------------------------------------------------------------------------------//
                //call grid intersection function to return min and max grid coordinate values
                GridIntersectCoord gridValueReturn = new GridIntersectCoord();

                var gridCoordValues = gridValueReturn.GridCoord();

                double gridXMin = gridCoordValues.gridXMin;
                double gridXMax = gridCoordValues.gridXMax;
                double gridYMin = gridCoordValues.gridYMin;
                double gridYMax = gridCoordValues.gridYMax;

                foreach (string clash in tradeClash)
                {
                    gridXMinCoord.Add(gridXMin);
                    gridXMaxCoord.Add(gridXMax);
                    gridYMinCoord.Add(gridYMin);
                    gridYMaxCoord.Add(gridYMax);
                }
                //-----------------------------------------------------------------------------------------//

                //-----------------------------------------------------------------------------------------//
                //Record level clashes occur for Clash Level
                //lvlElev = actual level elevation of level name (level)
                int clashIdx = 0;

                while (clashIdx < indiCoordZ.Count) //indiCoordZ = List of clash elevations
                {
                    int lvlIdx = 0; //resets level to lowest
                    bool lvlAssign = false; //flag for if clash elevation assigned a level identity

                    while (lvlIdx < level.Count && lvlAssign == false)
                    {
                        //determines if clash on intermediate level
                        if (lvlIdx != level.Count - 1 && indiCoordZ[clashIdx] >= lvlElev[lvlIdx] && indiCoordZ[clashIdx] < lvlElev[lvlIdx + 1])
                        {
                            clashLevel.Add(level[lvlIdx]);
                            lvlAssign = true;
                        }
                        //determines if clash occurs highest level
                        else if (lvlIdx == level.Count - 1 && indiCoordZ[clashIdx] >= lvlElev[lvlIdx])
                        {
                            clashLevel.Add(level[lvlIdx]);
                            lvlAssign = true;
                        }
                        //determines if clash occurs below lowest level
                        else if (lvlIdx == 0 && indiCoordZ[clashIdx] < lvlElev[lvlIdx])
                        {
                            clashLevel.Add("UNDERGROUND");
                            lvlAssign = true;
                        }
                        lvlIdx++;
                    }
                    clashIdx++;
                }

                //-----------------------------------------------------------------------------------------//



                //-----------------------------------------------------------------------------------------//

                // Export to Txt File

                List<string> header = new List<string>{"Date", "Focus Zone", "Test Name", "Discipline 1", "Discipline 2", "Clash", "Clash Level",
                    "Status", "Clash Location (X)", "Clash Location (Y)", "Clash Location (Z)", "Min X Grid Coordinate", "Min Y Grid Coordinate",
                    "Max X Grid Coordinate", "Max Y Grid Coordinate", "File Path", "Assigned To", "Approved By", "Approved Time", "Description",
                    "Discipline 1 GUID", "Discipline 2 GUID"};

                try
                {
                    //TxtIdxCounter = 0;
                    //GETS CURRENT DATE AND FORMATS FOR DEFAULT FILE NAME
                    string exportYr = DateTime.Now.Year.ToString();
                    string exportMonth = DateTime.Now.Month.ToString();
                    string exportDay = DateTime.Now.Day.ToString();

                    if (exportMonth.Length == 1)
                    {
                        exportMonth = "0" + exportMonth;
                    }

                    if (exportDay.Length == 1)
                    {
                        exportDay = "0" + exportDay;
                    }

                    string exportDate = exportYr + exportMonth + exportDay;

                    //CREATES NEW VARIABLE INSTANCE - ALLOWS TO OPEN WINDOWS EXPLORER SAVE FILE PROMPT
                    string filename = "";
                    System.Windows.Forms.SaveFileDialog saveExportData = new System.Windows.Forms.SaveFileDialog();

                    //SETS FILE TYPE TO BE SAVED AS .TXT
                    saveExportData.Title = "Save to...";
                    saveExportData.Filter = "Text Documents | *.txt";
                    saveExportData.FileName = exportDate + "-Clash_Data";

                    //OPENS WINDOWS EXPLORER TO BEGIN LIST SAVE PROCESS
                    if (saveExportData.ShowDialog() == DialogResult.OK)
                    {
                        filename = saveExportData.FileName.ToString();

                        //CHECKS USER HAS INPUTED A NAME FOR THE FILE
                        if (filename != "")
                        {
                            using (StreamWriter sw = new StreamWriter(filename))
                            {
                                //WRITES COLUMN HEADERS TO TXT FILE
                                foreach (string title in header)
                                {
                                    if (header.IndexOf(title) == 0)
                                    {
                                        sw.Write(title.ToString());
                                    }
                                    else
                                    {
                                        sw.Write("^" + title.ToString());
                                    }
                                }
                                sw.WriteLine("");

                                //WRITE EACH CLASH DATA TO TXT FILE
                                for (int i = 0; i < tradeDate.Count; i++)
                                {

                                    sw.Write(tradeDate[i]);
                                    sw.Write("^" + focusZone[i]);
                                    sw.Write("^" + indiTest[i]);
                                    sw.Write("^" + tradeDiscipline1[i]);
                                    sw.Write("^" + tradeDiscipline2[i]);
                                    sw.Write("^" + tradeClash[i]);
                                    sw.Write("^" + clashLevel[i]);
                                    sw.Write("^" + tradeStatus[i]);
                                    sw.Write("^" + indiCoordX[i]);
                                    sw.Write("^" + indiCoordY[i]);
                                    sw.Write("^" + indiCoordZ[i]);
                                    sw.Write("^" + gridXMinCoord[i]);
                                    sw.Write("^" + gridYMinCoord[i]);
                                    sw.Write("^" + gridXMaxCoord[i]);
                                    sw.Write("^" + gridYMaxCoord[i]);
                                    sw.Write("^" + tradeFile[i]);
                                    sw.Write("^" + clashAssignTo[i]);
                                    sw.Write("^" + clashApprovedBy[i]);
                                    sw.Write("^" + clashApproveTime[i]);
                                    sw.Write("^" + clashDescription[i]);
                                    sw.Write("^" + itemAGUID[i]);
                                    sw.Write("^" + itemBGUID[i]);
                                    sw.WriteLine("");
                                }

                                sw.Dispose();
                                sw.Close();
                            }

                        }
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error Writing in Txt File!  Original Message: " + exception.Message);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Writing in Txt File!  Original Message: " + exception.Message);
            }

            return 0;
        }


        //Iterates through models to find match for discipline/trade clash involvement
        public string ClashDiscipline_Search(List<ModelItem> itemList, Dictionary<string, string> trade)
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
                            //MessageBox.Show("iTradeValue: " + iTradeValue);
                        }
                    }
                }
            }
            return (iTradeValue);
        }
    }
}

