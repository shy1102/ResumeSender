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
		ListView current;
		Button send;
		List<String> pdfs = new List<String> ();
		static List<String> selectedPdfs = new List<String> ();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			copyAssetsToExternalDrive ();
			getOnlyPdfs ();
			ArrayAdapter ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, pdfs);
			SetContentView (Resource.Layout.SelectPanel);
			ListView lw = FindViewById<ListView> (Resource.Id.listView1);
			lw.ChoiceMode = ChoiceMode.Multiple;
			current = lw;
			lw.TextFilterEnabled = true;
			send = FindViewById<Button> (Resource.Id.buttonSend);
			lw.Adapter = ListAdapter;
			Console.Out.WriteLine(ListAdapter.AreAllItemsEnabled().ToString());
			lw.ItemClick += OnListItemClick;
			lw.ItemSelected += OnListItemClick2;
			send.Enabled = true;
			send.Click += OnSendButtonClicked;
		}
		public static List<string> getPdfList(){
			return selectedPdfs;
		}
		private void getOnlyPdfs(){
			foreach (string s in items) {
				if (s.Contains (".pdf")) {
					pdfs.Add (s);
				}
			}
		}
		protected void OnSendButtonClicked(object sender, System.EventArgs e){
			long[] List = current.GetCheckItemIds ();
			foreach (long l in List) {
				selectedPdfs.Add (pdfs.ElementAt ((int)l));
			}
			Intent receivers = new Intent(this, typeof(receivers));
			StartActivity (receivers);
		}
		protected void OnListItemClick2(object sender, Android.Widget.AdapterView.ItemSelectedEventArgs e){
			var listview = sender as ListView;
			if (listview.IsItemChecked (e.Position)) {
				current.SetItemChecked (e.Position, false);
			} else {
				current.SetItemChecked (e.Position, true);
			}
			if (current.GetCheckItemIds ().Length > 0) {
				send.Enabled = true;
			} else {
				send.Enabled = false;
			}
			listview.RefreshDrawableState ();
		} 
		protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e){
			var listview = sender as ListView;
			if (current.IsItemChecked (e.Position)) {
				current.SetItemChecked (e.Position, false);
			} else {
				current.SetItemChecked ((int)e.Id, true);
				current.SetItemChecked (e.Position, true);


				current.SetItemChecked (e.Position, true);
			}
			current.RefreshDrawableState ();
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
				System.IO.Stream input = null;
				System.IO.FileStream adapter = null;
				if (fStr.Contains ("pdf")) {
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

