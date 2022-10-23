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
        ShoppingCart cart;
        PageList<Album> pageList;
        bool test = false;
        public ShoppingGUI()
        {
            InitializeComponent();
            context = new MusicStoreContext();
            cart = new ShoppingCart();
            cbGenre.DataSource = context.Genres.ToList();
            cbGenre.DisplayMember = "Name";
            cbGenre.ValueMember = "GenreId";
            bindPanel(1);
        }

        void bindPanel(int pageIndex)
        {
            IQueryable<Album> albums = context.Albums.Where(r => r.Genre
            .Name.Contains(cbGenre.Text));

            pageList = PageList<Album>.Create(albums, pageIndex, 3);
            btnPrevious.Enabled = pageList.HasPreviousPage;
            btnNext.Enabled = pageList.HasNextPage;

            panel1.Controls.Clear();
            int height = panel1.Height - 20;
            int width = (panel1.Width - 20) / 3;
            int i = 0;
            foreach (Album album in pageList)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Text = album.Title;
                groupBox.Height = height;
                groupBox.Width = width;
                groupBox.Location = new Point(10 + i * width, 10);

                PictureBox pictureBox = new PictureBox();
                pictureBox.BorderStyle = BorderStyle.None;
                string s = Directory.GetCurrentDirectory();
                try
                {
                    pictureBox.Image = Image.FromFile(Directory.GetParent(s)
                        .Parent.Parent.FullName + album.AlbumUrl);
                }
                catch
                {
                    pictureBox.Image = null;
                }
                pictureBox.Size = new Size(120, 72);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Location = new Point(60, 30);
                groupBox.Controls.Add(pictureBox);

                Label lblPrice = new Label();
                lblPrice.AutoSize = true;
                lblPrice.Text = $"${album.Price.ToString()}";
                lblPrice.Location = new Point(100, 110);
                groupBox.Controls.Add(lblPrice);

                Label lblArtis = new Label();
                lblArtis.AutoSize = true;
                lblArtis.Text = context.Artists.Where(r => r.ArtistId == album.ArtistId)
                    .Select(r => r.Name).FirstOrDefault();
                lblArtis.Location = new Point(90, 130);
                groupBox.Controls.Add(lblArtis);

                Button btnAdd = new Button();
                btnAdd.Name = $"btn{album.AlbumId}";
                btnAdd.Text = "Add to cart";
                btnAdd.BackColor = Color.Blue;
                btnAdd.ForeColor = Color.White;
                btnAdd.AutoSize = true;
                btnAdd.Location = new Point(80,150);
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

        public void bindPanelSearch(int pageIndex)
        {
            IQueryable<Album> albums = context.Albums.Where(r => r.Title
            .Contains(txtTitle.Text.Trim()));

            pageList = PageList<Album>.Create(albums, pageIndex, 3);
            btnPrevious.Enabled = pageList.HasPreviousPage;
            btnNext.Enabled = pageList.HasNextPage;

            panel1.Controls.Clear();
            int height = panel1.Height - 20;
            int width = (panel1.Width - 20) / 3;
            int i = 0;
            foreach (Album album in pageList)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Text = album.Title;
                groupBox.Height = height;
                groupBox.Width = width;
                groupBox.Location = new Point(10 + i * width, 10);

                PictureBox pictureBox = new PictureBox();
                pictureBox.BorderStyle = BorderStyle.None;
                string s = Directory.GetCurrentDirectory();
                try
                {
                    pictureBox.Image = Image
                        .FromFile(Directory.GetParent(s).Parent.Parent.FullName + album.AlbumUrl);
                }
                catch
                {
                    pictureBox.Image = null;
                }
                pictureBox.Size = new Size(120, 72);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Location = new Point(60, 30);
                groupBox.Controls.Add(pictureBox);

                Label lblPrice = new Label();
                lblPrice.AutoSize = true;
                lblPrice.Text = $"${album.Price.ToString()}";
                lblPrice.Location = new Point(100, 110);
                groupBox.Controls.Add(lblPrice);

                Label lblArtis = new Label();
                lblArtis.AutoSize = true;
                lblArtis.Text = context.Artists.Where(r => r.ArtistId == album.ArtistId)
                    .Select(r => r.Name).FirstOrDefault();
                lblArtis.Location = new Point(100, 130);
                groupBox.Controls.Add(lblArtis);

                Button btnAdd = new Button();
                btnAdd.Name = $"btn{album.AlbumId}";
                btnAdd.Text = "Add to cart";
                btnAdd.BackColor = Color.Blue;
                btnAdd.ForeColor = Color.White;
                btnAdd.AutoSize = true;
                btnAdd.Location = new Point(80,150);
                groupBox.Controls.Add(btnAdd);
                btnAdd.Click += BtnAdd_Click;
                i++;
                panel1.Controls.Add(groupBox);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bindPanel(pageList.PageIndex - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bindPanel(pageList.PageIndex + 1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindPanelSearch(1);
        }

        private void comGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPanel(1);
        }
    }
}
