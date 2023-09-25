using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecapProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadProducts()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.ToList();
            }
        }
        private void LoadProductsByCategory(int Id)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p=> p.CategoryId==Id).ToList();
            }
        }
        private void LoadProductsByName(string name)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).ToList();
            }
        }
        private void LoadCategories()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                cmbbxCategories.DataSource= context.Categories.ToList();
                cmbbxCategories.DisplayMember= "CategoryName";
                cmbbxCategories.ValueMember = "CategoryId";
            }
        }

        private void cmbbxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadProductsByCategory(Convert.ToInt32(cmbbxCategories.SelectedValue));
            }
            catch 
            {
            }


        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            string key = txtProduct.Text;
            if (string.IsNullOrEmpty(key))
            {
                LoadProducts(); 
            }
            else
            {
                LoadProductsByName(key);

            }
        }
    }
}
