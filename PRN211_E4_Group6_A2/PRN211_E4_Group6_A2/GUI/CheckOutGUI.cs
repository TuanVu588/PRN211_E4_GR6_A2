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

        public CheckOutGUI(int Id, MusicStoreContext context)
        {
            InitializeComponent();
            FirstName.Text = FirstName.ToString();
            LastName.Text = LastName.ToString();
            Email.Text = Email.ToString();
            this.context = context;
            if (Id != -1)
            {
                Order order = context.Orders.Find(Total);
                User user = context.Users.Find(Id);
                FirstName.Text = user.FirstName;
                LastName.Text = user.LastName;
                Address.Text = user.Address;
                City.Text = user.City;
                State.Text = user.State;
                Country.Text = user.Country;
                Phone.Text = user.Phone;
                Email.Text = user.Email;
                Total.Text = Total.ToString();
            }

        }

        private void CheckOut_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(FirstName.Text) == -1)
            {
                Order order = new Order
                {
                    FirstName = FirstName.Text,
                    LastName = LastName.Text,
                    Address = Address.Text,
                    City = City.Text,
                    State = State.Text,
                    Country = Country.Text,
                    Phone = Phone.Text,
                    Email = Email.Text,
                };
                try
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                    MessageBox.Show("have been saved");
                }
                catch
                {
                    MessageBox.Show("Failed!");
                }
            }
        }


        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

  
  
