using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using Uri = Android.Net.Uri;

using Xamarin.Forms;
using Xamarin_quick_start.Droid;

[assembly: Dependency(typeof(Dialer))]
namespace Xamarin_quick_start.Droid
{
    public class Dialer : IDialer
    {
        public bool Dial(string number)
        {
            var context = MainActivity.Instance;
            if (context == null)
            {
                return false;
            }

            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Uri.Parse($"tel:{number}"));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        private static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;
            var list = packageManager.QueryIntentServices(intent, 0).Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
            {
                return true;
            }

            var manager = TelephonyManager.FromContext(context);
            return manager.PhoneType != PhoneType.None;
        }
    }
}