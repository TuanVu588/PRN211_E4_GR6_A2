using MusicStoreWin.Models;
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
    public partial class MainGUI : Form
    {
        public MainGUI()
        {
            InitializeComponent();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginGUI loginGUI = new LoginGUI();
            
            loginGUI.Show();

        }

        private void MainGUI_Activated(object sender, EventArgs e)
        {
            if(Settings.UserName != null && Settings.UserName != " ")
            {
                loginToolStripMenuItem.Text = $"Logout({Settings.UserName})";
            }
            else
            {
                loginToolStripMenuItem.Text = $"Login";
            }
            if (Settings.Role == 1) albumsToolStripMenuItem.Visible = true;
            else albumsToolStripMenuItem.Visible = false;
            ShoppingCart cart = ShoppingCart.GetCart();
            int count = cart.GetCount();
            cartToolStripMenuItem.Text = $"Cart {count}";
        }

        private void shoppingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShoppingGUI shoppingGUI = new ShoppingGUI();
            shoppingGUI.TopLevel = false;
            shoppingGUI.FormBorderStyle = FormBorderStyle.None;
            shoppingGUI.Show();

            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(shoppingGUI);
        }
    }
}
