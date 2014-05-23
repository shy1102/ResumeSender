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
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			copyAssetsToExternalDrive ();
			ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
			SetContentView (Resource.Layout.SelectPanel);
			ListView lw = FindViewById<ListView> (Resource.Id.listView1);
			Button btnDoneSelect = FindViewById<Button> (Resource.Id.btnSend);
			lw.Adapter = ListAdapter;

		}
		private void copyAssetsToExternalDrive(){
			Android.Content.Res.AssetManager manager = Assets;
			String[] files = null;
			try{
				files = manager.List("");
			} catch (IndexOutOfRangeException e){
				//e.Data;
			}
			for (int i = 0; i < files.Length; i++) {
				String fStr = files[i];
				Console.Out.WriteLine (fStr);
				System.IO.Stream input = null;
				System.IO.FileStream adapter = null;
				if (fStr.Contains (".pdf")) {
					try{
						input = manager.Open (files [i]);
						adapter = new System.IO.FileStream ("/sdcard/" + files [i],System.IO.FileMode.Create);
						copyFile(input,adapter);
						adapter.Flush();
						adapter.Close();
						adapter = null;
						break;
					} catch (Exception e){

					}
				}
			}
			items = files;
		}
		private void copyFile(System.IO.Stream i, System.IO.FileStream o ) {
			byte[] Buffer = new byte[1024];
			int read = 0;
			while((read = i.Read(Buffer,0,read)) != -1){
				o.Write (Buffer, 0, read);
			}
		}
	}
}

