using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PushDemo.Android
{
	[Activity (Label = "PushDemo.Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		public static string appRegID="";
		TextView txtRegID,txtMsg;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnGetRegID = FindViewById<Button> (Resource.Id.btnGetRegID);

			string senders = "374823337115";
			Intent intent = new Intent("com.google.android.c2dm.intent.REGISTER");
			intent.SetPackage("com.google.android.gsf");
			intent.PutExtra("app", PendingIntent.GetBroadcast(this, 0, new Intent(), 0));
			intent.PutExtra("sender", senders);
			this.StartService(intent);

			txtRegID = FindViewById<TextView> (Resource.Id.txtRegID);


			btnGetRegID.Click += delegate {

				txtRegID.Text = appRegID;
			};
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			var msg = Intent.GetStringExtra ("alert");
			if (msg != null)
				txtMsg.Text = msg;
		}
	}
}


