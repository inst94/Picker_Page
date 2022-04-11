using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Picker_app
{
    public partial class MainPage : ContentPage
    {
        Button Picker_btn, Table_btn;
        public MainPage()
        {
            Picker_btn = new Button
            {
                Text = "PickerView",
                BackgroundColor = Color.Red
            };
            Table_btn = new Button
            {
                Text = "TableView",
                BackgroundColor = Color.Orange
            };
            StackLayout st = new StackLayout
            {
                Children = { Picker_btn, Table_btn }
            };
            st.BackgroundColor = Color.Aqua;
            Content = st;

            Picker_btn.Clicked += Start_Pages;
            Table_btn.Clicked += Start_Pages;

        }
        private async void Start_Pages(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (sender == Picker_btn)
            {
                await Navigation.PushAsync(new PickerPage());
            }
            else if (sender == Table_btn)
            {
                await Navigation.PushAsync(new Table_Page());
            }
        }
    }
}
