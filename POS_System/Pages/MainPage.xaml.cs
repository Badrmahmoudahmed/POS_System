﻿

using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls;
using POS_System.ViewModels;

namespace POS_System.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel _homeViewModel;

        public MainPage(HomeViewModel homeViewModel)
        {
            BindingContext = homeViewModel;
            InitializeComponent();
            _homeViewModel = homeViewModel;
        }

        private  void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            if(sender is Element element && element.BindingContext is CategoryViewModel category)
            {
               _homeViewModel.SetSelected(category);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
        {
            if(sender is Element element && element.BindingContext is MenueitemViewModel menueitem)
            {
                _homeViewModel.SetChecked(menueitem);
            }
        }
    }

}
