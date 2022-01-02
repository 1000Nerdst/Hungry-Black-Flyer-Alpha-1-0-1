using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace dietNerdAlpha_1._0._1
{
    class GenerateNewMealPlanV1
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public void startGeneration(userMealPlanDataAll mealPlanScreenItems)
        {
            mealNumbers dbMeals = new mealNumbers();
            mealArraySizes mealSizes = new mealArraySizes();
            filledTypeArrays typeArrays = new filledTypeArrays();

            dbMeals = getDataBaseNumber();
            mealSizes = setArraySizes(dbMeals);

            int dbLargestId = dataBaseLargest();
            typeArrays = fillArrays(dbLargestId);

            //start the generation of new plan
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            mealTotals totals = new mealTotals();

            double targetCalorieNumber = double.Parse(config.AppSettings.Settings["totalDailyIntake"].Value);

            int breakfastNumber = int.Parse(config.AppSettings.Settings["breakfastNumber"].Value);
            int midMorningNumber = int.Parse(config.AppSettings.Settings["midMorningNumber"].Value);
            int lunchNumber = int.Parse(config.AppSettings.Settings["lunchNumber"].Value);
            int afternoonNumber = int.Parse(config.AppSettings.Settings["afternoonNumber"].Value);
            int dinnerNumber = int.Parse(config.AppSettings.Settings["dinnerNumber"].Value);
            int nightNumber = int.Parse(config.AppSettings.Settings["nightNumber"].Value);
            string startingDate = mealPlanScreenItems.mealPlanStartDate.ToString();
            //config.AppSettings.Settings["mealPlanStartDate"].ToString();
            string endingDate = mealPlanScreenItems.mealPlanEndDate.ToString();
            var startingDateParsed = DateTime.Parse(startingDate);
            var endingDateParsed = DateTime.Parse(endingDate);
            int intervals = mealPlanScreenItems.mealPrepInterval;

            planTotals planTotal = intializeTotals();

            //get the meal plan name from screen
            string mealPlanName = config.AppSettings.Settings["mealPlanName"].Value;

            //set up json fiile
            string jsonFile;
            jsonFile = mealPlanName + ".json";

            //var jsonPlan = new jsonFile();


            //jsonPlan.planName = mealPlanName;

            //start parts of the meal plan
            //var jsonStartDateList = new List<jsonStartDate>();
            //jsonPlan.startDates = new List<jsonStartDate>();

            //var jsonStartingDate = new jsonStartDate();
            DateTime startingDateHolder = startingDateParsed;
            //for loop for the amount of meals days needed based on user data
            for (int mealsPlanned = 0; startingDateParsed.AddDays(mealsPlanned * intervals) < endingDateParsed; mealsPlanned++)
            {
                planTotals resetTotal = new planTotals();

                bool meetsNutritonGoal = false;
                int attemptNumber = 1;
                while (meetsNutritonGoal == false)
                {
                    string attemptNumberString = attemptNumber.ToString();
                    Debug.WriteLine("attempt number: " + attemptNumberString);
                    bool lunchHasMainCourse = false;
                    bool lunchNeedsSideItems = true;
                    bool dinnerHasMainCourse = false;
                    bool dinnerNeedsSideItems = true;

                    //get current date
                    //this isnt needed right now - it can just be a simple string
                    DateTime currentPlanningDate = startingDateParsed.AddDays(mealsPlanned * intervals);
                    startingDate = currentPlanningDate.ToString();
                    //jsonStartingDate.startingDate = startingDate;

                    //var jsonMealNameList = new List<jsonMealName>();

                    //for loop that determterimes what goes into each breakfast meal(s)
                    for (int i = 0; i < breakfastNumber; i++)
                    {
                        //var jsonMealRecipieNameAndServingList = new List<jsonMealRecipieNameAndServing>();
                        //var jsonMealName = new jsonMealName();

                        string mealType = "Breakfast";

                        int mealNumForJson = i + 1;
                        string jsonBreakfastNumber = mealType + mealNumForJson.ToString();
                        //jsonMealName.mealName = jsonBreakfastNumber;

                        //check to see if their are duplated values
                        //start off assuming there are duplicates
                        bool thereAreDuplicates = true;
                        while (thereAreDuplicates == true)
                        {
                            grabFromDBV1 grabFromDB = new grabFromDBV1();

                            //get a random recipie with the totals from database 
                            totals = grabFromDB.loadMealTotals(mealType, false, true, dbLargestId, mealSizes, typeArrays);
                        }
                    }
                }
            }
        }

        public mealNumbers getDataBaseNumber()
        {
            mealNumbers mealNum = new mealNumbers();
            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True"))
            {
                cn.Open();

                string sqlCountCommand = "SELECT timing, COUNT(*) FROM recipeTable GROUP BY timing;";

                using (cmd = new SqlCommand(sqlCountCommand, cn))
                {
                    dr = cmd.ExecuteReader();

                    string[] meals = new string[20];
                    int[] count = new int[20];

                    int i = 0;
                    while (dr.Read())
                    {
                        char[] removeChars = { ' ', '}' };
                        string currentMeal = dr.GetString(0);
                        currentMeal = currentMeal.TrimEnd(removeChars);

                        int mealCount = dr.GetInt32(1);

                        meals[i] = currentMeal;
                        count[i] = mealCount;

                        i++;
                    }

                    mealNum.meals = meals;
                    mealNum.mealCounts = count;

                }

                cn.Close();
            }

            return mealNum;
        }

        private mealArraySizes setArraySizes(mealNumbers dbMeals)
        {
            mealArraySizes mealSizes = new mealArraySizes();
            string[] mealTypes = dbMeals.meals;
            int[] count = dbMeals.mealCounts;
            int size;
            int breakfastSize = 0; int morningSize = 0; int lunchSize = 0; int afternoonSize = 0; int dinnerSize = 0; int nightSize = 0; int lunchSidesSize = 0; int dinnerSidesSize = 0;

            for (int i = 0; i < 20; i++)
            {
                string currentType = mealTypes[i];

                switch (currentType)
                {
                    case "Breakfast":
                        size = count[i];
                        breakfastSize = breakfastSize + size;
                        break;
                    case "Morning":
                        size = count[i];
                        morningSize = morningSize + size;
                        break;
                    case "Lunch":
                        size = count[i];
                        lunchSize = lunchSize + size;
                        break;
                    case "Afternoon":
                        size = count[i];
                        afternoonSize = afternoonSize + size;
                        break;
                    case "Dinner":
                        size = count[i];
                        dinnerSize = dinnerSize + size;
                        break;
                    case "Night":
                        size = count[i];
                        nightSize = nightSize + size;
                        break;
                    case "Any":
                        size = count[i];
                        breakfastSize = breakfastSize + size;
                        morningSize = morningSize + size;
                        lunchSize = lunchSize + size;
                        afternoonSize = afternoonSize + size;
                        dinnerSize = dinnerSize + size;
                        nightSize = nightSize + size;
                        lunchSidesSize = lunchSidesSize + size;
                        dinnerSidesSize = dinnerSidesSize + size;
                        break;
                    case "Late":
                        size = count[i];
                        dinnerSize = dinnerSize + size;
                        nightSize = nightSize + size;
                        break;
                    case "Early":
                        size = count[i];
                        breakfastSize = breakfastSize + size;
                        morningSize = morningSize + size;
                        break;
                    case "Snack":
                        size = count[i];
                        morningSize = morningSize + size;
                        afternoonSize = afternoonSize + size;
                        nightSize = nightSize + size;
                        break;
                    case "Main Course":
                        size = count[i];
                        lunchSize = lunchSize + size;
                        dinnerSize = dinnerSize + size;
                        break;
                    case "Side":
                        size = count[i];
                        dinnerSidesSize = dinnerSidesSize + size;
                        lunchSidesSize = lunchSidesSize + size;
                        break;
                    case "Lunch Side":
                        size = count[i];
                        lunchSidesSize = lunchSidesSize + size;
                        break;
                    case "Dinner Side":
                        size = count[i];
                        dinnerSidesSize = dinnerSidesSize + size;
                        break;
                    case "Lunch Meal Prep":
                        size = count[i];
                        lunchSize = lunchSize + size;
                        break;
                    case "Dinner Meal Prep":
                        size = count[i];
                        dinnerSize = dinnerSize + size;
                        break;
                    case "Meal Prep":
                        size = count[i];
                        lunchSize = lunchSize + size;
                        dinnerSize = dinnerSize + size;
                        break;
                    case "Breakfast Meal Prep":
                        size = count[i];
                        breakfastSize = breakfastSize + size;
                        break;

                }

            }

            mealSizes.breakfastSize = breakfastSize;
            mealSizes.midMorningSize = morningSize;
            mealSizes.lunchSize = lunchSize;
            mealSizes.afternoonSize = afternoonSize;
            mealSizes.dinnerSize = dinnerSize;
            mealSizes.nightSize = nightSize;
            mealSizes.lunchSidesSize = lunchSidesSize;
            mealSizes.dinnerSidesSize = dinnerSidesSize;

            return mealSizes;
        }

        private int dataBaseLength()
        {
            int dbSize = 0;
            int recipieIdMax, previousMax;

            string SIZE_QUERY = "SELECT COUNT(*) FROM dbo.recipiesTable";

            //DataSet sizeDataSet = new DataSet();

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

        private int dataBaseLargest()
        {
            int largestVal = 0;
            string SIZE_QUERY = "SELECT MAX(recipeId) FROM dbo.recipiesTable";

            using (SqlConnection sizeCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Flyer Pitch\dietNerdAlpha 1.0.0\dietNerdAlpha 1.0.0\bin\Debug\wholeAppDataBase.mdf; Integrated Security = True"))
            {
                using (SqlCommand cmdGetDataCount = new SqlCommand(SIZE_QUERY, sizeCon))
                {
                    sizeCon.Open();
                    largestVal = (int)cmdGetDataCount.ExecuteScalar();
                }
                sizeCon.Close();
            }

            return largestVal;
        }
        private filledTypeArrays fillArrays(int dbLargest)
        {
            filledTypeArrays filledArrays = new filledTypeArrays();
            getRecipieInformation recipieInformation = new getRecipieInformation();
            string currentRecipeName;
            string currentRecipeTiming;

            var breakfastList = new List<string>();
            var mornigList = new List<string>();
            var lunchList = new List<string>();
            var afternoonList = new List<string>();
            var dinnerList = new List<string>();
            var nightList = new List<string>();
            var lunchSidesList = new List<string>();
            var dinnerSidesList = new List<string>();
            var breakfastIDList = new List<string>();
            var mornigIDList = new List<string>();
            var lunchIDList = new List<string>();
            var afternoonIDList = new List<string>();
            var dinnerIDList = new List<string>();
            var nightIDList = new List<string>();
            var lunchSidesIDList = new List<string>();
            var dinnerSidesIDList = new List<string>();

            for (int i = 0; i <= dbLargest; i++)
            {
                recipieInformation = getRecipieFromDatabase(i);

                char[] removeChars = { ' ', '}' };
                currentRecipeName = recipieInformation.recipeName;

                currentRecipeTiming = recipieInformation.recipeTiming;

                if (currentRecipeName != null)
                {
                    currentRecipeName = currentRecipeName.TrimEnd(removeChars);
                    currentRecipeTiming = currentRecipeTiming.TrimEnd(removeChars);
                    string idAsString = i.ToString();

                    switch (currentRecipeTiming)
                    {
                        case "Breakfast":
                            breakfastList.Add(currentRecipeName);
                            breakfastIDList.Add(idAsString);
                            break;
                        case "Morning":
                            mornigList.Add(currentRecipeName);
                            mornigIDList.Add(idAsString);
                            break;
                        case "Lunch":
                            lunchList.Add(currentRecipeName);
                            lunchIDList.Add(idAsString);
                            break;
                        case "Afternoon":
                            afternoonList.Add(currentRecipeName);
                            afternoonIDList.Add(idAsString);
                            break;
                        case "Dinner":
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            break;
                        case "Night":
                            nightList.Add(currentRecipeName);
                            nightIDList.Add(idAsString);
                            break;
                        case "Any":
                            breakfastList.Add(currentRecipeName);
                            breakfastIDList.Add(idAsString);
                            mornigList.Add(currentRecipeName);
                            mornigIDList.Add(idAsString);
                            lunchList.Add(currentRecipeName);
                            lunchIDList.Add(idAsString);
                            afternoonList.Add(currentRecipeName);
                            afternoonIDList.Add(idAsString);
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            nightList.Add(currentRecipeName);
                            nightIDList.Add(idAsString);
                            lunchSidesList.Add(currentRecipeName);
                            lunchSidesIDList.Add(idAsString);
                            dinnerSidesList.Add(currentRecipeName);
                            dinnerSidesIDList.Add(idAsString);
                            break;
                        case "Late":
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            nightList.Add(currentRecipeName);
                            nightIDList.Add(idAsString);
                            break;
                        case "Early":
                            breakfastList.Add(currentRecipeName);
                            breakfastIDList.Add(idAsString);
                            mornigList.Add(currentRecipeName);
                            mornigIDList.Add(idAsString);
                            break;
                        case "Snack":
                            mornigList.Add(currentRecipeName);
                            mornigIDList.Add(idAsString);
                            afternoonList.Add(currentRecipeName);
                            afternoonIDList.Add(idAsString);
                            nightList.Add(currentRecipeName);
                            nightIDList.Add(idAsString);
                            break;
                        case "Main Course":
                            lunchList.Add(currentRecipeName);
                            lunchIDList.Add(idAsString);
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            break;
                        case "Side":
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            lunchSidesList.Add(currentRecipeName);
                            lunchSidesIDList.Add(idAsString);
                            break;
                        case "Lunch Side":
                            lunchSidesList.Add(currentRecipeName);
                            lunchSidesIDList.Add(idAsString);
                            break;
                        case "Dinner Side":
                                                        dinnerSidesList.Add(currentRecipeName);
                            dinnerSidesIDList.Add(idAsString);
                            break;
                        case "Lunch Meal Prep":
                            lunchList.Add(currentRecipeName);
                            lunchIDList.Add(idAsString);
                            break;
                        case "Dinner Meal Prep":
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            break;
                        case "Meal Prep":
                            lunchList.Add(currentRecipeName);
                            lunchIDList.Add(idAsString);
                            dinnerList.Add(currentRecipeName);
                            dinnerIDList.Add(idAsString);
                            break;
                        case "Breakfast Meal Prep":
                            breakfastList.Add(currentRecipeName);
                            breakfastIDList.Add(idAsString);
                            break;
                    }
                }
            }

            filledArrays.breakfastRecipes = breakfastList.ToArray();
            filledArrays.breakfastRecipesIDs = breakfastIDList.ToArray();
            filledArrays.midMorningRecipes = mornigList.ToArray();
            filledArrays.midMorningRecipesIDs = mornigIDList.ToArray();
            filledArrays.lunchRecipes = lunchList.ToArray();
            filledArrays.lunchRecipesIDs = lunchIDList.ToArray();
            filledArrays.afternoonRecipes = afternoonList.ToArray();
            filledArrays.afternoonRecipesIDs = afternoonIDList.ToArray();
            filledArrays.dinnerRecipes = dinnerList.ToArray();
            filledArrays.dinnerRecipesIDs = dinnerIDList.ToArray();
            filledArrays.nightRecipes = nightList.ToArray();
            filledArrays.nightRecipesIDs = nightIDList.ToArray();
            filledArrays.lunchSidesRecipes = lunchSidesList.ToArray();
            filledArrays.lunchSidesRecipesIDs = lunchSidesIDList.ToArray();
            filledArrays.dinnerRecipes = dinnerSidesList.ToArray();
            filledArrays.dinnerSidesRecipesIDs = dinnerSidesIDList.ToArray();

            return filledArrays;
        }

        private getRecipieInformation getRecipieFromDatabase(int RecipieID)
        {
            getRecipieInformation recipieInformation = new getRecipieInformation();

            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True"))
            {
                cn.Open();

                string commandString = string.Format("Select * from dbo.recipiesTable where recipeId = '{0}'", RecipieID);

                using (SqlCommand cmdRecipie = new SqlCommand(commandString, cn))
                {
                    using (dr = cmdRecipie.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                string recipeName = (string)dr["recipeName"];
                                double recipeCalorie = (double)dr["recipeCalorie"];
                                string recipeMealTiming = (string)dr["recipeMealTiming"];
                                double recipeServes = (double)dr["recipeServes"];
                                //double recipeServings = (double)dr["recipeServings"];
                                double recipeProtien = (double)dr["recipeProtien"];
                                double recipeCarbs = (double)dr["recipeCarbs"];
                                double recipeFats = (double)dr["recipeFats"];
                                double recipeSodium = (double)dr["recipeSodium"];
                                double recipeCholesterol = (double)dr["recipeCholesterol"];
                                double recipieVitaminD = (double)dr["recipeVitaminD"];
                                double recipieCalcium = (double)dr["recipeCalcium"];
                                double recipieIron = (double)dr["recipeIron"];
                                double recipiePotassium = (double)dr["recipePotassium"];
                                double recipieVitaminA = (double)dr["recipeVitaminA"];
                                double recipieVitaminC = (double)dr["recipeVitaminC"];
                                double recipieVitaminE = (double)dr["recipeVitaminE"];
                                double recipieVitaminB6 = (double)dr["recipeVitaminB6"];
                                double recipieMagnesium = (double)dr["recipeMagnesium"];
                                double recipieZinc = (double)dr["recipeZinc"];

                                recipieInformation.recipeName = recipeName;
                                recipieInformation.recipeCalories = recipeCalorie;
                                recipieInformation.recipeTiming = recipeMealTiming;
                                recipieInformation.recipeServing = recipeServes;
                                //recipieInformation.recipeServings = recipeServings;
                                recipieInformation.recipeProtein = recipeProtien;
                                recipieInformation.recipeCarbs = recipeCarbs;
                                recipieInformation.recipeFats = recipeFats;
                                recipieInformation.recipeSodium = recipeSodium;
                                recipieInformation.recipeCholesterol = recipeCholesterol;
                                recipieInformation.recipeVitaminD = recipieVitaminD;
                                recipieInformation.recipeCalcium = recipieCalcium;
                                recipieInformation.recipeIron = recipieIron;
                                recipieInformation.recipePotassium = recipiePotassium;
                                recipieInformation.recipeVitaminA = recipieVitaminA;
                                recipieInformation.recipeVitaminC = recipieVitaminC;
                                recipieInformation.recipeVitaminE = recipieVitaminE;
                                recipieInformation.recipeVitaminB6 = recipieVitaminB6;
                                recipieInformation.recipeMagnesium = recipieMagnesium;
                                recipieInformation.recipeZinc = recipieZinc;
                            }
                        }
                    }
                }
            }

            return recipieInformation;
        }

        private planTotals intializeTotals()
        {
            planTotals total = new planTotals();

            ////types of thing to be removed from strings
            //string[,] newMealArray = new string[,] { { "", "" }, { "", "" }, { "", "" }, { "", "" } };

            //total.mealArray = newMealArray;
            total.totalCalories = 0;
            total.totalProtein = 0;
            total.totalCarbs = 0;
            total.totalFats = 0;
            total.totalSodium = 0;
            total.totalCholesterol = 0;
            total.totalVitaminD = 0;
            total.totalCalcium = 0;
            total.totalIron = 0;
            total.totalPotassium = 0;
            total.totalVitaminA = 0;
            total.totalVitaminC = 0;
            total.totalVitaminE = 0;
            total.totalVitaminB6 = 0;
            total.totalMagnesium = 0;
            total.totalZinc = 0;

            return total;
        }

    }
}
