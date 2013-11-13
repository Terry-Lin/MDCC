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

using Steema.TeeChart;
using Android;

namespace MonoAndroidDemo
{
  [Activity(Label = "Chart settings")]
  public class ChartEditor : Activity
  {
    private TChart tChart;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      SetContentView(Resource.Layout.Editor);

      tChart = ((TChartApplication)Application).Chart;

      CheckBox cb3D = FindViewById<CheckBox>(Resource.Id.cb3D);
      cb3D.Checked = tChart.Aspect.View3D;
      cb3D.Click += new EventHandler(cb3D_Click);

      Spinner spinnerZoom = FindViewById<Spinner>(Resource.Id.spinnerZoomStyle);

      spinnerZoom.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerZoom_ItemSelected); //4.1.1
      //spinnerZoom.ItemSelected += new EventHandler<ItemEventArgs>(spinnerZoom_ItemSelected); //4.0.6
      var adapterZoom = ArrayAdapter.CreateFromResource(
          this, Resource.Array.zoom_styles, Android.Resource.Layout.SimpleSpinnerItem);

      adapterZoom.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
      spinnerZoom.Adapter = adapterZoom;
      spinnerZoom.SetSelection((tChart.Zoom.Style == ZoomStyles.FullChart) ? 0 : 1, false);
    }

    //void spinnerZoom_ItemSelected(object sender, ItemEventArgs e) //4.0.6
    void spinnerZoom_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e) //4.1.1
    {
      Spinner spinner = (Spinner)sender;

      switch (e.Position)
      {
        //case 0:
        //  tChart.Zoom.Style = ZoomStyles.FullChart;
        //  break;
        case 1:
          tChart.Zoom.Style = ZoomStyles.InChart;
          break;
        default:
          tChart.Zoom.Style = ZoomStyles.FullChart;
          break;
      }
    }

    void cb3D_Click(object sender, EventArgs e)
    {
      tChart.Aspect.View3D = (sender as CheckBox).Checked;
    }
  }
}