using System;
using System.Net.Mail;
using System.Net;
using Experimental.System.Messaging;

namespace CommonLayer.Model
{
    public class MSMQ
    {
        MessageQueue message = new MessageQueue();


        public void SendMessage(string token)
        {

            message.Path = @".\Private$\Token";
            try
            {
                if (!MessageQueue.Exists(message.Path))
                {
                    MessageQueue.Create(message.Path);
                }
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                message.ReceiveCompleted += MessageQueue_RecieveCompleted;
                message.Send(token);
                message.BeginReceive();
                message.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MessageQueue_RecieveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = message.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "Google Keep Reset Password Link";
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, //for gmail
                    EnableSsl = true,
                    Credentials = new NetworkCredential("yashu19052@gmail.com", "zwjctqcbkqcrpytj"),
                };

                SMTP.Send("yashu19052@gmail.com", "yashaswinivoruganti@gmail.com", subject, token);
                message.BeginReceive();


            }
            catch
            {
                throw;
            }
        }
    }
}
