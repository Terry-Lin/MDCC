using System.Text;
using Android.App;
using Android.Content;
using Android.Util;
using Android.OS;
using Android.Support.V4.App;
using System;


[assembly: Permission(Name = "pushdemo.android.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "pushdemo.android.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace PushDemo.Android
{
	[Service]
	public class MyIntentService : IntentService
	{
		static PowerManager.WakeLock sWakeLock;
		static object LOCK = new object();

		public static void RunIntentInService(Context context, Intent intent)
		{
			lock (LOCK)
			{
				if (sWakeLock == null)
				{
					// This is called from BroadcastReceiver, there is no init.
					var pm = PowerManager.FromContext(context);
					sWakeLock = pm.NewWakeLock(
						WakeLockFlags.Partial, "My WakeLock Tag");
				}
			}

			sWakeLock.Acquire();
			intent.SetClass(context, typeof(MyIntentService));
			context.StartService(intent);
		}

		protected override void OnHandleIntent(Intent intent)
		{
			try {
				Context context = this.ApplicationContext;
				string action = intent.Action;

				if (action.Equals ("com.google.android.c2dm.intent.REGISTRATION")) {
					MainActivity.appRegID = intent.GetStringExtra ("registration_id");
					Console.WriteLine(intent.GetStringExtra ("registration_id"));
				} else if (action.Equals ("com.google.android.c2dm.intent.RECEIVE")) {

//					PendingIntent resultPendingIntent = PendingIntent.GetActivity(context,0,new Intent(context,typeof(MainActivity)),0);
//					// Build the notification
//					NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
//						.SetAutoCancel(true)
//							.SetContentIntent(resultPendingIntent)
//							.SetContentTitle("新通知來了") 
//							.SetSmallIcon(Resource.Drawable.xsicon) 
//							.SetContentText(intent.GetStringExtra("alert"));
//
//					NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
//					notificationManager.Notify(0, builder.Build());
					createNotification("您有新的信息",intent.GetStringExtra("alert"));
				}
			} finally {
				lock (LOCK) {
					//Sanity check for null as this is a public method
					if (sWakeLock != null)
						sWakeLock.Release ();
				}
			}
		}
   

		void createNotification(string title, string desc)
		{
			//Create notification
			var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

			//Create an intent to show ui
			var uiIntent = new Intent(this, typeof(MainActivity));

			//Create the notification
			var notification = new Notification(Resource.Drawable.xsicon, title);

			notification.Defaults = NotificationDefaults.Sound;
			//Auto cancel will remove the notification once the user touches it
			notification.Flags = NotificationFlags.AutoCancel;

			//Set the notification info
			//we use the pending intent, passing our ui intent over which will get called
			//when the notification is tapped.
			notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, 0));

			//Show the notification
			notificationManager.Notify(1, notification);
		}


	}

	//用來接收廣播事件的BroadcastReceiver
	[BroadcastReceiver(Permission= "com.google.android.c2dm.permission.SEND")]
	[IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] {"PushDemo.Android" })]
	[IntentFilter(new string[] { "com.google.android.c2dm.intent.REGISTRATION" }, Categories = new string[] {"PushDemo.Android" })]
	[IntentFilter(new string[] { "com.google.android.gcm.intent.RETRY" }, Categories = new string[] { "PushDemo.Android"})]
	public class MyGCMBroadcastReceiver : BroadcastReceiver
	{
		const string TAG = "PushHandlerBroadcastReceiver";
		public override void OnReceive(Context context, Intent intent)
		{
			MyIntentService.RunIntentInService(context, intent);
			SetResult(Result.Ok, null, null);
		}
	}
}

