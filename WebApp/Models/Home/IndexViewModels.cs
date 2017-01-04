using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Model;

namespace WebApp.Models
{
    public class HomeIndexViewModel
    {
        public ICollection<ADViewModel> ADViewModelList { get; set; }
        public ICollection<CategoryPhotoViewModel> CategoryPhotoList { get; set; }

        public HomeIndexViewModel(IEnumerable<AD> adList, IEnumerable<Category> topCategories)
        {
           ADViewModelList = new List<ADViewModel>();
            foreach (var ad in adList)
            {
              ADViewModelList.Add(new ADViewModel(ad));
            }

            CategoryPhotoList = new List<CategoryPhotoViewModel>();
            foreach (var category in topCategories)
            {
                CategoryPhotoList.Add(new CategoryPhotoViewModel(category.TopPhoto));
            }
        }
    }

    public class ADViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool NewWindow { get; set; }

        public ADViewModel(AD ad)
        {
            Id = ad.Id;
            Title = ad.Title;
            Url = ad.Url;
            NewWindow = ad.NewWindow;
        }
    }
}