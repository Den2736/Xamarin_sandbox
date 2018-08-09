using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin_quick_start
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void callButton_Clicked(object sender, EventArgs e)
        {
            var phoneNumber = phoneNumberEntry.Text;
            var alert = await this.DisplayAlert("Confirm dial", $"Would you like to call {phoneNumber}", "Yes", "No");
            if (alert)
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    App.CallHistory.Add(phoneNumber);
                    callHistoryButton.IsEnabled = true;
                    dialer.Dial(phoneNumber);
                }
            }
        }

        private async void callHistoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new СallHistoryPage());
        }
    }
}