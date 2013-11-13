// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Barcodeios
{
	[Register ("BarcodeiosViewController")]
	partial class BarcodeiosViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnGenerateBarCode { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnScan { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView img_code { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblResult { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txInput { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnScan != null) {
				btnScan.Dispose ();
				btnScan = null;
			}

			if (img_code != null) {
				img_code.Dispose ();
				img_code = null;
			}

			if (lblResult != null) {
				lblResult.Dispose ();
				lblResult = null;
			}

			if (txInput != null) {
				txInput.Dispose ();
				txInput = null;
			}

			if (btnGenerateBarCode != null) {
				btnGenerateBarCode.Dispose ();
				btnGenerateBarCode = null;
			}
		}
	}
}
