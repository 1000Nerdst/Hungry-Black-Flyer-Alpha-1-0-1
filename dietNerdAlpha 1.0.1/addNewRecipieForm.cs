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
        //SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        SqlConnection cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True");
        string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True";

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
            using (cn)
            {
                cn.Open();

                using (cmd = new SqlCommand("Select * from ingrentsTable", cn))
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
                                foodNameStringList.Add((string)dr["IngredentName"]);
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

            ingredientsListView.Items.Add(recipieIndgredientsListViewItem);
            //recipieIndgredientsListBox.Items.Add(addToRecipe.servingSizeFloat);
        }

        private void findIngredentButton_Click(object sender, EventArgs e)
        {
            string namedSearch = searchIngredentsTextBox.Text.ToString();
            searchDataBase(namedSearch);
        }

        private void searchDataBase(string namedSearch)
        {
            cn.Open();
            cmd = new SqlCommand("Select * from ingrentsTable where IngredentName = '" + searchIngredentsTextBox.Text + "'", cn);

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

        private void loadToDataBase(List<string> ingredientsItemList, List<float> ingredientServingSizeList)
        {
            CurrentRecipe namesSizesAndIds = new CurrentRecipe();
            CurrentRecipe recipeMacrosAndMicros = new CurrentRecipe();
            int sizeOfList = ingredientsItemList.Count();
            int i = 0;

            string recipieNameString = recipieNameTextBox.Text.ToString();
            float recipieServingFloat = float.Parse(recipieServingTextBox.Text.ToString());
            string mealTiming = ReadMealTiming();

            namesSizesAndIds = GetNamesSizesandIDs(ingredientsItemList, ingredientServingSizeList);
            recipeMacrosAndMicros = GetMacrosAndMicros(ingredientsItemList, ingredientServingSizeList);
            //calculateValue();

            saveReciepiesNutrionFacts(namesSizesAndIds, recipeMacrosAndMicros);
            //saveReciepiesSizes(recipieNameString, ingredientsItemList, ingredientServingSizeList);
        }

        private CurrentRecipe GetMacrosAndMicros(List<string> ingredientsItemList, List<float> ingredientServingSizeList)
        {
            CurrentRecipe currentRecipe = new CurrentRecipe();
            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True"))
            {
                cn.Open();

                float totalCalories = 0, totalFats = 0, totalCarbs = 0, totalProtein = 0, totalCholesterol = 0, totalVitaminA = 0, totalVitaminB1 = 0, totalVitaminB2 = 0, totalVitaminB3 = 0, totalVitaminB5 = 0, totalVitaminB6 = 0, totalVitaminB7 = 0, totalVitaminB9 = 0, totalVitaminB12 = 0, totalVitaminc = 0, totalVitaminD = 0, totalVitaminE = 0, totalVitaminK = 0, totalCholine = 0, totalCalcium = 0, totalChloride = 0, totalChromium = 0, totalCopper = 0, totalFluoride = 0, totalIodine = 0, totalIron = 0, totalMagnesium = 0, totalManganese = 0, totalMolybdenum = 0, totalPhosphorus = 0, totalPotassium = 0, totalSelenium = 0, totalSodium = 0, totalSulfur = 0, totalZinc = 0, totalOmega3 = 0, totalOmega6 = 0, totalALA = 0, totalEPA = 0, totalDPA = 0, totalDHA = 0, totalSaturatedFat = 0, totalTransFat = 0, totalFiber = 0, totalSugars = 0;

                string ingredientName;
                int i = 0;
                foreach (string ingredient in ingredientsItemList)
                {
                    float currentIngredentServingSizeFromList = ingredientServingSizeList[i];

                    char[] removeChars = { ' ', '}' };
                    ingredientName = ingredient.TrimEnd(removeChars);

                    string QUERY = string.Format("Select * from dbo.ingrentsTable where IngredentName = '{0}'", ingredientName);
                    using(SqlCommand cmd = new SqlCommand(QUERY, cn))
                    {
                        using(dr = cmd.ExecuteReader())
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {
                                    string stdServing = (string)dr["StdServingSize"];
                                    float ingredentCalories = float.Parse(dr["Calories"].ToString());
                                    float ingredentFats = float.Parse(dr["Fats_G"].ToString());
                                    float ingredentCarbs = float.Parse(dr["Carbs_G"].ToString());
                                    float ingredentProtein = float.Parse(dr["Protein_G"].ToString());
                                    float ingredentCholesterol = float.Parse(dr["Cholesterol_mg"].ToString());
                                    float ingredentVitaminA = float.Parse(dr["VitaminA_IU"].ToString());
                                    float ingredentVitaminB1 = float.Parse(dr["VitaminB1_mg"].ToString());
                                    float ingredentVitaminB2 = float.Parse(dr["VitaminB2_mg"].ToString());
                                    float ingredentVitaminB3 = float.Parse(dr["VitaminB3_mg"].ToString());
                                    float ingredentVitaminB5 = float.Parse(dr["VitaminB5_mg"].ToString());
                                    float ingredentVitaminB6 = float.Parse(dr["VitaminB6_mg"].ToString());
                                    float ingredentVitaminB7 = float.Parse(dr["VitaminB7_ug"].ToString());
                                    float ingredentVitaminB9 = float.Parse(dr["VitaminB9_ug"].ToString());
                                    float ingredentVitaminB12 = float.Parse(dr["VitaminB12_ug"].ToString());
                                    float ingredentVitaminc = float.Parse(dr["Vitaminc_mg"].ToString());
                                    float ingredentVitaminD = float.Parse(dr["VitaminD_IU"].ToString());
                                    float ingredentVitaminE = float.Parse(dr["VitaminE_mg"].ToString());
                                    float ingredentVitaminK = float.Parse(dr["VitaminK_ug"].ToString());
                                    float ingredentCholine = float.Parse(dr["Choline_mg"].ToString());
                                    float ingredentCalcium = float.Parse(dr["Calcium_mg"].ToString());
                                    float ingredentChloride = float.Parse(dr["Chloride_g"].ToString());
                                    float ingredentChromium = float.Parse(dr["Chromium_mcg"].ToString());
                                    float ingredentCopper = float.Parse(dr["Copper_mg"].ToString());
                                    float ingredentFluoride = float.Parse(dr["Fluoride_mg"].ToString());
                                    float ingredentIodine = float.Parse(dr["Iodine_ug"].ToString());
                                    float ingredentIron = float.Parse(dr["Iron_mg"].ToString());
                                    float ingredentMagnesium = float.Parse(dr["Magnesium_mg"].ToString());
                                    float ingredentManganese = float.Parse(dr["Manganese_mg"].ToString());
                                    float ingredentMolybdenum = float.Parse(dr["Molybdenum_ug"].ToString());
                                    float ingredentPhosphorus = float.Parse(dr["Phosphorus_mg"].ToString());
                                    float ingredentPotassium = float.Parse(dr["Potassium_g"].ToString());
                                    float ingredentSelenium = float.Parse(dr["Selenium_ug"].ToString());
                                    float ingredentSodium = float.Parse(dr["Sodium_mg"].ToString());
                                    float ingredentSulfur = float.Parse(dr["Sulfur_mg"].ToString());
                                    float ingredentZinc = float.Parse(dr["Zinc_mg"].ToString());
                                    float ingredentOmega3 = float.Parse(dr["Omega3_g"].ToString());
                                    float ingredentOmega6 = float.Parse(dr["Omega6_g"].ToString());
                                    float ingredentALA = float.Parse(dr["ALA_g"].ToString());
                                    float ingredentEPA = float.Parse(dr["EPA_g"].ToString());
                                    float ingredentDPA = float.Parse(dr["DPA_g"].ToString());
                                    float ingredentDHA = float.Parse(dr["DHA_g"].ToString());
                                    float ingredentSaturatedFat = float.Parse(dr["SaturatedFat_g"].ToString());
                                    float ingredentTransFat = float.Parse(dr["TransFat_g"].ToString());
                                    float ingredentFiber = float.Parse(dr["Fiber_g"].ToString());
                                    float ingredentSugars = float.Parse(dr["Sugars_g"].ToString());

                                    if(currentIngredentServingSizeFromList > 1)
                                    {
                                        float conversitionFactor = currentIngredentServingSizeFromList;
                                        
                                        totalCalories = totalCalories + (conversitionFactor * ingredentCalories);
                                        totalFats = totalFats + (conversitionFactor * ingredentFats);
                                        totalCarbs = totalCarbs + (conversitionFactor * ingredentCarbs);
                                        totalProtein = totalProtein + (conversitionFactor * ingredentProtein);
                                        totalCholesterol = totalCholesterol + (conversitionFactor * ingredentCholesterol);
                                        totalVitaminA = totalVitaminA + (conversitionFactor * ingredentVitaminA);
                                        totalVitaminB1 = totalVitaminB1 + (conversitionFactor * ingredentVitaminB1);
                                        totalVitaminB2 = totalVitaminB2 + (conversitionFactor * ingredentVitaminB2);
                                        totalVitaminB3 = totalVitaminB3 + (conversitionFactor * ingredentVitaminB3);
                                        totalVitaminB5 = totalVitaminB5 + (conversitionFactor * ingredentVitaminB5);
                                        totalVitaminB6 = totalVitaminB6 + (conversitionFactor * ingredentVitaminB6);
                                        totalVitaminB7 = totalVitaminB7 + (conversitionFactor * ingredentVitaminB7);
                                        totalVitaminB9 = totalVitaminB9 + (conversitionFactor * ingredentVitaminB9);
                                        totalVitaminB12 = totalVitaminB12 + (conversitionFactor * ingredentVitaminB12);
                                        totalVitaminc = totalVitaminc + (conversitionFactor * ingredentVitaminc);
                                        totalVitaminD = totalVitaminD + (conversitionFactor * ingredentVitaminD);
                                        totalVitaminE = totalVitaminE + (conversitionFactor * ingredentVitaminE);
                                        totalVitaminK = totalVitaminK + (conversitionFactor * ingredentVitaminK);
                                        totalCholine = totalCholine + (conversitionFactor * ingredentCholine);
                                        totalCalcium = totalCalcium + (conversitionFactor * ingredentCalcium);
                                        totalChloride = totalChloride + (conversitionFactor * ingredentChloride);
                                        totalChromium = totalChromium + (conversitionFactor * ingredentChromium);
                                        totalCopper = totalCopper + (conversitionFactor * ingredentCopper);
                                        totalFluoride = totalFluoride + (conversitionFactor * ingredentFluoride);
                                        totalIodine = totalIodine + (conversitionFactor * ingredentIodine);
                                        totalIron = totalIron + (conversitionFactor * ingredentIron);
                                        totalMagnesium = totalMagnesium + (conversitionFactor * ingredentMagnesium);
                                        totalManganese = totalManganese + (conversitionFactor * ingredentManganese);
                                        totalMolybdenum = totalMolybdenum + (conversitionFactor * ingredentMolybdenum);
                                        totalPhosphorus = totalPhosphorus + (conversitionFactor * ingredentPhosphorus);
                                        totalPotassium = totalPotassium + (conversitionFactor * ingredentPotassium);
                                        totalSelenium = totalSelenium + (conversitionFactor * ingredentSelenium);
                                        totalSodium = totalSodium + (conversitionFactor * ingredentSodium);
                                        totalSulfur = totalSulfur + (conversitionFactor * ingredentSulfur);
                                        totalZinc = totalZinc + (conversitionFactor * ingredentZinc);
                                        totalOmega3 = totalOmega3 + (conversitionFactor * ingredentOmega3);
                                        totalOmega6 = totalOmega6 + (conversitionFactor * ingredentOmega6);
                                        totalALA = totalALA + (conversitionFactor * ingredentALA);
                                        totalEPA = totalEPA + (conversitionFactor * ingredentEPA);
                                        totalDPA = totalDPA + (conversitionFactor * ingredentDPA);
                                        totalDHA = totalDHA + (conversitionFactor * ingredentDHA);
                                        totalSaturatedFat = totalSaturatedFat + (conversitionFactor * ingredentSaturatedFat);
                                        totalTransFat = totalTransFat + (conversitionFactor * ingredentTransFat);
                                        totalFiber = totalFiber + (conversitionFactor * ingredentFiber);
                                        totalSugars = totalSugars + (conversitionFactor * ingredentSugars);
                                    }
                                    if (currentIngredentServingSizeFromList < 1)
                                    {
                                        float conversitionFactor = 1 * currentIngredentServingSizeFromList;
                                        
                                        totalCalories = totalCalories + (conversitionFactor * ingredentCalories);
                                        totalFats = totalFats + (conversitionFactor * ingredentFats);
                                        totalCarbs = totalCarbs + (conversitionFactor * ingredentCarbs);
                                        totalProtein = totalProtein + (conversitionFactor * ingredentProtein);
                                        totalCholesterol = totalCholesterol + (conversitionFactor * ingredentCholesterol);
                                        totalVitaminA = totalVitaminA + (conversitionFactor * ingredentVitaminA);
                                        totalVitaminB1 = totalVitaminB1 + (conversitionFactor * ingredentVitaminB1);
                                        totalVitaminB2 = totalVitaminB2 + (conversitionFactor * ingredentVitaminB2);
                                        totalVitaminB3 = totalVitaminB3 + (conversitionFactor * ingredentVitaminB3);
                                        totalVitaminB5 = totalVitaminB5 + (conversitionFactor * ingredentVitaminB5);
                                        totalVitaminB6 = totalVitaminB6 + (conversitionFactor * ingredentVitaminB6);
                                        totalVitaminB7 = totalVitaminB7 + (conversitionFactor * ingredentVitaminB7);
                                        totalVitaminB9 = totalVitaminB9 + (conversitionFactor * ingredentVitaminB9);
                                        totalVitaminB12 = totalVitaminB12 + (conversitionFactor * ingredentVitaminB12);
                                        totalVitaminc = totalVitaminc + (conversitionFactor * ingredentVitaminc);
                                        totalVitaminD = totalVitaminD + (conversitionFactor * ingredentVitaminD);
                                        totalVitaminE = totalVitaminE + (conversitionFactor * ingredentVitaminE);
                                        totalVitaminK = totalVitaminK + (conversitionFactor * ingredentVitaminK);
                                        totalCholine = totalCholine + (conversitionFactor * ingredentCholine);
                                        totalCalcium = totalCalcium + (conversitionFactor * ingredentCalcium);
                                        totalChloride = totalChloride + (conversitionFactor * ingredentChloride);
                                        totalChromium = totalChromium + (conversitionFactor * ingredentChromium);
                                        totalCopper = totalCopper + (conversitionFactor * ingredentCopper);
                                        totalFluoride = totalFluoride + (conversitionFactor * ingredentFluoride);
                                        totalIodine = totalIodine + (conversitionFactor * ingredentIodine);
                                        totalIron = totalIron + (conversitionFactor * ingredentIron);
                                        totalMagnesium = totalMagnesium + (conversitionFactor * ingredentMagnesium);
                                        totalManganese = totalManganese + (conversitionFactor * ingredentManganese);
                                        totalMolybdenum = totalMolybdenum + (conversitionFactor * ingredentMolybdenum);
                                        totalPhosphorus = totalPhosphorus + (conversitionFactor * ingredentPhosphorus);
                                        totalPotassium = totalPotassium + (conversitionFactor * ingredentPotassium);
                                        totalSelenium = totalSelenium + (conversitionFactor * ingredentSelenium);
                                        totalSodium = totalSodium + (conversitionFactor * ingredentSodium);
                                        totalSulfur = totalSulfur + (conversitionFactor * ingredentSulfur);
                                        totalZinc = totalZinc + (conversitionFactor * ingredentZinc);
                                        totalOmega3 = totalOmega3 + (conversitionFactor * ingredentOmega3);
                                        totalOmega6 = totalOmega6 + (conversitionFactor * ingredentOmega6);
                                        totalALA = totalALA + (conversitionFactor * ingredentALA);
                                        totalEPA = totalEPA + (conversitionFactor * ingredentEPA);
                                        totalDPA = totalDPA + (conversitionFactor * ingredentDPA);
                                        totalDHA = totalDHA + (conversitionFactor * ingredentDHA);
                                        totalSaturatedFat = totalSaturatedFat + (conversitionFactor * ingredentSaturatedFat);
                                        totalTransFat = totalTransFat + (conversitionFactor * ingredentTransFat);
                                        totalFiber = totalFiber + (conversitionFactor * ingredentFiber);
                                        totalSugars = totalSugars + (conversitionFactor * ingredentSugars);
                                    }
                                    if (currentIngredentServingSizeFromList == 1)
                                    {
                                        float conversitionFactor = 1;
                                        
                                        totalCalories = totalCalories + (conversitionFactor * ingredentCalories);
                                        totalFats = totalFats + (conversitionFactor * ingredentFats);
                                        totalCarbs = totalCarbs + (conversitionFactor * ingredentCarbs);
                                        totalProtein = totalProtein + (conversitionFactor * ingredentProtein);
                                        totalCholesterol = totalCholesterol + (conversitionFactor * ingredentCholesterol);
                                        totalVitaminA = totalVitaminA + (conversitionFactor * ingredentVitaminA);
                                        totalVitaminB1 = totalVitaminB1 + (conversitionFactor * ingredentVitaminB1);
                                        totalVitaminB2 = totalVitaminB2 + (conversitionFactor * ingredentVitaminB2);
                                        totalVitaminB3 = totalVitaminB3 + (conversitionFactor * ingredentVitaminB3);
                                        totalVitaminB5 = totalVitaminB5 + (conversitionFactor * ingredentVitaminB5);
                                        totalVitaminB6 = totalVitaminB6 + (conversitionFactor * ingredentVitaminB6);
                                        totalVitaminB7 = totalVitaminB7 + (conversitionFactor * ingredentVitaminB7);
                                        totalVitaminB9 = totalVitaminB9 + (conversitionFactor * ingredentVitaminB9);
                                        totalVitaminB12 = totalVitaminB12 + (conversitionFactor * ingredentVitaminB12);
                                        totalVitaminc = totalVitaminc + (conversitionFactor * ingredentVitaminc);
                                        totalVitaminD = totalVitaminD + (conversitionFactor * ingredentVitaminD);
                                        totalVitaminE = totalVitaminE + (conversitionFactor * ingredentVitaminE);
                                        totalVitaminK = totalVitaminK + (conversitionFactor * ingredentVitaminK);
                                        totalCholine = totalCholine + (conversitionFactor * ingredentCholine);
                                        totalCalcium = totalCalcium + (conversitionFactor * ingredentCalcium);
                                        totalChloride = totalChloride + (conversitionFactor * ingredentChloride);
                                        totalChromium = totalChromium + (conversitionFactor * ingredentChromium);
                                        totalCopper = totalCopper + (conversitionFactor * ingredentCopper);
                                        totalFluoride = totalFluoride + (conversitionFactor * ingredentFluoride);
                                        totalIodine = totalIodine + (conversitionFactor * ingredentIodine);
                                        totalIron = totalIron + (conversitionFactor * ingredentIron);
                                        totalMagnesium = totalMagnesium + (conversitionFactor * ingredentMagnesium);
                                        totalManganese = totalManganese + (conversitionFactor * ingredentManganese);
                                        totalMolybdenum = totalMolybdenum + (conversitionFactor * ingredentMolybdenum);
                                        totalPhosphorus = totalPhosphorus + (conversitionFactor * ingredentPhosphorus);
                                        totalPotassium = totalPotassium + (conversitionFactor * ingredentPotassium);
                                        totalSelenium = totalSelenium + (conversitionFactor * ingredentSelenium);
                                        totalSodium = totalSodium + (conversitionFactor * ingredentSodium);
                                        totalSulfur = totalSulfur + (conversitionFactor * ingredentSulfur);
                                        totalZinc = totalZinc + (conversitionFactor * ingredentZinc);
                                        totalOmega3 = totalOmega3 + (conversitionFactor * ingredentOmega3);
                                        totalOmega6 = totalOmega6 + (conversitionFactor * ingredentOmega6);
                                        totalALA = totalALA + (conversitionFactor * ingredentALA);
                                        totalEPA = totalEPA + (conversitionFactor * ingredentEPA);
                                        totalDPA = totalDPA + (conversitionFactor * ingredentDPA);
                                        totalDHA = totalDHA + (conversitionFactor * ingredentDHA);
                                        totalSaturatedFat = totalSaturatedFat + (conversitionFactor * ingredentSaturatedFat);
                                        totalTransFat = totalTransFat + (conversitionFactor * ingredentTransFat);
                                        totalFiber = totalFiber + (conversitionFactor * ingredentFiber);
                                        totalSugars = totalSugars + (conversitionFactor * ingredentSugars);
                                    }
                                }
                            }
                        }
                    }

                    i++;
                }

                float servings = float.Parse(recipieServingTextBox.Text.ToString());

                totalCalories = totalCalories / servings;
                totalFats = totalFats / servings;
                totalCarbs = totalCarbs / servings;
                totalProtein = totalProtein / servings;
                totalCholesterol = totalCholesterol / servings;
                totalTransFat = totalTransFat / servings;
                totalSugars = totalSugars / servings;
                totalSaturatedFat = totalSaturatedFat / servings;
                totalFiber = totalFiber / servings;
                totalVitaminA = totalVitaminA / servings;
                totalVitaminB1 = totalVitaminB1 / servings;
                totalVitaminB2 = totalVitaminB2 / servings;
                totalVitaminB3 = totalVitaminB3 / servings;
                totalVitaminB5 = totalVitaminB5 / servings;
                totalVitaminB6 = totalVitaminB6 / servings;
                totalVitaminB7 = totalVitaminB7 / servings;
                totalVitaminB9 = totalVitaminB9 / servings;
                totalVitaminB12 = totalVitaminB12 / servings;
                totalVitaminc = totalVitaminc / servings;
                totalVitaminD = totalVitaminD / servings;
                totalVitaminE = totalVitaminE / servings;
                totalVitaminK = totalVitaminK / servings;
                totalCholine = totalCholine / servings;
                totalCalcium = totalCalcium / servings;
                totalChloride = totalChloride / servings;
                totalChromium = totalChromium / servings;
                totalCopper = totalCopper / servings;
                totalFluoride = totalFluoride / servings;
                totalIodine = totalIodine / servings;
                totalIron = totalIron / servings;
                totalMagnesium = totalMagnesium / servings;
                totalManganese = totalManganese / servings;
                totalMolybdenum = totalMolybdenum / servings;
                totalPhosphorus = totalPhosphorus / servings;
                totalPotassium = totalPotassium / servings;
                totalSelenium = totalSelenium / servings;
                totalSodium = totalSodium / servings;
                totalSulfur = totalSulfur / servings;
                totalZinc = totalZinc / servings;
                totalOmega3 = totalOmega3 / servings;
                totalOmega6 = totalOmega6 / servings;
                totalALA = totalALA / servings;
                totalEPA = totalEPA / servings;
                totalDPA = totalDPA / servings;
                totalDHA = totalDHA / servings;

                currentRecipe.recipeCalories = totalCalories;
                currentRecipe.recipeFats = totalFats;
                currentRecipe.recipeCarbohydrates = totalCarbs;
                currentRecipe.recipeProtein = totalProtein;
                currentRecipe.recipeCholesterol = totalCholesterol;
                currentRecipe.recipeTransFats = totalTransFat;
                currentRecipe.recipeSurgar = totalSugars;
                currentRecipe.recipeStaFats = totalSaturatedFat;
                currentRecipe.recipeFiber = totalFiber;
                currentRecipe.recipeVitaminA = totalVitaminA;
                currentRecipe.recipeVitaminB1 = totalVitaminB1;
                currentRecipe.recipeVitaminB2 = totalVitaminB2;
                currentRecipe.recipeVitaminB3 = totalVitaminB3;
                currentRecipe.recipeVitaminB5 = totalVitaminB5;
                currentRecipe.recipeVitaminB6 = totalVitaminB6;
                currentRecipe.recipeVitaminB7 = totalVitaminB7;
                currentRecipe.recipeVitaminB9 = totalVitaminB9;
                currentRecipe.recipeVitaminB12 = totalVitaminB12;
                currentRecipe.recipeVitaminC = totalVitaminc;
                currentRecipe.recipeVitaminD = totalVitaminD;
                currentRecipe.recipeVitaminE = totalVitaminE;
                currentRecipe.recipeVitaminK = totalVitaminK;
                currentRecipe.recipeCholine = totalCholine;
                currentRecipe.recipeCalcium = totalCalcium;
                currentRecipe.recipeChloride = totalChloride;
                currentRecipe.recipeChromium = totalChromium;
                currentRecipe.recipeCopper = totalCopper;
                currentRecipe.recipeFluoride = totalFluoride;
                currentRecipe.recipeIodine = totalIodine;
                currentRecipe.recipeIron = totalIron;
                currentRecipe.recipeMagnesium = totalMagnesium;
                currentRecipe.recipeManganese = totalManganese;
                currentRecipe.recipeMolybdenum = totalMolybdenum;
                currentRecipe.recipePhosphorus = totalPhosphorus;
                currentRecipe.recipePotassium = totalPotassium;
                currentRecipe.recipeSelenium = totalSelenium;
                currentRecipe.recipeSodium = totalSodium;
                currentRecipe.recipeSulfur = totalSulfur;
                currentRecipe.recipeZinc = totalZinc;
                currentRecipe.recipeOmega3 = totalOmega3;
                currentRecipe.recipeOmega6 = totalOmega6;
                currentRecipe.recipeALA = totalALA;
                currentRecipe.recipeEPA = totalEPA;
                currentRecipe.recipeDPA = totalDPA;
                currentRecipe.recipeDHA = totalDHA;

                cn.Close();

                return currentRecipe;
            }
        }

        private CurrentRecipe GetNamesSizesandIDs(List<string> ingredientsItemList, List<float> ingredientServingSizeList)
        {
            CurrentRecipe currentRecipe = new CurrentRecipe();

            string ingredientName;
            string ingredentNames = "";
            string ingredentIds = "";
            string ingredentSizes = "";

            int i = 0; 
            foreach (string ingredient in ingredientsItemList)
            {
                string currentSize = ingredientServingSizeList[i].ToString();
                int currentId = GetCurrentID(ingredient);

                char[] removeChars = { ' ', '}' };
                ingredientName = ingredient.TrimEnd(removeChars);

                if (i == 0)
                {
                    ingredentNames = ingredentNames + ingredientName;
                    ingredentSizes = ingredentSizes + currentSize;
                    ingredentIds = currentId + ingredentIds;
                }
                if(i != 0)
                {
                    ingredentNames = ingredentNames + "," + ingredientName;
                    ingredentSizes = ingredentSizes + "," + currentSize;
                    ingredentIds = currentId + "," + ingredentIds;
                }

                i++;
                
            }

            currentRecipe.ingredentNames = ingredentNames;
            currentRecipe.ingredentIds = ingredentIds;
            currentRecipe.ingredentSizes = ingredentSizes;

            return currentRecipe;
        }

        private int GetCurrentID(string ingredient)
        {
            int ingredentID = 0;
            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True"))
            {

                char[] removeChars = { ' ', '}' };
                ingredient = ingredient.TrimEnd(removeChars);

                cn.Open();
                string QUERY = string.Format("Select * from dbo.ingrentsTable where IngredentName = '{0}'", ingredient);

                using (SqlCommand cmdRecipie = new SqlCommand(QUERY, cn))
                {
                    using (dr = cmdRecipie.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                ingredentID = (int)dr["Id"];
                            }
                        }
                    }
                }
                cn.Close();
            }

            return ingredentID;
        }

        private void saveReciepiesNutrionFacts(CurrentRecipe namesSizesAndIds, CurrentRecipe recipeMacrosAndMicros)
        {
            //int currentDbLength = dBLength();

            cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True");

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string numOfNewRecipieItemString = config.AppSettings.Settings["numberOfNewRecipiesItems"].Value;  //int.Parse(config.AppSettings.Settings["numberOfNewIngredentItems"].Value);
            string oldRecipeIDString = config.AppSettings.Settings["oldRecipieIDValue"].Value;
            if (oldRecipeIDString == "")
            {
                oldRecipeIDString = "0";
            }

            int oldRecipeIDInt = int.Parse(oldRecipeIDString);

            int numOfNewRecipieItemInt = int.Parse(numOfNewRecipieItemString);

            DBManagement dB = new DBManagement();

            string tableName = "dbo.recipeTable";
            string tableType = "recipe";
            int maxID = dB.findMaxID(connectionString, tableName, tableType);

            int RecipeID = maxID + 1;

            if (oldRecipeIDInt == 0)
            {
                oldRecipeIDInt = maxID;
                RecipeID = maxID;
            }

            if (RecipeID == oldRecipeIDInt)
            {
                RecipeID = RecipeID + 1;
            }

            string recipeName = recipieNameTextBox.Text.ToString();
            char[] removeChars = { ' ', '}' };
            recipeName = recipeName.TrimEnd(removeChars);
            string mealTiming = ReadMealTiming();

            string QUERY = "INSERT INTO recipeTable" +
                "(Id, recipeName, timing, Servings, Calories, Fats_G, Carbs_G, Protein_G, Cholesterol_mg, VitaminA_IU, VitaminB1_mg, VitaminB2_mg, VitaminB3_mg, VitaminB5_mg, VitaminB6_mg, VitaminB7_ug, VitaminB9_ug, VitaminB12_ug, Vitaminc_mg, VitaminD_IU, VitaminE_mg, VitaminK_ug, Choline_mg, Calcium_mg, Chloride_g, Chromium_mcg, Copper_mg, Fluoride_mg, Iodine_ug, Iron_mg, Magnesium_mg, Manganese_mg, Molybdenum_ug, Phosphorus_mg, Potassium_g, Selenium_ug, Sodium_mg, Sulfur_mg, Zinc_mg, Omega3_g, Omega6_g, ALA_g, EPA_g, DPA_g, DHA_g, SaturatedFat_g, TransFat_g, Fiber_g, Sugars_g, ingredentsNames, ingredentsSizes, ingredentsIds)" +
                "VALUES (@Id, @recipeName, @timing, @Servings, @Calories, @Fats_G, @Carbs_G, @Protein_G, @Cholesterol_mg, @VitaminA_IU, @VitaminB1_mg, @VitaminB2_mg, @VitaminB3_mg, @VitaminB5_mg, @VitaminB6_mg, @VitaminB7_ug, @VitaminB9_ug, @VitaminB12_ug, @Vitaminc_mg, @VitaminD_IU, @VitaminE_mg, @VitaminK_ug, @Choline_mg, @Calcium_mg, @Chloride_g, @Chromium_mcg, @Copper_mg, @Fluoride_mg, @Iodine_ug, @Iron_mg, @Magnesium_mg, @Manganese_mg, @Molybdenum_ug, @Phosphorus_mg, @Potassium_g, @Selenium_ug, @Sodium_mg, @Sulfur_mg, @Zinc_mg, @Omega3_g, @Omega6_g, @ALA_g, @EPA_g, @DPA_g, @DHA_g, @SaturatedFat_g, @TransFat_g, @Fiber_g, @Sugars_g, @ingredentsNames, @ingredentsSizes, @ingredentsIds)";

            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@Id", RecipeID);
            CMD.Parameters.AddWithValue("@recipeName", recipeName);
            CMD.Parameters.AddWithValue("@timing", mealTiming);
            CMD.Parameters.AddWithValue("@Servings", float.Parse(recipieServingTextBox.Text.ToString()));
            CMD.Parameters.AddWithValue("@Calories", recipeMacrosAndMicros.recipeCalories);
            CMD.Parameters.AddWithValue("@Fats_G", recipeMacrosAndMicros.recipeFats);
            CMD.Parameters.AddWithValue("@Carbs_G", recipeMacrosAndMicros.recipeCarbohydrates);
            CMD.Parameters.AddWithValue("@Protein_G", recipeMacrosAndMicros.recipeProtein);
            CMD.Parameters.AddWithValue("@Cholesterol_mg", recipeMacrosAndMicros.recipeCholesterol);
            CMD.Parameters.AddWithValue("@VitaminA_IU", recipeMacrosAndMicros.recipeVitaminA);
            CMD.Parameters.AddWithValue("@VitaminB1_mg", recipeMacrosAndMicros.recipeVitaminB1);
            CMD.Parameters.AddWithValue("@VitaminB2_mg", recipeMacrosAndMicros.recipeVitaminB2);
            CMD.Parameters.AddWithValue("@VitaminB3_mg", recipeMacrosAndMicros.recipeVitaminB3);
            CMD.Parameters.AddWithValue("@VitaminB5_mg", recipeMacrosAndMicros.recipeVitaminB5);
            CMD.Parameters.AddWithValue("@VitaminB6_mg", recipeMacrosAndMicros.recipeVitaminB6);
            CMD.Parameters.AddWithValue("@VitaminB7_ug", recipeMacrosAndMicros.recipeVitaminB7);
            CMD.Parameters.AddWithValue("@VitaminB9_ug", recipeMacrosAndMicros.recipeVitaminB9);
            CMD.Parameters.AddWithValue("@VitaminB12_ug", recipeMacrosAndMicros.recipeVitaminB12);
            CMD.Parameters.AddWithValue("@Vitaminc_mg", recipeMacrosAndMicros.recipeVitaminC);
            CMD.Parameters.AddWithValue("@VitaminD_IU", recipeMacrosAndMicros.recipeVitaminD);
            CMD.Parameters.AddWithValue("@VitaminE_mg", recipeMacrosAndMicros.recipeVitaminE);
            CMD.Parameters.AddWithValue("@VitaminK_ug", recipeMacrosAndMicros.recipeVitaminK);
            CMD.Parameters.AddWithValue("@Choline_mg", recipeMacrosAndMicros.recipeCholine);
            CMD.Parameters.AddWithValue("@Calcium_mg", recipeMacrosAndMicros.recipeCalcium);
            CMD.Parameters.AddWithValue("@Chloride_g", recipeMacrosAndMicros.recipeChloride);
            CMD.Parameters.AddWithValue("@Chromium_mcg", recipeMacrosAndMicros.recipeChromium);
            CMD.Parameters.AddWithValue("@Copper_mg", recipeMacrosAndMicros.recipeCopper);
            CMD.Parameters.AddWithValue("@Fluoride_mg", recipeMacrosAndMicros.recipeFluoride);
            CMD.Parameters.AddWithValue("@Iodine_ug", recipeMacrosAndMicros.recipeIodine);
            CMD.Parameters.AddWithValue("@Iron_mg", recipeMacrosAndMicros.recipeIron);
            CMD.Parameters.AddWithValue("@Magnesium_mg", recipeMacrosAndMicros.recipeMagnesium);
            CMD.Parameters.AddWithValue("@Manganese_mg", recipeMacrosAndMicros.recipeManganese);
            CMD.Parameters.AddWithValue("@Molybdenum_ug", recipeMacrosAndMicros.recipeMolybdenum);
            CMD.Parameters.AddWithValue("@Phosphorus_mg", recipeMacrosAndMicros.recipePhosphorus);
            CMD.Parameters.AddWithValue("@Potassium_g", recipeMacrosAndMicros.recipePotassium);
            CMD.Parameters.AddWithValue("@Selenium_ug", recipeMacrosAndMicros.recipeSelenium);
            CMD.Parameters.AddWithValue("@Sodium_mg", recipeMacrosAndMicros.recipeSodium);
            CMD.Parameters.AddWithValue("@Sulfur_mg", recipeMacrosAndMicros.recipeSulfur);
            CMD.Parameters.AddWithValue("@Zinc_mg", recipeMacrosAndMicros.recipeZinc);
            CMD.Parameters.AddWithValue("@Omega3_g", recipeMacrosAndMicros.recipeOmega3);
            CMD.Parameters.AddWithValue("@Omega6_g", recipeMacrosAndMicros.recipeOmega6);
            CMD.Parameters.AddWithValue("@ALA_g", recipeMacrosAndMicros.recipeALA);
            CMD.Parameters.AddWithValue("@EPA_g", recipeMacrosAndMicros.recipeEPA);
            CMD.Parameters.AddWithValue("@DPA_g", recipeMacrosAndMicros.recipeDPA);
            CMD.Parameters.AddWithValue("@DHA_g", recipeMacrosAndMicros.recipeDHA);
            CMD.Parameters.AddWithValue("@SaturatedFat_g", recipeMacrosAndMicros.recipeStaFats);
            CMD.Parameters.AddWithValue("@TransFat_g", recipeMacrosAndMicros.recipeTransFats);
            CMD.Parameters.AddWithValue("@Fiber_g", recipeMacrosAndMicros.recipeFiber);
            CMD.Parameters.AddWithValue("@Sugars_g", recipeMacrosAndMicros.recipeSurgar);
            CMD.Parameters.AddWithValue("@ingredentsNames", namesSizesAndIds.ingredentNames);
            CMD.Parameters.AddWithValue("@ingredentsSizes", namesSizesAndIds.ingredentSizes);
            CMD.Parameters.AddWithValue("@ingredentsIds", namesSizesAndIds.ingredentIds);

            cn.Open();
            CMD.ExecuteNonQuery();

            numOfNewRecipieItemInt = numOfNewRecipieItemInt + 1;

            config.AppSettings.Settings["numberOfNewRecipiesItems"].Value = numOfNewRecipieItemInt.ToString();
            config.AppSettings.Settings["oldRecipieIDValue"].Value = RecipeID.ToString();
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            //cmd = new SqlCommand("SAVE wholeAppDataBase.mdf", cn);
            //cmd.ExecuteNonQuery();
            //Properties.Settings.Default.Save();
            //cmd.ExecuteNonQuery();
            cn.Close();
        }

        private CurrentRecipe calculateValue(List<string> ingredientsItemList, float recipieServingFloat, List<float> ingredientServingSizeList)
        {

            using (cn)
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

                CurrentRecipe recipieNutrition = new CurrentRecipe();
                //recipieNutrition.totalCalories = recipieTotalCalories;
                //recipieNutrition.totalProtein = recipieTotalProtein;
                //recipieNutrition.totalCarbs = recipieTotalCarbs;
                //recipieNutrition.totalFats = recipieTotalFats;
                //recipieNutrition.totalCalcium = int.Parse(Math.Round(totalCalcium).ToString());
                //recipieNutrition.totalSodium = int.Parse(Math.Round(totalSodium).ToString());
                //recipieNutrition.totalCholesterol = int.Parse(Math.Round(totalCholesterol).ToString());
                //recipieNutrition.totalVitaminD = int.Parse(Math.Round(totalVitaminD).ToString());
                //recipieNutrition.totalIron = int.Parse(Math.Round(totalIron).ToString());
                //recipieNutrition.totalPotassium = int.Parse(Math.Round(totalPotassium).ToString());
                //recipieNutrition.totalVitaminA = int.Parse(Math.Round(totalVitaminA).ToString());
                //recipieNutrition.totalVitaminC = int.Parse(Math.Round(totalVitaminC).ToString());
                //recipieNutrition.totalVitaminE = int.Parse(Math.Round(totalVitaminE).ToString());
                //recipieNutrition.totalVitaminB6 = int.Parse(Math.Round(totalVitaminB6).ToString());
                //recipieNutrition.totalMagnesium = int.Parse(Math.Round(totalMagnesium).ToString());
                //recipieNutrition.totalZinc = int.Parse(Math.Round(totalZinc).ToString());
                //recipieNutrition.recipieServing = recipieServingFloat;

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

                using (cn)
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
            int sizeOfListView = ingredientsListView.Items.Count;

            int i = 0;
            List<string> ingredientsItemList = new List<string>();
            while (i < sizeOfListView)
            {
                string liveViewItem = ingredientsListView.Items[i].SubItems[0].ToString();

                string newStringTest = liveViewItem.Remove(0, 18);

                ingredientsItemList.Add(newStringTest);

                i++;
            }

            return ingredientsItemList;
        }

        private List<float> getListViewToServingSizeList()
        {
            int sizeOfListView = ingredientsListView.Items.Count;

            int i = 0;

            List<float> ingredientServingSizeList = new List<float>();
            while (i < sizeOfListView)
            {
                string listViewServingSizeString = ingredientsListView.Items[i].SubItems[1].ToString();

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

            string SIZE_QUERY = "SELECT COUNT(*) FROM dbo.recipeTable";

            DataSet sizeDataSet = new DataSet();

            using (cn)
            {
                using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, cn))
                {
                    cn.Open();
                    dbSize = (int)cmdGetDataCount.ExecuteScalar();
                }
                cn.Close();
            }
            if (dbSize != 0)
            {
                using (cn)
                {
                    using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, cn))
                    {
                        cn.Open();
                        previousMax = (int)cmdGetDataCount.ExecuteScalar();
                    }
                    cn.Close();
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (recipieServingTextBox.Text.ToString() != "")
            {
                List<string> ingredientsItemList = new List<string>();
                List<float> ingredientServingSizeList = new List<float>();

                ingredientsItemList = getListViewToIngredientsList();
                ingredientServingSizeList = getListViewToServingSizeList();

                if (ingredientsItemList.Count() == ingredientServingSizeList.Count())
                {
                    //loadToDataBase(ingredientsItemList, ingredientServingSizeList, recipieNameString, recipieServingFloat, mealTimingString);

                    loadToDataBase(ingredientsItemList, ingredientServingSizeList);
                    updateMealTypes();
                    //close
                    this.Close();
                }
                else
                {
                    //there needs to be an error message here about how the lest aren't the same
                }
            }
            //move to loadToDataBase
            

            int test = 0;
        }

        private string ReadMealTiming()
        {
            string mealTiming = "Error";
            //read the values from the 
            string breakfastType = breakfastCheckBox.CheckState.ToString();
            string morninngType = morningCheckBox.CheckState.ToString();
            string lunchType = lunchCheckBox.CheckState.ToString();
            string afternoonType = afternoonCheckBox.CheckState.ToString();
            string dinnerType = dinnerCheckBox.CheckState.ToString();
            string nightType = nightCheckBox.CheckState.ToString();
            string sideType = sideCheckBox.CheckState.ToString();
            string mealPrepType = mealPrepCheckBox.CheckState.ToString();

            if(breakfastType == "Checked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked")
            {
                mealTiming = "Breakfast";
            }
            if (breakfastType == "Unhecked" && morninngType == "Checked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked")
            {
                mealTiming = "Morning";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Checked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked")
            {
                mealTiming = "Lunch";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Checked" && dinnerType == "Unchecked" && nightType == "Unchecked")
            {
                mealTiming = "Afternoon";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked")
            {
                mealTiming = "Dinner";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Checked")
            {
                mealTiming = "Night";
            }
            if (breakfastType == "Checked" && morninngType == "Checked" && lunchType == "Checked" && afternoonType == "Checked" && dinnerType == "Checked" && nightType == "Checked")
            {
                mealTiming = "Any";
            }
            if (breakfastType == "Checked" && morninngType == "Checked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked")
            {
                mealTiming = "Early";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Checked")
            {
                mealTiming = "Late";
            }
            if (breakfastType == "Unchecked" && morninngType == "Checked" && lunchType == "Unchecked" && afternoonType == "Checked" && dinnerType == "Unchecked" && nightType == "Checked")
            {
                mealTiming = "Snack";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Checked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked")
            {
                mealTiming = "Main Course";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Checked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked"  && sideType == "Checked")
            {
                mealTiming = "Side";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Checked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked" && sideType == "Checked")
            {
                mealTiming = "Lunch Side";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked" && sideType == "Checked")
            {
                mealTiming = "Dinner Side";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked" && sideType == "Checked")
            {
                mealTiming = "Side";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Checked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked" && sideType == "Unchecked" && mealPrepType == "Checked")
            {
                mealTiming = "Lunch Meal Prep";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked" && sideType == "Unchecked" && mealPrepType == "Checked")
            {
                mealTiming = "Dinner Meal Prep";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Checked" && afternoonType == "Unchecked" && dinnerType == "Checked" && nightType == "Unchecked" && sideType == "Unchecked" && mealPrepType == "Checked")
            {
                mealTiming = "Meal Prep";
            }
            if (breakfastType == "Unchecked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked" && sideType == "Unchecked" && mealPrepType == "Checked")
            {
                mealTiming = "Meal Prep";
            }
            if (breakfastType == "Checked" && morninngType == "Unchecked" && lunchType == "Unchecked" && afternoonType == "Unchecked" && dinnerType == "Unchecked" && nightType == "Unchecked" && sideType == "Unchecked" && mealPrepType == "Checked")
            {
                mealTiming = "Breakfast Meal Prep";
            }

            return mealTiming;
        }

        private void addToRecipieToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            string currentIngredientString = indgredentsListBox.SelectedItem.ToString();
            config.AppSettings.Settings["currentIngredientString"].Value = currentIngredientString;
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            getServingSizeForm getServingSizeScreen = new getServingSizeForm();
            getServingSizeScreen.ShowDialog();

            loadListBox();
        }
    }
}
