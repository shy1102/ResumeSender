using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace newTest
{
	[Activity (Label = "newTest", MainLauncher = true)]
	public class MainActivity : Activity
	{
		Intent Choose_Resume_view;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			View selectPanel = FindViewById<View> (Resource.Layout.SelectPanel);
			// Get our button from the layout resource,
			// and attach an event to it
			Button btnChooseResume = FindViewById<Button> (Resource.Id.btnChooseResume);
			Button btnSend = FindViewById<Button> (Resource.Id.btnSend);
			if (Choose_Resume_view == null) {
				Choose_Resume_view = new Intent(this, typeof(Choose_Resume));
			}
			btnChooseResume.Click += (sender, e) => {
				Choose_Resume_view = new Intent(this, typeof(Choose_Resume));
				StartActivity (Choose_Resume_view);
			};
		}
		protected override void OnPause(){
			base.OnPause();

		}
	}
}


