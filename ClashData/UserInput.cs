using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Configuration;
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
using TotalObjects;
using ClashData;
using ClashTxtExport;
using ClashData.Properties;

//-----For Navisworks 2019-----//
namespace UserInput_Form  //Created by Carlo Caparas
{
    public partial class UserInput : Form
    {

        public string Returnfz { get; set; }
        public Dictionary<string, string> Returnpd { get; set; }

        [STAThread]
        static void Main() 
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new UserInput());
        }

        public UserInput()
        {
            InitializeComponent();
            pdListView.View = System.Windows.Forms.View.Details;
            pdListView.FullRowSelect = true;
            pdListView.Columns.Add("Discipline Code", 90);
            pdListView.Columns.Add("Discipline Name", 148);
        }

        //Initializes when UI opens
        private void FZ_Input_Load(object sender, EventArgs e)
        {
            //Read In Default Project Disciplines
            string resource_data = Resources.Project_Disciplines;
            List<string> lines = resource_data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            pdListView.Items.RemoveAt(0);
            int i = 0;

            foreach (string line in lines)
            {
                if (line == "--")
                {
                    pdListView.Items.Add(new ListViewItem(new[]
                    {
                        lines[i+1],
                        lines[i+2]
                    }));
                }
                i++;
            }
        }

        //-----------------------------------------------------------------------------------------//
        //Focus Zone 

        //Enter Key action
        private void fzInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && fzInput.Text != "")
            {
                if (fzListBox.Items.Count == 0)
                {
                    fzListBox.Items.Add(fzInput.Text);
                }
                else
                {
                    fzListBox.Items.RemoveAt(0);
                    fzListBox.Items.Add(fzInput.Text);
                }
                fzInput.Clear();
            }
        }

        //Add Button
        private void fzAddBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (fzInput.Text != "")
            {
                if (fzListBox.Items.Count == 0)
                {
                    fzListBox.Items.Add(fzInput.Text);
                }
                else
                {
                    fzListBox.Items.RemoveAt(0);
                    fzListBox.Items.Add(fzInput.Text);
                }
                fzInput.Clear();
            }
        }

        //Cancel Button
        private void fzCancelBtn_Click(object sender, EventArgs e)
        {
            fzInput.Clear();
        }

        //Remove Button
        private void fzRemoveBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (fzListBox.Items.Count > 0)
            {
                fzListBox.Items.Clear();
            }
        }
        //-----------------------------------------------------------------------------------------//

        //-----------------------------------------------------------------------------------------//
        //Project Disciplines

        //Combine code and name inputs in one row
        private void dpAdd(String code, String name)
        {
            //row array
            String[] dpRow = { code, name };
            ListViewItem item = new ListViewItem(dpRow);
            pdListView.Items.Add(item);
        }

        //Add Button
        private void disAddBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (dcInputTextBox.Text == "" || dnInputTextBox.Text == "")
            {
                MessageBox.Show("Input Discipline Code and Discipline Name");
            }
            else
            {
                dpAdd(dcInputTextBox.Text, dnInputTextBox.Text);
                dcInputTextBox.Clear();
                dnInputTextBox.Clear();
            }

        }

        //Cancel Button
        private void disCancelBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (dcInputTextBox != null)
            {
                dcInputTextBox.Clear();
            }

            if (dnInputTextBox != null)
            {
                dnInputTextBox.Clear();
            }
        }

        //Remove Button
        private void disRemoveBtn_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem ListItem in pdListView.Items)
            {
                if (ListItem.Selected == true)
                {
                    pdListView.Items.Remove(ListItem);
                }
            }
        }

        //Load Button
        private void pdLoad_MouseClick(object sender, MouseEventArgs e)
        {
            string filename = "";
            OpenFileDialog pdLoad = new OpenFileDialog();

            pdLoad.Title = "Open File";
            pdLoad.Filter = "Text Documents | *.txt";

            if (pdLoad.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pdListView.Items.Clear();

                    filename = pdLoad.FileName.ToString();

                    var fileLines = File.ReadAllLines(filename);

                    int i = 0;

                    foreach (String line in fileLines)
                    {
                        if (line == "--")
                        {
                            pdListView.Items.Add(new ListViewItem(new[]
                            {
                            fileLines[i+1],
                            fileLines[i+2]
                            }));
                        }

                        i++;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + x.Message);
                }
            }
        }

        //Save Button
        private void pdSaveBtn_MouseClick(object sender, MouseEventArgs e)
        {
            string filename = "";
            SaveFileDialog pdSave = new SaveFileDialog();

            pdSave.Title = "Save to...";
            pdSave.Filter = "Text Documents | *.txt";

            if (pdSave.ShowDialog() == DialogResult.OK)
            {
                filename = pdSave.FileName.ToString();

                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        int i = 0;

                        sw.WriteLine("--");
                        foreach (ListViewItem item in pdListView.Items)
                        {
                            sw.WriteLine(item.SubItems[0].Text);
                            sw.WriteLine(item.SubItems[1].Text);

                            if (i != pdListView.Items.Count - 1)
                            {
                                sw.WriteLine("--");
                            }

                            i++;
                        }

                        sw.Dispose();
                        sw.Close();
                    }
                }

            }
        }
        //-----------------------------------------------------------------------------------------//

        //-----------------------------------------------------------------------------------------//
        //Return Values to Clash Test Module or Total Objects
        private void inEnterBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (fzListBox.Items.Count > 0 && pdListView.Items.Count > 0)
            {

                Dictionary<string, string> ProjectDisciplines = new Dictionary<string, string>();
                foreach (ListViewItem item in pdListView.Items)
                {
                    ProjectDisciplines.Add(item.SubItems[0].Text, item.SubItems[1].Text);
                }

                foreach (string lbItem in fzListBox.Items)
                {
                    Returnfz = lbItem;
                }

                Returnpd = ProjectDisciplines;
                this.Close();
            }
            else
            {
                MessageBox.Show("Check that Focus Zone & Project Disciplines/Trades have input values");
            }

        }

        //Cancel Button
        private void inCancelBtn_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            return;
        }
        //-----------------------------------------------------------------------------------------//
    }
}
