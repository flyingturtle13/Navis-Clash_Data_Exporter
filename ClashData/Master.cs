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
using ClashData;

//-----For Navisworks 2019-----//
namespace StartView //Created by Carlo Caparas, Last Updated: 12.20.2018
{
    [PluginAttribute("StartView.Start",    //Namespace.Starting class of the plugin (where the override function is)
     "CD.CAC",  // Your dev ID (It can be anything up to 7 letters I believe)
     ToolTip = "Export Clash Info to Excel",    //Plugin Tooltip content
     DisplayName = "Export Clash Data")]    //Name of the plugin button.
    [RibbonLayout("AddinRibbon.xaml")]
    [RibbonTab("Turner VDC Add-ins")]
    [Command("Clash_Data_Exporter", Icon = "Data-Export-16.png", LargeIcon = "Data-Export-32.png", ToolTip = "Export Clash Detective Data to Excel for Power BI")]

    public class Start:CommandHandlerPlugin
    {
        public override int ExecuteCommand(string name, params string[] parameters)
        {
            switch(name)
            {
                case "Clash_Data_Exporter":
                    Form1 form = new Form1(parameters);
                    form.ShowDialog();

                    form.Close();
                    break;
            }
            
            return 0;
        }
    }
}
