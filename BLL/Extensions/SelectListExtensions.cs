using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;

using Model;

namespace BLL
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(
             this IEnumerable<City> cities, int selectedId = 0)
        {
            return
                      cities.Select(city =>
                          new SelectListItem
                          {
                              Selected = (city.Id == selectedId),
                              Text = city.Name,
                              Value = city.Id.ToString()
                          });
        }
        public static IEnumerable<SelectListItem> ToSelectListItems(
              this IEnumerable<District> districts, int selectedId = 0)
        {
            return
                      districts.Select(d =>
                          new SelectListItem
                          {
                              Selected = (d.Id == selectedId),
                              Text = d.Name,
                              Value = d.Id.ToString()
                          });
        }
    }
}
