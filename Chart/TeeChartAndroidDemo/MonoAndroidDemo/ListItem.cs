using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MonoAndroidDemo
{
	[Activity (Label = "ListItem")]			
	public class ListItem : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

      SetContentView(Resource.Layout.ListItem);

      //var seriesAdapter = new SeriesAdapter(this); 
      //var listView = FindViewById<ListView>(Resource.Id.seriesListView);
      //listView.Adapter = seriesAdapter;
		}
	}
}

