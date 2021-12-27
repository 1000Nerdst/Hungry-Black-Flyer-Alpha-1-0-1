
namespace dietNerdAlpha_1._0._1
{
    partial class ingredientsForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.ingredientsListBox = new System.Windows.Forms.ListBox();
            this.addEditDeleteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchIngredientsTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addNewIngredientButton = new System.Windows.Forms.Button();
            this.resetTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEditDeleteMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingredients";
            // 
            // ingredientsListBox
            // 
            this.ingredientsListBox.ContextMenuStrip = this.addEditDeleteMenuStrip;
            this.ingredientsListBox.FormattingEnabled = true;
            this.ingredientsListBox.Location = new System.Drawing.Point(16, 66);
            this.ingredientsListBox.Name = "ingredientsListBox";
            this.ingredientsListBox.Size = new System.Drawing.Size(343, 225);
            this.ingredientsListBox.TabIndex = 2;
            //this.ingredientsListBox.SelectedIndexChanged += new System.EventHandler(this.ingredientsListBox_SelectedIndexChanged);
            // 
            // addEditDeleteMenuStrip
            // 
            this.addEditDeleteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.resetTableToolStripMenuItem});
            this.addEditDeleteMenuStrip.Name = "contextMenuStrip1";
            this.addEditDeleteMenuStrip.Size = new System.Drawing.Size(181, 114);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            //this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editToolStripMenuItem.Text = "Edit";
            //this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            //this.deleteToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // searchIngredientsTextBox
            // 
            this.searchIngredientsTextBox.Location = new System.Drawing.Point(16, 40);
            this.searchIngredientsTextBox.Name = "searchIngredientsTextBox";
            this.searchIngredientsTextBox.Size = new System.Drawing.Size(245, 20);
            this.searchIngredientsTextBox.TabIndex = 3;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(267, 40);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(91, 23);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            //this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(16, 298);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(167, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            //this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addNewIngredientButton
            // 
            this.addNewIngredientButton.Location = new System.Drawing.Point(192, 298);
            this.addNewIngredientButton.Name = "addNewIngredientButton";
            this.addNewIngredientButton.Size = new System.Drawing.Size(167, 23);
            this.addNewIngredientButton.TabIndex = 6;
            this.addNewIngredientButton.Text = "Add New Ingredient";
            this.addNewIngredientButton.UseVisualStyleBackColor = true;
            //this.addNewIngredientButton.Click += new System.EventHandler(this.addNewIngredientButton_Click);
            // 
            // resetTableToolStripMenuItem
            // 
            this.resetTableToolStripMenuItem.Name = "resetTableToolStripMenuItem";
            this.resetTableToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.resetTableToolStripMenuItem.Text = "Reset Table";
            //this.resetTableToolStripMenuItem.Click += new System.EventHandler(this.resetTableToolStripMenuItem_Click);
            // 
            // ingredientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 333);
            this.Controls.Add(this.addNewIngredientButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchIngredientsTextBox);
            this.Controls.Add(this.ingredientsListBox);
            this.Controls.Add(this.label1);
            this.Name = "ingredientsForm";
            this.Text = "ingredientsForm";
            this.addEditDeleteMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ingredientsListBox;
        private System.Windows.Forms.TextBox searchIngredientsTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addNewIngredientButton;
        private System.Windows.Forms.ContextMenuStrip addEditDeleteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetTableToolStripMenuItem;
    }
}