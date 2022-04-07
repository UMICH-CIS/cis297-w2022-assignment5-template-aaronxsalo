using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayTable
{
    /// <summary>
    /// class that controls the main form for the application
    /// </summary>
    public partial class DisplayAuthorsTable : Form
    {
        /// <summary>
        /// method that initializes the component
        /// </summary>
        public DisplayAuthorsTable()
        {
            InitializeComponent();
        }

        //Entity Framework DbContext
        private BooksExamples.BooksEntities dbcontext = new BooksExamples.BooksEntities();
        
        /// <summary>
        /// method that loads database into grid view
        /// </summary>
        private void DisplayAuthorsTable_Load(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;

        }

        /// <summary>
        /// method that refreshes items in grid view
        /// </summary>
        private void authorBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;
        }

        /// <summary>
        /// method that saves items on button click
        /// </summary>
        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            authorBindingSource.EndEdit();
            try
            {
                dbcontext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values", "Entity Validation Exception");
            }
        }

        /// <summary>
        /// method that searches by last name on button click and displays in grid view
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //load Authors table with searched criteria
            var lastNameQuery =
                 from author in dbcontext.Authors
                 where author.LastName.StartsWith(txtSearch.Text)
                 orderby author.LastName, author.FirstName
                 select author;
            //specify datasource for authorBindingSource
            authorDataGridView.DataSource = lastNameQuery.ToList();
        }

        /// <summary>
        /// method that clears searches, clears text box, and displays orginal in grid view
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorDataGridView.DataSource = dbcontext.Authors.Local;
            //clears search text box
            txtSearch.Text = "";
        }
    }
}
