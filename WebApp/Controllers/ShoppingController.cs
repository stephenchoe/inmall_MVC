using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using WebApp.Models;
using BLL;
using Model;
using WebApp.Services;
using Core.Extensions;
using Web.Core.Models;

namespace WebApp.Controllers
{
    public class ShoppingController : AppControllerBase
    {
        private readonly IShoppingService shoppingService;
        private readonly IProductService productService;

        public ShoppingController(IErrorLogService errorLogService, IShoppingService shoppingService, IProductService productService)
            : base(errorLogService)
        {

            this.shoppingService = shoppingService;
            this.productService = productService;
        }
        // GET: Shopping
        public ActionResult Index()
        {
            var model = new ShoppingCartViewModel();

            return CartItemList(model);
        }

        ActionResult CartItemList(ShoppingCartViewModel model)
        {
            var cartItemList = shoppingService.GetCartItemList();
            if (cartItemList.IsNullOrEmpty())// return View(model);
            {
                if (IsAjax) return PartialView("Index", model);
                return View("Index", model);
            }

            foreach (var item in cartItemList)
            {
                var product = productService.GetById(item.Id);

                model.CartItemViewList.Add(new CartItemViewModel(product, item.Quantity));
            }

            model.CarryFee = shoppingService.CountCarryFee(model.TotalAmount);

            if (IsAjax) return PartialView("Index", model);
            return View("Index", model);
        }
        [HttpGet]
        public int GetCartItemsCount()
        {
            int itemsCount = shoppingService.GetCartItemsCount();
            return itemsCount;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(AddToCartModel model)
        {
            try
            {
                AddUpdateCartItem(model);

                int itemsCount = GetCartItemsCount();
                return Json(new { items = itemsCount }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                HandleException(ex);
                return ThrowJsonError();
            }

        }

        void AddUpdateCartItem(UpdateCartModel model)
        {
            int pid = model.ProductId;
            int quantity = model.Quantity;

            var product = productService.GetById(pid);
            if (product == null) throw new Exception("商品不存在");

            shoppingService.AddToCart(pid, quantity);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(UpdateCartModel model)
        {
            try
            {
                if (model.Quantity == 0) DeleteItem(model);
                else AddUpdateCartItem(model);

                var resultStatus = GetUpdateCartResultModel(StatusAlertType.Success, "", "", false, UpdateCartResult.Success);

                var shoppingCartModel = new ShoppingCartViewModel(resultStatus);
                return CartItemList(shoppingCartModel);

            }
            catch (Exception ex)
            {
                HandleException(ex);

                string title = "更新購物車失敗";
                string msg = ex.Message;
                bool showConfirm = true;
                var resultStatus = GetUpdateCartResultModel(StatusAlertType.Error, title, msg, showConfirm, UpdateCartResult.Exception);

                var shoppingCartModel = new ShoppingCartViewModel(resultStatus);
                return CartItemList(shoppingCartModel);
            }


        }
        UpdateCartResultModel GetUpdateCartResultModel(StatusAlertType type, string title, string msg, bool showConfirm, UpdateCartResult result)
        {
            var model = new UpdateCartResultModel() { UpdateCartResult = result };
            SetStatusAlert(model, type, title, msg, showConfirm);

            return model;
        }
        void DeleteItem(UpdateCartModel model)
        {
            int pid = model.ProductId;

            var product = productService.GetById(pid);
            if (product == null) throw new Exception("商品不存在");

            shoppingService.DeleteItem(pid);
        }

        public ActionResult Order()
        {
            return View();
        }
        public ActionResult test()
        {
            var model = new AddToCartModel { ProductId = 155, Quantity = 1 };
            return View(model);
        }
    }
}