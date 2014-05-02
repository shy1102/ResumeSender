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
using Android;

namespace newTest
{
	[Activity (Label = "Choose_Resume")]			
	public class Choose_Resume : Activity
	{
		string[] items;
		Intent Main_view;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.SelectPanel, items);
			SetContentView (Resource.Layout.SelectPanel);

			Button btnDoneSelect = FindViewById<Button> (Resource.Id.btnDoneSelect);
			if (Main_view == null) {
				Main_view = new Intent(this, typeof(MainActivity));
			}
			btnDoneSelect.Click += delegate {
				StartActivity (Main_view);
			}; 
		}
		protected void OnListItemClick(ListView l, View v, int position, long id)
		{
			var t = items[position];
			Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
		}

	}
}

