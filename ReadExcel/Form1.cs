using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Excel;

namespace ReadExcel
{
    public partial class Form1 : Form
    {
        private String destinationFolder = "";
        public Form1()
        {
            InitializeComponent();
        }
        DataSet result;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook | *.xls", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(fs);
                    excelReader.IsFirstRowAsColumnNames = true;
                    result = excelReader.AsDataSet();
                    comboBoxSheet.Items.Clear();
                    foreach (DataTable dt in result.Tables)
                    {
                        comboBoxSheet.Items.Add(dt.TableName);
                    }//for
                    excelReader.Close();
                }//if
            }
        }

        private void combobox_Sheet_selectedItem(object sender, EventArgs e)
        {
            dataGridView.DataSource = result.Tables[comboBoxSheet.SelectedIndex];
        }//combobox_Shet_selectedItem


        private void buttonDownloadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = true;
            fbd.Description = "Choose folder to download all images.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                destinationFolder = fbd.SelectedPath+"\\";
            }//if
        }//buttonDownloadFolder_Click
    }
}
