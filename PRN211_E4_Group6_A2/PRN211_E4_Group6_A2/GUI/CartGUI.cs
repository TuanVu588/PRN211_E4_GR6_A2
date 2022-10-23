using MusicStoreWin.Models;
using PRN211_E4_Group6_A2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRN211_E4_Group6_A2.GUI
{
    public partial class CartGUI : Form
    {
        MusicStoreContext context;
        ShoppingCart cart;
        public CartGUI()
        {
            InitializeComponent();
            context = new MusicStoreContext();
            bindGrid();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            
        }
        public void bindGrid()
        {
            cart = ShoppingCart.GetCart();
            tbTotal.Text = cart.GetTotal().ToString();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = cart.GetCartItems().ToList();
            dataGridView1.Columns["CartId"].Visible = false;
            dataGridView1.Columns["RecordId"].Visible = false;
            dataGridView1.Columns["Album"].Visible = false;

            if (dataGridView1.Rows.Count > 0 && Settings.UserName != null)
            {
                btnCheckout.Enabled = true;
            }
            int count = dataGridView1.Columns.Count;
            DataGridViewButtonColumn btnAdd = new DataGridViewButtonColumn
            {
                Text = "Add to cart",
                Name = "Add",
                UseColumnTextForButtonValue = true,
            };
            dataGridView1.Columns.Insert(0, btnAdd);

            DataGridViewButtonColumn btnRemove = new DataGridViewButtonColumn
            {
                Text = "Remove from cart",
                Name = "Remove",
                UseColumnTextForButtonValue = true,
            };
            dataGridView1.Columns.Insert(count + 1, btnRemove);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Add"].Index)
            {
                int albumId = (int)dataGridView1.Rows[e.RowIndex].Cells["AlbumId"].Value;
                Album album = context.Albums.Find(albumId);
                cart.AddToCart(album);
                bindGrid();
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Remove"].Index)
            {
                int recordId = (int)dataGridView1.Rows[e.RowIndex].Cells["RecordId"].Value;
                cart.RemoveFromCart(recordId);
                bindGrid();
            }
        }
    }
}
