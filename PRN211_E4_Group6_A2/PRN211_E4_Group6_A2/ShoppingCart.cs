using Microsoft.EntityFrameworkCore;
using PRN211_E4_Group6_A2;
using PRN211_E4_Group6_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MusicStoreWin.Models
{
    public partial class ShoppingCart
    {
        MusicStoreContext storeDB = new MusicStoreContext();
        string ShoppingCartId { get; set; }
   //     public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart()
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId();
            return cart;
        }
  
        public void AddToCart(Album album)
        {
            // Get the matching cart and album instances
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.AlbumId == album.AlbumId);
                
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the matching cart and album id
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.RecordId == id);               

            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts
                .Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId)
                .Include(cart => cart.Album)
                .ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Album.Price).Sum();
            return total ?? 0;
        }
        public int CreateOrder(Order order)
        {
         
            var cartItems = GetCartItems();

            // Save the order
            try
            {
                storeDB.Orders.Add(order);                
                storeDB.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            int orderID = storeDB.Orders.Select(o => o.OrderId).Max();            
            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = orderID,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                try
                {
                    storeDB.OrderDetails.Add(orderDetail);
                    storeDB.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return orderID;
        }
        public string GetCartId()
        {
            if (Settings.CartId == null)
            {
                if (Settings.UserName != null)
                    Settings.CartId = Settings.UserName;
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    Settings.CartId = tempCartId.ToString();
                }
            }
            return Settings.CartId;
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart()
        {
            var shoppingCart = storeDB.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = Settings.UserName;
            }
            storeDB.SaveChanges();
            Settings.CartId = null;
        }
    }
}
