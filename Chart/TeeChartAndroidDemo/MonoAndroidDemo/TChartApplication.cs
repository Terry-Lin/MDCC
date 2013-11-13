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

namespace MonoAndroidDemo
{
  [Application]
  public class TChartApplication : Application
  {
    public TChart Chart { get; set; }

    public TChartApplication(IntPtr handle, JniHandleOwnership transfer)
      : base(handle, transfer)
    {
    }

    public override void OnCreate()
    {
      base.OnCreate();

      Chart = new TChart(this);
    }
  }
}