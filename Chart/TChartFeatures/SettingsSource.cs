
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace TChartFeatures
{
	public class SettingsSource : UITableViewSource
	{
		private List<string> _items;
		private string _cellId;
		
		public SettingsSource ()
		{
			_cellId = "cellid";
			_items = new List<string>()
			{
				"Aspect",
				"Themes",
				"Color Palettes",
				"Legend",
				"Tools",
				"Functions",
			};
		}
		
		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			return _items.Count;
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return "Settings";
		}
		
		public override string TitleForFooter (UITableView tableView, int section)
		{
			return "";
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(_cellId); 
			
			if (cell == null )
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, _cellId);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			
			cell.TextLabel.Text = _items[indexPath.Row];
			
			return cell; 
		}
	}
}

