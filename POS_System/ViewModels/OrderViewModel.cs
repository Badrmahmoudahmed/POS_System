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
    public partial class OrderViewModel : ObservableObject
    {
        private readonly IUntiofWork _untiofWork;

        [ObservableProperty]
        private List<Order> orders;

        [ObservableProperty]
        private ObservableCollection<OrderItem> orderItems;
        public OrderViewModel(IUntiofWork untiofWork)
        {
            
            _untiofWork = untiofWork;
            OrderItems = new ObservableCollection<OrderItem>();
            IntilizeModel();
           
        }

        public void IntilizeModel()
        {
            var spec = new OrderWithItemsSpec();
            Orders =  _untiofWork.GetRepository<Order>().GetAllWithSpec(spec).ToList(); 
        }

        [RelayCommand]
        private void GetItems(int id)
        {
            var spec = new OrderWithItemsSpec(id);
            var order = _untiofWork.GetRepository<Order>().GetWithSpec(spec);

            orderItems.Clear();
            foreach (var item in order.OrderItems)
            {
                OrderItems.Add(item);
            }

            

        }
    }
}
