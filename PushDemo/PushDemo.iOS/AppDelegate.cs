using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace PushDemo.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		PushDemo_iOSViewController viewController;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			UIApplication
				.SharedApplication
					.RegisterForRemoteNotificationTypes (UIRemoteNotificationType.Alert
					                                     | UIRemoteNotificationType.Badge
					                                     | UIRemoteNotificationType.Sound);

			viewController = new PushDemo_iOSViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();


			
			return true;
		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			var RegID = deviceToken.Description.Replace ("<", "").Replace (">", "").Replace (" ", "");
			Console.WriteLine ("Register ID :" + RegID);
		}

		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			var aps = userInfo.ObjectForKey (new NSString ("aps")) as NSDictionary;
			var alert = aps.ObjectForKey (new NSString ("alert")).ToString ();
			var av = new UIAlertView ("推播通知", alert, null, "確定", null);
			av.Show ();
		}
	}
}

