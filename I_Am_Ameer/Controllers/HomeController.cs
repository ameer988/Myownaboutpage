using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System;
using System.IO;

namespace I_Am_Ameer.Controllers
{
    public class HomeController : Controller
    {
        public static object AppSettings { get; private set; }

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                if(IsSend())
                {
                    string datetime = DateTime.Now.ToString();
                    //string IpAddress = GetIpAddress();
                    string platform = Request.Browser.Type;
                    string body = "Platform: " + platform + "\n Datetime: " + datetime;
                    Logs(body, "Welcome_Website");
                }
            }
            catch(Exception ex)
            {
                if (IsSend())
                {
                    Logs(ex.Message, "ErrorLog");
                }
            }
           
            return View();
            
        }
        public JsonResult Email(string tomail, string body, string subject)
        {
            bool b = false;
            try
            {
                if (IsSend())
                {
                    string platform = Request.Browser.Type;
                    b = SendingMail(tomail, body, subject);
                }
                return Json(b);
            }
            catch(Exception ex)
            {
                if (IsSend())
                {
                    Logs(ex.Message, "ErrorLog");
                }
            }
            return Json(b);
        }

        private static bool SendingMail(string tomail, string body, string subject)
        {
            try
            {
                string sendEmail = ConfigurationManager.AppSettings["sendemails"].ToString();
                string smtpHost = ConfigurationManager.AppSettings["smtpHost"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(sendEmail);
                msg.To.Add(tomail);
                msg.CC.Add("ameershaik5124@gmail.com");
                msg.Subject =subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpHost;
                System.Net.NetworkCredential networkcred = new System.Net.NetworkCredential();
                networkcred.UserName = sendEmail;
                networkcred.Password = password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkcred;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(msg);
                return true;
            }
            catch
            {
                throw;
            }
        }

        //private static string GetIpAddress()
        //{
        //    string clientIp = "";
        //    try
        //    {
        //        clientIp = clientIp = System.Web.HttpContext.Current.Request.UserHostAddress;
        //        var xffHeader = System.Web.HttpContext.Current.Request.Headers["X-Forwarded-For"];
        //        if (!string.IsNullOrEmpty(xffHeader))
        //        {
        //            clientIp = xffHeader.Split(',')[0];
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return clientIp;
        //}


        private static bool Logs(string body, string subject)
        {
            try
            {

                string sendEmail = ConfigurationManager.AppSettings["sendemails"].ToString();
                string smtpHost = ConfigurationManager.AppSettings["smtpHost"].ToString();
                string password = ConfigurationManager.AppSettings["password"].ToString();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(sendEmail);
                msg.To.Add("ameershaik5124@gmail.com");
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpHost;
                System.Net.NetworkCredential networkcred = new System.Net.NetworkCredential();
                networkcred.UserName = sendEmail;
                networkcred.Password = password;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkcred;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(msg);
                return true;
            }
            catch
            {
                throw;
            }
        }

        private bool IsSend()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["IsSend"].ToString());
        }
      
    }
}