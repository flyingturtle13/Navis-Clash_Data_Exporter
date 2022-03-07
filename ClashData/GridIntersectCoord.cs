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
namespace GridIntersectionCoord  //Created by Carlo Caparas, Last Updated: 12.20.2018
{
    class GridIntersectCoord
    {
        //using Tuple to return values
        public (double gridXMin, double gridXMax, double gridYMin, double gridYMax) GridCoord()
        {
            Document document = Autodesk.Navisworks.Api.Application.ActiveDocument;
            DocumentClash documentClash = document.GetClash();
            DocumentClashTests allTests = documentClash.TestsData;
            DocumentModels docModel = document.Models;

            DocumentGrids docGrids = document.Grids;
            GridSystem docGridSys = docGrids.ActiveSystem;

            List<double> gridXCoord = new List<double>();
            List<double> gridYCoord = new List<double>();

            try
            {
                //get objects in project
                foreach (Model model in docModel)
                {
                    ModelItem root = model.RootItem as ModelItem;

                    string dn = root.DisplayName.ToString();
                    string[] disName = dn.Split('_', '-', '.', ' ');

                    //determine source file type by searching model file properties
                    foreach (PropertyCategory oPC in root.PropertyCategories)
                    {
                        if (oPC.DisplayName.ToString() == "Item")
                        {
                            foreach (DataProperty oDP in oPC.Properties)
                            {
                                if (oDP.DisplayName.ToString() == "Source File Name")
                                {
                                    string val = oDP.Value.ToDisplayString();
                                    string[] valName = val.Split('.');

                                    //source file is RVT (Revit)
                                    if (valName.Last() == "rvt")
                                    {
                                        foreach (ModelItem item in root.Children)
                                        {
                                            ModelItem subLayer2 = item as ModelItem;

                                            foreach (ModelItem subLaye3 in subLayer2.Children)
                                            {
                                                ModelItem subLayer4 = subLaye3 as ModelItem;

                                                foreach (ModelItem subLayer5 in subLayer4.Children)
                                                {
                                                    ModelItem subLayer6 = subLayer5 as ModelItem;

                                                    foreach (ModelItem subLayer7 in subLayer6.Children)
                                                    {
                                                        ModelItem subLayer8 = subLayer7 as ModelItem;

                                                        foreach (ModelItem subLayer9 in subLayer8.Children)
                                                        {
                                                            //Get object center position (X,Y,Z) by setting a 3D bounding box
                                                            ModelItem subLayer10 = subLayer9 as ModelItem;

                                                            if (subLayer10 != null)
                                                            {
                                                                BoundingBox3D bbox = subLayer10.BoundingBox();

                                                                //find closest grid intersection to object center position using API
                                                                GridIntersection gridCross = docGridSys.ClosestIntersection(bbox.Center);

                                                                //Get closest grid intersection X,Y coord
                                                                gridXCoord.Add(gridCross.Position.X);
                                                                gridYCoord.Add(gridCross.Position.Y);
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //if source file is DWG format (AutoCAD)
                                    else if (valName.Last() == "dwg")
                                    {
                                        foreach (ModelItem item in root.Children)
                                        {
                                            ModelItem subLayer2 = item as ModelItem;

                                            foreach (ModelItem subLayer3 in subLayer2.Children)
                                            {
                                                if (subLayer3 != null)
                                                {
                                                    //Get object center position (X,Y,Z) by setting a 3D bounding box
                                                    BoundingBox3D bbox = subLayer3.BoundingBox();

                                                    //find closest grid intersection to object center position using API
                                                    GridIntersection gridCross = docGridSys.ClosestIntersection(bbox.Center);

                                                    //Get closest grid intersection X,Y coord
                                                    gridXCoord.Add(gridCross.Position.X);
                                                    gridYCoord.Add(gridCross.Position.Y);
                                                }

                                            }
                                        }
                                    }
                                    //if file in selection tree is an NWD file
                                    else if (disName.Last() == "nwd")
                                    {
                                        foreach (ModelItem item in root.Children)
                                        {
                                            ModelItem disfile = item as ModelItem;

                                            string disNwd = disfile.DisplayName.ToString();
                                            string[] disNameNwd = disNwd.Split('_', '-', '.');

                                            foreach (PropertyCategory oPCnwd in disfile.PropertyCategories)
                                            {
                                                if (oPCnwd.DisplayName.ToString() == "Item")
                                                {
                                                    foreach (DataProperty oDPnwd in oPCnwd.Properties)
                                                    {
                                                        if (oDPnwd.DisplayName.ToString() == "Source File Name")
                                                        {
                                                            string valNwd = oDPnwd.Value.ToDisplayString();
                                                            string[] valNameNwd = valNwd.Split('.');

                                                            if (valNameNwd.Last() == "rvt")
                                                            {
                                                                foreach (ModelItem itemNwd in disfile.Children)
                                                                {
                                                                    ModelItem subLayer2Nwd = itemNwd as ModelItem;

                                                                    foreach (ModelItem subLayer3Nwd in subLayer2Nwd.Children)
                                                                    {
                                                                        ModelItem subLayer4Nwd = subLayer3Nwd as ModelItem;

                                                                        foreach (ModelItem subLayer5Nwd in subLayer4Nwd.Children)
                                                                        {
                                                                            ModelItem subLayer6Nwd = subLayer5Nwd as ModelItem;

                                                                            foreach (ModelItem subLayer7Nwd in subLayer6Nwd.Children)
                                                                            {
                                                                                ModelItem subLayer8Nwd = subLayer7Nwd as ModelItem;

                                                                                foreach (ModelItem subLayer9Nwd in subLayer8Nwd.Children)
                                                                                {
                                                                                    if (subLayer9Nwd != null)
                                                                                    {
                                                                                        //Get object center position (X,Y,Z) by setting a 3D bounding box
                                                                                        BoundingBox3D bbox = subLayer9Nwd.BoundingBox();

                                                                                        //find closest grid intersection to object center position using API
                                                                                        GridIntersection gridCross = docGridSys.ClosestIntersection(bbox.Center);

                                                                                        gridXCoord.Add(gridCross.Position.X);
                                                                                        gridYCoord.Add(gridCross.Position.Y);
                                                                                    }

                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            //Source file is DWG format (AutoCAD)
                                                            else if (valNameNwd.Last() == "dwg")
                                                            {
                                                                foreach (ModelItem itemNwd in disfile.Children)
                                                                {
                                                                    ModelItem subLayer2Nwd = itemNwd as ModelItem;

                                                                    foreach (ModelItem subLayer3Nwd in subLayer2Nwd.Children)
                                                                    {
                                                                        if (subLayer3Nwd != null)
                                                                        {
                                                                            //Get object center position (X,Y,Z) by setting a 3D bounding box
                                                                            BoundingBox3D bbox = subLayer3Nwd.BoundingBox();

                                                                            //find closest grid intersection to object center position using API
                                                                            GridIntersection gridCross = docGridSys.ClosestIntersection(bbox.Center);

                                                                            //Get closest grid intersection X,Y coord
                                                                            gridXCoord.Add(gridCross.Position.X);
                                                                            gridYCoord.Add(gridCross.Position.Y);
                                                                        }

                                                                    }
                                                                }
                                                            }

                                                        }

                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                //store max & min values to return for scatter plot data
                double gridXMin = gridXCoord.Min();
                double gridXMax = gridXCoord.Max();
                double gridYMin = gridYCoord.Min();
                double gridYMax = gridYCoord.Max();

                return (gridXMin, gridXMax, gridYMin, gridYMax);
            }

            catch (Exception exception)
            {
                MessageBox.Show("Error in Grid Coordinates Analysis!");
                exception.Message.ToString();
            }

            return (0, 0, 0, 0);
        }
    }
}
