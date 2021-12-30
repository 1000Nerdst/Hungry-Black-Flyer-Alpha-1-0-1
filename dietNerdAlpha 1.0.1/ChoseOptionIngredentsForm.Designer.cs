
namespace dietNerdAlpha_1._0._1
{
    partial class ChoseOptionIngredentsForm
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
            this.servingOptionsListBox = new System.Windows.Forms.ListBox();
            this.chooseServingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // servingOptionsListBox
            // 
            this.servingOptionsListBox.FormattingEnabled = true;
            this.servingOptionsListBox.ItemHeight = 15;
            this.servingOptionsListBox.Location = new System.Drawing.Point(12, 12);
            this.servingOptionsListBox.Name = "servingOptionsListBox";
            this.servingOptionsListBox.Size = new System.Drawing.Size(179, 139);
            this.servingOptionsListBox.TabIndex = 0;
            // 
            // chooseServingButton
            // 
            this.chooseServingButton.Location = new System.Drawing.Point(12, 157);
            this.chooseServingButton.Name = "chooseServingButton";
            this.chooseServingButton.Size = new System.Drawing.Size(179, 23);
            this.chooseServingButton.TabIndex = 1;
            this.chooseServingButton.Text = "Choose";
            this.chooseServingButton.UseVisualStyleBackColor = true;
            this.chooseServingButton.Click += new System.EventHandler(this.chooseServingButton_Click);
            // 
            // ChoseOptionIngredentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 198);
            this.Controls.Add(this.chooseServingButton);
            this.Controls.Add(this.servingOptionsListBox);
            this.Name = "ChoseOptionIngredentsForm";
            this.Text = "ChoseOptionIngredentsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox servingOptionsListBox;
        private System.Windows.Forms.Button chooseServingButton;
    }
}