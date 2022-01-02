using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace dietNerdAlpha_1._0._1
{
    class grabFromDBV1
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        Random random = new Random();
        public mealTotals loadMealTotals(string MealTiming, bool hasMainCourse, bool needsSideItems, int numOfMealInDB, mealArraySizes mealSizes, filledTypeArrays typeArrays)
        {
            //types of thing to be removed from strings
            //string[,] mealArray = new string[,] { { "", "" }, { "", "" }, { "", "" }, { "", "" } };
            List<string> mealNames = new List<string>();
            List<string> mealSizesString = new List<string>();

            //get the lenght of the data base
            //int dataBaseSize = dBLength();

            //class mealtotals class as totals
            mealTotals finalTotal = new mealTotals();

            //set up the total variables
            mealTotals planTotals = intializeTotals();

            //intitalize what meal this is
            int currentItemsInMeal = 0;

            //start or reset bool values
            //bool hasMainCourse = false;
            //bool needsSideItems = false;

            switch (MealTiming)
            {
                case "Breakfast":

                    int mainSize = mealSizes.breakfastSize;

                    int numberOfItemsInThisMeal = random.Next(1, 3);
                    int randomMainRecipieNumber = random.Next(1, mainSize);
                    int randomSideRecipeNumber = 0;
                    float servingSizeMultiplier = random.Next(2, 8);
                    float servingMultiplier = (float)(servingSizeMultiplier * 0.25);

                    while (numberOfItemsInThisMeal > currentItemsInMeal)
                    {
                        string[] recpieMainID = typeArrays.midMorningRecipesIDs;
                        //string[] recpieSideID = typeArrays.dinnerSidesRecipes;
                        string[] breakfastNames = typeArrays.breakfastRecipes;


                        string idMainAsString = recpieMainID[randomMainRecipieNumber];
                        int randomMainRecipieID = Int32.Parse(idMainAsString);

                        //string idSideAsString = recpieSideID[randomMainRecipieNumber];
                        int randomSideRecipieID = randomMainRecipieID;


                        bool hasMain = false;

                        CurrentRecipe recipeInfo = getRecipeInfo(randomMainRecipieID, randomSideRecipieID, servingMultiplier, hasMain);

                        mealNames.Add(recipeInfo.recipeName.ToString());
                        mealSizesString.Add(servingMultiplier.ToString());

                        planTotals = totalToPlan(planTotals, recipeInfo);

                        currentItemsInMeal++;
                        //for lunch and dinner
                        //if(currentItemsInMeal > 1)
                        //{
                        //    hasMain = true;
                        //}
                    }

                    break;
                case "Morning":

                    mainSize = mealSizes.midMorningSize;

                    numberOfItemsInThisMeal = random.Next(1, 2);
                    randomMainRecipieNumber = random.Next(1, mainSize);
                    randomSideRecipeNumber = 0;
                    servingSizeMultiplier = random.Next(2, 8);
                    servingMultiplier = (float)(servingSizeMultiplier * 0.25);

                    while (numberOfItemsInThisMeal > currentItemsInMeal)
                    {
                        string[] recpieMainID = typeArrays.midMorningRecipesIDs;
                        //string[] recpieSideID = typeArrays.dinnerSidesRecipes;
                        string[] breakfastNames = typeArrays.midMorningRecipes;


                        string idMainAsString = recpieMainID[randomMainRecipieNumber];
                        int randomMainRecipieID = Int32.Parse(idMainAsString);

                        //string idSideAsString = recpieSideID[randomMainRecipieNumber];
                        int randomSideRecipieID = randomMainRecipieID;

                        bool hasMain = false;

                        CurrentRecipe recipeInfo = getRecipeInfo(randomMainRecipieID, randomSideRecipieID, servingMultiplier, hasMain);

                        mealNames.Add(recipeInfo.recipeName.ToString());
                        mealSizesString.Add(servingMultiplier.ToString());

                        planTotals = totalToPlan(planTotals, recipeInfo);

                        currentItemsInMeal++;
                        //for lunch and dinner
                        //if(currentItemsInMeal > 1)
                        //{
                        //    hasMain = true;
                        //}
                    }

                    break;
                case "Lunch":

                    mainSize = mealSizes.lunchSize;
                    int sideSize = mealSizes.lunchSidesSize;

                    numberOfItemsInThisMeal = random.Next(1, 5);
                    randomMainRecipieNumber = random.Next(1, mainSize);
                    randomSideRecipeNumber = random.Next(1, sideSize);
                    servingSizeMultiplier = random.Next(2, 8);
                    servingMultiplier = (float)(servingSizeMultiplier * 0.25);

                    while (numberOfItemsInThisMeal > currentItemsInMeal)
                    {
                        string[] recpieMainID = typeArrays.lunchRecipesIDs;
                        string[] recpieSideID = typeArrays.lunchSidesRecipesIDs;
                        string[] breakfastNames = typeArrays.midMorningRecipes;


                        string idMainAsString = recpieMainID[randomMainRecipieNumber];
                        int randomMainRecipieID = Int32.Parse(idMainAsString);

                        string idSideAsString = recpieSideID[randomSideRecipeNumber];
                        int randomSideRecipieID = Int32.Parse(idSideAsString);

                        bool hasMain = false;

                        CurrentRecipe recipeInfo = getRecipeInfo(randomMainRecipieID, randomSideRecipieID, servingMultiplier, hasMain);

                        mealNames.Add(recipeInfo.recipeName.ToString());
                        mealSizesString.Add(servingMultiplier.ToString());

                        planTotals = totalToPlan(planTotals, recipeInfo);

                        currentItemsInMeal++;
                        //for lunch and dinner
                        if (currentItemsInMeal > 1)
                        {
                            hasMain = true;
                        }
                    }

                    break;
                case "Afternoon":

                    mainSize = mealSizes.afternoonSize;

                    numberOfItemsInThisMeal = random.Next(1, 2);
                    randomMainRecipieNumber = random.Next(1, mainSize);
                    randomSideRecipeNumber = 0;
                    servingSizeMultiplier = random.Next(2, 8);
                    servingMultiplier = (float)(servingSizeMultiplier * 0.25);

                    while (numberOfItemsInThisMeal > currentItemsInMeal)
                    {
                        string[] recpieMainID = typeArrays.afternoonRecipesIDs;
                        //string[] recpieSideID = typeArrays.dinnerSidesRecipes;
                        string[] breakfastNames = typeArrays.midMorningRecipes;


                        string idMainAsString = recpieMainID[randomMainRecipieNumber];
                        int randomMainRecipieID = Int32.Parse(idMainAsString);

                        //string idSideAsString = recpieSideID[randomMainRecipieNumber];
                        int randomSideRecipieID = randomMainRecipieID;

                        bool hasMain = false;

                        CurrentRecipe recipeInfo = getRecipeInfo(randomMainRecipieID, randomSideRecipieID, servingMultiplier, hasMain);

                        mealNames.Add(recipeInfo.recipeName.ToString());
                        mealSizesString.Add(servingMultiplier.ToString());

                        planTotals = totalToPlan(planTotals, recipeInfo);

                        currentItemsInMeal++;
                        //for lunch and dinner
                        //if (currentItemsInMeal > 1)
                        //{
                        //    hasMain = true;
                        //}
                    }
                    break;
                case "Dinner":

                    mainSize = mealSizes.dinnerSize;
                    sideSize = mealSizes.dinnerSidesSize;

                    numberOfItemsInThisMeal = random.Next(1, 5);
                    randomMainRecipieNumber = random.Next(1, mainSize);
                    randomSideRecipeNumber = random.Next(1, sideSize);
                    servingSizeMultiplier = random.Next(2, 8);
                    servingMultiplier = (float)(servingSizeMultiplier * 0.25);

                    while (numberOfItemsInThisMeal > currentItemsInMeal)
                    {
                        string[] recpieMainID = typeArrays.midMorningRecipesIDs;
                        string[] recpieSideID = typeArrays.dinnerSidesRecipes;
                        string[] breakfastNames = typeArrays.midMorningRecipes;


                        string idMainAsString = recpieMainID[randomMainRecipieNumber];
                        int randomMainRecipieID = Int32.Parse(idMainAsString);

                        string idSideAsString = recpieSideID[randomSideRecipeNumber];
                        int randomSideRecipieID = Int32.Parse(idSideAsString);

                        bool hasMain = false;

                        CurrentRecipe recipeInfo = getRecipeInfo(randomMainRecipieID, randomSideRecipieID, servingMultiplier, hasMain);

                        mealNames.Add(recipeInfo.recipeName.ToString());
                        mealSizesString.Add(servingMultiplier.ToString());

                        planTotals = totalToPlan(planTotals, recipeInfo);

                        currentItemsInMeal++;
                        //for lunch and dinner
                        if (currentItemsInMeal > 1)
                        {
                            hasMain = true;
                        }
                    }
                    break;
                case "Night":

                    mainSize = mealSizes.nightSize;

                    numberOfItemsInThisMeal = random.Next(1, 2);
                    randomMainRecipieNumber = random.Next(1, mainSize);
                    randomSideRecipeNumber = 0;
                    servingSizeMultiplier = random.Next(2, 8);
                    servingMultiplier = (float)(servingSizeMultiplier * 0.25);

                    while (numberOfItemsInThisMeal > currentItemsInMeal)
                    {
                        string[] recpieMainID = typeArrays.midMorningRecipesIDs;
                        //string[] recpieSideID = typeArrays.dinnerSidesRecipes;
                        string[] breakfastNames = typeArrays.midMorningRecipes;


                        string idMainAsString = recpieMainID[randomMainRecipieNumber];
                        int randomMainRecipieID = Int32.Parse(idMainAsString);

                        //string idSideAsString = recpieSideID[randomMainRecipieNumber];
                        int randomSideRecipieID = randomMainRecipieID;

                        bool hasMain = false;

                        CurrentRecipe recipeInfo = getRecipeInfo(randomMainRecipieID, randomSideRecipieID, servingMultiplier, hasMain);

                        mealNames.Add(recipeInfo.recipeName.ToString());
                        mealSizesString.Add(servingMultiplier.ToString());

                        planTotals = totalToPlan(planTotals, recipeInfo);

                        currentItemsInMeal++;
                        //for lunch and dinner
                        //if (currentItemsInMeal > 1)
                        //{
                        //    hasMain = true;
                        //}
                    }

                    break;
            }

            //load final values into mealTotals class so it can be returned
            finalTotal = planTotals;
            //add the array of values

            return finalTotal;
        }

        private mealTotals totalToPlan(mealTotals planTotals, CurrentRecipe recipeInfo)
        {
            planTotals.totalCalories = planTotals.totalCalories + recipeInfo.recipeCalories;
            planTotals.totalFats = planTotals.totalFats + recipeInfo.recipeFats;
            planTotals.totalCarbohydrates = planTotals.totalCarbohydrates + recipeInfo.recipeCarbohydrates;
            planTotals.totalProtein = planTotals.totalProtein + recipeInfo.recipeProtein;
            planTotals.totalCholesterol = planTotals.totalCholesterol + recipeInfo.recipeCholesterol;
            planTotals.totalTransFats = planTotals.totalTransFats + recipeInfo.recipeTransFats;
            planTotals.totalSurgar = planTotals.totalSurgar + recipeInfo.recipeSurgar;
            planTotals.totalStaFats = planTotals.totalStaFats + recipeInfo.recipeStaFats;
            planTotals.totalFiber = planTotals.totalFiber + recipeInfo.recipeFiber;
            planTotals.totalVitaminA = planTotals.totalVitaminA + recipeInfo.recipeVitaminA;
            planTotals.totalVitaminB1 = planTotals.totalVitaminB1 + recipeInfo.recipeVitaminB1;
            planTotals.totalVitaminB2 = planTotals.totalVitaminB2 + recipeInfo.recipeVitaminB2;
            planTotals.totalVitaminB3 = planTotals.totalVitaminB3 + recipeInfo.recipeVitaminB3;
            planTotals.totalVitaminB5 = planTotals.totalVitaminB5 + recipeInfo.recipeVitaminB5;
            planTotals.totalVitaminB6 = planTotals.totalVitaminB6 + recipeInfo.recipeVitaminB6;
            planTotals.totalVitaminB7 = planTotals.totalVitaminB7 + recipeInfo.recipeVitaminB7;
            planTotals.totalVitaminB9 = planTotals.totalVitaminB9 + recipeInfo.recipeVitaminB9;
            planTotals.totalVitaminB12 = planTotals.totalVitaminB12 + recipeInfo.recipeVitaminB12;
            planTotals.totalVitaminC = planTotals.totalVitaminC + recipeInfo.recipeVitaminC;
            planTotals.totalVitaminD = planTotals.totalVitaminD + recipeInfo.recipeVitaminD;
            planTotals.totalVitaminE = planTotals.totalVitaminE + recipeInfo.recipeVitaminE;
            planTotals.totalVitaminK = planTotals.totalVitaminK + recipeInfo.recipeVitaminK;
            planTotals.totalCholine = planTotals.totalCholine + recipeInfo.recipeCholine;
            planTotals.totalCalcium = planTotals.totalCalcium + recipeInfo.recipeCalcium;
            planTotals.totalChloride = planTotals.totalChloride + recipeInfo.recipeChloride;
            planTotals.totalChromium = planTotals.totalChromium + recipeInfo.recipeChromium;
            planTotals.totalCopper = planTotals.totalCopper + recipeInfo.recipeCopper;
            planTotals.totalFluoride = planTotals.totalFluoride + recipeInfo.recipeFluoride;
            planTotals.totalIodine = planTotals.totalIodine + recipeInfo.recipeIodine;
            planTotals.totalIron = planTotals.totalIron + recipeInfo.recipeIron;
            planTotals.totalMagnesium = planTotals.totalMagnesium + recipeInfo.recipeMagnesium;
            planTotals.totalManganese = planTotals.totalManganese + recipeInfo.recipeManganese;
            planTotals.totalMolybdenum = planTotals.totalMolybdenum + recipeInfo.recipeMolybdenum;
            planTotals.totalPhosphorus = planTotals.totalPhosphorus + recipeInfo.recipePhosphorus;
            planTotals.totalPotassium = planTotals.totalPotassium + recipeInfo.recipePotassium;
            planTotals.totalSelenium = planTotals.totalSelenium + recipeInfo.recipeSelenium;
            planTotals.totalSodium = planTotals.totalSodium + recipeInfo.recipeSodium;
            planTotals.totalSulfur = planTotals.totalSulfur + recipeInfo.recipeSulfur;
            planTotals.totalZinc = planTotals.totalZinc + recipeInfo.recipeZinc;
            planTotals.totalOmega3 = planTotals.totalOmega3 + recipeInfo.recipeOmega3;
            planTotals.totalOmega6 = planTotals.totalOmega6 + recipeInfo.recipeOmega6;
            planTotals.totalALA = planTotals.totalALA + recipeInfo.recipeALA;
            planTotals.totalEPA = planTotals.totalEPA + recipeInfo.recipeEPA;
            planTotals.totalDPA = planTotals.totalDPA + recipeInfo.recipeDPA;
            planTotals.totalDHA = planTotals.totalDHA + recipeInfo.recipeDHA;

            return planTotals;
        }

        private CurrentRecipe getRecipeInfo(int randomMainRecipieID, int randomSideRecipieID, float servingMultiplier, bool hasMain)
        {
            CurrentRecipe recipeInformation = new CurrentRecipe();
            switch (hasMain)
            {
                case false:
                    recipeInformation = getRecipieFromDatabase(randomMainRecipieID, servingMultiplier);
                    break;
                case true:
                    recipeInformation = getRecipieFromDatabase(randomSideRecipieID, servingMultiplier);
                    break;
            }
            return recipeInformation;
        }

        private mealTotals intializeTotals()
        {
            mealTotals total = new mealTotals();

            total.totalCalories = 0;
            total.totalFats = 0;
            total.totalCarbohydrates = 0;
            total.totalProtein = 0;
            total.totalCholesterol = 0;
            total.totalTransFats = 0;
            total.totalSurgar = 0;
            total.totalStaFats = 0;
            total.totalFiber = 0;
            total.totalVitaminA = 0;
            total.totalVitaminB1 = 0;
            total.totalVitaminB2 = 0;
            total.totalVitaminB3 = 0;
            total.totalVitaminB5 = 0;
            total.totalVitaminB6 = 0;
            total.totalVitaminB7 = 0;
            total.totalVitaminB9 = 0;
            total.totalVitaminB12 = 0;
            total.totalVitaminC = 0;
            total.totalVitaminD = 0;
            total.totalVitaminE = 0;
            total.totalVitaminK = 0;
            total.totalCholine = 0;
            total.totalCalcium = 0;
            total.totalChloride = 0;
            total.totalChromium = 0;
            total.totalCopper = 0;
            total.totalFluoride = 0;
            total.totalIodine = 0;
            total.totalIron = 0;
            total.totalMagnesium = 0;
            total.totalManganese = 0;
            total.totalMolybdenum = 0;
            total.totalPhosphorus = 0;
            total.totalPotassium = 0;
            total.totalSelenium = 0;
            total.totalSodium = 0;
            total.totalSulfur = 0;
            total.totalZinc = 0;
            total.totalOmega3 = 0;
            total.totalOmega6 = 0;
            total.totalALA = 0;
            total.totalEPA = 0;
            total.totalDPA = 0;
            total.totalDHA = 0;

            return total;
        }

        //private getRecipieInformation getRecipieInfo(int randomRecipieID, float servingMultiplier, string MealTiming, int currentItemsInMeal, string[,] mealArray, bool hasMainCourse, bool needsSideItems)
        //{
        //    getRecipieInformation recipieInformation = new getRecipieInformation();
        //    getRecipieInformation recipieInfo = new getRecipieInformation();

        //    bool accurateMealTiming = false;
        //    bool isMain = false;
        //    bool isSide = false;
        //    bool isMealPrep = false;
        //    bool isAny = false;
        //    bool isBeforeLunch = false;
        //    bool isLunchOrMini = false;
        //    bool isLunchOrDinner = false;

        //    recipieInformation = getRecipieFromDatabase(randomRecipieID, servingMultiplier);
        //    string currentMealTiming = recipieInformation.recipeTiming;

        //    char[] removeChars = { ' ', '}' };
        //    string nullString = null;

        //    string recipeName = recipieInformation.recipeName;
        //    recipeName = recipeName.TrimEnd(removeChars);
        //    currentMealTiming = currentMealTiming.TrimEnd(removeChars);

        //    //make sure current meal timing is correct

        //    switch (MealTiming)
        //    {
        //        case "breakfast":
        //            recipieInfo = recipieInformation;
        //            break;
        //        case "mid morning":
        //            recipieInfo = recipieInformation;
        //            break;
        //        case "breakfast":
        //            recipieInfo = recipieInformation;
        //            break;
        //    }
        //    if (MealTiming == "breakfast")
        //    {
        //        isMain = true;
        //    }
        //    if (needsSideItems == true)
        //    {
        //        if (currentMealTiming == "Side")
        //        {

        //        }
        //        if (currentMealTiming != "Side")
        //        {

        //        }
        //    }

        //    return recipieInfo;
        //}

        private CurrentRecipe getRecipieFromDatabase(int RecipieID, float servingMultiplier)
        {
            CurrentRecipe recipieInformation = new CurrentRecipe();

            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\wholeAppDataBase.mdf; Integrated Security = True"))
            {
                cn.Open();

                string commandString = string.Format("Select * from dbo.recipeTable where Id = '{0}'", RecipieID);

                using (SqlCommand cmdRecipie = new SqlCommand(commandString, cn))
                {
                    using (dr = cmdRecipie.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                float recipeCalories = float.Parse(dr["Calories"].ToString());
                                float recipeFats = float.Parse(dr["Fats_G"].ToString());
                                float recipeCarbs = float.Parse(dr["Carbs_G"].ToString());
                                float recipeProtein = float.Parse(dr["Protein_G"].ToString());
                                float recipeCholesterol = float.Parse(dr["Cholesterol_mg"].ToString());
                                float recipeVitaminA = float.Parse(dr["VitaminA_IU"].ToString());
                                float recipeVitaminB1 = float.Parse(dr["VitaminB1_mg"].ToString());
                                float recipeVitaminB2 = float.Parse(dr["VitaminB2_mg"].ToString());
                                float recipeVitaminB3 = float.Parse(dr["VitaminB3_mg"].ToString());
                                float recipeVitaminB5 = float.Parse(dr["VitaminB5_mg"].ToString());
                                float recipeVitaminB6 = float.Parse(dr["VitaminB6_mg"].ToString());
                                float recipeVitaminB7 = float.Parse(dr["VitaminB7_ug"].ToString());
                                float recipeVitaminB9 = float.Parse(dr["VitaminB9_ug"].ToString());
                                float recipeVitaminB12 = float.Parse(dr["VitaminB12_ug"].ToString());
                                float recipeVitaminc = float.Parse(dr["Vitaminc_mg"].ToString());
                                float recipeVitaminD = float.Parse(dr["VitaminD_IU"].ToString());
                                float recipeVitaminE = float.Parse(dr["VitaminE_mg"].ToString());
                                float recipeVitaminK = float.Parse(dr["VitaminK_ug"].ToString());
                                float recipeCholine = float.Parse(dr["Choline_mg"].ToString());
                                float recipeCalcium = float.Parse(dr["Calcium_mg"].ToString());
                                float recipeChloride = float.Parse(dr["Chloride_g"].ToString());
                                float recipeChromium = float.Parse(dr["Chromium_mcg"].ToString());
                                float recipeCopper = float.Parse(dr["Copper_mg"].ToString());
                                float recipeFluoride = float.Parse(dr["Fluoride_mg"].ToString());
                                float recipeIodine = float.Parse(dr["Iodine_ug"].ToString());
                                float recipeIron = float.Parse(dr["Iron_mg"].ToString());
                                float recipeMagnesium = float.Parse(dr["Magnesium_mg"].ToString());
                                float recipeManganese = float.Parse(dr["Manganese_mg"].ToString());
                                float recipeMolybdenum = float.Parse(dr["Molybdenum_ug"].ToString());
                                float recipePhosphorus = float.Parse(dr["Phosphorus_mg"].ToString());
                                float recipePotassium = float.Parse(dr["Potassium_g"].ToString());
                                float recipeSelenium = float.Parse(dr["Selenium_ug"].ToString());
                                float recipeSodium = float.Parse(dr["Sodium_mg"].ToString());
                                float recipeSulfur = float.Parse(dr["Sulfur_mg"].ToString());
                                float recipeZinc = float.Parse(dr["Zinc_mg"].ToString());
                                float recipeOmega3 = float.Parse(dr["Omega3_g"].ToString());
                                float recipeOmega6 = float.Parse(dr["Omega6_g"].ToString());
                                float recipeALA = float.Parse(dr["ALA_g"].ToString());
                                float recipeEPA = float.Parse(dr["EPA_g"].ToString());
                                float recipeDPA = float.Parse(dr["DPA_g"].ToString());
                                float recipeDHA = float.Parse(dr["DHA_g"].ToString());
                                float recipeSaturatedFat = float.Parse(dr["SaturatedFat_g"].ToString());
                                float recipeTransFat = float.Parse(dr["TransFat_g"].ToString());
                                float recipeFiber = float.Parse(dr["Fiber_g"].ToString());
                                float recipeSugars = float.Parse(dr["Sugars_g"].ToString());

                                recipeCalories = servingMultiplier * recipeCalories;
                                recipeFats = servingMultiplier * recipeFats;
                                recipeCarbs = servingMultiplier * recipeCarbs;
                                recipeProtein = servingMultiplier * recipeProtein;
                                recipeCholesterol = servingMultiplier * recipeCholesterol;
                                recipeTransFat = servingMultiplier * recipeTransFat;
                                recipeSugars = servingMultiplier * recipeSugars;
                                recipeSaturatedFat = servingMultiplier * recipeSaturatedFat;
                                recipeFiber = servingMultiplier * recipeFiber;
                                recipeVitaminA = servingMultiplier * recipeVitaminA;
                                recipeVitaminB1 = servingMultiplier * recipeVitaminB1;
                                recipeVitaminB2 = servingMultiplier * recipeVitaminB2;
                                recipeVitaminB3 = servingMultiplier * recipeVitaminB3;
                                recipeVitaminB5 = servingMultiplier * recipeVitaminB5;
                                recipeVitaminB6 = servingMultiplier * recipeVitaminB6;
                                recipeVitaminB7 = servingMultiplier * recipeVitaminB7;
                                recipeVitaminB9 = servingMultiplier * recipeVitaminB9;
                                recipeVitaminB12 = servingMultiplier * recipeVitaminB12;
                                recipeVitaminc = servingMultiplier * recipeVitaminc;
                                recipeVitaminD = servingMultiplier * recipeVitaminD;
                                recipeVitaminE = servingMultiplier * recipeVitaminE;
                                recipeVitaminK = servingMultiplier * recipeVitaminK;
                                recipeCholine = servingMultiplier * recipeCholine;
                                recipeCalcium = servingMultiplier * recipeCalcium;
                                recipeChloride = servingMultiplier * recipeChloride;
                                recipeChromium = servingMultiplier * recipeChromium;
                                recipeCopper = servingMultiplier * recipeCopper;
                                recipeFluoride = servingMultiplier * recipeFluoride;
                                recipeIodine = servingMultiplier * recipeIodine;
                                recipeIron = servingMultiplier * recipeIron;
                                recipeMagnesium = servingMultiplier * recipeMagnesium;
                                recipeManganese = servingMultiplier * recipeManganese;
                                recipeMolybdenum = servingMultiplier * recipeMolybdenum;
                                recipePhosphorus = servingMultiplier * recipePhosphorus;
                                recipePotassium = servingMultiplier * recipePotassium;
                                recipeSelenium = servingMultiplier * recipeSelenium;
                                recipeSodium = servingMultiplier * recipeSodium;
                                recipeSulfur = servingMultiplier * recipeSulfur;
                                recipeZinc = servingMultiplier * recipeZinc;
                                recipeOmega3 = servingMultiplier * recipeOmega3;
                                recipeOmega6 = servingMultiplier * recipeOmega6;
                                recipeALA = servingMultiplier * recipeALA;
                                recipeEPA = servingMultiplier * recipeEPA;
                                recipeDPA = servingMultiplier * recipeDPA;
                                recipeDHA = servingMultiplier * recipeDHA;


                                recipieInformation.recipeCalories = recipeCalories;
                                recipieInformation.recipeFats = recipeFats;
                                recipieInformation.recipeCarbohydrates = recipeCarbs;
                                recipieInformation.recipeProtein = recipeProtein;
                                recipieInformation.recipeCholesterol = recipeCholesterol;
                                recipieInformation.recipeTransFats = recipeTransFat;
                                recipieInformation.recipeSurgar = recipeSugars;
                                recipieInformation.recipeStaFats = recipeSaturatedFat;
                                recipieInformation.recipeFiber = recipeFiber;
                                recipieInformation.recipeVitaminA = recipeVitaminA;
                                recipieInformation.recipeVitaminB1 = recipeVitaminB1;
                                recipieInformation.recipeVitaminB2 = recipeVitaminB2;
                                recipieInformation.recipeVitaminB3 = recipeVitaminB3;
                                recipieInformation.recipeVitaminB5 = recipeVitaminB5;
                                recipieInformation.recipeVitaminB6 = recipeVitaminB6;
                                recipieInformation.recipeVitaminB7 = recipeVitaminB7;
                                recipieInformation.recipeVitaminB9 = recipeVitaminB9;
                                recipieInformation.recipeVitaminB12 = recipeVitaminB12;
                                recipieInformation.recipeVitaminC = recipeVitaminc;
                                recipieInformation.recipeVitaminD = recipeVitaminD;
                                recipieInformation.recipeVitaminE = recipeVitaminE;
                                recipieInformation.recipeVitaminK = recipeVitaminK;
                                recipieInformation.recipeCholine = recipeCholine;
                                recipieInformation.recipeCalcium = recipeCalcium;
                                recipieInformation.recipeChloride = recipeChloride;
                                recipieInformation.recipeChromium = recipeChromium;
                                recipieInformation.recipeCopper = recipeCopper;
                                recipieInformation.recipeFluoride = recipeFluoride;
                                recipieInformation.recipeIodine = recipeIodine;
                                recipieInformation.recipeIron = recipeIron;
                                recipieInformation.recipeMagnesium = recipeMagnesium;
                                recipieInformation.recipeManganese = recipeManganese;
                                recipieInformation.recipeMolybdenum = recipeMolybdenum;
                                recipieInformation.recipePhosphorus = recipePhosphorus;
                                recipieInformation.recipePotassium = recipePotassium;
                                recipieInformation.recipeSelenium = recipeSelenium;
                                recipieInformation.recipeSodium = recipeSodium;
                                recipieInformation.recipeSulfur = recipeSulfur;
                                recipieInformation.recipeZinc = recipeZinc;
                                recipieInformation.recipeOmega3 = recipeOmega3;
                                recipieInformation.recipeOmega6 = recipeOmega6;
                                recipieInformation.recipeALA = recipeALA;
                                recipieInformation.recipeEPA = recipeEPA;
                                recipieInformation.recipeDPA = recipeDPA;
                                recipieInformation.recipeDHA = recipeDHA;
                            }
                        }
                    }
                }
            }

            return recipieInformation;
        }
    }
}