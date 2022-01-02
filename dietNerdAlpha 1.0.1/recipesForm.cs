using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dietNerdAlpha_1._0._1
{
    public partial class recipesForm : Form
    {
        //SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        SqlConnection cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True");

        public recipesForm()
        {
            InitializeComponent();
            fillRecipieListBox();
        }

        private void addNewRecipeButton_Click(object sender, EventArgs e)
        {
            addNewRecipieForm addNewRecipie = new addNewRecipieForm();
            addNewRecipie.ShowDialog();

            fillRecipieListBox();

            mainMenuForm mainMenu = new mainMenuForm();
            mainMenu.updateMainWindow();
        }

        private void fillRecipieListBox()
        {
            using (cn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\solow\OneDrive\Desktop\Projects\Hungry Flyer\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\dietNerdAlpha 1.0.1\wholeAppData.mdf; Integrated Security = True"))
            {
                cn.Close();
                cn.Open();

                using (cmd = new SqlCommand("Select * from recipeTable", cn))
                {
                    using (dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            List<string> clearIngredientListBox = new List<string>();
                            clearIngredientListBox.Add(" ");

                            recipesListBox.DataSource = clearIngredientListBox;

                            List<string> recipeNameStringList = new List<string>();
                            while (dr.Read())
                            {
                                recipeNameStringList.Add((string)dr["recipeName"]);
                            }
                            recipesListBox.DataSource = recipeNameStringList;
                        }
                    }
                    dr.Dispose();
                    dr.Close();
                }
                cmd.Dispose();
            }

            cn.Dispose();
            cn.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string namedSearch = recipesSearchTextBox.Text.ToString();
            searchDataBase(namedSearch);
        }

        private void searchDataBase(string namedSearch)
        {
            
            cn.Open();
            cmd = new SqlCommand("SELECT * FROM dbo.recipeTable WHERE recipeName = '" + namedSearch + "'", cn);

            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            recipesListBox.DataSource = dt;
            recipesListBox.DisplayMember = "recipeName";
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void addNewRecipieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNewRecipieForm addNewRecipie = new addNewRecipieForm();
            addNewRecipie.ShowDialog();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
