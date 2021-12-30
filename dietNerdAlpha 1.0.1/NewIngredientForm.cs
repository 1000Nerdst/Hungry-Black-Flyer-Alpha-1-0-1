using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace dietNerdAlpha_1._0._1
{
    public partial class NewIngredientForm : Form
    {
        SqlConnection cn;
        SqlCommand cmd;

        string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True";
        public NewIngredientForm()
        {
            InitializeComponent();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            
            if(config.AppSettings.Settings["addedManually"].Value == "false")
            {
                LoadIngredientValues();
            }
            
        }

        private void LoadIngredientValues()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string webpage = config.AppSettings.Settings["newIngredentWebpage"].Value;

            RunPythonScript runPython = new RunPythonScript();
            runPython.GetIngrendentInfromation();

            ReadXmlFile();
        }

        private void ReadXmlFile()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            servingSizeTextBox.Text = config.AppSettings.Settings["newIngredentServingSize"].Value;


            string xmlPath = @"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Config Files\newIngredient.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            foreach(XmlNode node in doc.DocumentElement)
            {
                string nodeName = node.Name;
                string value = node.InnerXml;

                switch (nodeName)
                {
                    case "Calories":
                        caloriesTextBox.Text = value;
                        break;
                    case "Fats":
                        fatsTextBox.Text = value;
                        break;
                    case "Carbohydrates":
                        carbsTextBox.Text = value;
                        break;
                    case "Protein":
                        proteinTextBox.Text = value;
                        break;
                    case "Cholesterol":
                        cholesterolTextBox.Text = value;
                        break;
                    case "Trans_Fats":
                        transFatTextBox.Text = value;
                        break;
                    case "Surgar":
                        sugarTextBox.Text = value;
                        break;
                    case "Sta_Fats":
                        saturatedFatTextBox.Text = value;
                        break;
                    case "Fiber":
                        fiberTextBox.Text = value;
                        break;
                    case "VitaminA":
                        vitaminATextBox.Text = value;
                        break;
                    case "VitaminB1":
                        vitaminB1TextBox.Text = value;
                        break;
                    case "VitaminB2":
                        vitaminB2TextBox.Text = value;
                        break;
                    case "VitaminB3":
                        vitaminB3TextBox.Text = value;
                        break;
                    case "VitaminB5":
                        vitaminB5TextBox.Text = value;
                        break;
                    case "VitaminB6":
                        vitaminB6TextBox.Text = value;
                        break;
                    case "VitaminB7":
                        vitaminB7TextBox.Text = value;
                        break;
                    case "VitaminB9":
                        vitaminB9TextBox.Text = value;
                        break;
                    case "VitaminB12":
                        vitaminB12TextBox.Text = value;
                        break;
                    case "VitaminC":
                        vitaminCTextBox.Text = value;
                        break;
                    case "VitaminD":
                        vitaminDTextBox.Text = value;
                        break;
                    case "VitaminE":
                        vitaminETextBox.Text = value;
                        break;
                    case "VitaminK":
                        vitaminKTextBox.Text = value;
                        break;
                    case "Choline":
                        cholineTextBox.Text = value;
                        break;
                    case "Calcium":
                        calciumTextBox.Text = value;
                        break;
                    case "Chloride":
                        chlorideTextBox.Text = value;
                        break;
                    case "Chromium":
                        chromiumTextBox.Text = value;
                        break;
                    case "Copper":
                        copperTextBox.Text = value;
                        break;
                    case "Fluoride":
                        fluorideTextBox.Text = value;
                        break;
                    case "Iodine":
                        iodineTextBox.Text = value;
                        break;
                    case "Iron":
                        ironTextBox.Text = value;
                        break;
                    case "Magnesium":
                        magnesiumTextBox.Text = value;
                        break;
                    case "Manganese":
                        manganeseTextBox.Text = value;
                        break;
                    case "Molybdenum":
                        molybdenumTextBox.Text = value;
                        break;
                    case "Phosphorus":
                        phosphorusTextBox.Text = value;
                        break;
                    case "Potassium":
                        potassiumTextBox.Text = value;
                        break;
                    case "Selenium":
                        seleniumTextBox.Text = value;
                        break;
                    case "Sodium":
                        sodiumTextBox.Text = value;
                        break;
                    case "Sulfur":
                        sulfurTextBox.Text = value;
                        break;
                    case "Zinc":
                        zincTextBox.Text = value;
                        break;
                    case "Omega3":
                        omega3TextBox.Text = value;
                        break;
                    case "Omega6":
                        omega6TextBox.Text = value;
                        break;
                    case "ALA":
                        alaTextBox.Text = value;
                        break;
                    case "EPA":
                        epaTextBox.Text = value;
                        break;
                    case "DPA":
                        dpaTextBox.Text = value;
                        break;
                    case "DHA":
                        dhaTextBox.Text = value;
                        break;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(connectionString);
            cn.Open();

            CurrentIngredients currentScreen = readCurrentScreenValues();

            CurrentIngredients currentIngredients = new CurrentIngredients();
            SaveNewIngredentItem(currentScreen);

            this.Close();
        }

        private void SaveNewIngredentItem(CurrentIngredients currentIngredients)
        {
            //int currentDbLength = dBLength();

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            string numOfNewIngredentItemString = config.AppSettings.Settings["numberOfNewIngredentsItems"].Value;  //int.Parse(config.AppSettings.Settings["numberOfNewIngredentItems"].Value);
            string oldIngredentIDString = config.AppSettings.Settings["oldIngredentIDValue"].Value;
            if (oldIngredentIDString == "")
            {
                oldIngredentIDString = "0";
            }

            int oldIngredentIDInt = int.Parse(oldIngredentIDString);

            int numOfNewIngredentItemInt = int.Parse(numOfNewIngredentItemString);

            DBManagement dB = new DBManagement();

            string tableName = "dbo.ingrentsTable";
            string tableType = "ingredent";
            int maxID = dB.findMaxID(connectionString, tableName, tableType);

            int IngredentID = maxID + 1;

            if (oldIngredentIDInt == 0)
            {
                oldIngredentIDInt = maxID;
                IngredentID = maxID;
            }

            if (IngredentID == oldIngredentIDInt)
            {
                IngredentID = IngredentID + 1;
            }

            cn = new SqlConnection(connectionString);

            string QUERY = "INSERT INTO ingrentsTable" +
                "(Id, IngredentName, StdServingSize, Calories, Fats_G, Carbs_G, Protein_G, Cholesterol_mg, VitaminA_IU, VitaminB1_mg, VitaminB2_mg, VitaminB3_mg, VitaminB5_mg, VitaminB6_mg, VitaminB7_ug, VitaminB9_ug, VitaminB12_ug, Vitaminc_mg, VitaminD_IU, VitaminE_mg, VitaminK_ug, Choline_mg, Calcium_mg, Chloride_g, Chromium_mcg, Copper_mg, Fluoride_mg, Iodine_ug, Iron_mg, Magnesium_mg, Manganese_mg, Molybdenum_ug, Phosphorus_mg, Potassium_g, Selenium_ug, Sodium_mg, Sulfur_mg, Zinc_mg, Omega3_g, Omega6_g, ALA_g, EPA_g, DPA_g, DHA_g, SaturatedFat_g, TransFat_g, Fiber_g, Sugars_g)" +
                "VALUES (@Id, @IngredentName, @StdServingSize, @Calories, @Fats_G, @Carbs_G, @Protein_G, @Cholesterol_mg, @VitaminA_IU, @VitaminB1_mg, @VitaminB2_mg, @VitaminB3_mg, @VitaminB5_mg, @VitaminB6_mg, @VitaminB7_ug, @VitaminB9_ug, @VitaminB12_ug, @Vitaminc_mg, @VitaminD_IU, @VitaminE_mg, @VitaminK_ug, @Choline_mg, @Calcium_mg, @Chloride_g, @Chromium_mcg, @Copper_mg, @Fluoride_mg, @Iodine_ug, @Iron_mg, @Magnesium_mg, @Manganese_mg, @Molybdenum_ug, @Phosphorus_mg, @Potassium_g, @Selenium_ug, @Sodium_mg, @Sulfur_mg, @Zinc_mg, @Omega3_g, @Omega6_g, @ALA_g, @EPA_g, @DPA_g, @DHA_g, @SaturatedFat_g, @TransFat_g, @Fiber_g, @Sugars_g)";

            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@Id", IngredentID);
            CMD.Parameters.AddWithValue("@IngredentName", currentIngredients.ingredientName);
            CMD.Parameters.AddWithValue("@StdServingSize", currentIngredients.ingredientStdServingSize);
            CMD.Parameters.AddWithValue("@Calories", currentIngredients.ingredientCalories);
            CMD.Parameters.AddWithValue("@Fats_G", currentIngredients.ingredientFats);
            CMD.Parameters.AddWithValue("@Carbs_G", currentIngredients.ingredientCarbohydrates);
            CMD.Parameters.AddWithValue("@Protein_G", currentIngredients.ingredientProtein);
            CMD.Parameters.AddWithValue("@Cholesterol_mg", currentIngredients.ingredientCholesterol);
            CMD.Parameters.AddWithValue("@VitaminA_IU", currentIngredients.ingredientVitaminA);
            CMD.Parameters.AddWithValue("@VitaminB1_mg", currentIngredients.ingredientVitaminB1);
            CMD.Parameters.AddWithValue("@VitaminB2_mg", currentIngredients.ingredientVitaminB2);
            CMD.Parameters.AddWithValue("@VitaminB3_mg", currentIngredients.ingredientVitaminB3);
            CMD.Parameters.AddWithValue("@VitaminB5_mg", currentIngredients.ingredientVitaminB5);
            CMD.Parameters.AddWithValue("@VitaminB6_mg", currentIngredients.ingredientVitaminB6);
            CMD.Parameters.AddWithValue("@VitaminB7_ug", currentIngredients.ingredientVitaminB7);
            CMD.Parameters.AddWithValue("@VitaminB9_ug", currentIngredients.ingredientVitaminB9);
            CMD.Parameters.AddWithValue("@VitaminB12_ug", currentIngredients.ingredientVitaminB12);
            CMD.Parameters.AddWithValue("@Vitaminc_mg", currentIngredients.ingredientVitaminC);
            CMD.Parameters.AddWithValue("@VitaminD_IU", currentIngredients.ingredientVitaminD);
            CMD.Parameters.AddWithValue("@VitaminE_mg", currentIngredients.ingredientVitaminE);
            CMD.Parameters.AddWithValue("@VitaminK_ug", currentIngredients.ingredientVitaminK);
            CMD.Parameters.AddWithValue("@Choline_mg", currentIngredients.ingredientCholine);
            CMD.Parameters.AddWithValue("@Calcium_mg", currentIngredients.ingredientCalcium);
            CMD.Parameters.AddWithValue("@Chloride_g", currentIngredients.ingredientChloride);
            CMD.Parameters.AddWithValue("@Chromium_mcg", currentIngredients.ingredientChromium);
            CMD.Parameters.AddWithValue("@Copper_mg", currentIngredients.ingredientCopper);
            CMD.Parameters.AddWithValue("@Fluoride_mg", currentIngredients.ingredientFluoride);
            CMD.Parameters.AddWithValue("@Iodine_ug", currentIngredients.ingredientIodine);
            CMD.Parameters.AddWithValue("@Iron_mg", currentIngredients.ingredientIron);
            CMD.Parameters.AddWithValue("@Magnesium_mg", currentIngredients.ingredientMagnesium);
            CMD.Parameters.AddWithValue("@Manganese_mg", currentIngredients.ingredientManganese);
            CMD.Parameters.AddWithValue("@Molybdenum_ug", currentIngredients.ingredientMolybdenum);
            CMD.Parameters.AddWithValue("@Phosphorus_mg", currentIngredients.ingredientPhosphorus);
            CMD.Parameters.AddWithValue("@Potassium_g", currentIngredients.ingredientPotassium);
            CMD.Parameters.AddWithValue("@Selenium_ug", currentIngredients.ingredientSelenium);
            CMD.Parameters.AddWithValue("@Sodium_mg", currentIngredients.ingredientSodium);
            CMD.Parameters.AddWithValue("@Sulfur_mg", currentIngredients.ingredientSulfur);
            CMD.Parameters.AddWithValue("@Zinc_mg", currentIngredients.ingredientZinc);
            CMD.Parameters.AddWithValue("@Omega3_g", currentIngredients.ingredientOmega3);
            CMD.Parameters.AddWithValue("@Omega6_g", currentIngredients.ingredientOmega6);
            CMD.Parameters.AddWithValue("@ALA_g", currentIngredients.ingredientALA);
            CMD.Parameters.AddWithValue("@EPA_g", currentIngredients.ingredientEPA);
            CMD.Parameters.AddWithValue("@DPA_g", currentIngredients.ingredientDPA);
            CMD.Parameters.AddWithValue("@DHA_g", currentIngredients.ingredientDHA);
            CMD.Parameters.AddWithValue("@SaturatedFat_g", currentIngredients.ingredientStaFats);
            CMD.Parameters.AddWithValue("@TransFat_g", currentIngredients.ingredientTransFats);
            CMD.Parameters.AddWithValue("@Fiber_g", currentIngredients.ingredientSurgar);
            CMD.Parameters.AddWithValue("@Sugars_g", currentIngredients.ingredientFiber);

            cn.Open();
            CMD.ExecuteNonQuery();

            numOfNewIngredentItemInt = numOfNewIngredentItemInt + 1;

            config.AppSettings.Settings["numberOfNewIngredentsItems"].Value = numOfNewIngredentItemInt.ToString();
            config.AppSettings.Settings["oldIngredentIDValue"].Value = IngredentID.ToString();
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            //cmd = new SqlCommand("SAVE wholeAppDataBase.mdf", cn);
            //cmd.ExecuteNonQuery();
            //Properties.Settings.Default.Save();
            //cmd.ExecuteNonQuery();
            cn.Close();
        }

        private CurrentIngredients readCurrentScreenValues()
        {
            CurrentIngredients currentIngredients = new CurrentIngredients();

            currentIngredients.ingredientName = ingredientNameTextBox.Text.ToString();
            currentIngredients.ingredientStdServingSize = servingSizeTextBox.Text.ToString();
            currentIngredients.ingredientCalories = float.Parse(caloriesTextBox.Text);
            currentIngredients.ingredientFats = float.Parse(fatsTextBox.Text);
            currentIngredients.ingredientCarbohydrates = float.Parse(carbsTextBox.Text);
            currentIngredients.ingredientProtein = float.Parse(proteinTextBox.Text);
            currentIngredients.ingredientCholesterol = float.Parse(cholesterolTextBox.Text);
            currentIngredients.ingredientTransFats = float.Parse(transFatTextBox.Text);
            currentIngredients.ingredientStaFats = float.Parse(saturatedFatTextBox.Text);
            currentIngredients.ingredientSurgar = float.Parse(sugarTextBox.Text);
            currentIngredients.ingredientFiber = float.Parse(fiberTextBox.Text);
            currentIngredients.ingredientVitaminA = float.Parse(vitaminATextBox.Text);
            currentIngredients.ingredientVitaminB1 = float.Parse(vitaminB1TextBox.Text);
            currentIngredients.ingredientVitaminB2 = float.Parse(vitaminB2TextBox.Text);
            currentIngredients.ingredientVitaminB3 = float.Parse(vitaminB3TextBox.Text);
            currentIngredients.ingredientVitaminB5 = float.Parse(vitaminB5TextBox.Text);
            currentIngredients.ingredientVitaminB6 = float.Parse(vitaminB6TextBox.Text);
            currentIngredients.ingredientVitaminB7 = float.Parse(vitaminB7TextBox.Text);
            currentIngredients.ingredientVitaminB9 = float.Parse(vitaminB9TextBox.Text);
            currentIngredients.ingredientVitaminB12 = float.Parse(vitaminB12TextBox.Text);
            currentIngredients.ingredientVitaminC = float.Parse(vitaminCTextBox.Text);
            currentIngredients.ingredientVitaminD = float.Parse(vitaminDTextBox.Text);
            currentIngredients.ingredientVitaminE = float.Parse(vitaminETextBox.Text);
            currentIngredients.ingredientVitaminK = float.Parse(vitaminKTextBox.Text);
            currentIngredients.ingredientCholine = float.Parse(cholineTextBox.Text);
            currentIngredients.ingredientCalcium = float.Parse(calciumTextBox.Text);
            currentIngredients.ingredientChloride = float.Parse(chlorideTextBox.Text);
            currentIngredients.ingredientChromium = float.Parse(chromiumTextBox.Text);
            currentIngredients.ingredientCopper = float.Parse(copperTextBox.Text);
            currentIngredients.ingredientFluoride = float.Parse(fluorideTextBox.Text);
            currentIngredients.ingredientIodine = float.Parse(iodineTextBox.Text);
            currentIngredients.ingredientIron = float.Parse(ironTextBox.Text);
            currentIngredients.ingredientMagnesium = float.Parse(magnesiumTextBox.Text);
            currentIngredients.ingredientManganese = float.Parse(manganeseTextBox.Text);
            currentIngredients.ingredientMolybdenum = float.Parse(molybdenumTextBox.Text);
            currentIngredients.ingredientPhosphorus = float.Parse(phosphorusTextBox.Text);
            currentIngredients.ingredientPotassium = float.Parse(potassiumTextBox.Text);
            currentIngredients.ingredientSelenium = float.Parse(seleniumTextBox.Text);
            currentIngredients.ingredientSodium = float.Parse(sodiumTextBox.Text);
            currentIngredients.ingredientSulfur = float.Parse(sulfurTextBox.Text);
            currentIngredients.ingredientZinc = float.Parse(zincTextBox.Text);
            currentIngredients.ingredientOmega3 = float.Parse(omega3TextBox.Text);
            currentIngredients.ingredientOmega6 = float.Parse(omega6TextBox.Text);
            currentIngredients.ingredientALA = float.Parse(alaTextBox.Text);
            currentIngredients.ingredientEPA = float.Parse(epaTextBox.Text);
            currentIngredients.ingredientDPA = float.Parse(dpaTextBox.Text);
            currentIngredients.ingredientDHA = float.Parse(dhaTextBox.Text);

            return currentIngredients;
        }
    }
}
