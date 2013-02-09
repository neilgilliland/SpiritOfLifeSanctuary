using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace SpiritOfLifeSanctuary.Controllers
{
    public class EmailController : Controller
    {
        [HttpPost]
        public ActionResult ContactUs( int contactId, string name, string email, string text)
        {
            try
            {
                string connection = ConfigurationManager.AppSettings["SQLSERVER_CONNECTION_STRING"];     

                //save mail to database//
                using (SqlConnection db = new SqlConnection(connection))
                {
                    SqlCommand cmd = db.CreateCommand();
                    cmd.CommandText = "insert into Emails (name,email,text,contactId) values (@name,@email,@text,@contactId)";
                    cmd.Parameters.AddWithValue( "@name", name );
                    cmd.Parameters.AddWithValue( "@email", email );
                    cmd.Parameters.AddWithValue( "@text", text );
                    cmd.Parameters.AddWithValue( "@contactId", contactId );
                    db.Open();
                    cmd.ExecuteNonQuery();
                }

                var toAddress = new MailAddress("info@spiritoflifesanctuary.org.uk", "Spirit of Life Sanctuary");
                var punterAddress = new MailAddress(email, name);

                string subject = "Contact from spiritoflifesanctuary.org.uk";
                string body = text;
        
                string host = ConfigurationManager.AppSettings["MAILGUN_SMTP_SERVER"];     
                int port = Convert.ToInt32( ConfigurationManager.AppSettings["MAILGUN_SMTP_PORT"] );
                string uid = ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"];
                string pwd = ConfigurationManager.AppSettings["MAILGUN_SMTP_PASSWORD"];
            
                var fromAddress = new MailAddress("thespiritoflifesanctuary@gmail.com", "spiritoflifesanctuary.org.uk");

                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(uid, pwd)
                };
                var message = new MailMessage(fromAddress, toAddress){ Subject=subject, Body=body };
                message.ReplyToList.Add( punterAddress );
                smtp.Send(message);

                ViewBag.Result = "Message successfully delivered";
                return View("ConfirmContact");
            }
            catch(Exception ex)
            {
                ViewBag.Result = ex.StackTrace;
                return View("ConfirmContact");
            }
        }
    }
}
