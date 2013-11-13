TeeChart is a charting component library designed to provide you a wide range
of preprepared and configurable charts and charting options for inclusion in 
your project.

TeeChart provides support for iOS and Android, in either case the syntax for using 
the component is virtually identical beyond the specific code required to add the 
chart to the screen view that varies between environments and the interaction syntax for
touch and gestures. The example below shows TeeChart being used in an iOS project.

The TChart control inherits from the UIView so it can be added as subview in other views, 
while other controls, too, can be added to the chart.

	using Steema.TeeChart;
	using System.Drawing;
	...

	public override void ViewDidLoad ()
	{
		...
		 
		var chart = new TChart ();
		chart.Frame = new RectangleF (0, 0, 320, 460);
		var pieStyle = new Steema.TeeChart.Styles.Pie ();
		chart.Series.Add (pieStyle);
		 
		// Loading data to the Pie series, we can use Random data just to test
		pieStyle.FillSampleValues (4);
		 
		// or add specific values for the Series
		foreach (int datum in new [] {10, 20, 30, 40})
		pieStyle.Add (datum);
		 
		// Customising our Chart and Series.
		// Setting Chart to 2D and hiding legend
		pieStyle.Aspect.View3D = false;
		pieStyle.Legend.Visible = false;
		 
		// Setting Pie series as Circular, and marks visible
		pieStyle.Circled = true;
		pieStyle.Marks.Visible = true;
		 
		View.AddSubView (chart);
	}