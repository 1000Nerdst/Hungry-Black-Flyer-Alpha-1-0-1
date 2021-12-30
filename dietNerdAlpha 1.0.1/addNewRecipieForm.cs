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
using Microsoft.VisualBasic;

namespace dietNerdAlpha_1._0._1
{
    public partial class addNewRecipieForm : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        //addingredientToRecipe addToRecipe = new addingredientToRecipe();
        public addNewRecipieForm()
        {
            InitializeComponent();
            fillIngredientsListBox();

            //addToRecipe.ingredientString = "empty";
            //addToRecipe.servingSizeFloat = 0;
        }

        private void fillIngredientsListBox()
        {
            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True"))
            {
                cn.Open();

                using (cmd = new SqlCommand("Select * from tblIndiviualFoods", cn))
                {
                    using (dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            List<string> clearIngredientListBox = new List<string>();
                            clearIngredientListBox.Add(" ");

                            indgredentsListBox.DataSource = clearIngredientListBox;

                            List<string> foodNameStringList = new List<string>();
                            while (dr.Read())
                            {
                                foodNameStringList.Add((string)dr["foodName"]);
                            }
                            indgredentsListBox.DataSource = foodNameStringList;
                        }
                    }
                    dr.Dispose();
                    dr.Close();
                }
                cmd.Dispose();
            }

            cn.Dispose();
            cn.Close();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addNewIngredientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //addNewSingleFoodItemForm addNewSingleFoodItemForm = new addNewSingleFoodItemForm();
            //addNewSingleFoodItemForm.ShowDialog();
            mainMenuForm mainMenu = new mainMenuForm();
            fillIngredientsListBox();
            mainMenu.updateMainWindow();
        }
        private void addToRecipieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            string currentIngredientString = indgredentsListBox.SelectedItem.ToString();
            config.AppSettings.Settings["currentIngredientString"].Value = currentIngredientString;
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            getServingSizeForm getServingSizeScreen = new getServingSizeForm();
            getServingSizeScreen.ShowDialog();

            loadListBox();
            //int testInt = addToRecipe.servingSizeFloat();
        }

        private void loadListBox()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            string currentIngredientString = config.AppSettings.Settings["currentIngredientString"].Value;
            string currentServingSizeFloat = config.AppSettings.Settings["currentServingSize"].Value;

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            //addToRecipe.ingredientString = currentIngredientString;

            ListViewItem recipieIndgredientsListViewItem = new ListViewItem(currentIngredientString);
            recipieIndgredientsListViewItem.SubItems.Add(currentServingSizeFloat);

            ingfredientsListView.Items.Add(recipieIndgredientsListViewItem);
            //recipieIndgredientsListBox.Items.Add(addToRecipe.servingSizeFloat);
        }

        private void findIngredentButton_Click(object sender, EventArgs e)
        {
            string namedSearch = searchIngredentsTextBox.Text.ToString();
            searchDataBase(namedSearch);
        }

        private void searchDataBase(string namedSearch)
        {
            cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True");
            cn.Open();
            cmd = new SqlCommand("Select * from dbo.tblIndiviualFoods where foodName = '" + searchIngredentsTextBox.Text + "'", cn);

            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            indgredentsListBox.DataSource = dt;
            indgredentsListBox.DisplayMember = "foodName";
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillIngredientsListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //have a are you sure pop up box appear here
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (recipieServingTextBox.Text.ToString() != "")
            {
                string recipieNameString = recipieNameTextBox.Text.ToString();
                string recipieServingString = recipieServingTextBox.Text.ToString();
                float recipieServingFloat = float.Parse(recipieServingString);
                //going to have use case statments to get the right meal timing
                //string mealTimingString = mealTimingComboBox.Text.ToString();

                List<string> ingredientsItemList = new List<string>();
                List<float> ingredientServingSizeList = new List<float>();

                ingredientsItemList = getListViewToIngredientsList();
                ingredientServingSizeList = getListViewToServingSizeList();

                if (ingredientsItemList.Count() == ingredientServingSizeList.Count())
                {
                    //loadToDataBase(ingredientsItemList, ingredientServingSizeList, recipieNameString, recipieServingFloat, mealTimingString);

                    updateMealTypes();
                    //close
                    this.Close();
                }
                else
                {
                    //there needs to be an error message here about how the lest aren't the same
                }
            }

        }

        private void loadToDataBase(List<string> ingredientsItemList, List<float> ingredientServingSizeList, string recipieNameString, float recipieServingFloat, string mealTimingString)
        {
            int sizeOfList = ingredientsItemList.Count();
            int i = 0;

            saveReciepiesNutrionFacts(ingredientsItemList, ingredientServingSizeList, recipieNameString, recipieServingFloat, mealTimingString);
            saveReciepiesSizes(recipieNameString, ingredientsItemList, ingredientServingSizeList);
        }

        private void saveReciepiesNutrionFacts(List<string> ingredientsItemList, List<float> ingredientServingSizeList, string recipieNameString, float recipieServingFloat, string mealTimingString)
        {
            //one of these numbers may need to be reset
            int currentDbLength = dBLength();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string oldRecipieIDString = config.AppSettings.Settings["oldRecipieIDValue"].Value;
            if (oldRecipieIDString == "")
            {
                oldRecipieIDString = "0";
            }

            string numOfNewRecipiesString = config.AppSettings.Settings["numberOfNewRecipiesItems"].Value;  //int.Parse(config.AppSettings.Settings["numberOfNewFoodItems"].Value);
            //im not sure if i'll need this again, but just so the code will fun - 9/3/21
            int DbStartingLength = currentDbLength;
            if (numOfNewRecipiesString == "" || numOfNewRecipiesString == "0")
            {
                numOfNewRecipiesString = "0";
                DbStartingLength = DbStartingLength + 1;
                config.AppSettings.Settings["startingDbID"].Value = DbStartingLength.ToString();
                config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            }

            DbStartingLength = int.Parse(config.AppSettings.Settings["startingDbID"].Value);
            int numOfNewRecipiesInt = int.Parse(numOfNewRecipiesString);
            int RecipieID = DbStartingLength + numOfNewRecipiesInt;

            if (int.Parse(oldRecipieIDString) == RecipieID)
            {
                RecipieID = RecipieID + 1;
            }

            newRecipieNutrition totalRecipieNutrition = calculateValue(ingredientsItemList, recipieServingFloat, ingredientServingSizeList);

            cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True");
            cn.Open();

            string QUERY = "INSERT INTO dbo.recipiesTable" +
                "(recipeId, recipeName, recipeCalorie, recipeMealTiming, recipeServes, recipeProtien, recipeCarbs, recipeFats, recipeSodium, recipeCholesterol, recipeVitaminD, recipeCalcium, recipeIron, recipePotassium, recipeVitaminA, recipeVitaminC, recipeVitaminE, recipeVitaminB6, recipeMagnesium, recipeZinc)" +
                "VALUES (@recipeId, @recipeName, @recipeCalorie, @recipeMealTiming, @recipeServes, @recipeProtien, @recipeCarbs, @recipeFats, @recipeSodium, @recipeCholesterol, @recipeVitaminD, @recipeCalcium, @recipeIron, @recipePotassium, @recipeVitaminA, @recipeVitaminC, @recipeVitaminE, @recipeVitaminB6, @recipeMagnesium, @recipeZinc)";
            //cmd = new SqlCommand("SAVE tblIndiviualFoods", cn);

            //recipeServingUnit has been taken out and needs to be put back later
            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@recipeId", SqlDbType.NChar).Value = RecipieID;
            CMD.Parameters.AddWithValue("@recipeName", SqlDbType.NChar).Value = recipieNameString;
            CMD.Parameters.AddWithValue("@recipeCalorie", totalRecipieNutrition.totalCalories);
            CMD.Parameters.AddWithValue("@recipeMealTiming", mealTimingString);
            CMD.Parameters.AddWithValue("@recipeServes", recipieServingFloat);
            CMD.Parameters.AddWithValue("@recipeProtien", totalRecipieNutrition.totalProtein);
            CMD.Parameters.AddWithValue("@recipeCarbs", totalRecipieNutrition.totalCarbs);
            CMD.Parameters.AddWithValue("@recipeFats", totalRecipieNutrition.totalFats);
            CMD.Parameters.AddWithValue("@recipeSodium", totalRecipieNutrition.totalSodium);
            CMD.Parameters.AddWithValue("@recipeCholesterol", totalRecipieNutrition.totalCholesterol);
            CMD.Parameters.AddWithValue("@recipeVitaminD", totalRecipieNutrition.totalVitaminD);
            CMD.Parameters.AddWithValue("@recipeCalcium", totalRecipieNutrition.totalCalcium);
            CMD.Parameters.AddWithValue("@recipeIron", totalRecipieNutrition.totalIron);
            CMD.Parameters.AddWithValue("@recipePotassium", totalRecipieNutrition.totalPotassium);
            CMD.Parameters.AddWithValue("@recipeVitaminA", totalRecipieNutrition.totalVitaminA);
            CMD.Parameters.AddWithValue("@recipeVitaminC", totalRecipieNutrition.totalVitaminC);
            CMD.Parameters.AddWithValue("@recipeVitaminE", totalRecipieNutrition.totalVitaminE);
            CMD.Parameters.AddWithValue("@recipeVitaminB6", totalRecipieNutrition.totalVitaminB6);
            CMD.Parameters.AddWithValue("@recipeMagnesium", totalRecipieNutrition.totalMagnesium);
            CMD.Parameters.AddWithValue("@recipeZinc", totalRecipieNutrition.totalZinc);
            //CMD.Parameters.AddWithValue("@recipeServingUnit", totalRecipieNutrition.unit);
            CMD.ExecuteNonQuery();

            numOfNewRecipiesInt = numOfNewRecipiesInt + 1;

            config.AppSettings.Settings["numberOfNewRecipiesItems"].Value = numOfNewRecipiesInt.ToString();
            config.AppSettings.Settings["oldRecipieIDValue"].Value = RecipieID.ToString();
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            cmd = new SqlCommand("SAVE wholeAppDataBase.mdf", cn);
            //Properties.Settings.Default.Save();

            cn.Close();
        }

        private newRecipieNutrition calculateValue(List<string> ingredientsItemList, float recipieServingFloat, List<float> ingredientServingSizeList)
        {

            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True"))
            {
                cn.Open();

                int sizeOfList = ingredientsItemList.Count();
                int i = 0;

                int recipieTotalProtein = 0, recipieTotalFats = 0, recipieTotalCarbs = 0, recipieTotalCalories = 0;
                double totalCalories = 0, totalSodium = 0, totalCholesterol = 0, totalVitaminD = 0, totalCalcium = 0, totalIron = 0, totalPotassium = 0, totalVitaminA = 0, totalVitaminC = 0, totalVitaminE = 0, totalVitaminB6 = 0, totalMagnesium = 0, totalZinc = 0;

                while (i < sizeOfList)
                {
                    string currentIngredentFromList = ingredientsItemList[i];
                    float currentIngredentServingSizeFromList = ingredientServingSizeList[i];

                    char[] removeChars = { ' ', '}' };
                    currentIngredentFromList = currentIngredentFromList.TrimEnd(removeChars);
                    string commandString = string.Format("Select * from dbo.tblIndiviualFoods where foodName = '{0}'", currentIngredentFromList);

                    using (SqlCommand cmdRecipie = new SqlCommand(commandString, cn))

                    {
                        using (dr = cmdRecipie.ExecuteReader())
                        {
                            if (dr != null)
                            {
                                List<string> ingredentFactsList = new List<string>();
                                while (dr.Read())
                                {
                                    //I need to figure out what to do with foodServing vs foodStdServingSize
                                    int foodCalories = (int)dr["foodCalories"];
                                    double foodServing = (double)dr["foodServing"];
                                    double foodStdServingSize = (double)dr["foodStdServingSize"];
                                    int foodProtien = (int)dr["foodProtien"];
                                    int foodCarbs = (int)dr["foodCarbs"];
                                    int foodFats = (int)dr["foodFats"];
                                    double foodSodium = (double)dr["foodSodium"];
                                    double foodCholesterol = (double)dr["foodCholesterol"];
                                    double foodVitaminD = (double)dr["foodVitaminD"];
                                    double foodCalcium = (double)dr["foodCalcium"];
                                    double foodIron = (double)dr["foodIron"];
                                    double foodPotassium = (double)dr["foodPotassium"];
                                    double foodVitaminA = (double)dr["foodVitaminA"];
                                    double foodVitaminC = (double)dr["foodVitaminC"];
                                    double foodVitaminE = (double)dr["foodVitaminE"];
                                    double foodVitaminB6 = (double)dr["foodVitaminB6"];
                                    double foodMagnesium = (double)dr["foodMagnesium"];
                                    double foodZinc = (double)dr["foodZinc"];
                                    string foodServingUnit = (string)dr["foodServingUnit"];

                                    if (currentIngredentServingSizeFromList > foodServing)
                                    {
                                        double multiFactor = currentIngredentServingSizeFromList / foodServing;

                                        totalCalories = totalCalories + (multiFactor * foodCalories);
                                        recipieTotalProtein = (int)(recipieTotalProtein + (multiFactor * foodProtien));
                                        recipieTotalFats = (int)(recipieTotalFats + (multiFactor * foodFats));
                                        recipieTotalCarbs = (int)(recipieTotalCarbs + (multiFactor * foodCarbs));

                                        totalCalcium = totalCalcium + (multiFactor * foodCalcium);
                                        totalSodium = totalSodium + (multiFactor * foodSodium);
                                        totalCholesterol = totalCholesterol + (multiFactor * foodCholesterol);
                                        totalVitaminD = totalVitaminD + (multiFactor * foodVitaminD);
                                        totalIron = totalIron + (multiFactor * foodIron);
                                        totalPotassium = totalPotassium + (multiFactor * foodPotassium);
                                        totalVitaminA = totalVitaminA + (multiFactor * foodVitaminA);
                                        totalVitaminC = totalVitaminC + (multiFactor * foodVitaminC);
                                        totalVitaminE = totalVitaminE + (multiFactor * foodVitaminE);
                                        totalVitaminB6 = totalVitaminB6 + (multiFactor * foodVitaminB6);
                                        totalMagnesium = totalMagnesium + (multiFactor * foodMagnesium);
                                        totalZinc = totalZinc + (multiFactor * foodZinc);
                                    }
                                    if (currentIngredentServingSizeFromList == foodServing)
                                    {
                                        totalCalories = totalCalories + foodCalories;
                                        recipieTotalProtein = recipieTotalProtein + foodProtien;
                                        recipieTotalFats = recipieTotalFats + foodFats;
                                        recipieTotalCarbs = recipieTotalCarbs + foodCarbs;

                                        totalCalcium = totalCalcium + foodCalcium;
                                        totalSodium = totalSodium + foodSodium;
                                        totalCholesterol = totalCholesterol + foodCholesterol;
                                        totalVitaminD = totalVitaminD + foodVitaminD;
                                        totalIron = totalIron + foodIron;
                                        totalPotassium = totalPotassium + foodPotassium;
                                        totalVitaminA = totalVitaminA + foodVitaminA;
                                        totalVitaminC = totalVitaminC + foodVitaminC;
                                        totalVitaminE = totalVitaminE + foodVitaminE;
                                        totalVitaminB6 = totalVitaminB6 + foodVitaminB6;
                                        totalMagnesium = totalMagnesium + foodMagnesium;
                                        totalZinc = totalZinc + foodZinc;
                                    }
                                    if (currentIngredentServingSizeFromList < foodServing)
                                    {
                                        double multiFactor = currentIngredentServingSizeFromList / foodServing;

                                        totalCalories = totalCalories + (multiFactor * foodCalories);
                                        recipieTotalProtein = (int)(recipieTotalProtein + (multiFactor * foodProtien));
                                        recipieTotalFats = (int)(recipieTotalFats + (multiFactor * foodFats));
                                        recipieTotalCarbs = (int)(recipieTotalCarbs + (multiFactor * foodCarbs));

                                        totalCalcium = totalCalcium + (multiFactor * foodCalcium);
                                        totalSodium = totalSodium + (multiFactor * foodSodium);
                                        totalCholesterol = totalCholesterol + (multiFactor * foodCholesterol);
                                        totalVitaminD = totalVitaminD + (multiFactor * foodVitaminD);
                                        totalIron = totalIron + (multiFactor * foodIron);
                                        totalPotassium = totalPotassium + (multiFactor * foodPotassium);
                                        totalVitaminA = totalVitaminA + (multiFactor * foodVitaminA);
                                        totalVitaminC = totalVitaminC + (multiFactor * foodVitaminC);
                                        totalVitaminE = totalVitaminE + (multiFactor * foodVitaminE);
                                        totalVitaminB6 = totalVitaminB6 + (multiFactor * foodVitaminB6);
                                        totalMagnesium = totalMagnesium + (multiFactor * foodMagnesium);
                                        totalZinc = totalZinc + (multiFactor * foodZinc);
                                    }
                                    else
                                    {
                                        //this shouldnt be hit
                                    }
                                }
                            }
                        }
                    }

                    i++;
                }

                recipieTotalCalories = (int)(totalCalories / recipieServingFloat);
                recipieTotalProtein = (int)(recipieTotalProtein / recipieServingFloat);
                recipieTotalCarbs = (int)(recipieTotalCarbs / recipieServingFloat);
                recipieTotalFats = (int)(recipieTotalFats / recipieServingFloat);

                totalCalcium = totalCalcium / recipieServingFloat;
                totalSodium = totalSodium / recipieServingFloat;
                totalCholesterol = totalCholesterol / recipieServingFloat;
                totalVitaminD = totalVitaminD / recipieServingFloat;
                totalIron = totalIron / recipieServingFloat;
                totalPotassium = totalPotassium / recipieServingFloat;
                totalVitaminA = totalVitaminA / recipieServingFloat;
                totalVitaminC = totalVitaminC / recipieServingFloat;
                totalVitaminE = totalVitaminE / recipieServingFloat;
                totalVitaminB6 = totalVitaminB6 / recipieServingFloat;
                totalMagnesium = totalMagnesium / recipieServingFloat;
                totalZinc = totalZinc / recipieServingFloat;

                newRecipieNutrition recipieNutrition = new newRecipieNutrition();
                recipieNutrition.totalCalories = recipieTotalCalories;
                recipieNutrition.totalProtein = recipieTotalProtein;
                recipieNutrition.totalCarbs = recipieTotalCarbs;
                recipieNutrition.totalFats = recipieTotalFats;
                recipieNutrition.totalCalcium = int.Parse(Math.Round(totalCalcium).ToString());
                recipieNutrition.totalSodium = int.Parse(Math.Round(totalSodium).ToString());
                recipieNutrition.totalCholesterol = int.Parse(Math.Round(totalCholesterol).ToString());
                recipieNutrition.totalVitaminD = int.Parse(Math.Round(totalVitaminD).ToString());
                recipieNutrition.totalIron = int.Parse(Math.Round(totalIron).ToString());
                recipieNutrition.totalPotassium = int.Parse(Math.Round(totalPotassium).ToString());
                recipieNutrition.totalVitaminA = int.Parse(Math.Round(totalVitaminA).ToString());
                recipieNutrition.totalVitaminC = int.Parse(Math.Round(totalVitaminC).ToString());
                recipieNutrition.totalVitaminE = int.Parse(Math.Round(totalVitaminE).ToString());
                recipieNutrition.totalVitaminB6 = int.Parse(Math.Round(totalVitaminB6).ToString());
                recipieNutrition.totalMagnesium = int.Parse(Math.Round(totalMagnesium).ToString());
                recipieNutrition.totalZinc = int.Parse(Math.Round(totalZinc).ToString());
                recipieNutrition.recipieServing = recipieServingFloat;

                return recipieNutrition;

            }


            cn.Close();

        }
        private void saveReciepiesSizes(string recipieNameString, List<string> ingredientsItemList, List<float> ingredientServingSizeList)
        {

            //cn.Open();

            int sizeOfList = ingredientsItemList.Count();
            int i = 0;

            while (i < sizeOfList)
            {
                string ingredientNameVar, ingredientServingVar;
                string currentIngredientNameVar, currentIngredientServingVar;

                string currentIngredentFromList = ingredientsItemList[i];
                float currentIngredentServingSizeFromList = ingredientServingSizeList[i];

                string currentNumberString = i.ToString();

                ingredientNameVar = "ingredientName" + currentNumberString;
                ingredientServingVar = "ingredientServing" + currentNumberString;

                char[] removeChars = { ' ', '}' };
                currentIngredientNameVar = currentIngredentFromList.TrimEnd(removeChars);
                currentIngredientServingVar = currentIngredentServingSizeFromList.ToString();

                var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

                using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True"))
                {
                    cn.Open();

                    string QUERY = "UPDATE recipiesTable SET " + ingredientNameVar + " = @" + ingredientNameVar + " , " + ingredientServingVar + " = @" + ingredientServingVar + "  " +
                    "WHERE recipeName = '" + recipieNameString + "' ";


                    using (SqlCommand updateTable = new SqlCommand(QUERY, cn))
                    {
                        updateTable.Parameters.Add("@" + ingredientNameVar, SqlDbType.NChar).Value = currentIngredientNameVar;
                        updateTable.Parameters.Add("@" + ingredientServingVar, SqlDbType.Float).Value = currentIngredentServingSizeFromList;

                        updateTable.ExecuteNonQuery();
                    }


                    cn.Close();

                    i++;
                }

            }

        }
        private List<string> getListViewToIngredientsList()
        {
            int sizeOfListView = ingfredientsListView.Items.Count;

            int i = 0;
            List<string> ingredientsItemList = new List<string>();
            while (i < sizeOfListView)
            {
                string liveViewItem = ingfredientsListView.Items[i].SubItems[0].ToString();

                string newStringTest = liveViewItem.Remove(0, 18);

                ingredientsItemList.Add(newStringTest);

                i++;
            }

            return ingredientsItemList;
        }

        private List<float> getListViewToServingSizeList()
        {
            int sizeOfListView = ingfredientsListView.Items.Count;

            int i = 0;

            List<float> ingredientServingSizeList = new List<float>();
            while (i < sizeOfListView)
            {
                string listViewServingSizeString = ingfredientsListView.Items[i].SubItems[1].ToString();

                listViewServingSizeString = listViewServingSizeString.Remove(0, 18);
                listViewServingSizeString = listViewServingSizeString.Replace("}", string.Empty);

                float ingredientServingSizeFloat = float.Parse(listViewServingSizeString);

                ingredientServingSizeList.Add(ingredientServingSizeFloat);
                //liveViewServingSizeString = liveViewServingSizeString.Remove(1);

                i++;
            }

            return ingredientServingSizeList;
        }

        private int dBLength()
        {
            int dbSize = 0;
            int recipieIdMax, previousMax;

            string SIZE_QUERY = "SELECT COUNT(*) FROM dbo.recipiesTable";

            DataSet sizeDataSet = new DataSet();

            using (SqlConnection sizeCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\bin\Debug\wholeAppDataBase.mdf; Integrated Security = True"))
            {
                using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, sizeCon))
                {
                    sizeCon.Open();
                    dbSize = (int)cmdGetDataCount.ExecuteScalar();
                }
                sizeCon.Close();
            }
            if (dbSize != 0)
            {
                using (SqlConnection sizeCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\bin\Debug\wholeAppDataBase.mdf; Integrated Security = True"))
                {
                    using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, sizeCon))
                    {
                        sizeCon.Open();
                        previousMax = (int)cmdGetDataCount.ExecuteScalar();
                    }
                    sizeCon.Close();
                }
                recipieIdMax = previousMax + 1;
            }
            else
            {
                recipieIdMax = dbSize;
            }
            return recipieIdMax;
        }

        private void updateMealTypes()
        {

        }
    }
}
