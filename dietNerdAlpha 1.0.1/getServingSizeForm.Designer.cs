
namespace dietNerdAlpha_1._0._1
{
    partial class getServingSizeForm
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
            this.enterServingSizeTextBox = new System.Windows.Forms.TextBox();
            this.enterButton = new System.Windows.Forms.Button();
            this.unitLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Serving Size";
            // 
            // enterServingSizeTextBox
            // 
            this.enterServingSizeTextBox.Location = new System.Drawing.Point(16, 30);
            this.enterServingSizeTextBox.Name = "enterServingSizeTextBox";
            this.enterServingSizeTextBox.Size = new System.Drawing.Size(91, 20);
            this.enterServingSizeTextBox.TabIndex = 1;
            this.enterServingSizeTextBox.TextChanged += new System.EventHandler(this.enterServingSizeTextBox_TextChanged);
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(16, 56);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(181, 23);
            this.enterButton.TabIndex = 2;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.Location = new System.Drawing.Point(114, 36);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(31, 13);
            this.unitLabel.TabIndex = 3;
            this.unitLabel.Text = "Units";
            // 
            // getServingSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 88);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.enterServingSizeTextBox);
            this.Controls.Add(this.label1);
            this.Name = "getServingSizeForm";
            this.Text = "getServingSizeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox enterServingSizeTextBox;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.Label unitLabel;
    }
}