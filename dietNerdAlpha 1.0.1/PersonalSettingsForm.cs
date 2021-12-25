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
    public partial class PersonalSettingsForm : Form
    {
        public PersonalSettingsForm()
        {
            InitializeComponent();
            loadPersonalSettings();
        }

        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            readPersonalSettings();
            loadPersonalSettings();
            this.Close();
        }

        private void loadPersonalSettings()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            genderComboBox.SelectedItem = config.AppSettings.Settings["userGender"].Value;
            ageTextBox.Text = config.AppSettings.Settings["userAge"].Value;
            heightTextBox.Text = config.AppSettings.Settings["userHeight"].Value;
            weightTextBox.Text = config.AppSettings.Settings["userWeight"].Value;
            weeklyActiveDaysTextBox.Text = config.AppSettings.Settings["userWeeklyActiveDays"].Value;
            weeklyGoalComboBox.SelectedItem = config.AppSettings.Settings["currentGoal"].Value;
            caloricGoalTextBox.Text = config.AppSettings.Settings["totalDailyIntake"].Value;
            maintanceCaloriesTextBox.Text = config.AppSettings.Settings["dailyMaintanceCalories"].Value;
            minWaterIntakeTextBox.Text = config.AppSettings.Settings["minWaterIntake"].Value;
        }

        private userDataClass readPersonalSettings()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings["userGender"].Value = genderComboBox.SelectedItem.ToString();
            string userGenderString = genderComboBox.SelectedItem.ToString();
            config.AppSettings.Settings["userAge"].Value = ageTextBox.Text;
            string userAgeString = ageTextBox.Text;
            int userAgeInt = int.Parse(userAgeString);
            config.AppSettings.Settings["userHeight"].Value = heightTextBox.Text;
            string userHeightString = heightTextBox.Text;
            float userHeightFloat = float.Parse(userHeightString);
            config.AppSettings.Settings["userWeight"].Value = weightTextBox.Text;
            string userWeightString = weightTextBox.Text;
            int userWeightInt = int.Parse(userWeightString);
            config.AppSettings.Settings["userWeeklyActiveDays"].Value = weeklyActiveDaysTextBox.Text;
            string userWeeklyActiveDaysString = weeklyActiveDaysTextBox.Text;
            int userWeeklyActiveDaysInt = int.Parse(userWeeklyActiveDaysString);
            config.AppSettings.Settings["currentGoal"].Value = weeklyGoalComboBox.SelectedItem.ToString();
            string userCurrentGoalString = weeklyGoalComboBox.SelectedItem.ToString();
            config.AppSettings.Settings["totalDailyIntake"].Value = caloricGoalTextBox.Text;
            config.AppSettings.Settings["dailyMaintanceCalories"].Value = maintanceCaloriesTextBox.Text;
            config.AppSettings.Settings["minWaterIntake"].Value = minWaterIntakeTextBox.Text;


            userDataClass userPersonalData = new userDataClass
            {
                userGender = userGenderString,
                userAge = userAgeInt,
                userHeight = userHeightFloat,
                userWeight = userWeightInt,
                userWeeklyActiveDays = userWeeklyActiveDaysInt,
                userWeeklyWaitChangeGoal = userCurrentGoalString
            };
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);

            return userPersonalData;
        }

        private float WeeklyGoalToCals(string goal)
        {
            float additionalCalories;
            switch (goal)
            {
                case "1 lb gain per week":
                    additionalCalories = 500;
                    break;
                case ".5 lb gain per week":
                    additionalCalories = 250;
                    break;
                case "maintain current weight":
                    additionalCalories = 0;
                    break;
                case ".5 lb loss per week":
                    additionalCalories = -250;
                    break;
                case "1 lb loss per week":
                    additionalCalories = -500;
                    break;
                default:
                    additionalCalories = 0;
                    break;
            }

            return additionalCalories;
        }

        private userCalulatedData calcuateCalorieData()
        {
            userDataClass userPersonalData = readPersonalSettings();
            float userBMR, dailyActiveCaloriesLost, userDailyMaintiance, userTotalDailyIntake, waterIntake;
            float age, weight, daysActive;
            float height;
            string gender;

            age = (float)userPersonalData.userAge;
            height = (float)userPersonalData.userHeight;
            weight = (float)userPersonalData.userWeight;
            gender = userPersonalData.userGender;
            //float weightFloat = weight;

            //geneder switch case
            switch (gender)
            {
                case "Male":
                    userBMR = ((float)((13.397 * weight) + (4.799 * height) - (5.677 * age) + 88.362));
                    break;
                case "Female":
                    userBMR = ((float)((9.247 * weight) + (3.098 * height) - (4.330 * age) + 447.593));
                    break;
                case "Non - Binary":
                    userBMR = ((float)((13.397 * weight) + (4.799 * height) - (5.677 * age) + 88.362));
                    break;
                case "Other":
                    userBMR = ((float)((13.397 * weight) + (4.799 * height) - (5.677 * age) + 88.362));
                    break;
                default:
                    userBMR = ((float)((13.397 * weight) + (4.799 * height) - (5.677 * age) + 88.362));
                    break;
            }


            daysActive = (float)userPersonalData.userWeeklyActiveDays;
            dailyActiveCaloriesLost = (400 * (daysActive / 7));
            userDailyMaintiance = userBMR + dailyActiveCaloriesLost;

            string goal = userPersonalData.userWeeklyWaitChangeGoal;
            float additonalCalories = WeeklyGoalToCals(goal);

            userTotalDailyIntake = userDailyMaintiance + additonalCalories;

            waterIntake = ((float)(weight * 0.75));

            userCalulatedData calulatedData = new userCalulatedData();

            calulatedData.dailyMaintiance = userDailyMaintiance;
            calulatedData.totalDailyCal = userTotalDailyIntake;
            calulatedData.minWaterIntake = waterIntake;

            return calulatedData;
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

        private void calculateCaloriesButton_Click(object sender, EventArgs e)
        {
            userCalulatedData calorieData = calcuateCalorieData();
            caloricGoalTextBox.Text = calorieData.totalDailyCal.ToString();
            maintanceCaloriesTextBox.Text = calorieData.dailyMaintiance.ToString();
            minWaterIntakeTextBox.Text = calorieData.minWaterIntake.ToString();

            this.Update();
        }
    }
}
