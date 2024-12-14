using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Interfaces;
using POS_System.Interfaces.ISpecifications;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.ViewModels
{
    public partial class HomeViewModel  : ObservableObject
    {
        private readonly IUntiofWork _unitofwork;
        
        [ObservableProperty]
        private List<CategoryViewModel> categories;
        [ObservableProperty]
        private CategoryViewModel selectedCategory;
        [ObservableProperty]
        private List<MenueitemViewModel> items;
        [ObservableProperty]
        private MenueitemViewModel selectedMenueitem;

        public HomeViewModel(IUntiofWork unitofwork)
        {
            _unitofwork = unitofwork;
            IntilizeMainPage();
        }

        private  void IntilizeMainPage()
        {
            Categories = (_unitofwork.GetRepository<Category>().GetAll()).Select(C => new CategoryViewModel { Id = C.Id, Name = C.Name, Icon = C.Icon, IsChecked = false }).ToList();
            Categories[0].IsChecked = true;
            SelectedCategory = Categories[0];
            GetItems(SelectedCategory.Id);
        }
        private void GetItems(int id)
        {
            var spec = new ItemWithCategorySpec(id);
            Items = (_unitofwork.GetRepository<Item>().GetAllWithSpec(spec)).Select(i => new MenueitemViewModel() { Name = i.Name, Price = i.Price, Description = i.Description, Icon = i.Icon, IsSelected = false, CategoryId = i.CategoryId }).ToList();
        }
        public void SetSelected(CategoryViewModel category) 
        {
            if (SelectedCategory.Id == category.Id)
                return;

            foreach (var item in Categories) {
            
                item.IsChecked = false;
            }
            category.IsChecked = true;
            SelectedCategory = category;
            GetItems(SelectedCategory.Id);

        }
        public void SetChecked(MenueitemViewModel item)
        {
            if (SelectedMenueitem.Id == item.Id)
                return;

            foreach(var entity in items)
            {
                entity.IsSelected = false;
            }
            item.IsSelected = true;
            SelectedMenueitem = item;
        }
    }
}
