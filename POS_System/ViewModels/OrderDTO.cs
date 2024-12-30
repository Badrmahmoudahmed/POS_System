using CommunityToolkit.Mvvm.ComponentModel;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.ViewModels
{
    public partial class OrderDTO : ObservableObject
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ItemsCount { get; set; }
        public decimal OrderPrice { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        [ObservableProperty]
        public bool isSelected;
    }
}
