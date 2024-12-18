using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Interfaces;
using POS_System.Interfaces.ISpecifications;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.ViewModels
{
    public partial class HomeViewModel : ObservableObject
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
        [ObservableProperty]
        private ObservableCollection<CartItemViewModel> cart;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TaxAmount))]
        [NotifyPropertyChangedFor(nameof(TotalAmount))]
        private decimal subTotal;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TaxAmount))]
        [NotifyPropertyChangedFor(nameof(TotalAmount))]
        private int taxPercentage;
        public decimal TaxAmount => (SubTotal * TaxPercentage) / 100;
        public decimal TotalAmount => SubTotal + TaxAmount;

        public HomeViewModel(IUntiofWork unitofwork)
        {
            _unitofwork = unitofwork;
            IntilizeMainPage();
            Cart.CollectionChanged += Cart_CollectionChanged;
        }

        private void Cart_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            RecalculateSubTotal();
        }

        private void IntilizeMainPage()
        {
            Categories = (_unitofwork.GetRepository<Category>().GetAll()).Select(C => new CategoryViewModel { Id = C.Id, Name = C.Name, Icon = C.Icon, IsChecked = false }).ToList();
            Categories[0].IsChecked = true;
            SelectedCategory = Categories[0];
            GetItems(SelectedCategory.Id);
            Cart = new ObservableCollection<CartItemViewModel>();
        }
        private void GetItems(int id)
        {
            var spec = new ItemWithCategorySpec(id);
            Items = (_unitofwork.GetRepository<Item>().GetAllWithSpec(spec)).Select(i => new MenueitemViewModel() { Id = i.Id, Name = i.Name, Price = i.Price, Description = i.Description, Icon = i.Icon, IsSelected = false, CategoryId = i.CategoryId }).ToList();
        }
        public void SetSelected(CategoryViewModel category)
        {
            if (SelectedCategory.Id == category.Id)
                return;

            foreach (var item in Categories)
            {

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

            foreach (var entity in items)
            {
                entity.IsSelected = false;
            }
            item.IsSelected = true;
            SelectedMenueitem = item;
        }
        public void AddToCart(MenueitemViewModel model)
        {
            var cartmodel = Cart.FirstOrDefault(c => c.ItemId == model.Id);

            if (cartmodel is null)
            {
                var NewItem = new CartItemViewModel()
                {
                    ItemId = model.Id,
                    Price = model.Price,
                    Icon = model.Icon,
                    Name = model.Name,
                    Quentity = 1
                };
                Cart.Add(NewItem);

            }
            else
            {

                cartmodel.Quentity++;
                RecalculateSubTotal();
            }

        }
        [RelayCommand]
        private void IncreaseQuantity(CartItemViewModel cartitem)
        {
            var curritem = Cart.FirstOrDefault(c => c.ItemId == cartitem.ItemId);
            if (curritem is not null)
            {
                cartitem.Quentity++;
                RecalculateSubTotal();
            }
        }
        [RelayCommand]
        private void DecreaseQuantity(CartItemViewModel cartitem)
        {
            var curritem = Cart.FirstOrDefault(c => c.ItemId == cartitem.ItemId);
            if (curritem is not null)
            {
                if (cartitem.Quentity == 1)
                    Delete(cartitem);
                else
                {
                    cartitem.Quentity--;
                    RecalculateSubTotal();
                }


            }
        }
        [RelayCommand]
        private void Delete(CartItemViewModel cartitem)
        {
            var curritem = Cart.FirstOrDefault(c => c.ItemId == cartitem.ItemId);
            if (curritem is not null)
                Cart.Remove(cartitem);
        }
        private void RecalculateSubTotal()
        {
            SubTotal = Cart.Sum(c => c.Amount);
        }
        [RelayCommand]
        private async void SetTax()
        {
          if (!Cart.Any()) return;
          var EnteredTax = await  Shell.Current.DisplayPromptAsync("Tax%", "Enter Tax Percentage");
            if(!string.IsNullOrEmpty(EnteredTax))
            {
               var result = int.TryParse(EnteredTax, out var quantity);
               if(result)
                   { TaxPercentage = quantity; }
               else 
                await Shell.Current.DisplayAlert("Warning", "Invalid Tax Percentage", "I'am So Sorry");

            }
          

        }
        [RelayCommand]
        private async void RemoveCart()
        {
            if (!Cart.Any())
                return;
            var result = await Shell.Current.DisplayAlert("Warning", "Are You Sure!", "Confirm", "Cancel");
            if (!result)
                return;

            Cart.Clear();
        }
        [RelayCommand]
        private async Task CreateOrdar(string PayMethod)
        {
            if (!Cart.Any()) return;
            var order = new Order()
            {
                OrderDate = DateTime.Now,
                ItemsCount = Cart.Count,
                OrderPrice = TotalAmount,
                PaymentMethod = PayMethod,
                OrderItems = Cart.Select(c => new OrderItem() { ItemId = c.ItemId, Icon = c.Icon, Name = c.Name, Price = c.Price, Quantity = c.Quentity }).ToList()
            };
           await _unitofwork.GetRepository<Order>().AddAsync(order);
           var count =  await _unitofwork.SaveChangesAsync();
            if (count > 0)
            {
                await Shell.Current.DisplayAlert("Success", "Order Added Successfully", "Confirm");
                Cart.Clear();
            }
            else {
              await Shell.Current.DisplayAlert("Error Message", "Invalid Order", "OK");
            }
        }
    }
}
