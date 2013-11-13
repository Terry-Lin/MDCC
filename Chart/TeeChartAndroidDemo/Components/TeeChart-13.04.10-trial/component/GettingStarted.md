There are two TeeChart components for use with Xamarin, TeeChart for iOS and TeeChart for Android. Both use the TChart
class as the foundation stone, `Steema.TeeChart.TChart`.

TChart is a data visualisation control that creates a graphical representation of data. It can handle high amounts of data, 
from a database if required. It supports many different series (display) types; standard types like Line, Area, Bar, Pie, Gantt; 
professional 2D or 3D series types, like Surface, TriSurface, Contour and indicators like Circular Gauges, Horizontal Indicators, 
and more..

This document provides a short guide to getting started.

Xamarin.iOS
-----------

<b>Including TeeChart in an iOS application</b> 

In this getting started example we'll create a TChart and add it to a UIView.

	using Steema.TeeChart;
	using System.Drawing;
	...

	//In the class define a new TChart control :
	TChart chart1; 
	Steema.TeeChart.Styles.Bar bar;

	public override void ViewDidLoad ()
	{
		...

		var chart1 = new TChart();
	  
		// Setting the Chart dimensions
		chart1.Frame = new RectangleF(0,190,320,270);

		// Setting automatic Zoom and Scroll to manual
		chart1.Aspect.ZoomScrollStyle=Steema.TeeChart.Drawing.Aspect.ZoomScrollStyles.Manual;

		// Adding Series to the Chart. More Series could be added to make a multi-Series Chart.
		var bar = new Steema.TeeChart.Styles.Bar();

		// Some settings for the Bar Series type
		bar.BarStyle = Steema.TeeChart.Styles.BarStyles.Arrow;
		bar.BarWidthPercent = 200;
		bar.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
		bar.ColorEach = true;

		// Adding the Bar Series to the Chart 
		chart1.Series.Add(bar);

		bar.Add(3, "Pears", UIColor.Red.CGColor);       
		bar.Add(4, "Apples", UIColor.Blue.CGColor);       
		bar.Add(2, "Oranges", UIColor.Green.CGColor);     

		// Other Chart settings may be modified to suit
		// specific requirements. Some examples:
		chart1.Aspect.View3D = false;
		chart1.Legend.Visible = false;
		chart1.Axes.Bottom.Title.Text = "Customer Invoices";
		chart1.Axes.Left.AxisPen.Width = 1;
		chart1.Axes.Left.Increment = 40;
		chart1.Axes.Bottom.AxisPen.Width = 1;
		chart1.Header.Text = "TeeChart NET for iOS";
		chart1.Header.Font.Color = UIColor.Black.CGColor;
		chart1.Panel.MarginTop = 0;
		chart1.Walls.Back.Visible = false;

		//And finally add the Chart to the Main View as a subview:

		View.AddSubview(chart1);
	}


Xamarin.Android
---------------

<b>Including TeeChart in an Android application</b> 

TeeChart inherits from a FrameLayout. This means that it contains all the characteristics of a FrameLayout and a View, from which 
FrameLayout is derived. 

<b>Coded data input</b> 

For programmed input of data you will need to write some code. The code sample below shows you the steps necessary to build a chart with coded 
input. When adding a Series at runtime it will not have any values by default applied to it. Only after the Series is added can we use the 
appropriate methods for each case and add each point to manually populate the Series. 
  
	protected override void OnCreate(Bundle bundle)
	{
		base.OnCreate(bundle);

		Steema.TeeChart.TChart tChart1 = new Steema.TeeChart.TChart(this);        
		Steema.TeeChart.Styles.Bar bar1 = new Steema.TeeChart.Styles.Bar();       
		tChart1.Series.Add(bar1);        
		bar1.Add(3, "Pears", Color.Red);       
		bar1.Add(4, "Apples", Color.Blue);       
		bar1.Add(2, "Oranges", Color.Green);        
		Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(tChart1.Chart);       
		theme.Apply();        
		SetContentView(tChart1);   
	}     

Run the project and three new bars will appear on your Chart. That's it!! There's no more to it. 


## Other Resources

* [Steema Support forums](http://support.steema.com)
* [Product Description Resources](http://www.steema.com/teechart/mobile)
