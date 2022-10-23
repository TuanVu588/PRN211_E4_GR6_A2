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
                logoutToolStripMenuItem.Text = $"Logout({Settings.UserName})";
            }
            else
            {
                loginToolStripMenuItem.Text = $"Login";
            }
            if(Settings.Role == 1 && Settings.UserName != null && Settings.UserName != " ")
            {
                albumToolStripMenuItem.Visible = true;
                logoutToolStripMenuItem.Visible = true;
                loginToolStripMenuItem.Visible = false;
            }
            else if(Settings.Role == 0 && Settings.UserName != null && Settings.UserName != " ")
            {
                albumToolStripMenuItem.Visible = false;
                loginToolStripMenuItem.Visible = false;
                logoutToolStripMenuItem.Visible = true;
            }
            ShoppingCart cart = ShoppingCart.GetCart();
            int count = cart.GetCount();
            cartToolStripMenuItem.Text = $"Cart ({count})";
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

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShoppingCart cart = new ShoppingCart();
            logoutToolStripMenuItem.Visible = false;
            loginToolStripMenuItem.Visible = true;
            albumToolStripMenuItem.Visible = false;
            cart = ShoppingCart.GetCart();
            Settings.UserName = null;
            Settings.CartId = null;
            cartToolStripMenuItem.Text = "Cart (0)";
        }
    }
}
