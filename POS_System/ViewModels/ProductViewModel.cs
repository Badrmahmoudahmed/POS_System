using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Infrastructure;
using POS_System.Infrastructure.Contexts;
using POS_System.Interfaces;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly IUntiofWork _untiofWork;
        [ObservableProperty]
        private string productname;
        [ObservableProperty]
        private decimal productprice;
        [ObservableProperty]
        private int productstock;
        [ObservableProperty]
        private string productcategory;
        [ObservableProperty]
        private ObservableCollection<Product> products;

        public ProductViewModel(IUntiofWork untiofWork)
        {
            _untiofWork = untiofWork;
            products = new ObservableCollection<Product>();
        }

        [RelayCommand]
        public async void AddProduct()
        {
            var product = new Product()
            {
                Name = Productname,
                Price = Productprice,
                Stock = Productstock,
                Category = Productcategory
            };

            await _untiofWork.GetRepository<Product>().AddAsync(product);
            Products = new ObservableCollection<Product>(await _untiofWork.GetRepository<Product>().GetAllAsync());

            Productname = "";
            Productprice = 0;
            Productstock = 0;
            Productcategory = "";


        }
    }
}
