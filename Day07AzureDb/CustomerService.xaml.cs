using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day07AzureDb
{
    /// <summary>
    /// Interaction logic for CustomerService.xaml
    /// </summary>
    public partial class CustomerService : Page
    {
        public CustomerService()
        {
            InitializeComponent();
        }

        private void BtnAttatchFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                LblFileName.Content = openFileDialog.FileName;
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string smtpAddress = "smtp.google.com";
            int smtpPort = 587;
            bool smtpSSL = true;
            string smtpId = "";
            string smtpPw = "";
            bool isBodyHtml = false;

            using (SmtpClient smtp = new SmtpClient(smtpAddress, smtpPort))
            {
                smtp.TargetName = smtpAddress;
                smtp.EnableSsl = smtpSSL;
                smtp.Credentials = new NetworkCredential(smtpId, smtpPw);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                using (MailMessage mail = new MailMessage())
                {
                    try
                    {
                        mail.From = new MailAddress(TbxEmail.Text);
                        mail.To.Add("jqrohan@gmail.com");
                        mail.SubjectEncoding = Encoding.UTF8;
                        mail.Subject = TbxSubject.Text;

                        mail.BodyEncoding = Encoding.UTF8;
                        mail.Body = $" Name: {TbxName.Text}\n Phone: {TbxPhone.Text}\n" +
                            StringFromRichTextBox(RTBBody);

                        mail.IsBodyHtml = isBodyHtml;

                        System.Net.Mail.Attachment attachment;
                        attachment = new System.Net.Mail.Attachment(LblFileName.Content.ToString());
                        mail.Attachments.Add(attachment);

                        smtp.Send(mail);
                        MessageBox.Show("Sending mail ok", "Mail sending successfull", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex) when (ex is SmtpException | ex is ArgumentException) 
                    {
                        MessageBox.Show("Fail sending mail: " + ex.Message, "Mail sending fail", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
    }
}
