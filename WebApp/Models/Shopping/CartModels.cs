using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Model;
using Core.Extensions;
using Web.Core.Models;

namespace WebApp.Models
{
    public class ShoppingCartViewModel
    {
        public int ItemCount
        {
            get
            {
                if (CartItemViewList.IsNullOrEmpty()) return 0;
                return CartItemViewList.Count;
            }
        }
        public int TotalAmount
        {
            get
            {
                if (CartItemViewList.IsNullOrEmpty()) return 0;
                return CartItemViewList.Sum(c => c.Amount);
            }
        }

        public int CarryFee { get; set; }

        public int TotalMoney
        {
            get
            {
                return TotalAmount + CarryFee;
            }
        }

        public ICollection<CartItemViewModel> CartItemViewList { get; set; }

        public UpdateCartModel UpdateCartModel { get; set; }

        public ShoppingCartViewModel()
        {
            CartItemViewList = new List<CartItemViewModel>();
            UpdateCartModel = new UpdateCartModel();
        }

        public ShoppingCartViewModel(UpdateCartResultModel resultStatus)
        {
            CartItemViewList = new List<CartItemViewModel>();
            UpdateCartModel = new UpdateCartModel() { ResultStatus = resultStatus };
        
        }
    }
    public class CartItemViewModel 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Amount
        {

            get { return this.Price * Quantity; }

        }

        public CartItemPhotoViewModel ItemPhotoViewModel { get; set; }
     

        public CartItemViewModel(Product product , int quantity)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            Quantity = quantity;

            var photo = product.DefaultPhoto;

            ItemPhotoViewModel = new CartItemPhotoViewModel(photo);
        }

      
    }

    public class CartItemPhotoViewModel : PhotoViewModel
    { 
       public CartItemPhotoViewModel(Photo photo)
        {
            SetValues(photo);
        }
    }

    public class UpdateCartModel
    {
        public string UpdateTargetId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public UpdateCartResultModel ResultStatus { get; set; }
    
    }

    public class AddToCartModel : UpdateCartModel
    {
        public IEnumerable<System.Web.Mvc.SelectListItem> QuantityOptions { get; set; }

     

        public AddToCartModel()
        {
            var optionList = new List<SelectListItem>();
            for (int i = 1; i <=12 ; i++)
            {
                optionList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            QuantityOptions = optionList;

        }

    }

    public class UpdateCartResultModel : StatusAlert
    {
        public UpdateCartResult UpdateCartResult { get; set; }
    }

    public enum UpdateCartResult
    {
        Success,
        Failure,
        Exception

    }

    
}