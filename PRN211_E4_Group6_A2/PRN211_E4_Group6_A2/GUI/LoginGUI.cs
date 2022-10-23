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
    public partial class LoginGUI : Form
    {
        
        public LoginGUI()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            bool isUser = false;
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            MusicStoreContext context = new MusicStoreContext();
            if(userName != String.Empty && password != String.Empty)
            {
                IQueryable<User> admins = context.Users.Where(a => a.Role == 1);
                IQueryable<User> users = context.Users.Where(a => a.Role == 0);
                
                foreach (User user in admins)
                {
                    if (userName.Equals(user.UserName) && password.Equals(user.Password))
                    {
                        Settings.UserName = user.UserName;
                        Settings.Role = user.Role;                       
                        this.Close();
                        isUser = true;
                    }
                                      
                }
                foreach(User user in users)
                {
                    if(userName.Equals(user.UserName) && password.Equals(user.Password))
                    {                       
                        Settings.UserName = user.UserName;
                        Settings.Role = user.Role;
                        this.Close();
                        isUser = true;
                    }
                }
                if (isUser == false)
                {
                    MessageBox.Show("That member does not exist!");
                }
            }
            else
            {
                MessageBox.Show("Please fill into blank!");
            }
        }
    }
}
