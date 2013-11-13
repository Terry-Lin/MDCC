
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Steema.TeeChart;

namespace TChartFeatures
{
	public class TypesTableController : UITableViewController
	{
		public TChart chart;
		public ChartViewController chartController;

		public TypesTableController () : base (UITableViewStyle.Grouped)
		{
			this.chartController =new ChartViewController();
			this.chart = chartController.chart;
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register the TableView's data source
			TableView.Source = new TypesTableSource ();
			TableView.Delegate = new SeriesTypesDelegate(this,this.chartController);
		}
	}
}

