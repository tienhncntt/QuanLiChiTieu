using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace QuanLiChiTieu.Droid
{
    [Activity(Label = "IMoney", Icon = "@drawable/AppIcon", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.SmallestScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(typeof(MainActivity));
            Thread.Sleep(1000);
            Finish();
            OverridePendingTransition(0,0);
        }
    }
}