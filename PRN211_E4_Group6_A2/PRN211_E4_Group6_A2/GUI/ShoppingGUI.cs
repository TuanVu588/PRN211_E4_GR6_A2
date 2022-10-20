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
    public partial class ShoppingGUI : Form
    {
        MusicStoreContext context;
        PageList<Album> pageList;
        public ShoppingGUI()
        {
            InitializeComponent();
            context = new MusicStoreContext();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindPanel(1);
        }
        void bindPanel(int pageIndex)
        {
            IQueryable<Album> albums = context.Albums
                .Where(a => a.Title.Contains(txtTitle.Text.Trim()));

            pageList = PageList<Album>.Create(albums, pageIndex, 3);
            btnPrevious.Enabled = pageList.HasPreviousPage;
            btnNext.Enabled = pageList.HasNextPage;

            panel1.Controls.Clear();
            int i = 0;
            int width = (panel1.Width - 20) / 3;
            int height = panel1.Height - 20;
            foreach (Album album in pageList)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Width = width;
                groupBox.Height = height;
                groupBox.Location = new Point(10 + i * width, 10);

                Label lblTitle = new Label();
                lblTitle.Text = album.Title;
                lblTitle.AutoSize = true;
                lblTitle.Location = new Point(10, 40);
                groupBox.Controls.Add(lblTitle);

                Button btnAdd = new Button();
                btnAdd.Name = $"btn{album.AlbumId}";
                btnAdd.Text = "Add to cart";
                btnAdd.BackColor = Color.Blue;
                btnAdd.ForeColor = Color.White;
                btnAdd.AutoSize = true;
                btnAdd.Location = new Point((width - btnAdd.Width) / 2, 80);
                groupBox.Controls.Add(btnAdd);
                btnAdd.Click += BtnAdd_Click;

                i++;
                panel1.Controls.Add(groupBox);
            }

        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            int albumId = int.Parse(((Button)sender).Name.Substring(3));
            MessageBox.Show($"AlbumId = {albumId}");
            Album album = context.Albums.Find(albumId);

            ShoppingCart shoppingCart = ShoppingCart.GetCart();
            shoppingCart.AddToCart(album);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bindPanel(pageList.PageIndex + 1);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bindPanel(pageList.PageIndex - 1);
        }
    }
}
