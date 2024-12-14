using CommunityToolkit.Mvvm.ComponentModel;
using POS_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System.ViewModels
{
    public partial class CategoryViewModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        [ObservableProperty]
        private bool isChecked;

       
    }
}
