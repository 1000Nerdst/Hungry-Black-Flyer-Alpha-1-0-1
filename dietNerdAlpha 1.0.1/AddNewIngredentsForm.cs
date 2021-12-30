using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace dietNerdAlpha_1._0._1
{
    public partial class AddNewIngredentsForm : Form
    {
        //var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        public AddNewIngredentsForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void manuallyAddButton_Click(object sender, EventArgs e)
        {
            //make manual set to true
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["addedManually"].Value = "true";

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            NewIngredientForm newIngredientForm = new NewIngredientForm();
            newIngredientForm.ShowDialog();
        }

        private void searchLinkButton_Click(object sender, EventArgs e)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["addedManually"].Value = "false";

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            string website = searchLinkTextBox.Text.ToString();

            //var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["newIngredentWebpage"].Value = website;

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            makeToPythonCsv();

            //getPythonOptions pythonOptions = new getPythonOptions();
            RunPythonScript runPython = new RunPythonScript();
            runPython.chooseServingSize();

            ChoseOptionIngredentsForm choseOptionIngredentsForm = new ChoseOptionIngredentsForm();
            choseOptionIngredentsForm.ShowDialog();

        }

        private void makeToPythonCsv()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            

            StringBuilder csvContent = new StringBuilder();
            StringBuilder tempContent = new StringBuilder();
            string csvPath = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Config Files\newIngredientInformation.csv";
            //make and empty file
            tempContent.AppendLine(" ");
            File.AppendAllText(csvPath, tempContent.ToString());
            //delete the empty file
            File.Delete(csvPath);

            csvContent.AppendLine("Site, Conversion");
            string website = config.AppSettings.Settings["newIngredentWebpage"].Value;
            string conversion = config.AppSettings.Settings["newIngredientConversion"].Value;

            string singleLine = website + "," + conversion;

            csvContent.AppendLine(singleLine);

            File.AppendAllText(csvPath, csvContent.ToString());
        }

        //public getPythonOptions readSavedOptions()
        //{
        //    int test = 0;
        //    return pythonOptions;
        //}

        private void searchNutrientOptimiserButton_Click(object sender, EventArgs e)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["addedManually"].Value = "false";

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
        }
    }
}
