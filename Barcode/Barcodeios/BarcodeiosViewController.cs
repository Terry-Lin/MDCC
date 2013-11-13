using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ZXing.Mobile;
using ZXing;
using ZXing.Common;


namespace Barcodeios
{
	public partial class BarcodeiosViewController : UIViewController
	{
		public BarcodeiosViewController () : base ("BarcodeiosViewController", null)
		{
		}

	
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			lblResult.Text = "";

			txInput.Text = "http://thinkpower.info/xamarin/cn/";

			//扫描条码
			btnScan.TouchUpInside += async (sender, e) => {
				var scanner = new ZXing.Mobile.MobileBarcodeScanner();
				var result = await scanner.Scan();

				if (result != null)
					this.lblResult.Text = result.Text;
			};

			//产生条码
			btnGenerateBarCode.TouchUpInside += (sender, e) => {
				txInput.ResignFirstResponder();
				var writer = new BarcodeWriter
				{
					Format = BarcodeFormat.QR_CODE
						
				};
				var bitmap = writer.Write(txInput.Text);
				this.img_code.Image = bitmap;
			};

		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			txInput.ResignFirstResponder ();
		}

	}
}

