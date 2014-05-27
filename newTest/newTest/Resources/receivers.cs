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
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace newTest
{
	[Activity (Label = "receivers")]			
	public class receivers : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.receivers);

			List<string> a = Choose_Resume.getPdfList ();
			Console.Out.WriteLine(a.ToArray().Length+ "ASDASDADASD");
			sendEmail2 ();

			// Create your application here
		}
		protected void sendEmail2(){
			Intent i = new Intent(Intent.ActionSend);
			i.SetType("message/rfc822");
			//i.SetType("text/plain");
			i.PutExtra(Intent.ExtraEmail  , new String[]{"androidresumebot@gmail.com"});
			i.PutExtra(Intent.ExtraSubject, "subject of email");
			i.PutExtra(Intent.ExtraText   , "body of email");
			try {
				StartActivity(Intent.CreateChooser(i, "Send mail..."));
			} catch (ActivityNotFoundException e){}
		}
		protected void sendEmail(){
			try
			{
				SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");

				// set smtp-client with basicAuthentication
				mySmtpClient.UseDefaultCredentials = false;
				System.Net.NetworkCredential basicAuthenticationInfo = new
					System.Net.NetworkCredential("androidresumebot@gmail.com", "resumebot");
				mySmtpClient.Credentials = basicAuthenticationInfo;

				// add from,to mailaddresses
				MailAddress from = new MailAddress("androidresumebot@gmail.com", "me");
				MailAddress to = new MailAddress("androidresumebot@gmail.com", "to");
				MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

				// add ReplyTo
				MailAddress replyto = new MailAddress("reply@example.com");
				myMail.ReplyTo = replyto;

				// set subject and encoding
				myMail.Subject = "Test message";
				myMail.SubjectEncoding = System.Text.Encoding.UTF8;

				// set body-message and encoding
				myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
				myMail.BodyEncoding = System.Text.Encoding.UTF8;
				// text or html
				myMail.IsBodyHtml = true;

				mySmtpClient.Send(myMail);
			}

			catch (SmtpException ex)
			{
				throw new ApplicationException
				("SmtpException has occured: " + ex.Message);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

