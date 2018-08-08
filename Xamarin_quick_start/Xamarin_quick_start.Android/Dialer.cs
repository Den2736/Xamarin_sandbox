using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin_quick_start.Droid;

[assembly: Dependency(typeof(PhoneDialer))]
namespace Xamarin_quick_start.Droid
{
    public class Dialer: IDialer
    {
    }
}