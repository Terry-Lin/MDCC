using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing;

namespace BarcodeDroid
{
	[Activity (Label = "BarcodeDroid", MainLauncher = true)]
	public class MainActivity : Activity
	{


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//载入页面
			SetContentView (Resource.Layout.Main);

			//将页面上的物件实体化
			var btnScan = FindViewById<Button> (Resource.Id.btnScan);
			var txtResult = FindViewById<TextView> (Resource.Id.txtResult);
			var txInput = FindViewById<TextView> (Resource.Id.txInput);
			var imgBarcode = FindViewById<ImageView> (Resource.Id.imgBarcode);
			var btnGenerateBarcode = FindViewById<Button> (Resource.Id.btnGenerateBarcode);

			imgBarcode.Visibility = ViewStates.Invisible;

			//扫描条码
			btnScan.Click += async (sender, e) => {
				var Scanner = new ZXing.Mobile.MobileBarcodeScanner(this);
				Scanner.UseCustomOverlay = false;
				Scanner.TopText = "请保持摄像头与条码至少六英寸的距离";
				Scanner.BottomText = "请等候，扫描将自动完成";
				var result = await Scanner.Scan();

				if (result !=null)
					txtResult.Text = result.Text;
			};

			btnGenerateBarcode.Click += (sender, e) => {
				var writer = new BarcodeWriter
				{
					Format = BarcodeFormat.QR_CODE

				};
				var bitmap = writer.Write(txInput.Text);
				imgBarcode.SetImageBitmap(bitmap);
				imgBarcode.Visibility=ViewStates.Visible;
			};

			}
		}
	}



