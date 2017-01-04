using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL;
using Model;

namespace AdminApp.Controllers
{
    public class CategoryController : AppControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(IErrorLogService errorLogService, ICategoryService categoryService)
            : base(errorLogService)
        {

            this.categoryService = categoryService;

        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
            //var category = new ProductCategory()
            //{
            //    Active = true,
            //    DisplayOrder = 1,
            //    Parent = 0,
            //    Name = "個性飾品",
            //    Prefix = ""

            //};

            //category = categoryService.Create(category);
            //return View(category);
        }
    }
}