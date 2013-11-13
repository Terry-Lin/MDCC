using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MonoAndroidDemo
{
  [Activity(Label = "MonoAndroidDemo", MainLauncher = true, Icon = "@drawable/icon")]
  public class Activity1 : Activity  
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);				

      var seriesAdapter = new SeriesAdapter(this);
      var seriesListView = FindViewById<ListView>(Resource.Id.seriesListView);
      seriesListView.DrawingCacheEnabled = false;
      seriesListView.Adapter = seriesAdapter;
      //seriesListView.ItemClick += new EventHandler<ItemEventArgs>(seriesListView_ItemClick); //4.0.6
      seriesListView.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(seriesListView_ItemClick);  //4.1.1
    }

    //void seriesListView_ItemClick(object sender, ItemEventArgs e) //4.0.6
    void seriesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)  //4.1.1
    {      
      Intent myIntent = new Intent(e.View.Context, typeof(ChartView));
		  myIntent.PutExtra("SeriesPosition", e.Position);
      StartActivityForResult(myIntent, 1);      
    }

  }
}

