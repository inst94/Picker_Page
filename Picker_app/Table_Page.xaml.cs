using System;
using System.Collections.Generic;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Picker_app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Table_Page : ContentPage
    {
        ViewCell lisavoimalused;
        Button sms_btn, call_btn, mail_btn;
        EntryCell telefon, e_mail, sonum, nimi;
        StackLayout stack;
        TableView tableView;
        SwitchCell sc;
        ImageCell ic;
        TableSection fotosection;
        public Table_Page()
        {
            sms_btn = new Button { Text = "Saada sms" };
            call_btn = new Button { Text = "Helista" };
            mail_btn = new Button { Text = "Saada email" };
            sms_btn.Clicked += Sms_btn_Clicked;
            call_btn.Clicked += Call_btn_Clicked;
            mail_btn.Clicked += Mail_btn_Clicked;
            stack = new StackLayout
            {
                Children = { sms_btn, call_btn, mail_btn },
                Orientation = StackOrientation.Horizontal
            };
            lisavoimalused = new ViewCell();
            lisavoimalused.View = stack;

            nimi = new EntryCell
            {
                Label = "Nimi:",
                Placeholder = "Sisesta oma sobra nimi",
                Keyboard = Keyboard.Default
            };
            telefon = new EntryCell
            {
                Label = "Telefon",
                Placeholder = "Sisesta tel. number",
                Keyboard = Keyboard.Telephone
            };
            e_mail = new EntryCell
            {
                Label = "Email",
                Placeholder = "Sisesta email",
                Keyboard = Keyboard.Email
            };
            sonum = new EntryCell
            {
                Label = "Sonum",
                Placeholder = "Sisesta sonum",
                Keyboard = Keyboard.Default
            };
            sc = new SwitchCell { Text = "Naita veel" };
            sc.OnChanged += Sc_OnChanged;
            ic = new ImageCell
            {
                ImageSource = ImageSource.FromFile("duck.jpg"),
                Text = "Foto nimetus",
                Detail = "Foto kirjeldus"
            };
            fotosection = new TableSection();
            tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Andmete sisestamine")
                {
                    new TableSection("Pohiandmed:")
                    {
                        nimi
                    },
                    new TableSection("Andmed:")
                    {
                        telefon,
                        e_mail,
                        sonum,
                        sc,
                        lisavoimalused
                    },
                    fotosection
                }
            };
            Content = tableView;
        }

        private async void Mail_btn_Clicked(object sender, EventArgs e)
        {
            if (e_mail.Text == "")
            {
                await DisplayAlert("Error", "Palun taida Email", "OK");
            }
            else if (sonum.Text == "")
            {
                await DisplayAlert("Error", "Palun sisesta sonum", "OK");
            }
            else
            {
                var mail = CrossMessaging.Current.EmailMessenger;
                if (mail.CanSendEmail)
                {
                    mail.SendEmail(e_mail.Text, nimi.Text, sonum.Text);
                }
            }

        }

        private async void Call_btn_Clicked(object sender, EventArgs e)
        {
            if (telefon.Text == "")
            {
                await DisplayAlert("Error", "Palun taida telefoni number", "OK");
            }
            else
            {
                var call = CrossMessaging.Current.PhoneDialer;
                if (call.CanMakePhoneCall)
                {
                    call.MakePhoneCall(telefon.Text);
                }
            }
            
        }

        private async void Sms_btn_Clicked(object sender, EventArgs e)
        {
            if (telefon.Text == "")
            {
                await DisplayAlert("Error", "Palun taida telefoni number", "OK");
            }
            else if (sonum.Text == "")
            {
                await DisplayAlert("Error", "Palun sisesta sonum", "OK");
            }
            else
            {
                var sms = CrossMessaging.Current.SmsMessenger;
                if (sms.CanSendSms)
                {
                    sms.SendSms(telefon.Text, sonum.Text);
                }
            }
        }

        private void Sc_OnChanged(object sender, ToggledEventArgs e)
        {
            if(e.Value)
            {
                fotosection.Title = "Foto:";
                fotosection.Add(ic);
                sc.Text = "Peida";
            }
            else
            {
                fotosection.Title = "";
                fotosection.Remove(ic);
                sc.Text = "Naita veel";
            }
        }
    }
}
