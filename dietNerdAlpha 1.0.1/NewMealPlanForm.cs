using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dietNerdAlpha_1._0._1
{
    public partial class NewMealPlanForm : Form
    {
        public NewMealPlanForm()
        {
            InitializeComponent();
            loadMealPlanSettings();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            bool proceed = mealNumberValidation();
            if (proceed == true)
            {
                userMealPlanDataAll mealPlanScreenItems = readMealPlanScreen();
                DateTime startDate = mealPlanScreenItems.mealPlanStartDate;
                DateTime endDate = mealPlanScreenItems.mealPlanEndDate;
                int mealPrepInterval = mealPlanScreenItems.mealPrepInterval;

                float numberOfDays = (float)(endDate - startDate).TotalDays;
                float numberOfMealToPlan = (float)(numberOfDays / mealPrepInterval);
                numberOfMealToPlan = (float)Math.Ceiling(numberOfMealToPlan);
                mealPlanScreenItems.numberOfMealToPlan = numberOfMealToPlan;

                saveToSettings();

                GenerateNewMealPlanV1 generateNewMealPlan = new GenerateNewMealPlanV1();
                //generateNewMealPlanV1 generatePlan = new generateNewMealPlanV1();
                //generateNewPlanV3 generatePlanBeta = new generateNewPlanV3();
                //generateNewPlanV2 generatePlan = new generateNewPlanV2();
                //generatePlanBeta.startGeneration(mealPlanScreenItems);
                //generatePlan.startGeneration(mealPlanScreenItems);


            }
        }

        private bool mealNumberValidation()
        {
            int mealsPerDay = int.Parse(mealPerDayTextBox.Text);
            int breakfastMeals = int.Parse(breakfastTextBox.Text);
            int midMoriningMeals = int.Parse(midMorningTextBox.Text);
            int lunchMeals = int.Parse(lunchTextBox.Text);
            int afternoonMeals = int.Parse(afternoonTextBox.Text);
            int dinnerMeals = int.Parse(dinnerTextBox.Text);
            int nightMeals = int.Parse(nightSnackTextBox.Text);

            int allMealsTotal = breakfastMeals + midMoriningMeals + lunchMeals + afternoonMeals + dinnerMeals + nightMeals;

            if (allMealsTotal != mealsPerDay)
            {
                MessageBox.Show("The Total Number of Meals must equal the number of Indicated Meals per day");
                return false;
            }

            return true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool proceed = mealNumberValidation();
            if (proceed == true)
            {
                saveToSettings();
                this.Close();
            }
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

        private void breakfastTextBox_TextChanged(object sender, EventArgs e)
        {
            if (breakfastTextBox.Text != "")
            {
                bool otherSettings = OtherEmptySettings();
                if (otherSettings == false)
                {
                    mealNumberValidation();
                }
            }
        }

        private void midMorningTextBox_TextChanged(object sender, EventArgs e)
        {
            if (midMorningTextBox.Text != "")
            {
                bool otherSettings = OtherEmptySettings();
                if (otherSettings == false)
                {
                    mealNumberValidation();
                }
            }
        }

        private void lunchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (lunchTextBox.Text != "")
            {
                bool otherSettings = OtherEmptySettings();
                if (otherSettings == false)
                {
                    mealNumberValidation();
                }
            }
        }

        private void afternoonTextBox_TextChanged(object sender, EventArgs e)
        {
            if (afternoonTextBox.Text != "")
            {
                bool otherSettings = OtherEmptySettings();
                if (otherSettings == false)
                {
                    mealNumberValidation();
                }
            }
        }

        private void dinnerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (dinnerTextBox.Text != "")
            {
                bool otherSettings = OtherEmptySettings();
                if (otherSettings == false)
                {
                    mealNumberValidation();
                }
            }
        }

        private void nightSnackTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nightSnackTextBox.Text != "")
            {
                bool otherSettings = OtherEmptySettings();
                if (otherSettings == false)
                {
                    mealNumberValidation();
                }
            }
        }

        private bool OtherEmptySettings()
        {
            if (breakfastTextBox.Text == "" || midMorningTextBox.Text == "" || lunchTextBox.Text == "" || afternoonTextBox.Text == "" || dinnerTextBox.Text == "" || nightSnackTextBox.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private userMealPlanDataAll readMealPlanScreen()
        {
            string mealPlanName = planNameTextBox.Text;
            DateTime planStartDate = DateTime.Parse(planStartDateTextBox.Text);
            DateTime planEndtDate = DateTime.Parse(planEndDateTextBox.Text);
            int mealPrepInterval = int.Parse(mealPrepIntervalsTextBox.Text);
            int mealsPerDay = int.Parse(mealPerDayTextBox.Text);
            int breakfastMeals = int.Parse(breakfastTextBox.Text);
            int midMorningMeals = int.Parse(midMorningTextBox.Text);
            int lunchMeals = int.Parse(lunchTextBox.Text);
            int afternoonMeals = int.Parse(afternoonTextBox.Text);
            int dinnerMeals = int.Parse(afternoonTextBox.Text);
            int nightMeals = int.Parse(afternoonTextBox.Text);

            userMealPlanDataAll readMealPlanScreen = new userMealPlanDataAll { };

            readMealPlanScreen.mealPlanName = mealPlanName;
            readMealPlanScreen.mealPlanStartDate = planStartDate;
            readMealPlanScreen.mealPlanEndDate = planEndtDate;
            readMealPlanScreen.mealPrepInterval = mealPrepInterval;
            readMealPlanScreen.mealsPerDay = mealsPerDay;
            readMealPlanScreen.breakfastMeals = breakfastMeals;
            readMealPlanScreen.midmorningMeals = midMorningMeals;
            readMealPlanScreen.lunchMeals = lunchMeals;
            readMealPlanScreen.afternoonMeals = afternoonMeals;
            readMealPlanScreen.dinnerMeals = dinnerMeals;
            readMealPlanScreen.nightMeals = nightMeals;

            return readMealPlanScreen;
        }

        private void saveToSettings()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            config.AppSettings.Settings["mealPlanName"].Value = planNameTextBox.Text;
            string startDateString = planStartDateTextBox.Text;
            DateTime startDate = DateTime.Parse(startDateString);
            startDateString = startDate.ToString();
            string endDateString = planEndDateTextBox.Text;
            DateTime endDate = DateTime.Parse(endDateString);
            endDateString = endDate.ToString();
            config.AppSettings.Settings["mealPlanStartDate"].Value = startDateString;
            config.AppSettings.Settings["mealPlanEndDate"].Value = endDateString;
            config.AppSettings.Settings["mealPrepDayIntervals"].Value = mealPrepIntervalsTextBox.Text;
            config.AppSettings.Settings["mealsPerDay"].Value = mealPerDayTextBox.Text;
            config.AppSettings.Settings["breakfastNumber"].Value = breakfastTextBox.Text;
            config.AppSettings.Settings["midMorningNumber"].Value = midMorningTextBox.Text;
            config.AppSettings.Settings["lunchNumber"].Value = lunchTextBox.Text;
            config.AppSettings.Settings["afternoonNumber"].Value = afternoonTextBox.Text;
            config.AppSettings.Settings["dinnerNumber"].Value = dinnerTextBox.Text;
            config.AppSettings.Settings["nightNumber"].Value = nightSnackTextBox.Text;

            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
        }
        private void loadMealPlanSettings()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            planNameTextBox.Text = config.AppSettings.Settings["mealPlanName"].Value;
            planStartDateTextBox.Text = config.AppSettings.Settings["mealPlanStartDate"].Value;
            planEndDateTextBox.Text = config.AppSettings.Settings["mealPlanEndDate"].Value;
            mealPrepIntervalsTextBox.Text = config.AppSettings.Settings["mealPrepDayIntervals"].Value;
            mealPerDayTextBox.Text = config.AppSettings.Settings["mealsPerDay"].Value;
            breakfastTextBox.Text = config.AppSettings.Settings["breakfastNumber"].Value;
            midMorningTextBox.Text = config.AppSettings.Settings["midMorningNumber"].Value;
            lunchTextBox.Text = config.AppSettings.Settings["lunchNumber"].Value;
            afternoonTextBox.Text = config.AppSettings.Settings["afternoonNumber"].Value;
            dinnerTextBox.Text = config.AppSettings.Settings["dinnerNumber"].Value;
            nightSnackTextBox.Text = config.AppSettings.Settings["nightNumber"].Value;
        }
    }
}
