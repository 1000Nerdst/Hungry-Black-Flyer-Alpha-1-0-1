
namespace dietNerdAlpha_1._0._1
{
    partial class AddNewIngredentsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.seachNutrientOptimiserTextBox = new System.Windows.Forms.TextBox();
            this.searchLinkTextBox = new System.Windows.Forms.TextBox();
            this.searchNutrientOptimiserButton = new System.Windows.Forms.Button();
            this.searchLinkButton = new System.Windows.Forms.Button();
            this.manuallyAddButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search from Nutrient Optimiser";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nutrient Optimiser link";
            // 
            // seachNutrientOptimiserTextBox
            // 
            this.seachNutrientOptimiserTextBox.Location = new System.Drawing.Point(192, 13);
            this.seachNutrientOptimiserTextBox.Name = "seachNutrientOptimiserTextBox";
            this.seachNutrientOptimiserTextBox.Size = new System.Drawing.Size(220, 23);
            this.seachNutrientOptimiserTextBox.TabIndex = 2;
            // 
            // searchLinkTextBox
            // 
            this.searchLinkTextBox.Location = new System.Drawing.Point(192, 48);
            this.searchLinkTextBox.Name = "searchLinkTextBox";
            this.searchLinkTextBox.Size = new System.Drawing.Size(220, 23);
            this.searchLinkTextBox.TabIndex = 3;
            // 
            // searchNutrientOptimiserButton
            // 
            this.searchNutrientOptimiserButton.Location = new System.Drawing.Point(419, 12);
            this.searchNutrientOptimiserButton.Name = "searchNutrientOptimiserButton";
            this.searchNutrientOptimiserButton.Size = new System.Drawing.Size(75, 23);
            this.searchNutrientOptimiserButton.TabIndex = 4;
            this.searchNutrientOptimiserButton.Text = "Search";
            this.searchNutrientOptimiserButton.UseVisualStyleBackColor = true;
            this.searchNutrientOptimiserButton.Click += new System.EventHandler(this.searchNutrientOptimiserButton_Click);
            // 
            // searchLinkButton
            // 
            this.searchLinkButton.Location = new System.Drawing.Point(419, 48);
            this.searchLinkButton.Name = "searchLinkButton";
            this.searchLinkButton.Size = new System.Drawing.Size(75, 23);
            this.searchLinkButton.TabIndex = 5;
            this.searchLinkButton.Text = "Search";
            this.searchLinkButton.UseVisualStyleBackColor = true;
            this.searchLinkButton.Click += new System.EventHandler(this.searchLinkButton_Click);
            // 
            // manuallyAddButton
            // 
            this.manuallyAddButton.Location = new System.Drawing.Point(256, 77);
            this.manuallyAddButton.Name = "manuallyAddButton";
            this.manuallyAddButton.Size = new System.Drawing.Size(238, 23);
            this.manuallyAddButton.TabIndex = 6;
            this.manuallyAddButton.Text = "Manually Add Ingredent";
            this.manuallyAddButton.UseVisualStyleBackColor = true;
            this.manuallyAddButton.Click += new System.EventHandler(this.manuallyAddButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 77);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(238, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddNewIngredentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 111);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.manuallyAddButton);
            this.Controls.Add(this.searchLinkButton);
            this.Controls.Add(this.searchNutrientOptimiserButton);
            this.Controls.Add(this.searchLinkTextBox);
            this.Controls.Add(this.seachNutrientOptimiserTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddNewIngredentsForm";
            this.Text = "AddNewIngredentsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox seachNutrientOptimiserTextBox;
        private System.Windows.Forms.TextBox searchLinkTextBox;
        private System.Windows.Forms.Button searchNutrientOptimiserButton;
        private System.Windows.Forms.Button searchLinkButton;
        private System.Windows.Forms.Button manuallyAddButton;
        private System.Windows.Forms.Button cancelButton;
    }
}