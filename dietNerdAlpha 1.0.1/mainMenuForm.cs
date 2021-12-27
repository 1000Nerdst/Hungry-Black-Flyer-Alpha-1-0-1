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
    public partial class mainMenuForm : Form
    {
        //SqlConnection cn;
        //SqlCommand cmd;
        //SqlDataReader dr;
        //SqlDataAdapter da;
        public mainMenuForm()
        {
            InitializeComponent();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            activeDayTextBox.Text = config.AppSettings.Settings["userWeeklyActiveDays"].Value;
            weeklyGoalTextBox.Text = config.AppSettings.Settings["currentGoal"].Value;
            caloricGoalTextBox.Text = config.AppSettings.Settings["totalDailyIntake"].Value;

            int numberOfNewFoodItems = 0, recipiesOfNewFoodItems = 0;
            config.AppSettings.Settings["numberOfNewFoodItems"].Value = numberOfNewFoodItems.ToString();
            config.AppSettings.Settings["numberOfNewRecipiesItems"].Value = recipiesOfNewFoodItems.ToString();
            activeDayTextBox.Text = System.Configuration.ConfigurationManager.AppSettings["activeDays"];
            weeklyGoalTextBox.Text = System.Configuration.ConfigurationManager.AppSettings["currentGoal"];
            caloricGoalTextBox.Text = System.Configuration.ConfigurationManager.AppSettings["caloricGoal"];

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            //fillIngredientsListBox();
            //fillRecipieListBox();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    PersonalSettingsForm personalSettingsForm = new PersonalSettingsForm();
        //    personalSettingsForm.ShowDialog();

        //    var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        //    activeDayTextBox.Text = config.AppSettings.Settings["userWeeklyActiveDays"].Value;
        //    weeklyGoalTextBox.Text = config.AppSettings.Settings["currentGoal"].Value;
        //    caloricGoalTextBox.Text = config.AppSettings.Settings["totalDailyIntake"].Value;

        //    this.Update();
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    ingredientsForm ingredientsForm = new ingredientsForm();
        //    ingredientsForm.ShowDialog();
        //    fillIngredientsListBox();
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    recipesForm recipesForm = new recipesForm();
        //    recipesForm.ShowDialog();
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    GroceryAndPantryForm groceryAndPantryForm = new GroceryAndPantryForm();
        //    groceryAndPantryForm.ShowDialog();
        //}

        private void mainMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void personalSettingsButton_Click(object sender, EventArgs e)
        {
            PersonalSettingsForm personalSettingsForm = new PersonalSettingsForm();
            personalSettingsForm.ShowDialog();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            activeDayTextBox.Text = config.AppSettings.Settings["userWeeklyActiveDays"].Value;
            weeklyGoalTextBox.Text = config.AppSettings.Settings["currentGoal"].Value;
            caloricGoalTextBox.Text = config.AppSettings.Settings["totalDailyIntake"].Value;

            this.Update();
        }

        private void newMealPlanButton_Click(object sender, EventArgs e)
        {
            NewMealPlanForm newMealPlanForm = new NewMealPlanForm();
            newMealPlanForm.ShowDialog();
        }

        private void ingredientsButton_Click(object sender, EventArgs e)
        {
            ingredientsForm ingredientsForm = new ingredientsForm();
            ingredientsForm.ShowDialog();
        }

        //private void fillIngredientsListBox()
        //{
        //    cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True");
        //    cmd = new SqlCommand();
        //    cn.Open();
        //    cmd.Connection = cn;
        //    cmd.CommandText = "Select * from tblIndiviualFoods";
        //    dr = cmd.ExecuteReader();
        //    List<string> clearIngredientListBox = new List<string>();
        //    clearIngredientListBox.Add(" ");

        //    avaliableIngredientsListBox.DataSource = clearIngredientListBox;

        //    List<string> foodNameStringList = new List<string>();
        //    while (dr.Read())
        //    {
        //        foodNameStringList.Add((string)dr["foodName"]);
        //    }
        //    avaliableIngredientsListBox.DataSource = foodNameStringList;

        //    dr.Dispose();
        //    dr.Close();
        //    cn.Close();
        //}

        //private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    addNewSingleFoodItemForm addNewSingleFoodItemForm = new addNewSingleFoodItemForm();
        //    addNewSingleFoodItemForm.ShowDialog();
        //    fillIngredientsListBox();
        //}

        //public void updateMainWindow()
        //{
        //    fillIngredientsListBox();
        //}

        //private void fillRecipieListBox()
        //{
        //    using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True"))
        //    {
        //        cn.Open();

        //        using (cmd = new SqlCommand("Select * from recipiesTable", cn))
        //        {
        //            using (dr = cmd.ExecuteReader())
        //            {
        //                if (dr != null)
        //                {
        //                    List<string> clearIngredientListBox = new List<string>();
        //                    clearIngredientListBox.Add(" ");

        //                    avaliableRecipiecesListBox.DataSource = clearIngredientListBox;

        //                    List<string> recipeNameStringList = new List<string>();
        //                    while (dr.Read())
        //                    {
        //                        recipeNameStringList.Add((string)dr["recipeName"]);
        //                    }
        //                    avaliableRecipiecesListBox.DataSource = recipeNameStringList;
        //                }
        //            }
        //            dr.Dispose();
        //            dr.Close();
        //        }
        //        cmd.Dispose();
        //    }

        //    cn.Dispose();
        //    cn.Close();
        //}

        //private void editToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

        //    string currentIngredientString = avaliableIngredientsListBox.SelectedItem.ToString();
        //    config.AppSettings.Settings["currentIngredientString"].Value = currentIngredientString;

        //    //I was not saving before Im not sure what this does
        //    //config.Save(System.Configuration.ConfigurationSaveMode.Modified);

        //    FoodItemsEdit foodItemsEditForm = new FoodItemsEdit();
        //    foodItemsEditForm.ShowDialog();
        //    fillIngredientsListBox();
        //}
    }
}
