using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace fabrica.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel.BaseViewModel();
        }
    }
}
