using System;
using PushSharp;
using PushSharp.Apple;
using System.IO;
using PushSharp.Android;
using System.Collections.Generic;

namespace PushDemo.Server
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//APNS
            var push = new PushBroker();
            var cert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                        "<specify your .p12 file here>"));
            
            string iphoneToken = "<PUT YOUR DEVICE TOKEN HERE>";

            var settings = new ApplePushChannelSettings(cert, "pushdemo");

			push.RegisterAppleService(settings);

            var Notification = new AppleNotification()
				.ForDeviceToken(iphoneToken)
					.WithAlert("欢迎来到中国移动者开发大会!")
                    .WithSound("sound.caf")
                    .WithBadge(4);

			push.QueueNotification(Notification);
            Console.WriteLine("Waiting for Queue to Finish...");



            Console.WriteLine("Queue Finished, press return to exit...");
            Console.ReadLine();	
		
			//GCM


			
			var RegID_emulator = "<PUT YUOR REGISTER ID HERE>";

			push.RegisterGcmService(new GcmPushChannelSettings("<PUT YOUR GOOGLE API SERVER KEY HERE>"));

			push.QueueNotification (new GcmNotification ().ForDeviceRegistrationId (RegID_emulator)
			                       .WithJson("{\"alert\":\"欢迎来到中国移动者开发大会!\",\"badge\":7,\"sound\":\"sound.caf\"}"));


			push.StopAllServices();

           
		}
	}
}
