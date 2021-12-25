using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dietNerdAlpha_1._0._1
{
    class userDataClass
    {
        public string userGender { get; internal set; }
        public int userAge { get; internal set; }
        public float userHeight { get; internal set; }
        public int userWeight { get; internal set; }
        public int userWeeklyActiveDays { get; internal set; }
        public string userWeeklyWaitChangeGoal { get; internal set; }
    }

    class userCalulatedData
    {
        public float totalDailyCal { get; internal set; }
        public float dailyMaintiance { get; internal set; }
        public float minWaterIntake { get; internal set; }

    }

    class usersMealsPerDay
    {
        public string mealPlanName { get; internal set; }
        public DateTime mealPlanStartDate { get; internal set; }
        public DateTime mealPlanEndDate { get; internal set; }
        public int mealPrepInterval { get; internal set; }
        public int mealsPerDay { get; internal set; }
    }

    class userDailyMealTiming
    {
        public int breakfastMeals { get; internal set; }
        public int midmorningMeals { get; internal set; }
        public int lunchMeals { get; internal set; }
        public int afternoonMeals { get; internal set; }
        public int dinnerMeals { get; internal set; }
        public int nightMeals { get; internal set; }

    }

    class userMealPlanDataAll
    {
        public string mealPlanName { get; internal set; }
        public DateTime mealPlanStartDate { get; internal set; }
        public DateTime mealPlanEndDate { get; internal set; }
        public float numberOfMealToPlan { get; internal set; }
        public int mealPrepInterval { get; internal set; }
        public int mealsPerDay { get; internal set; }
        public int breakfastMeals { get; internal set; }
        public int midmorningMeals { get; internal set; }
        public int lunchMeals { get; internal set; }
        public int afternoonMeals { get; internal set; }
        public int dinnerMeals { get; internal set; }
        public int nightMeals { get; internal set; }

    }

    class newRecipieNutrition
    {
        public int totalCalories { get; internal set; }
        public int totalProtein { get; internal set; }
        public int totalCarbs { get; internal set; }
        public int totalFats { get; internal set; }
        public int totalCalcium { get; internal set; }
        public int totalSodium { get; internal set; }
        public int totalCholesterol { get; internal set; }
        public int totalVitaminD { get; internal set; }
        public int totalIron { get; internal set; }
        public int totalPotassium { get; internal set; }
        public int totalVitaminA { get; internal set; }
        public int totalVitaminC { get; internal set; }
        public int totalVitaminE { get; internal set; }
        public int totalVitaminB6 { get; internal set; }
        public int totalMagnesium { get; internal set; }
        public int totalZinc { get; internal set; }
        public float recipieServing { get; internal set; }

    }

    class editIngredientClass
    {
        public int foodID { get; internal set; }
        public string foodItem { get; internal set; }
        public int foodCalories { get; internal set; }
        public double foodServing { get; internal set; }
        public double foodStdServingSize { get; internal set; }
        public int foodProtein { get; internal set; }
        public int foodCarbs { get; internal set; }
        public int foodFats { get; internal set; }
        public double foodCalcium { get; internal set; }
        public double foodSodium { get; internal set; }
        public double foodCholesterol { get; internal set; }
        public double foodVitaminD { get; internal set; }
        public double foodIron { get; internal set; }
        public double foodPotassium { get; internal set; }
        public double foodVitaminA { get; internal set; }
        public double foodVitaminC { get; internal set; }
        public double foodVitaminE { get; internal set; }
        public double foodVitaminB6 { get; internal set; }
        public double foodMagnesium { get; internal set; }
        public double foodZinc { get; internal set; }
        public string foodUnit { get; internal set; }
        public string foodTiming { get; internal set; }
    }

    class getRecipieInformation
    {
        public string recipeName { get; internal set; }
        public double recipeCalories { get; internal set; }
        public string recipeTiming { get; internal set; }
        public double recipeServing { get; internal set; }
        public double recipeServings { get; internal set; }
        public double newRecipeServings { get; internal set; }
        public double recipeProtein { get; internal set; }
        public double recipeCarbs { get; internal set; }
        public double recipeFats { get; internal set; }
        public double recipeCalcium { get; internal set; }
        public double recipeSodium { get; internal set; }
        public double recipeCholesterol { get; internal set; }
        public double recipeVitaminD { get; internal set; }
        public double recipeIron { get; internal set; }
        public double recipePotassium { get; internal set; }
        public double recipeVitaminA { get; internal set; }
        public double recipeVitaminC { get; internal set; }
        public double recipeVitaminE { get; internal set; }
        public double recipeVitaminB6 { get; internal set; }
        public double recipeMagnesium { get; internal set; }
        public double recipeZinc { get; internal set; }
        public string recipeUnit { get; internal set; }
        public bool hasMainCourse { get; internal set; }
        public bool needsSideItems { get; internal set; }
        public bool previousIsMealPrep { get; internal set; }
    }

    class mealTotals
    {
        public string[,] mealArray { get; internal set; }
        public double totalCalories { get; internal set; }
        public double totalProtein { get; internal set; }
        public double totalCarbs { get; internal set; }
        public double totalFats { get; internal set; }
        public double totalCalcium { get; internal set; }
        public double totalSodium { get; internal set; }
        public double totalCholesterol { get; internal set; }
        public double totalVitaminD { get; internal set; }
        public double totalIron { get; internal set; }
        public double totalPotassium { get; internal set; }
        public double totalVitaminA { get; internal set; }
        public double totalVitaminC { get; internal set; }
        public double totalVitaminE { get; internal set; }
        public double totalVitaminB6 { get; internal set; }
        public double totalMagnesium { get; internal set; }
        public double totalZinc { get; internal set; }
        public bool hasMainCourse { get; internal set; }
        public bool needsSideItems { get; internal set; }

    }

    class planTotals
    {
        public string[,] mealArray { get; internal set; }
        public double totalCalories { get; internal set; }
        public double totalProtein { get; internal set; }
        public double totalCarbs { get; internal set; }
        public double totalFats { get; internal set; }
        public double totalCalcium { get; internal set; }
        public double totalSodium { get; internal set; }
        public double totalCholesterol { get; internal set; }
        public double totalVitaminD { get; internal set; }
        public double totalIron { get; internal set; }
        public double totalPotassium { get; internal set; }
        public double totalVitaminA { get; internal set; }
        public double totalVitaminC { get; internal set; }
        public double totalVitaminE { get; internal set; }
        public double totalVitaminB6 { get; internal set; }
        public double totalMagnesium { get; internal set; }
        public double totalZinc { get; internal set; }

    }

    class mealNumbers
    {
        public string[] meals { get; internal set; }
        public int[] mealCounts { get; internal set; }
    }

    class mealArraySizes
    {
        public int breakfastSize { get; internal set; }
        public int midMorningSize { get; internal set; }
        public int lunchSize { get; internal set; }
        public int afternoonSize { get; internal set; }
        public int dinnerSize { get; internal set; }
        public int nightSize { get; internal set; }
        public int sidesSize { get; internal set; }
    }

    class filledTypeArrays
    {
        public string[] breakfastRecipes { get; internal set; }
        public string[] midMorningRecipes { get; internal set; }
        public string[] lunchRecipes { get; internal set; }
        public string[] afternoonRecipes { get; internal set; }
        public string[] dinnerRecipes { get; internal set; }
        public string[] nightRecipes { get; internal set; }
        public string[] sidesRecipes { get; internal set; }
        public string[] breakfastRecipesIDs { get; internal set; }
        public string[] midMorningRecipesIDs { get; internal set; }
        public string[] lunchRecipesIDs { get; internal set; }
        public string[] afternoonRecipesIDs { get; internal set; }
        public string[] dinnerRecipesIDs { get; internal set; }
        public string[] nightRecipesIDs { get; internal set; }
        public string[] sidesRecipesIDs { get; internal set; }
    }
}
