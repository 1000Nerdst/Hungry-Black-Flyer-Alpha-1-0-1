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
    public partial class ingredientsForm : Form
    {
        //SqlConnection cn;
        //SqlCommand cmd;
        //SqlDataReader dr;
        //SqlDataAdapter da;
        public ingredientsForm()
        {
            InitializeComponent();
            fillIngredientsListBox();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult messageBoxResult = MessageBox.Show("Close without Saving?", "Close New Meal Plan", MessageBoxButtons.YesNo);
            if (messageBoxResult == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {

            }
        }

        private void addNewIngredientButton_Click(object sender, EventArgs e)
        {
            //addNewSingleFoodItemForm addNewSingleFoodItemForm = new addNewSingleFoodItemForm();
            //addNewSingleFoodItemForm.ShowDialog();
            fillIngredientsListBox();
            mainMenuForm mainMenu = new mainMenuForm();
            updateIngredientScreen();
            //mainMenu.updateMainWindow();
        }

        private void ingredientsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fillIngredientsListBox()
        {
            //cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True");
            //cmd = new SqlCommand();
            //cn.Open();
            //cmd.Connection = cn;
            //cmd.CommandText = "Select * from tblIndiviualFoods";
            //dr = cmd.ExecuteReader();

            List<string> clearIngredientListBox = new List<string>();
            clearIngredientListBox.Add(" ");

            ingredientsListBox.DataSource = clearIngredientListBox;

            List<string> foodNameStringList = new List<string>();
            //while (dr.Read())
            //{
            //    foodNameStringList.Add((string)dr["foodName"]);
            //}
            //ingredientsListBox.DataSource = foodNameStringList;

            //dr.Dispose();
            //dr.Close();

            //cn.Close();
        }
        private void getCurrentIngredientsDataBase()
        {
            //cmd = new SqlCommand("Select * from tblIndiviualFoods", cn);
            //da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

        }

        //this is the delete context menu strip I dont know whats up
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ingredientsListBox.SelectedIndex.ToString() != null)
            {
                string selectedItem = ingredientsListBox.SelectedItem.ToString();
                string selectedItemIndex = ingredientsListBox.SelectedIndex.ToString();

                string table = "tblIndiviualFoods";
                string columbName = "foodName";
                string columnItem = selectedItem;
                deleteRow(table, columbName, columnItem);
                DataTable dt = new DataTable();

                dt.Clear();
                ingredientsListBox.DataSource = dt;
                //ingredientsListBox.DisplayMember = "foodName";

                //ingredientsListBox.Items.Clear();

                mainMenuForm mainMenu = new mainMenuForm();
                //mainMenu.updateMainWindow();

                fillIngredientsListBox();

            }
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //addNewSingleFoodItemForm addNewSingleFoodItemForm = new addNewSingleFoodItemForm();
            //addNewSingleFoodItemForm.ShowDialog();
            fillIngredientsListBox();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            char[] removeChars = { ' ', '}' };

            string currentIngredientString = ingredientsListBox.SelectedItem.ToString();
            currentIngredientString = currentIngredientString.TrimEnd(removeChars);

            config.AppSettings.Settings["currentIngredientString"].Value = currentIngredientString;

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            //FoodItemsEdit foodItemsEditForm = new FoodItemsEdit();
            //foodItemsEditForm.ShowDialog();
            fillIngredientsListBox();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string namedSearch = searchIngredientsTextBox.Text.ToString();
            searchDataBase(namedSearch);
        }

        public void searchDataBase(string namedSearch)
        {
            //cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True");
            //cn.Open();
            //cmd = new SqlCommand("Select * from dbo.tblIndiviualFoods where foodName = '" + searchIngredientsTextBox.Text + "'", cn);

            //da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //ingredientsListBox.DataSource = dt;
            //ingredientsListBox.DisplayMember = "foodName";
            //cmd.ExecuteNonQuery();
            //cn.Close();
        }
        public static void deleteRow(string table, string columnName, string columnItem)
        {
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True"))
            //    {
            //        con.Open();
            //        using (SqlCommand command = new SqlCommand("DELETE FROM " + table + " WHERE " + columnName + " = '" + columnItem + "'", con))
            //        {
            //            command.ExecuteNonQuery();
            //        }
            //        con.Close();
            //    }
            //}
            //catch (SystemException ex)
            //{
            //    MessageBox.Show(string.Format("An error occurred: {0}", ex.Message));
            //}
        }

        private void resetTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ingredientsListBox.
            fillIngredientsListBox();
        }

        private void updateIngredientScreen()
        {
            fillIngredientsListBox();
        }
    }
}