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
        private ObservableCollection<OrderDTO> orders;

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
            var AllOrders = _untiofWork.GetRepository<Order>().GetAllWithSpec(spec).Select(o => new OrderDTO()
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                ItemsCount = o.ItemsCount,
                OrderPrice = o.OrderPrice,
                PaymentMethod = o.PaymentMethod,
                OrderItems = o.OrderItems,
                isSelected = false

            });
            Orders = new ObservableCollection<OrderDTO>();
            foreach (var order in AllOrders)
            {
                Orders.Add(order);
            }
        }
        [RelayCommand]
        private void GetItems(int id)
        {
            var spec = new OrderWithItemsSpec(id);
            var order = Orders.FirstOrDefault(o => o.Id == id);
            var selectedOrder = Orders.FirstOrDefault(i => i.IsSelected);
            if(selectedOrder is not null)
            {
                selectedOrder.IsSelected = false;
                if(selectedOrder.Id == order.Id)
                {
                    order.IsSelected = false;
                    OrderItems.Clear();
                    return;
                }
            }

            orderItems.Clear();
            foreach (var item in order.OrderItems)
            {
                OrderItems.Add(item);
            }
            order.IsSelected = true;

            

        }
    }
}
