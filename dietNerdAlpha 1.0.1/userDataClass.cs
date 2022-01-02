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
        public int lunchSidesSize { get; internal set; }
        public int dinnerSidesSize { get; internal set; }
    }

    class filledTypeArrays
    {
        public string[] breakfastRecipes { get; internal set; }
        public string[] midMorningRecipes { get; internal set; }
        public string[] lunchRecipes { get; internal set; }
        public string[] afternoonRecipes { get; internal set; }
        public string[] dinnerRecipes { get; internal set; }
        public string[] nightRecipes { get; internal set; }
        public string[] lunchSidesRecipes { get; internal set; }
        public string[] dinnerSidesRecipes { get; internal set; }
        public string[] breakfastRecipesIDs { get; internal set; }
        public string[] midMorningRecipesIDs { get; internal set; }
        public string[] lunchRecipesIDs { get; internal set; }
        public string[] afternoonRecipesIDs { get; internal set; }
        public string[] dinnerRecipesIDs { get; internal set; }
        public string[] nightRecipesIDs { get; internal set; }
        public string[] lunchSidesRecipesIDs { get; internal set; }
        public string[] dinnerSidesRecipesIDs { get; internal set; }
    }

    class getPythonOptions
    {
        public string[] servingUnit { get; internal set; }
        public float[] multipler { get; internal set; }
    }

    class CurrentIngredients
    {
        public string ingredientName { get; internal set; }
        public string ingredientStdServingSize { get; internal set; }
        public float ingredientCalories { get; internal set; }
        public float ingredientFats { get; internal set; }
        public float ingredientCarbohydrates { get; internal set; }
        public float ingredientProtein { get; internal set; }
        public float ingredientCholesterol { get; internal set; }
        public float ingredientTransFats { get; internal set; }
        public float ingredientSurgar { get; internal set; }
        public float ingredientStaFats { get; internal set; }
        public float ingredientFiber { get; internal set; }
        public float ingredientVitaminA { get; internal set; }
        public float ingredientVitaminB1 { get; internal set; }
        public float ingredientVitaminB2 { get; internal set; }
        public float ingredientVitaminB3 { get; internal set; }
        public float ingredientVitaminB5 { get; internal set; }
        public float ingredientVitaminB6 { get; internal set; }
        public float ingredientVitaminB7 { get; internal set; }
        public float ingredientVitaminB9 { get; internal set; }
        public float ingredientVitaminB12 { get; internal set; }
        public float ingredientVitaminC { get; internal set; }
        public float ingredientVitaminD { get; internal set; }
        public float ingredientVitaminE { get; internal set; }
        public float ingredientVitaminK { get; internal set; }
        public float ingredientCholine { get; internal set; }
        public float ingredientCalcium { get; internal set; }
        public float ingredientChloride { get; internal set; }
        public float ingredientChromium { get; internal set; }
        public float ingredientCopper { get; internal set; }
        public float ingredientFluoride { get; internal set; }
        public float ingredientIodine { get; internal set; }
        public float ingredientIron { get; internal set; }
        public float ingredientMagnesium { get; internal set; }
        public float ingredientManganese { get; internal set; }
        public float ingredientMolybdenum { get; internal set; }
        public float ingredientPhosphorus { get; internal set; }
        public float ingredientPotassium { get; internal set; }
        public float ingredientSelenium { get; internal set; }
        public float ingredientSodium { get; internal set; }
        public float ingredientSulfur { get; internal set; }
        public float ingredientZinc { get; internal set; }
        public float ingredientOmega3 { get; internal set; }
        public float ingredientOmega6 { get; internal set; }
        public float ingredientALA { get; internal set; }
        public float ingredientEPA { get; internal set; }
        public float ingredientDPA { get; internal set; }
        public float ingredientDHA { get; internal set; }

    }
    class CurrentRecipe
    {
        public string recipeName { get; internal set; }
        public string recipeTiming { get; internal set; }
        public float recipeCalories { get; internal set; }
        public float recipeFats { get; internal set; }
        public float recipeCarbohydrates { get; internal set; }
        public float recipeProtein { get; internal set; }
        public float recipeCholesterol { get; internal set; }
        public float recipeTransFats { get; internal set; }
        public float recipeSurgar { get; internal set; }
        public float recipeStaFats { get; internal set; }
        public float recipeFiber { get; internal set; }
        public float recipeVitaminA { get; internal set; }
        public float recipeVitaminB1 { get; internal set; }
        public float recipeVitaminB2 { get; internal set; }
        public float recipeVitaminB3 { get; internal set; }
        public float recipeVitaminB5 { get; internal set; }
        public float recipeVitaminB6 { get; internal set; }
        public float recipeVitaminB7 { get; internal set; }
        public float recipeVitaminB9 { get; internal set; }
        public float recipeVitaminB12 { get; internal set; }
        public float recipeVitaminC { get; internal set; }
        public float recipeVitaminD { get; internal set; }
        public float recipeVitaminE { get; internal set; }
        public float recipeVitaminK { get; internal set; }
        public float recipeCholine { get; internal set; }
        public float recipeCalcium { get; internal set; }
        public float recipeChloride { get; internal set; }
        public float recipeChromium { get; internal set; }
        public float recipeCopper { get; internal set; }
        public float recipeFluoride { get; internal set; }
        public float recipeIodine { get; internal set; }
        public float recipeIron { get; internal set; }
        public float recipeMagnesium { get; internal set; }
        public float recipeManganese { get; internal set; }
        public float recipeMolybdenum { get; internal set; }
        public float recipePhosphorus { get; internal set; }
        public float recipePotassium { get; internal set; }
        public float recipeSelenium { get; internal set; }
        public float recipeSodium { get; internal set; }
        public float recipeSulfur { get; internal set; }
        public float recipeZinc { get; internal set; }
        public float recipeOmega3 { get; internal set; }
        public float recipeOmega6 { get; internal set; }
        public float recipeALA { get; internal set; }
        public float recipeEPA { get; internal set; }
        public float recipeDPA { get; internal set; }
        public float recipeDHA { get; internal set; }
        public string ingredentNames { get; internal set; }
        public string ingredentSizes { get; internal set; }
        public string ingredentIds { get; internal set; }

    }

    class mealTotals
    {
        public string[,] namesAndServing { get; internal set; }
        public float totalCalories { get; internal set; }
        public float totalFats { get; internal set; }
        public float totalCarbohydrates { get; internal set; }
        public float totalProtein { get; internal set; }
        public float totalCholesterol { get; internal set; }
        public float totalTransFats { get; internal set; }
        public float totalSurgar { get; internal set; }
        public float totalStaFats { get; internal set; }
        public float totalFiber { get; internal set; }
        public float totalVitaminA { get; internal set; }
        public float totalVitaminB1 { get; internal set; }
        public float totalVitaminB2 { get; internal set; }
        public float totalVitaminB3 { get; internal set; }
        public float totalVitaminB5 { get; internal set; }
        public float totalVitaminB6 { get; internal set; }
        public float totalVitaminB7 { get; internal set; }
        public float totalVitaminB9 { get; internal set; }
        public float totalVitaminB12 { get; internal set; }
        public float totalVitaminC { get; internal set; }
        public float totalVitaminD { get; internal set; }
        public float totalVitaminE { get; internal set; }
        public float totalVitaminK { get; internal set; }
        public float totalCholine { get; internal set; }
        public float totalCalcium { get; internal set; }
        public float totalChloride { get; internal set; }
        public float totalChromium { get; internal set; }
        public float totalCopper { get; internal set; }
        public float totalFluoride { get; internal set; }
        public float totalIodine { get; internal set; }
        public float totalIron { get; internal set; }
        public float totalMagnesium { get; internal set; }
        public float totalManganese { get; internal set; }
        public float totalMolybdenum { get; internal set; }
        public float totalPhosphorus { get; internal set; }
        public float totalPotassium { get; internal set; }
        public float totalSelenium { get; internal set; }
        public float totalSodium { get; internal set; }
        public float totalSulfur { get; internal set; }
        public float totalZinc { get; internal set; }
        public float totalOmega3 { get; internal set; }
        public float totalOmega6 { get; internal set; }
        public float totalALA { get; internal set; }
        public float totalEPA { get; internal set; }
        public float totalDPA { get; internal set; }
        public float totalDHA { get; internal set; }

    }
}
