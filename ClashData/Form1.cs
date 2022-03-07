using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.DocumentParts;
using Autodesk.Navisworks.Api.Clash;
using Autodesk.Navisworks.Internal.ApiImplementation;
using Autodesk.Navisworks.Api.Automation;
using Autodesk.Navisworks.Api.Plugins;
using ClashData;
using ClashTxtExport;
using ClashExcelExport;
using TotalObjects;
using UserInput_Form;

//-----For Navisworks 2019-----//
namespace ClashData  //Created by Carlo Caparas
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        string[] parameters;

        public Form1(params string[] parametersx)
        {
            InitializeComponent();
            parameters = parametersx;
        }

        private void reportTest_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                FileTypeExport typeExport = new FileTypeExport(parameters);
                typeExport.ShowDialog();
                //TestExportToExcel excelExport = new TestExportToExcel(); // for excel file export
                //excelExport.Execute(parameters);
                //this.Show();
            }
            catch (Exception g)
            {
                MessageBox.Show("Navisworks", "Error Message: " + g);
            }

            //MessageBox.Show("Report by Clash Test Created");
        }

        private void totObj_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                TotObj objectCount = new TotObj();
                objectCount.Execute(parameters);
                this.Show();
            }
            catch (Exception g)
            {
                MessageBox.Show("Navisworks", "Error Message: " + g);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
