using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebService
{
	//http://WCFConvert.azurewebsites.net/wcftempservice.svc?wsdl
	//http://soapconvert.azurewebsites.net/tempconvert.asmx?wsdl
	//http://restfulconvert.azurewebsites.net/tempconvert.svc/tof/50

	public partial class WebServiceViewController : UIViewController
	{
		public WebServiceViewController () : base ("WebServiceViewController", null)
		{
		}

	

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			lblResult.Text = "";


			//添加转换钮的事件处理
			this.btnConvert.TouchUpInside += btnConvert_Click;

		}


		//转换钮的事件
		void btnConvert_Click (object sender, EventArgs e)
		{
			this.txtTemp.ResignFirstResponder();

			switch (SegService.SelectedSegment)
			{
				//使用SOAP
				case 0:
				var converter1 = new SOAPConvert.TempConvert();
				if (SegType.SelectedSegment == 0)
				{ this.lblResult.Text = converter1.CelsiusToFahrenheit(txtTemp.Text); }
				else
				{ this.lblResult.Text = converter1.FahrenheitToCelsius(txtTemp.Text); }
				break;

				//使用WCF
				case 1:
				var converter2 = new WCFConvert.WCFTempService();
				if (SegType.SelectedSegment == 0)
				{ this.lblResult.Text = converter2.CelsiusToFahrenheit(txtTemp.Text); }
				else
				{ this.lblResult.Text = converter2.FahrenheitToCelsius(txtTemp.Text); }
				break;


				//使用REST with JSON
				case 2:
				string uri;
				if (SegType.SelectedSegment == 0){
					uri = @"http://restfulconvert.azurewebsites.net/TempConvert.svc/ToF/" + txtTemp.Text; }
				else{
					uri = @"http://restfulconvert.azurewebsites.net/TempConvert.svc/ToC/" + txtTemp.Text; }

				var request = HttpWebRequest.Create(uri);
				request.Method = "GET";
				request.ContentType = "application/json";

				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
				{
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						var content = JObject.Parse(reader.ReadToEnd());
						if (SegType.SelectedSegment == 0)
						{
							this.lblResult.Text = content["Fahrenheit"].ToString();
						}
						else
						{
							this.lblResult.Text = content["Celsius"].ToString();
						}
					}
				}
				break;
			}
		}
	
		//键盘收合事件
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			txtTemp.ResignFirstResponder ();
		}
	}
}

