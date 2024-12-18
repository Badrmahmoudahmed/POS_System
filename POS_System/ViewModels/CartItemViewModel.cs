using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.ViewModels
{
     public partial class  CartItemViewModel : ObservableObject
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount))]
        private int quentity;

        public decimal Price { get; set; }
        public decimal Amount => quentity * Price;


    }
}
