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
                dialer?.Dial(phoneNumber);
            }
        }
    }
}
