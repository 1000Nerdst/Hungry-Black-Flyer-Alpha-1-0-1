
namespace dietNerdAlpha_1._0._1
{
    partial class PersonalSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.genderComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ageTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.weightTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.weeklyActiveDaysTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.weeklyGoalComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.calculateCaloriesButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.caloricGoalTextBox = new System.Windows.Forms.TextBox();
            this.maintanceCaloriesTextBox = new System.Windows.Forms.TextBox();
            this.minWaterIntakeTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // genderComboBox
            // 
            this.genderComboBox.FormattingEnabled = true;
            this.genderComboBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Non-Binary",
            "Other"});
            this.genderComboBox.Location = new System.Drawing.Point(126, 36);
            this.genderComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.genderComboBox.Name = "genderComboBox";
            this.genderComboBox.Size = new System.Drawing.Size(163, 23);
            this.genderComboBox.TabIndex = 0;
            this.genderComboBox.SelectedIndexChanged += new System.EventHandler(this.genderComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Personal Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gender";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Age";
            // 
            // ageTextBox
            // 
            this.ageTextBox.Location = new System.Drawing.Point(229, 68);
            this.ageTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.Size = new System.Drawing.Size(60, 23);
            this.ageTextBox.TabIndex = 4;
            this.ageTextBox.Text = "21";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Height";
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(229, 98);
            this.heightTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(60, 23);
            this.heightTextBox.TabIndex = 6;
            this.heightTextBox.Text = "6.083";
            // 
            // weightTextBox
            // 
            this.weightTextBox.Location = new System.Drawing.Point(229, 128);
            this.weightTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.weightTextBox.Name = "weightTextBox";
            this.weightTextBox.Size = new System.Drawing.Size(60, 23);
            this.weightTextBox.TabIndex = 7;
            this.weightTextBox.Text = "160";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Weight";
            // 
            // weeklyActiveDaysTextBox
            // 
            this.weeklyActiveDaysTextBox.Location = new System.Drawing.Point(229, 158);
            this.weeklyActiveDaysTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.weeklyActiveDaysTextBox.Name = "weeklyActiveDaysTextBox";
            this.weeklyActiveDaysTextBox.Size = new System.Drawing.Size(60, 23);
            this.weeklyActiveDaysTextBox.TabIndex = 9;
            this.weeklyActiveDaysTextBox.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 166);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Weekly Days Active";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 196);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Weekly Goal";
            // 
            // weeklyGoalComboBox
            // 
            this.weeklyGoalComboBox.FormattingEnabled = true;
            this.weeklyGoalComboBox.Items.AddRange(new object[] {
            "1 lb gain per week",
            ".5 lb gain per week",
            "maintain current weight",
            ".5 lb loss per week",
            "1 lb loss per week"});
            this.weeklyGoalComboBox.Location = new System.Drawing.Point(126, 193);
            this.weeklyGoalComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.weeklyGoalComboBox.Name = "weeklyGoalComboBox";
            this.weeklyGoalComboBox.Size = new System.Drawing.Size(163, 23);
            this.weeklyGoalComboBox.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 106);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "feet ( i.e. 6.083 for 6\'1 )";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(296, 136);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "lbs";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(296, 76);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 15);
            this.label10.TabIndex = 16;
            this.label10.Text = "years old";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(296, 166);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "days";
            // 
            // calculateCaloriesButton
            // 
            this.calculateCaloriesButton.Location = new System.Drawing.Point(160, 237);
            this.calculateCaloriesButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.calculateCaloriesButton.Name = "calculateCaloriesButton";
            this.calculateCaloriesButton.Size = new System.Drawing.Size(130, 27);
            this.calculateCaloriesButton.TabIndex = 18;
            this.calculateCaloriesButton.Text = "Calculate Calories";
            this.calculateCaloriesButton.UseVisualStyleBackColor = true;
            this.calculateCaloriesButton.Click += new System.EventHandler(this.calculateCaloriesButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(19, 280);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(372, 27);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(457, 76);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 15);
            this.label12.TabIndex = 20;
            this.label12.Text = "Total Daily Caloric Intake Goal";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(457, 106);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 15);
            this.label13.TabIndex = 21;
            this.label13.Text = "Daily Maintance Calories";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(457, 166);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 15);
            this.label14.TabIndex = 22;
            this.label14.Text = "Minimum Water Intake";
            // 
            // caloricGoalTextBox
            // 
            this.caloricGoalTextBox.Location = new System.Drawing.Point(639, 68);
            this.caloricGoalTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.caloricGoalTextBox.Name = "caloricGoalTextBox";
            this.caloricGoalTextBox.Size = new System.Drawing.Size(70, 23);
            this.caloricGoalTextBox.TabIndex = 23;
            // 
            // maintanceCaloriesTextBox
            // 
            this.maintanceCaloriesTextBox.Location = new System.Drawing.Point(639, 98);
            this.maintanceCaloriesTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.maintanceCaloriesTextBox.Name = "maintanceCaloriesTextBox";
            this.maintanceCaloriesTextBox.Size = new System.Drawing.Size(70, 23);
            this.maintanceCaloriesTextBox.TabIndex = 24;
            // 
            // minWaterIntakeTextBox
            // 
            this.minWaterIntakeTextBox.Location = new System.Drawing.Point(639, 158);
            this.minWaterIntakeTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.minWaterIntakeTextBox.Name = "minWaterIntakeTextBox";
            this.minWaterIntakeTextBox.Size = new System.Drawing.Size(70, 23);
            this.minWaterIntakeTextBox.TabIndex = 25;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(718, 166);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 15);
            this.label15.TabIndex = 26;
            this.label15.Text = "Ounces";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(396, 280);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(372, 27);
            this.saveButton.TabIndex = 27;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // PersonalSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 322);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.minWaterIntakeTextBox);
            this.Controls.Add(this.maintanceCaloriesTextBox);
            this.Controls.Add(this.caloricGoalTextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.calculateCaloriesButton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.weeklyGoalComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.weeklyActiveDaysTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.weightTextBox);
            this.Controls.Add(this.heightTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ageTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.genderComboBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PersonalSettingsForm";
            this.Text = "PersonalSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox genderComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ageTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.TextBox weightTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox weeklyActiveDaysTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox weeklyGoalComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button calculateCaloriesButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox caloricGoalTextBox;
        private System.Windows.Forms.TextBox maintanceCaloriesTextBox;
        private System.Windows.Forms.TextBox minWaterIntakeTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button saveButton;
    }
}