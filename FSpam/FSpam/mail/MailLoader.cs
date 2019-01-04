using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace FSpam.mail
{
    class MailLoader
    {
        public void readMail(string username, string password)
        {
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();
                string response;
                string title;
                string content;
               
                XmlDocument doc = new XmlDocument();

                objClient.Credentials = new System.Net.NetworkCredential(username,password);
                response = Encoding.UTF8.GetString(
                           objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(
                     @"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

                doc.LoadXml(response);
                MessageBox.Show(response);

                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    title = node.SelectSingleNode("title").InnerText;
                    content = node.SelectSingleNode("summary").InnerText;
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show("Check your network connection");
            }
        }
    }
}
