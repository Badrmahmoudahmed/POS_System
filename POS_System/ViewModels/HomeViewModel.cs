using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Interfaces;
using POS_System.Interfaces.ISpecifications;
using POS_System.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace POS_System.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IUntiofWork _unitofwork;
        private readonly OrderViewModel _orderViewModel;

        [ObservableProperty]
        private List<CategoryViewModel> categories;

        [ObservableProperty]
        private CategoryViewModel selectedCategory;

        [ObservableProperty]
        private List<MenueitemViewModel> items;

        [ObservableProperty]
        private MenueitemViewModel selectedMenuItem;

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

        public HomeViewModel(IUntiofWork unitOfWork, OrderViewModel orderViewModel)
        {
            _unitofwork = unitOfWork;
            _orderViewModel = orderViewModel;

            InitializeMainPage();
            Cart.CollectionChanged += Cart_CollectionChanged;
        }

        private void Cart_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            RecalculateSubTotal();
        }

        private void InitializeMainPage()
        {
            Categories = _unitofwork.GetRepository<Category>().GetAll()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Icon = c.Icon,
                    IsChecked = false
                }).ToList();

            if (Categories.Any())
            {
                Categories[0].IsChecked = true;
                SelectedCategory = Categories[0];
                GetItems(SelectedCategory.Id);
            }

            Cart = new ObservableCollection<CartItemViewModel>();
        }

        private void GetItems(int categoryId)
        {
            var spec = new ItemWithCategorySpec(categoryId);
            Items = _unitofwork.GetRepository<Item>().GetAllWithSpec(spec)
                .Select(i => new MenueitemViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    Description = i.Description,
                    Icon = i.Icon,
                    IsSelected = false,
                    CategoryId = i.CategoryId
                }).ToList();
        }

        public void SetSelected(CategoryViewModel category)
        {
            if (SelectedCategory?.Id == category.Id)
                return;

            Categories.ForEach(c => c.IsChecked = false);
            category.IsChecked = true;
            SelectedCategory = category;

            GetItems(SelectedCategory.Id);
        }

        public void SetChecked(MenueitemViewModel menuItem)
        {
            if (selectedMenuItem?.Id == menuItem.Id)
                return;

            Items.ForEach(i => i.IsSelected = false);
            menuItem.IsSelected = true;
            selectedMenuItem = menuItem;
        }

        public void AddToCart(MenueitemViewModel model)
        {
            var existingCartItem = Cart.FirstOrDefault(c => c.ItemId == model.Id);
            if (existingCartItem == null)
            {
                Cart.Add(new CartItemViewModel
                {
                    ItemId = model.Id,
                    Price = model.Price,
                    Icon = model.Icon,
                    Name = model.Name,
                    Quentity = 1
                });
            }
            else
            {
                existingCartItem.Quentity++;
                RecalculateSubTotal();
            }
        }

        [RelayCommand]
        private void IncreaseQuantity(CartItemViewModel cartItem)
        {
            cartItem.Quentity++;
            RecalculateSubTotal();
        }

        [RelayCommand]
        private void DecreaseQuantity(CartItemViewModel cartItem)
        {
            if (cartItem.Quentity == 1)
            {
                Delete(cartItem);
            }
            else
            {
                cartItem.Quentity--;
                RecalculateSubTotal();
            }
        }

        [RelayCommand]
        private void Delete(CartItemViewModel cartItem)
        {
            Cart.Remove(cartItem);
        }

        private void RecalculateSubTotal()
        {
            SubTotal = Cart.Sum(c => c.Amount);
        }

        [RelayCommand]
        private async Task SetTaxAsync()
        {
            if (!Cart.Any()) return;

            var enteredTax = await Shell.Current.DisplayPromptAsync("Tax%", "Enter Tax Percentage");
            if (int.TryParse(enteredTax, out var tax))
            {
                TaxPercentage = tax;
            }
            else
            {
                await Shell.Current.DisplayAlert("Warning", "Invalid Tax Percentage", "OK");
            }
        }

        [RelayCommand]
        private async Task RemoveCartAsync()
        {
            if (!Cart.Any()) return;

            var confirm = await Shell.Current.DisplayAlert("Warning", "Are you sure?", "Confirm", "Cancel");
            if (confirm) Cart.Clear();
        }

        [RelayCommand]
        private async Task CreateOrderAsync(string paymentMethod)
        {
            if (!Cart.Any()) return;

            var order = new Order
            {
                OrderDate = DateTime.Now,
                ItemsCount = Cart.Count,
                OrderPrice = TotalAmount,
                PaymentMethod = paymentMethod,
                OrderItems = Cart.Select(c => new OrderItem
                {
                    ItemId = c.ItemId,
                    Icon = c.Icon,
                    Name = c.Name,
                    Price = c.Price,
                    Quantity = c.Quentity
                }).ToList()
            };

            await _unitofwork.GetRepository<Order>().AddAsync(order);
            var saveResult = await _unitofwork.SaveChangesAsync();

            if (saveResult > 0)
            {
                await Shell.Current.DisplayAlert("Success", "Order added successfully", "OK");
                _orderViewModel.IntilizeModel();
                Cart.Clear();
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add order. Please try again.", "OK");
            }
        }

    }
}
