using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;

using BLL;
using System.Web.Mvc;

namespace WebApp.Services
{
    public class CategoryDynamicNodeProvider : DynamicNodeProviderBase
    {
        private readonly ICategoryService categoryService;

        public CategoryDynamicNodeProvider(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            var topCategoryList = categoryService.GetTopCategories();
            foreach (var topCategory in topCategoryList)
            {
                string controller = "Product";
                string action = "Category";
                string title = topCategory.Name;

                string id = topCategory.Id.ToString();
                string topCategoryKey = "parent_" + id;
                DynamicNode topCategoryNode = new DynamicNode();
                topCategoryNode.Action = action;
                topCategoryNode.Controller = controller;
                topCategoryNode.Title = title;
                topCategoryNode.RouteValues.Add("id",id);
                // dynamicNode.RouteValues.Add("id", new { id = id });

                topCategoryNode.Key = topCategoryKey;

                yield return topCategoryNode;

                var subCategoryList = categoryService.GetSubCategories(topCategory.Id);

                foreach (var subCategory in subCategoryList)
                {
                    var subCategoryNode = new DynamicNode();
                    subCategoryNode.Title = subCategory.Name;
                    subCategoryNode.Controller = controller;
                    subCategoryNode.Action = action;
                    subCategoryNode.RouteValues.Add("id", subCategory.Id);
                    subCategoryNode.ParentKey = topCategoryKey;
                    yield return subCategoryNode;  
                }
            }
        }
    }
}