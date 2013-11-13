// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace WebService
{
	[Register ("WebServiceViewController")]
	partial class WebServiceViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnConvert { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblResult { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl SegService { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISegmentedControl SegType { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtTemp { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SegService != null) {
				SegService.Dispose ();
				SegService = null;
			}

			if (txtTemp != null) {
				txtTemp.Dispose ();
				txtTemp = null;
			}

			if (btnConvert != null) {
				btnConvert.Dispose ();
				btnConvert = null;
			}

			if (lblResult != null) {
				lblResult.Dispose ();
				lblResult = null;
			}

			if (SegType != null) {
				SegType.Dispose ();
				SegType = null;
			}
		}
	}
}
