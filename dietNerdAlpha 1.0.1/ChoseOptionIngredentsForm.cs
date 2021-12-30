using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace dietNerdAlpha_1._0._1
{
    public partial class ChoseOptionIngredentsForm : Form
    {
        public ChoseOptionIngredentsForm()
        {
            InitializeComponent();
            ReadCsvFile();

        }

        private void ReadCsvFile()
        {
            string csvPath = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Config Files\servingOptions.csv";

            var reader = new StreamReader(File.OpenRead(csvPath));
            List<string> servingUnits = new List<string>();
            List<string> servingConversions = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(",");

                servingUnits.Add(values[0]);
                servingConversions.Add(values[1]);
            }
            //throw new NotImplementedException();

            for(int i = 1; i < servingUnits.Count; i++)
            {
                servingOptionsListBox.Items.Add(servingUnits[i]);
            }

            reader.Close();
        }

        private void chooseServingButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = (servingOptionsListBox.SelectedIndex + 1);

            string csvPath = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Config Files\servingOptions.csv";

            var reader = new StreamReader(File.OpenRead(csvPath));
            List<string> servingUnits = new List<string>();
            List<string> servingConversions = new List<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(",");

                servingUnits.Add(values[0]);
                servingConversions.Add(values[1]);
            }

            string selectedUnits = servingUnits[selectedIndex];
            string selectedConversion = servingConversions[selectedIndex];

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["newIngredientConversion"].Value = selectedConversion;
            config.AppSettings.Settings["newIngredentServingSize"].Value = selectedUnits;
            config.AppSettings.Settings["addedManually"].Value = "false";

            makeToPythonCsv();

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            reader.Close();

            NewIngredientForm newIngredientForm = new NewIngredientForm();
            newIngredientForm.ShowDialog();

            this.Close();
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
    }
}
