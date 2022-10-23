using System;
using PRN211_E4_Group6_A2.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Net;
using MusicStoreWin.Models;
using Microsoft.Extensions.Configuration;

namespace PRN211_E4_Group6_A2.GUI
{
    public partial class CheckOutGUI : Form
    {
        MusicStoreContext context;
        public double total = 0;
        private List<OrderDetail> ods;

        public CheckOutGUI(double v, List<OrderDetail> ods)
        {
            InitializeComponent();
            context = new MusicStoreContext();
            total = v;
            setInfo();
            ods = new List<OrderDetail>();
        }

        public void setInfo()
        {
            if (Settings.UserName != null || Settings.UserName != "")
            {
                User user = new User();
                FirstName.Text = user.FirstName;
                LastName.Text = user.LastName;
                Address.Text = user.Address;
                City.Text = user.City;
                State.Text = user.State;
                Country.Text = user.Country;
                Phone.Text = user.Phone;
                Email.Text = user.Email;
                Total.Text = total.ToString();
            }
        }

        private void CheckOut_Load(object sender, EventArgs e)
        {

        }
        private Boolean checkTxtBox(TextBox tx)
        {
            if (tx.Text.Length == 0)
            {
                return false;
            }
            return true;
        }
        private void cancelResult()
        {
            this.DialogResult = DialogResult.None;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkTxtBox(FirstName))
            {
                MessageBox.Show("First Name is empty!");
                cancelResult();
            }
            else if (!checkTxtBox(LastName))
            {
                MessageBox.Show("Last Name is empty!");
                cancelResult();
            }
            else if (!checkTxtBox(Email))
            {
                MessageBox.Show("Email is empty!");
                cancelResult();
            }
            else
            {

                Order or = new Order();
                or.UserName = Settings.UserName;
                or.FirstName = FirstName.Text;
                or.LastName = LastName.Text;
                or.Address = Address.Text;
                or.City = City.Text;
                or.State = State.Text;
                or.Country = Country.Text;
                or.Phone = Phone.Text;
                or.Email = Email.Text;
                or.Total = decimal.Parse(Total.Text);
                context.SaveChanges();
                MessageBox.Show("Save successfully!");
                var order = ShoppingCart.GetCart();
                order.EmptyCart();
                
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
  
  
