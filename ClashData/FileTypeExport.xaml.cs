using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using ClashExcelExport;
using ClashTxtExport;

namespace ClashData
{
    /// <summary>
    /// Interaction logic for FileTypeExport.xaml
    /// </summary>
    public partial class FileTypeExport : Window
    {
        string[] parameters;

        public FileTypeExport(params string[] parametersx)
        {
            InitializeComponent(); // make sure XAML build action set to 'Page'
            parameters = parametersx;
        }


        //DIRECT TO EXPORT AS TXT FILE
        private void txtBtn_Click(object sender, RoutedEventArgs e)
        {
            TestExportToTxt txtExport = new TestExportToTxt();
            txtExport.Execute(parameters);
            this.Close();
        }


        //DIRECT TO EXPORT AS SPREADSHEET (.XLSX)
        private void exlBtn_Click(object sender, RoutedEventArgs e)
        {
            TestExportToExcel exlExport = new TestExportToExcel();
            exlExport.Execute(parameters);
            this.Close();
        }
    }
}
