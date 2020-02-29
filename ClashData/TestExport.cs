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
using System.Windows.Forms;
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
namespace ClashData //Created by Carlo Caparas
{

    public class ClashDataExport : AddInPlugin
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
                //MessageBox.Show("Cancelled Operation");
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

                                    tradeName1 = ClashDiscipline_Search(lItem1, trade); //go to line 808 - searches for appropriate discipline by discipline code
                                }
                                else
                                {

                                    ModelItemCollection oSelA = test.SelectionA.Selection.GetSelectedItems();
                                    List<ModelItem> lItemA = new List<ModelItem>();

                                    if (oSelA.First.HasModel == true)
                                    {
                                        lItemA.Add(oSelA.First);
                                    }
                                    else
                                    {
                                        lItemA = oSelA.First.Ancestors.ToList();
                                    }

                                    tradeName1 = ClashDiscipline_Search(lItemA, trade);//go to line 808 - searches for appropriate discipline by discipline code
                                }

                                //Checking if Item2 is null (due to resolved) and need to use Selection-B
                                if (item.Item2 != null)
                                {
                                    List<ModelItem> lItem2 = item.Item2.Ancestors.ToList();

                                    tradeName2 = ClashDiscipline_Search(lItem2, trade);//go to line 808 - searches for appropriate discipline by discipline code
                                }
                                else
                                {

                                    ModelItemCollection oSelB = test.SelectionB.Selection.GetSelectedItems();
                                    List<ModelItem> lItemB = new List<ModelItem>();
                                    //MessageBox.Show(oSelB.First.DisplayName);

                                    if (oSelB.First.HasModel == true)
                                    {
                                        lItemB.Add(oSelB.First);
                                    }
                                    else
                                    {
                                        //MessageBox.Show("flag 2B");
                                        lItemB = oSelB.First.Ancestors.ToList();
                                    }

                                    tradeName2 = ClashDiscipline_Search(lItemB, trade);
                                }

                                //Prompt User when no Discipline match found
                                //User may be missing a discipline/trade in initial input
                                if (tradeName1 == "" || tradeName2 == "")
                                {
                                    MessageBox.Show("Discipline Missing.  Check Project Disciplines Input File (.txt)." + "\n"
                                        + "Clash Test: " + test.DisplayName + "\n"
                                        + "Clash Name: " + item.DisplayName + "\n"
                                        + "Discipline 1: " + tradeName1 + "\n"
                                        + "Discipline 2: " + tradeName2);

                                    return 0;
                                }

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

                                //for Clash Summary
                                if (null != item && item.Status.ToString() == "New")
                                {
                                    countNew = countNew + 1;
                                }
                                else if (null != item && item.Status.ToString() == "Active")
                                {
                                    countActive = countActive + 1;
                                }
                                else if (null != item && item.Status.ToString() == "Reviewed")
                                {
                                    countReviewed = countReviewed + 1;
                                }
                                else if (null != item && item.Status.ToString() == "Approved")
                                {
                                    countApproved = countApproved + 1;
                                }
                                else
                                {
                                    countResolved = countResolved + 1;
                                }
                            }
                        }
                        else
                        {
                            ClashResult rawItem = issue as ClashResult;

                            //Checking if Item1 is null (due to resolved) and need to use Selection-A
                            if (rawItem.Item1 != null)
                            {
                                List<ModelItem> lItem1 = rawItem.Item1.Ancestors.ToList();

                                tradeName1 = ClashDiscipline_Search(lItem1, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }
                            else
                            {
                                ModelItemCollection oSelA = test.SelectionA.Selection.GetSelectedItems();
                                List<ModelItem> lItemA = new List<ModelItem>();

                                if (oSelA.First.HasModel == true)
                                {
                                    lItemA.Add(oSelA.First);
                                }
                                else
                                {
                                    lItemA = oSelA.First.Ancestors.ToList();
                                }

                                tradeName1 = ClashDiscipline_Search(lItemA, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }

                            //Checking if Item1 is null (due to resolved) and need to use Selection-B
                            if (rawItem.Item2 != null)
                            {
                                List<ModelItem> lItem2 = rawItem.Item2.Ancestors.ToList();

                                tradeName2 = ClashDiscipline_Search(lItem2, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }
                            else
                            {
                                ModelItemCollection oSelB = test.SelectionB.Selection.GetSelectedItems();
                                List<ModelItem> lItemB = new List<ModelItem>();

                                if (oSelB.First.HasModel == true)
                                {
                                    lItemB.Add(oSelB.First);
                                }
                                else
                                {
                                    lItemB = oSelB.First.Ancestors.ToList();
                                }

                                tradeName2 = ClashDiscipline_Search(lItemB, trade);  //go to line 808 - searches for appropriate discipline by discipline code
                            }

                            if (tradeName1 == "" || tradeName2 == "")
                            {
                                MessageBox.Show("Discipline Missing.  Check Project Disciplines Input File (.txt)." + "\n"
                                    + "Clash Test: " + test.DisplayName + "\n"
                                    + "Clash Name: " + rawItem.DisplayName + "\n"
                                    + "Discipline 1: " + tradeName1 + "\n"
                                    + "Discipline 2: " + tradeName2);

                                return 0;
                            }

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

                            if (test.LastRun == null)
                            {
                                testDate.Add("Test Not Run");
                            }
                            else
                            {
                                tradeDate.Add(test.LastRun.Value.ToShortDateString());
                            }

                            if (rawItem.Status.ToString() == "New")
                            {
                                countNew = countNew + 1;
                            }
                            else if (rawItem.Status.ToString() == "Active")
                            {
                                countActive = countActive + 1;
                            }
                            else if (rawItem.Status.ToString() == "Reviewed")
                            {
                                countReviewed = countReviewed + 1;
                            }
                            else if (rawItem.Status.ToString() == "Approved")
                            {
                                countApproved = countApproved + 1;
                            }
                            else
                            {
                                countResolved = countResolved + 1;
                            }

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
                //Totals current Open(New + Active), Closed(Resolved + Approved), Field Coordinate(Reviewed)
                int totOpen = resultNew.Aggregate((a, b) => a + b) + resultActive.Aggregate((a, b) => a + b);
                int totClosed = resultResolved.Aggregate((a, b) => a + b) + resultApproved.Aggregate((a, b) => a + b);
                int totReviewed = resultReviewed.Aggregate((a, b) => a + b);
                int totNew = resultNew.Aggregate((a, b) => a + b);
                int totActive = resultActive.Aggregate((a, b) => a + b);
                int totApproved = resultApproved.Aggregate((a, b) => a + b);
                int totResolved = resultResolved.Aggregate((a, b) => a + b);
                //-----------------------------------------------------------------------------------------//

                //-----------------------------------------------------------------------------------------//
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
                Excel.Worksheet xlWorksheet_trade = (Excel.Worksheet)xlWorkbook.Worksheets.Add();
                xlWorksheet.Name = "Clash Detective Summary";
                xlWorksheet_trade.Name = "Individual Clashes";

                //Label Column Headers - Summary Worksheet
                xlWorksheet.Cells[1, 1] = "Test Date";
                xlWorksheet.Cells[1, 2] = "Test Name";
                xlWorksheet.Cells[1, 3] = "New";
                xlWorksheet.Cells[1, 4] = "Active";
                xlWorksheet.Cells[1, 5] = "Reviewed";
                xlWorksheet.Cells[1, 6] = "Approved";
                xlWorksheet.Cells[1, 7] = "Resolved";
                xlWorksheet.Cells[1, 9] = "Total New";
                xlWorksheet.Cells[1, 10] = "Total Active";
                xlWorksheet.Cells[1, 11] = "Total Reviewed";
                xlWorksheet.Cells[1, 12] = "Total Approved";
                xlWorksheet.Cells[1, 13] = "Total Resolved";
                xlWorksheet.Cells[1, 15] = "Total Open (New + Active)";
                xlWorksheet.Cells[1, 16] = "Total Closed (Approved + Resolved)";
                xlWorksheet.Cells[1, 17] = "Total Deferred to Field Coordination (Reviewed)";

                //Label Column Headers - Worksheet By Trade Involvement
                xlWorksheet_trade.Cells[1, 1] = "Date";
                xlWorksheet_trade.Cells[1, 2] = "Focus Zone";
                xlWorksheet_trade.Cells[1, 3] = "Test Name";
                xlWorksheet_trade.Cells[1, 4] = "Discipline 1";
                xlWorksheet_trade.Cells[1, 5] = "Discipline 2";
                xlWorksheet_trade.Cells[1, 6] = "Clash";
                xlWorksheet_trade.Cells[1, 7] = "Clash Level";
                xlWorksheet_trade.Cells[1, 8] = "Status";
                xlWorksheet_trade.Cells.Cells[1, 9] = "Clash Location (X)";
                xlWorksheet_trade.Cells.Cells[1, 10] = "Clash Location (Y)";
                xlWorksheet_trade.Cells.Cells[1, 11] = "Clash Location (Z)";
                xlWorksheet_trade.Cells.Cells[1, 12] = "Min X Grid Coordinate";
                xlWorksheet_trade.Cells.Cells[1, 13] = "Min Y Grid Coordinate";
                xlWorksheet_trade.Cells.Cells[1, 14] = "Max X Grid Coordinate";
                xlWorksheet_trade.Cells.Cells[1, 15] = "Max Y Grid Coordinate";
                xlWorksheet_trade.Cells.Cells[1, 16] = "File Path";
                xlWorksheet_trade.Cells.Cells[1, 17] = "Assigned To";
                xlWorksheet_trade.Cells.Cells[1, 18] = "Approved By";
                xlWorksheet_trade.Cells.Cells[1, 19] = "Approved Time";
                xlWorksheet_trade.Cells.Cells[1, 20] = "Description";


                //write clash statuses to excel file by Test
                int counterSumDate = 2;
                foreach (string name in sumTestDate)
                {
                    string cellName = "A" + counterSumDate.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = name;
                    counterSumDate++;
                }

                int counterTest = 2;
                foreach (string name in testName)
                {
                    string cellName = "B" + counterTest.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = name;
                    counterTest++;
                }

                int counterNew = 2;
                foreach (int valueNew in resultNew)
                {
                    string cellName = "C" + counterNew.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueNew;
                    counterNew++;
                }

                int counterActive = 2;
                foreach (int valueActive in resultActive)
                {
                    string cellName = "D" + counterActive.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueActive;
                    counterActive++;
                }

                int counterReviewed = 2;
                foreach (int valueReviewed in resultReviewed)
                {
                    string cellName = "E" + counterReviewed.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueReviewed;
                    counterReviewed++;
                }

                int counterApproved = 2;
                foreach (int valueApproved in resultApproved)
                {
                    string cellName = "F" + counterApproved.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueApproved;
                    counterApproved++;
                }

                int counterResolved = 2;
                foreach (int valueResolved in resultResolved)
                {
                    string cellName = "G" + counterResolved.ToString();
                    var range = xlWorksheet.get_Range(cellName, cellName);
                    range.Value2 = valueResolved;
                    counterResolved++;
                }

                //write totals Open, Closed, Field Coordinate to Cells
                xlWorksheet.Cells[2, 9] = totNew;
                xlWorksheet.Cells[2, 10] = totActive;
                xlWorksheet.Cells[2, 11] = totReviewed;
                xlWorksheet.Cells[2, 12] = totApproved;
                xlWorksheet.Cells[2, 13] = totResolved;
                xlWorksheet.Cells[2, 15] = totOpen;
                xlWorksheet.Cells[2, 16] = totClosed;
                xlWorksheet.Cells[2, 17] = totReviewed;

                //Complete Data on Worksheet (per Discipline Clash Involvement)
                int tradeDateCount = 2;
                foreach (string date in tradeDate)
                {
                    string cellName = "A" + tradeDateCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = date;
                    tradeDateCount++;
                }

                int counterFz = 2;
                foreach (string valueFz in focusZone)
                {
                    string cellName = "B" + counterFz.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = valueFz;
                    counterFz++;
                }

                int testNameCount = 2;
                foreach (string tn in indiTest)
                {
                    string cellName = "C" + testNameCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = tn;
                    testNameCount++;
                }

                int dis1Count = 2;
                foreach (string dis1 in tradeDiscipline1)
                {
                    string cellName = "D" + dis1Count.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = dis1;
                    dis1Count++;
                }

                int dis2Count = 2;
                foreach (string dis2 in tradeDiscipline2)
                {
                    string cellName = "E" + dis2Count.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = dis2;
                    dis2Count++;
                }

                int tradeClashCount = 2;
                foreach (string clash in tradeClash)
                {
                    string cellName = "F" + tradeClashCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = clash;
                    tradeClashCount++;
                }

                int levelCount = 2;
                foreach (string lvl in clashLevel)
                {
                    string cellName = "G" + levelCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = lvl;
                    levelCount++;
                }

                int tradeStatusCount = 2;
                foreach (string status in tradeStatus)
                {
                    string cellName = "H" + tradeStatusCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = status;
                    tradeStatusCount++;
                }

                int coordXCount = 2;
                foreach (double x in indiCoordX)
                {
                    string cellName = "I" + coordXCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = x;
                    coordXCount++;
                }

                int coordYCount = 2;
                foreach (double y in indiCoordY)
                {
                    string cellName = "J" + coordYCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = y;
                    coordYCount++;
                }

                int coordZCount = 2;
                foreach (double z in indiCoordZ)
                {
                    string cellName = "K" + coordZCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = z;
                    coordZCount++;
                }

                int xMinCount = 2;
                foreach (double xMin in gridXMinCoord)
                {
                    string cellName = "L" + xMinCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = xMin;
                    xMinCount++;
                }

                int yMinCount = 2;
                foreach (double yMin in gridYMinCoord)
                {
                    string cellName = "M" + yMinCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = yMin;
                    yMinCount++;
                }

                int xMaxCount = 2;
                foreach (double xMax in gridXMaxCoord)
                {
                    string cellName = "N" + xMaxCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = xMax;
                    xMaxCount++;
                }

                int yMaxCount = 2;
                foreach (double yMax in gridYMaxCoord)
                {
                    string cellName = "O" + yMaxCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = yMax;
                    yMaxCount++;
                }

                int tradeFileCount = 2;
                foreach (string file in tradeFile)
                {
                    string cellName = "P" + tradeFileCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = file;
                    tradeFileCount++;
                }

                int assignToCount = 2;
                foreach (string assign in clashAssignTo)
                {
                    string cellName = "Q" + assignToCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = assign;
                    assignToCount++;
                }

                int approvedByCount = 2;
                foreach (string approve in clashApprovedBy)
                {
                    string cellName = "R" + approvedByCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = approve;
                    approvedByCount++;
                }

                int approveTimeCount = 2;
                foreach (string time in clashApproveTime)
                {
                    string cellName = "S" + approveTimeCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = time;
                    approveTimeCount++;
                }

                int descriptionCount = 2;
                foreach (string description in clashDescription)
                {
                    string cellName = "T" + descriptionCount.ToString();
                    var range = xlWorksheet_trade.get_Range(cellName, cellName);
                    range.Value2 = description;
                    descriptionCount++;
                }

                //Locate file save location
                string[] clashDate = tradeDate[0].Split('/');
                string modDate = "";

                if (clashDate[0].Length == 1)
                {
                    clashDate[0] = "0" + clashDate[0];
                }

                if (clashDate[1].Length == 1)
                {
                    clashDate[1] = "0" + clashDate[1];
                }
                modDate = clashDate[2] + clashDate[0] + clashDate[1];


                SaveFileDialog saveClashData = new SaveFileDialog();

                saveClashData.Title = "Save to...";
                saveClashData.Filter = "Excel Workbook | *.xlsx|Excel 97-2003 Workbook | *.xls";
                saveClashData.FileName = modDate + "-Clash_Test_Data-" + focusZone[0].ToString();

                if (saveClashData.ShowDialog() == DialogResult.OK)
                {
                    string path = saveClashData.FileName;
                    xlWorkbook.SaveCopyAs(path);
                    xlWorkbook.Saved = true;
                    xlWorkbook.Close(true, Missing.Value, Missing.Value);
                    xlApp.Quit();
                }

                xlApp.Visible = false;
                //-----------------------------------------------------------------------------------------//
            }

            catch (Exception exception)
            {
                MessageBox.Show("Error! Check if clash test(s) exist or previously run.  Original Message: " + exception.Message);
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

