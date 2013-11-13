using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WebService.Android
{
	//http://WCFConvert.azurewebsites.net/wcftempservice.svc?wsdl
	//http://soapconvert.azurewebsites.net/tempconvert.asmx?wsdl
	//http://restfulconvert.azurewebsites.net/tempconvert.svc/tof/50

	[Activity (Label = "WebService.Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//載入頁面
			SetContentView (Resource.Layout.Main);

			//取得控制項實體

			var rbSoap = FindViewById< RadioButton>(Resource.Id.rbSoap);
			var rbWCF = FindViewById< RadioButton>(Resource.Id.rbWCF);
			var rbRest = FindViewById< RadioButton>(Resource.Id.rbRest);
			var rbCtoF = FindViewById< RadioButton>(Resource.Id.rbCtoF);
			var rbFtoC = FindViewById< RadioButton>(Resource.Id.rbFtoC);
			var txtResult = FindViewById< TextView>(Resource.Id.txtResult);
			var txtInput = FindViewById<EditText> (Resource.Id.txtInput);
			var btnConvert = FindViewById<Button> (Resource.Id.btnConvert);
			txtResult.Text = "";

			//轉換按鈕的事件處理
			btnConvert.Click += (sender, e) => {
				//使用SOAP
				if (rbSoap.Checked) {
					var converter1 = new soapconvert.TempConvert();
					if (rbCtoF.Checked)
					{ txtResult.Text = converter1.CelsiusToFahrenheit(txtInput.Text); }
					else
					{ txtResult.Text = converter1.FahrenheitToCelsius(txtInput.Text); }

				}

				//使用WCF
				if (rbWCF.Checked) {
					var converter2 = new wcfconvert.WCFTempService();
					if (rbCtoF.Checked)
					{ txtResult.Text = converter2.CelsiusToFahrenheit(txtInput.Text); }
					else
					{ txtResult.Text = converter2.FahrenheitToCelsius(txtInput.Text); }
				}

				//使用REST with JSON
				if (rbRest.Checked) {
					string uri;
					if (rbCtoF.Checked){
						uri = @"http://restfulconvert.azurewebsites.net/TempConvert.svc/ToF/" + txtInput.Text; }
					else{
						uri = @"http://restfulconvert.azurewebsites.net/TempConvert.svc/ToC/" + txtInput.Text; }

					//透過HttpWebRequest呼叫Restful service
					var request = HttpWebRequest.Create(uri);
					request.Method = "GET";
					request.ContentType = "application/json";

					//取得Response 並透過JObject處理Json
					using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
					{
						using (StreamReader reader = new StreamReader(response.GetResponseStream()))
						{
							var content = JObject.Parse(reader.ReadToEnd());
							if (rbCtoF.Checked)
							{
								txtResult.Text = content["Fahrenheit"].ToString();
							}
							else
							{
								txtResult.Text = content["Celsius"].ToString();
							}
						}
					}

				}

			};
		}
	}
}


