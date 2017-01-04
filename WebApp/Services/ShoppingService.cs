using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DAL;
using WebApp.Models;
using Core.Extensions;
using DAL.Infrastructure;

namespace WebApp.Services
{
    public interface IShoppingService
    {
        int CountCarryFee(int totalAmount);
        IEnumerable<CartItem> GetCartItemList();
        void AddToCart(int pid, int quantity);
        void DeleteItem(int pid);
        int GetCartItemsCount();

        void MigrateCart(ShoppingCart cart);
    }
    public class ShoppingService : IShoppingService
    {
        private readonly ICartItemRepository cartItemRepository;
        private readonly IUnitOfWork unitOfWork;
        public ShoppingService(IUnitOfWork unitOfWork, ICartItemRepository cartItemRepository)
        {
            var context = unitOfWork.Context;
            cartItemRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.cartItemRepository = cartItemRepository;

            this.profile = ProfileCommon.GetProfile();
        }
        bool Authenticated
        {

            get { return HttpContext.Current.Request.IsAuthenticated; }
        }

        #region  Member
        string UserName
        {

            get { return HttpContext.Current.User.Identity.Name; }
        }
        #endregion
        #region   Anonymous
        ProfileCommon profile;
        ShoppingCart Cart
        {
            get
            {
                if (profile.ShoppingCart == null) profile.ShoppingCart = new ShoppingCart { Created = DateTime.Now.ConvertToTaipeiTime(), CartItems = new Dictionary<string, CartItem>() };
                return profile.ShoppingCart;
            }
        }
        #endregion
        public int CountCarryFee(int totalAmount)
        {
            if (totalAmount > 799) return 0;
            return 120;
        }
        public int GetCartItemsCount()
        {
            if (Authenticated) return cartItemRepository.GetMany(i => i.CartId == UserName).Count();
            else  return Cart.CartItems.Count;            
        }

        public IEnumerable<CartItem> GetCartItemList()
        {

            if (Authenticated)
            {
                var itemList = cartItemRepository.GetMany(i => i.CartId == UserName);
                if (itemList.IsNullOrEmpty()) return null;
                var cartItemList = new List<CartItem>();
                foreach (var item in itemList)
                {
                    cartItemList.Add(new CartItem(item.ProductId, item.Quantity));
                }
                return cartItemList;
            }
            else
            {
                if (Cart.CartItems.Count < 1) return null;
                var cartItemList = new List<CartItem>();
                var keys = Cart.CartItems.Keys.ToList();
                foreach (var key in keys)
                {
                    cartItemList.Add(Cart.CartItems[key]);
                }
                return cartItemList;
            }

        }

        Model.CartItem NewCartItem(int pid, int quantity)
        {
            var newItem = new Model.CartItem
            {
                CartId = UserName,
                ProductId = pid,
                Quantity = quantity,
                CreateDate = DateTime.Now.ConvertToTaipeiTime(),
                LastUpdate = DateTime.Now.ConvertToTaipeiTime()
            };

            return newItem;
        }
        public void AddToCart(int pid, int quantity)
        {
            if (pid < 1) throw new Exception("商品不存在");

            if (Authenticated)
            {
                var item = cartItemRepository.Get(i => i.CartId == UserName && i.ProductId == pid);
                if (item != null)
                {
                    item.Quantity = quantity;
                    item.LastUpdate = DateTime.Now.ConvertToTaipeiTime();
                    Save();
                }
                else
                {
                    cartItemRepository.Insert(NewCartItem(pid, quantity));
                    Save();
                }
            }
            else
            {
                string key = pid.ToString();
                if (Cart.CartItems.ContainsKey(key))
                {
                    UpdateItem(key, quantity);
                }
                else
                {
                    Cart.CartItems.Add(key, new CartItem(pid, quantity));

                    Save();
                }
            }
        }

        public void DeleteItem(int pid)
        {
            if (Authenticated)
            {
                var item = cartItemRepository.Get(i => i.CartId == UserName && i.ProductId == pid);
                if (item == null) return;
                cartItemRepository.Delete(item);
                Save();
            }
            else
            {
                string itemId = pid.ToString();
                var existItem = Cart.CartItems[itemId];
                if (existItem != null)
                {
                    Cart.CartItems.Remove(itemId);
                    Save();
                }
            }
           
        }

        public void MigrateCart(ShoppingCart cart)
        {
            var keys = cart.CartItems.Keys.ToList();
            foreach (var key in keys)
            {
                var item = cart.CartItems[key];
                int pid = item.Id;
                int quantity = item.Quantity;
                cartItemRepository.Insert(NewCartItem(pid, quantity));
            }
            Save();
        }

        void UpdateItem(string itemId, int quantity)
        {
            if (Authenticated) return;

            var item = Cart.CartItems[itemId];
            item.Quantity = quantity;
            Save();
        }

        void Save()
        {
            if (Authenticated) unitOfWork.Commit();
            else profile.Save();

        }
    }
}