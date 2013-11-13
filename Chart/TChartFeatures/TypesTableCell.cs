
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TChartFeatures
{
	public class TypesTableCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("TypesTableCell");
		
		public TypesTableCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			//TextLabel.Text = "TextLabel";
		}
	}
}

