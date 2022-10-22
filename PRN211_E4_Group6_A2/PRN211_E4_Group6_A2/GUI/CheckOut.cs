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
    public partial class CheckOut : Form
    {
        MusicStoreContext context;
        public double total = 0;
        private List<OrderDetail> ods;

        public CheckOut(double v,MusicStoreContext context, List<OrderDetail> ods)
        {
            InitializeComponent();
            this.context = context;
            total = v;
            setInfo();
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
            if(tx.Text.Length == 0)
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
                Order o = new Order();
                o.OrderId = ShoppingCart.CreateOrder(o);
                o.UserName = Settings.UserName;
                o.FirstName = FirstName.Text;
                o.LastName = LastName.Text;
                o.Address = Address.Text;
                o.City = City.Text;
                o.State = State.Text;
                o.Country = Country.Text;
                o.Phone = Phone.Text;
                o.Email = Email.Text;
                o.Total = decimal.Parse(Total.Text);
                if (Order.Insert(o))
                {
                    int orderID = Order.GetMaxID();
                    foreach (OrderDetail ot in ods)
                    {
                        ot.OrderID = orderID;
                        OrderDetail.Insert(ot);
                    }

                    MessageBox.Show("Save successfully!");
                    var cart = ShoppingCart.GetCart();
                    cart.EmptyCart();
                    this.Close();
                }
                else
                {
                    cancelResult();
                }
            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
  
    }
}
