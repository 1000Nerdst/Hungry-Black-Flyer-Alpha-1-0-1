using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dietNerdAlpha_1._0._1
{
    public partial class getServingSizeForm : Form
    {
        //SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        SqlConnection cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True");

        public getServingSizeForm()
        {
            InitializeComponent();
            string unitOfItem = readItemUnits();
            unitLabel.Text = unitOfItem;
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            string servingSize = enterServingSizeTextBox.Text.ToString();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["currentServingSize"].Value = servingSize;
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            //addOrEditSingleRecipie recipieForm = new addOrEditSingleRecipie();

            this.Close();
        }

        private string readItemUnits()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            string currentIngredientString = config.AppSettings.Settings["currentIngredientString"].Value;

            //cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True");
            string query = "SELECT * FROM ingrentsTable WHERE IngredentName = '" + currentIngredientString + "' ";

            SqlCommand command = new SqlCommand(query, cn);
            cmd = new SqlCommand();

            cn.Open();
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            string itemUnit = reader["StdServingSize"].ToString();
            cn.Close();

            return itemUnit;

        }

        private void enterServingSizeTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

