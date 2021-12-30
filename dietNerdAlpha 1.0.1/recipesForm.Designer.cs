
namespace dietNerdAlpha_1._0._1
{
    partial class recipesForm
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
            this.addNewRecipeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.recipesSearchTextBox = new System.Windows.Forms.TextBox();
            this.recipesListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.recipieListBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewRecipieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.recipieListBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // addNewRecipeButton
            // 
            this.addNewRecipeButton.Location = new System.Drawing.Point(188, 296);
            this.addNewRecipeButton.Name = "addNewRecipeButton";
            this.addNewRecipeButton.Size = new System.Drawing.Size(167, 23);
            this.addNewRecipeButton.TabIndex = 12;
            this.addNewRecipeButton.Text = "Add New Recipe";
            this.addNewRecipeButton.UseVisualStyleBackColor = true;
            this.addNewRecipeButton.Click += new System.EventHandler(this.addNewRecipeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 296);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(167, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(263, 38);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(91, 23);
            this.searchButton.TabIndex = 10;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // recipesSearchTextBox
            // 
            this.recipesSearchTextBox.Location = new System.Drawing.Point(12, 38);
            this.recipesSearchTextBox.Name = "recipesSearchTextBox";
            this.recipesSearchTextBox.Size = new System.Drawing.Size(245, 20);
            this.recipesSearchTextBox.TabIndex = 9;
            this.recipesSearchTextBox.Text = "Search Recipies";
            // 
            // recipesListBox
            // 
            this.recipesListBox.ContextMenuStrip = this.recipieListBoxContextMenuStrip;
            this.recipesListBox.FormattingEnabled = true;
            this.recipesListBox.Location = new System.Drawing.Point(12, 64);
            this.recipesListBox.Name = "recipesListBox";
            this.recipesListBox.Size = new System.Drawing.Size(343, 225);
            this.recipesListBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Recipes";
            // 
            // recipieListBoxContextMenuStrip
            // 
            this.recipieListBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewRecipieToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.deleteToolStripMenuItem1});
            this.recipieListBoxContextMenuStrip.Name = "recipieListBoxContextMenuStrip";
            this.recipieListBoxContextMenuStrip.Size = new System.Drawing.Size(165, 92);
            // 
            // addNewRecipieToolStripMenuItem
            // 
            this.addNewRecipieToolStripMenuItem.Name = "addNewRecipieToolStripMenuItem";
            this.addNewRecipieToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addNewRecipieToolStripMenuItem.Text = "Add New Recipie";
            this.addNewRecipieToolStripMenuItem.Click += new System.EventHandler(this.addNewRecipieToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.editToolStripMenuItem.Text = "Reset";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItem.Text = "Edit";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            // 
            // recipesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 327);
            this.Controls.Add(this.addNewRecipeButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.recipesSearchTextBox);
            this.Controls.Add(this.recipesListBox);
            this.Controls.Add(this.label1);
            this.Name = "recipesForm";
            this.Text = "recipesForm";
            this.recipieListBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addNewRecipeButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox recipesSearchTextBox;
        private System.Windows.Forms.ListBox recipesListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip recipieListBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewRecipieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
    }
}