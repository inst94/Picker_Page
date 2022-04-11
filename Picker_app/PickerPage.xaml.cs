using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Picker_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        ImageButton home_btn, previous_page_btn;
        Entry line;
        Picker picker;
        WebView webView;
        StackLayout st, entry_layout;
        Frame frame;
        string[] lehed = new string[4] { "https://tahvel.edu.ee/", "https://moodle.edu.ee/", "https://www.tthk.ee", "https://www.google.com/" };
        public PickerPage()
        {
            picker = new Picker
            {
                Title = "Webilehed"
            };
            picker.Items.Add("Tahvel");
            picker.Items.Add("Moodle");
            picker.Items.Add("TTHK");
            //picker.Items.Add("Google");
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            webView = new WebView { };
            SwipeGestureRecognizer swipe = new SwipeGestureRecognizer();
            swipe.Swiped += Swipe_Swiped;
            swipe.Direction = SwipeDirection.Right;
            line = new Entry
            {
                Placeholder = "Kirjuta veebileht: www.",
                PlaceholderColor=Color.Black,
                WidthRequest = 200,
                TextColor=Color.Black,
                FontSize=12,
            };
            home_btn = new ImageButton
            {
                Source = "home_btn_image.png",
            };
            previous_page_btn = new ImageButton
            {
                Source = "back_btn_image.png",
            };
            line.Completed += Line_Completed;
            home_btn.Clicked += Home_btn_Clicked;
            previous_page_btn.Clicked += Previous_page_btn_Clicked;
            entry_layout = new StackLayout
            {
                Children = { line, home_btn, previous_page_btn },
                Orientation = StackOrientation.Horizontal
            };
            frame = new Frame
            {
                Content = entry_layout,
                BorderColor = Color.AliceBlue,
                CornerRadius = 20,
                HeightRequest = 40,
                WidthRequest = 400,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HasShadow = true
            };
            st = new StackLayout
            {
                Children = { picker, frame }
            };
            frame.GestureRecognizers.Add(swipe);
            Content = st;
        }

        private async void Line_Completed(object sender, EventArgs e)
        {
            webView.Source = "https://www." + line.Text;
            bool result = await DisplayAlert("Pop up", "Kas soovid leht lisada?", "Yah", "Ei");
            if (result)
            {
                List<string> lehed = new List<string> { "Lisatud leht" };
                picker.Items.Add("https://www." + line.Text);
            }
            else
                await DisplayAlert("", "Mine tagasi", "OK");
        }

        private void Previous_page_btn_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
        }

        private void Home_btn_Clicked(object sender, EventArgs e)
        {
            webView.Source = "https://www.google.com/";
        }

        private void Swipe_Swiped(object sender, SwipedEventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = lehed[3] };
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (webView != null)
            {
                st.Children.Remove(webView);
            }
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            st.Children.Add(webView);
        }
    }
}

