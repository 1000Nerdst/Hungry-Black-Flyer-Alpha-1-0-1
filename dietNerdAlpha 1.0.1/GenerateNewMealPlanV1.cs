using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            float targetCalorieNumber = float.Parse(config.AppSettings.Settings["totalDailyIntake"].Value);

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

            mealTotals planTotal = new mealTotals();

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
            DateTime currentDateHolder = new DateTime();
            //for loop for the amount of meals days needed based on user data

            string documentText = "Meal Plan:" + mealPlanName + "\n\n";

            List<string> mealTypeCount = new List<string>();
            List<string> numberInMeal = new List<string>();
            List<string> allRecpiesNames = new List<string>();
            List<string> allRecpiesNamesToSave = new List<string>();
            List<string> allRecpiesNamesTemp = new List<string>();
            for (int mealsPlanned = 0; startingDateParsed.AddDays(mealsPlanned * intervals) < endingDateParsed; mealsPlanned++)
            {
                planTotals resetTotal = new planTotals();

                currentDateHolder = startingDateParsed.AddDays(mealsPlanned * intervals);

                bool meetsNutritonGoal = false;
                int attemptNumber = 1;
                while (meetsNutritonGoal == false)
                {
                    allRecpiesNamesTemp = allRecpiesNames;
                    allRecpiesNamesToSave = allRecpiesNames;

                    mealTypeCount.Clear();
                    numberInMeal.Clear();

                    planTotal = intializeTotals();
                    List<string> allNames = new List<string>();
                    string[] allNamesArray = new string[40]; 
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
                        List<string> namesList = new List<string>();
                        List<string> namesListTemp = new List<string>();
                        while (thereAreDuplicates == true)
                        {
                            grabFromDBV1 grabFromDB = new grabFromDBV1();

                            //get a random recipie with the totals from database 
                            totals = grabFromDB.loadMealTotals(mealType, false, true, dbLargestId, mealSizes, typeArrays);

                            string[,] nameAndServing = totals.namesAndServing;

                            string[] names = GetNamesFromArray(nameAndServing);

                            //List<string> namesList = names.ToList();

                            //List<string> temp = (List<string>)allNames.Concat(names);

                            //allNames = temp;

                            //allNamesArray = allNames.ToArray();

                            thereAreDuplicates = CheckDuplicates(names);

                            allRecpiesNamesTemp = allRecpiesNames;

                            if (thereAreDuplicates == false)
                            {
                                namesListTemp = names.ToList();
                                allRecpiesNamesTemp = allRecpiesNamesTemp.Concat(namesListTemp).ToList();

                                thereAreDuplicates = CheckDuplicates(allRecpiesNamesTemp.ToArray());
                            }
                            namesListTemp = names.ToList();
                        }

                        allRecpiesNamesToSave = allRecpiesNamesToSave.Concat(namesListTemp).ToList();

                        planTotal = AddToTotal(totals, planTotal);
                        string numberOfItems = totals.totalItemsInMeal.ToString();
                        mealTypeCount.Add(jsonBreakfastNumber);
                        numberInMeal.Add(numberOfItems);
                    }
                    for (int i = 0; i < lunchNumber; i++)
                    {
                        //var jsonMealRecipieNameAndServingList = new List<jsonMealRecipieNameAndServing>();
                        //var jsonMealName = new jsonMealName();

                        string mealType = "Lunch";

                        int mealNumForJson = i + 1;
                        string jsonBreakfastNumber = mealType + mealNumForJson.ToString();
                        //jsonMealName.mealName = jsonBreakfastNumber;

                        //check to see if their are duplated values
                        //start off assuming there are duplicates
                        bool thereAreDuplicates = true;
                        List<string> namesList = new List<string>();
                        List<string> namesListTemp = new List<string>();
                        while (thereAreDuplicates == true)
                        {
                            grabFromDBV1 grabFromDB = new grabFromDBV1();

                            //get a random recipie with the totals from database 
                            totals = grabFromDB.loadMealTotals(mealType, false, true, dbLargestId, mealSizes, typeArrays);

                            string[,] nameAndServing = totals.namesAndServing;

                            string[] names = GetNamesFromArray(nameAndServing);

                            thereAreDuplicates = CheckDuplicates(names);

                            allRecpiesNamesTemp = allRecpiesNames;

                            if (thereAreDuplicates == false)
                            {
                                namesListTemp = names.ToList();
                                allRecpiesNamesTemp = allRecpiesNamesTemp.Concat(namesListTemp).ToList();

                                thereAreDuplicates = CheckDuplicates(allRecpiesNamesTemp.ToArray());
                            }
                            namesListTemp = names.ToList();
                        }

                        allRecpiesNamesToSave = allRecpiesNamesToSave.Concat(namesListTemp).ToList();

                        planTotal = AddToTotal(totals, planTotal);
                        string numberOfItems = totals.totalItemsInMeal.ToString();
                        mealTypeCount.Add(jsonBreakfastNumber);
                        numberInMeal.Add(numberOfItems);
                    }
                    for (int i = 0; i < dinnerNumber; i++)
                    {
                        //var jsonMealRecipieNameAndServingList = new List<jsonMealRecipieNameAndServing>();
                        //var jsonMealName = new jsonMealName();

                        string mealType = "Dinner";

                        int mealNumForJson = i + 1;
                        string jsonBreakfastNumber = mealType + mealNumForJson.ToString();
                        //jsonMealName.mealName = jsonBreakfastNumber;

                        //check to see if their are duplated values
                        //start off assuming there are duplicates
                        bool thereAreDuplicates = true;
                        List<string> namesList = new List<string>();
                        List<string> namesListTemp = new List<string>();
                        while (thereAreDuplicates == true)
                        {
                            grabFromDBV1 grabFromDB = new grabFromDBV1();

                            //get a random recipie with the totals from database 
                            totals = grabFromDB.loadMealTotals(mealType, false, true, dbLargestId, mealSizes, typeArrays);

                            string[,] nameAndServing = totals.namesAndServing;

                            string[] names = GetNamesFromArray(nameAndServing);

                            thereAreDuplicates = CheckDuplicates(names);
                            allRecpiesNamesTemp = allRecpiesNames;

                            if (thereAreDuplicates == false)
                            {
                                namesListTemp = names.ToList();
                                allRecpiesNamesTemp = allRecpiesNamesTemp.Concat(namesListTemp).ToList();

                                thereAreDuplicates = CheckDuplicates(allRecpiesNamesTemp.ToArray());
                            }
                            namesListTemp = names.ToList();
                        }

                        allRecpiesNamesToSave = allRecpiesNamesToSave.Concat(namesListTemp).ToList();

                        planTotal = AddToTotal(totals, planTotal);
                        string numberOfItems = totals.totalItemsInMeal.ToString();
                        mealTypeCount.Add(jsonBreakfastNumber);
                        numberInMeal.Add(numberOfItems);
                    }

                    meetsNutritonGoal = CheckMacrosAndMicros(planTotal, targetCalorieNumber);

                    

                    //meets calories

                    attemptNumber++;
                    Debug.WriteLine("total calories: " + planTotal.totalCalories.ToString());
                }

                //allRecpiesNames = allRecpiesNames.Concat(allRecpiesNamesToSave).ToList();
                allRecpiesNames = allRecpiesNamesToSave;
                int test = 0;

                string currentDateString = currentDateHolder.ToString();
                string calories = planTotal.totalCalories.ToString();
                string protien = planTotal.totalProtein.ToString();
                string fats = planTotal.totalFats.ToString();
                string carbs = planTotal.totalCarbohydrates.ToString();

                string allMeals = FormatMealsToText(planTotal, mealTypeCount, numberInMeal);

                documentText = documentText + "Starting Date: " + currentDateString + "\n";
                documentText = documentText + "Total Calories: " + calories + "Total Protien: " + protien + "Total Fats: " + fats + "Total Carbs: " + carbs + "\n";
                documentText = documentText + allMeals + "\n";

                
                //make a document
            }
            CreatTextDoc(documentText, mealPlanName);

            //display new plan
            config.AppSettings.Settings["completedMealPlan"].Value = documentText;
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            DisplayMealPlanForm displayMealPlanForm = new DisplayMealPlanForm();
            displayMealPlanForm.ShowDialog();
        }

        private void CreatTextDoc(string documentText,string planName)
        {
            string fileName = string.Format(@"C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\Meal Plans\{0}.txt",planName);

            StringBuilder textContent = new StringBuilder();
            StringBuilder tempContent = new StringBuilder();

            tempContent.AppendLine(" ");
            File.AppendAllText(fileName, tempContent.ToString());
            File.Delete(fileName);

            textContent.AppendLine(documentText);
            File.AppendAllText(fileName, textContent.ToString());

            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine(documentText);
                }
            }

            //try
            //{
            //    if (File.Exists(fileName))
            //    {
            //        File.Delete(fileName);
            //    }

            //    using (FileStream fs = File.Create(fileName))
            //    {
            //        Byte[] text = new UTF8Encoding(true).GetBytes(documentText);
            //        fs.Write(text, 0, text.Length);
            //    }
            //}
            //catch(Exception Ex)
            //{
            //    Console.WriteLine(Ex.ToString());
            //}
        }

        private string FormatMealsToText(mealTotals planTotal, List<string> mealTypeCount, List<string> numberInMeal)
        {
            string toReturn = "";

            string[,] namesAndServings = planTotal.namesAndServing;

            string[] names = new string[(namesAndServings.Length)/2];
            string[] servings = new string[(namesAndServings.Length) / 2];

            int i = 1;
            int index = 0;
            foreach(string nameOrServing in namesAndServings)
            {
                bool isEven = IsNumberEven(i);
                if(isEven != true)
                {
                    names[index] = nameOrServing;
                }
                if (isEven == true)
                {
                    servings[index] = nameOrServing;
                    index++;
                }
                i++;
            }

            int startingArrayIndex = 1;
            i = 0;
            foreach(string meal in mealTypeCount)
            {
                int add = int.Parse(numberInMeal[i]);
                int endingIndex = startingArrayIndex + add;

                toReturn = toReturn + meal + " :\n";

                int arrayIndex = startingArrayIndex;
                while (arrayIndex < endingIndex)
                {
                    if(arrayIndex < names.Length)
                    {
                        string printName = names[arrayIndex];
                        string printServings = servings[arrayIndex];

                        char[] removeChars = { ' ', '}' };
                        printName = printName.TrimEnd(removeChars);

                        toReturn = toReturn + "\t Recipie: " + printName + " Serving Size: " + printServings + "\n";
                    }
                    arrayIndex++;
                }
                startingArrayIndex = endingIndex;
            }

            return toReturn;
        }

        private bool CheckMacrosAndMicros(mealTotals planTotal, float targetCalorieNumber)
        {

            bool meetsNutritonGoal = false;
            bool meetMacros = false;

            float lowerLimitCalories = (float)(targetCalorieNumber - (targetCalorieNumber * .01));
            float upperLimitCalories = (float)(targetCalorieNumber + (targetCalorieNumber * .01));

            float protienGoalGrams = ((float)((targetCalorieNumber * 0.25) / 4));
            float fatGoalGrams = ((float)((targetCalorieNumber * 0.6) / 9));

            bool meetsCalories = CheckCalories(planTotal, lowerLimitCalories, upperLimitCalories);
            if (meetsCalories == true)
            {
                meetMacros = CheckMacros(planTotal, protienGoalGrams, fatGoalGrams);
            }


            if (meetsCalories == true && meetMacros == true)
            {
                meetsNutritonGoal = true;
            }

            return meetsNutritonGoal;
        }

        private bool CheckMacros(mealTotals planTotal, float protienGoalGrams, float fatGoalGrams)
        {
            bool meetsMacros = false;

            float planTotalProtien = planTotal.totalProtein;
            float planTotalFat = planTotal.totalFats;
            float planTotalCarbs = planTotal.totalCarbohydrates;

            if (protienGoalGrams < planTotalProtien && fatGoalGrams < planTotalFat)
            {
                meetsMacros = true;
            }
            return meetsMacros;
        }

        private bool CheckCalories(mealTotals planTotal, float lowerLimitCalories, float upperLimitCalories)
        {
            bool meetsCalories = false;

            float planTotalCalories = planTotal.totalCalories;
            if(lowerLimitCalories < planTotalCalories && planTotalCalories < upperLimitCalories)
            {
                meetsCalories = true;
            }

            if(meetsCalories == true)
            {
                int pause = 0;
            }
            return meetsCalories;
        }

        private mealTotals AddToTotal(mealTotals totals, mealTotals planTotal)
        {
            List<string> totalNameString = new List<string>();
            List<string> totalSizeString = new List<string>();
            List<string> planTotalNameString = new List<string>();
            List<string> planTotalSizeString = new List<string>();

            totals.totalCalories = totals.totalCalories + planTotal.totalCalories;
            totals.totalFats = totals.totalFats + planTotal.totalFats;
            totals.totalCarbohydrates = totals.totalCarbohydrates + planTotal.totalCarbohydrates;
            totals.totalProtein = totals.totalProtein + planTotal.totalProtein;
            totals.totalCholesterol = totals.totalCholesterol + planTotal.totalCholesterol;
            totals.totalTransFats = totals.totalTransFats + planTotal.totalTransFats;
            totals.totalSurgar = totals.totalSurgar + planTotal.totalSurgar;
            totals.totalStaFats = totals.totalStaFats + planTotal.totalStaFats;
            totals.totalFiber = totals.totalFiber + planTotal.totalFiber;
            totals.totalVitaminA = totals.totalVitaminA + planTotal.totalVitaminA;
            totals.totalVitaminB1 = totals.totalVitaminB1 + planTotal.totalVitaminB1;
            totals.totalVitaminB2 = totals.totalVitaminB2 + planTotal.totalVitaminB2;
            totals.totalVitaminB3 = totals.totalVitaminB3 + planTotal.totalVitaminB3;
            totals.totalVitaminB5 = totals.totalVitaminB5 + planTotal.totalVitaminB5;
            totals.totalVitaminB6 = totals.totalVitaminB6 + planTotal.totalVitaminB6;
            totals.totalVitaminB7 = totals.totalVitaminB7 + planTotal.totalVitaminB7;
            totals.totalVitaminB9 = totals.totalVitaminB9 + planTotal.totalVitaminB9;
            totals.totalVitaminB12 = totals.totalVitaminB12 + planTotal.totalVitaminB12;
            totals.totalVitaminC = totals.totalVitaminC + planTotal.totalVitaminC;
            totals.totalVitaminD = totals.totalVitaminD + planTotal.totalVitaminD;
            totals.totalVitaminE = totals.totalVitaminE + planTotal.totalVitaminE;
            totals.totalVitaminK = totals.totalVitaminK + planTotal.totalVitaminK;
            totals.totalCholine = totals.totalCholine + planTotal.totalCholine;
            totals.totalCalcium = totals.totalCalcium + planTotal.totalCalcium;
            totals.totalChloride = totals.totalChloride + planTotal.totalChloride;
            totals.totalChromium = totals.totalChromium + planTotal.totalChromium;
            totals.totalCopper = totals.totalCopper + planTotal.totalCopper;
            totals.totalFluoride = totals.totalFluoride + planTotal.totalFluoride;
            totals.totalIodine = totals.totalIodine + planTotal.totalIodine;
            totals.totalIron = totals.totalIron + planTotal.totalIron;
            totals.totalMagnesium = totals.totalMagnesium + planTotal.totalMagnesium;
            totals.totalManganese = totals.totalManganese + planTotal.totalManganese;
            totals.totalMolybdenum = totals.totalMolybdenum + planTotal.totalMolybdenum;
            totals.totalPhosphorus = totals.totalPhosphorus + planTotal.totalPhosphorus;
            totals.totalPotassium = totals.totalPotassium + planTotal.totalPotassium;
            totals.totalSelenium = totals.totalSelenium + planTotal.totalSelenium;
            totals.totalSodium = totals.totalSodium + planTotal.totalSodium;
            totals.totalSulfur = totals.totalSulfur + planTotal.totalSulfur;
            totals.totalZinc = totals.totalZinc + planTotal.totalZinc;
            totals.totalOmega3 = totals.totalOmega3 + planTotal.totalOmega3;
            totals.totalOmega6 = totals.totalOmega6 + planTotal.totalOmega6;
            totals.totalALA = totals.totalALA + planTotal.totalALA;
            totals.totalEPA = totals.totalEPA + planTotal.totalEPA;
            totals.totalDPA = totals.totalDPA + planTotal.totalDPA;
            totals.totalDHA = totals.totalDHA + planTotal.totalDHA;

            int i = 1;
            if (planTotal.namesAndServing != null)
            {
                foreach (string name in planTotal.namesAndServing)
                {
                    bool isEven = IsNumberEven(i);
                    if (isEven == false)
                    {
                        totalNameString.Add(name);
                    }
                    if (isEven == true)
                    {
                        totalSizeString.Add(name);
                    }
                    i++;
                }
            }
            
            if(totals.namesAndServing != null)
            {
                i = 1;
                foreach (string name in totals.namesAndServing)
                {
                    bool isEven = IsNumberEven(i);
                    if (isEven == false)
                    {
                        totalNameString.Add(name);
                    }
                    if (isEven == true)
                    {
                        totalSizeString.Add(name);
                    }
                    i++;
                }
            }

            string[] totalNameArray = totalNameString.ToArray();
            string[] totalSizeArray = totalSizeString.ToArray();

            string[,] meals = CombineArrays(totalNameArray, totalSizeArray);

            totals.namesAndServing = meals;


            return totals;
        }

        private bool IsNumberEven(int i)
        {
            bool isEven = new bool();
            if(i % 2 == 0)
            {
                isEven = true;
            }
            if (i % 2 != 0)
            {
                isEven = false;
            }

            return isEven;
        }

        private string[,] CombineArrays(string[] columb1, string[] columb2)
        {
            int rows = columb1.Length;

            string[,] finalArray = new string[rows, 2];

            int row = 0;
            foreach (string item in columb1)
            {
                finalArray[row, 0] = columb1[row];
                finalArray[row, 1] = columb2[row];
                row++;
            }

            return finalArray;
        }

        private string[] GetNamesFromArray(string[,] nameAndServing)
        {
            int arrayLenght = (nameAndServing.Length/2);
            string[] names = new string[arrayLenght];

            for(int i = 0; i < arrayLenght; i++)
            {
                names[i] = nameAndServing[i, 0];
            }

            return names;
        }

        private bool CheckDuplicates(string[] names)
        {
            bool containtsDups = false;
            if(names.Length != names.Distinct().Count())
            {
                containtsDups = true;
            }

            return containtsDups;
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
                            dinnerSidesList.Add(currentRecipeName);
                            dinnerSidesIDList.Add(idAsString);
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

                string commandString = string.Format("Select * from dbo.recipeTable where Id = '{0}'", RecipieID);

                using (SqlCommand cmdRecipie = new SqlCommand(commandString, cn))
                {
                    using (dr = cmdRecipie.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                string recipeName = dr["recipeName"].ToString();
                                string recipeTiming = dr["timing"].ToString();
                                float recipeServings = float.Parse(dr["Servings"].ToString());
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

                                recipieInformation.recipeName = recipeName;
                                recipieInformation.recipeCalories = recipeCalories;
                                recipieInformation.recipeTiming = recipeTiming;
                                recipieInformation.recipeServing = recipeServings;
                                //recipieInformation.recipeServings = recipeServings;
                                recipieInformation.recipeCalories = recipeCalories;
                                recipieInformation.recipeFats = recipeFats;
                                //recipieInformation.recipeCarbohydrates = recipeCarbs;
                                recipieInformation.recipeProtein = recipeProtein;
                                recipieInformation.recipeCholesterol = recipeCholesterol;
                                //recipieInformation.recipeTransFats = recipeTransFat;
                                //recipieInformation.recipeSurgar = recipeSugars;
                                //recipieInformation.recipeStaFats = recipeSaturatedFat;
                                //recipieInformation.recipeFiber = recipeFiber;
                                recipieInformation.recipeVitaminA = recipeVitaminA;
                                //recipieInformation.recipeVitaminB1 = recipeVitaminB1;
                                //recipieInformation.recipeVitaminB2 = recipeVitaminB2;
                                //recipieInformation.recipeVitaminB3 = recipeVitaminB3;
                                //recipieInformation.recipeVitaminB5 = recipeVitaminB5;
                                //recipieInformation.recipeVitaminB6 = recipeVitaminB6;
                                //recipieInformation.recipeVitaminB7 = recipeVitaminB7;
                                //recipieInformation.recipeVitaminB9 = recipeVitaminB9;
                                //recipieInformation.recipeVitaminB12 = recipeVitaminB12;
                                recipieInformation.recipeVitaminC = recipeVitaminc;
                                recipieInformation.recipeVitaminD = recipeVitaminD;
                                recipieInformation.recipeVitaminE = recipeVitaminE;
                                //recipieInformation.recipeVitaminK = recipeVitaminK;
                                //recipieInformation.recipeCholine = recipeCholine;
                                //recipieInformation.recipeCalcium = recipeCalcium;
                                //recipieInformation.recipeChloride = recipeChloride;
                                //recipieInformation.recipeChromium = recipeChromium;
                                //recipieInformation.recipeCopper = recipeCopper;
                                //recipieInformation.recipeFluoride = recipeFluoride;
                                //recipieInformation.recipeIodine = recipeIodine;
                                recipieInformation.recipeIron = recipeIron;
                                recipieInformation.recipeMagnesium = recipeMagnesium;
                                //recipieInformation.recipeManganese = recipeManganese;
                                //recipieInformation.recipeMolybdenum = recipeMolybdenum;
                                //recipieInformation.recipePhosphorus = recipePhosphorus;
                                recipieInformation.recipePotassium = recipePotassium;
                                //recipieInformation.recipeSelenium = recipeSelenium;
                                recipieInformation.recipeSodium = recipeSodium;
                                //recipieInformation.recipeSulfur = recipeSulfur;
                                recipieInformation.recipeZinc = recipeZinc;
                                //recipieInformation.recipeOmega3 = recipeOmega3;
                                //recipieInformation.recipeOmega6 = recipeOmega6;
                                //recipieInformation.recipeALA = recipeALA;
                                //recipieInformation.recipeEPA = recipeEPA;
                                //recipieInformation.recipeDPA = recipeDPA;
                                //recipieInformation.recipeDHA = recipeDHA;
                            }
                        }
                    }
                }
            }

            return recipieInformation;
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

            string[,] nameAndSize = new string[1,2];

            total.namesAndServing = nameAndSize;

            return total;
        }

    }
}
