using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
            string smtpAddress = "smtp.gmail.com";
            int smtpPort = 587;
            bool smtpSSL = true;
            string smtpId = "jqrohan@gmail.com";
            string smtpPw = "lrvkcxokqtbqlrph";
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
                        mail.Body = $"- Name: {TbxName.Text}\n- Phone: {TbxPhone.Text}\n\n" +
                            StringFromRichTextBox(RTBBody) + 
                            $"- Employee: {LoginPage.CurrentUser.users.Full_name}\n" +
                            $"- Email: {LoginPage.CurrentUser.users.Email}";

                        mail.IsBodyHtml = isBodyHtml;

                        if (LblFileName.Content != null)
                        {
                            System.Net.Mail.Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(LblFileName.Content.ToString());
                            mail.Attachments.Add(attachment);
                        }

                        smtp.Send(mail);
                        MessageBox.Show("Sending mail ok", "Mail sending successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetInput();

                    }
                    catch (Exception ex) when (ex is SmtpException | ex is ArgumentException | 
                                                ex is ArgumentNullException | ex is FormatException) 
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
            return textRange.Text + "\n\n\n\n";
        }

        private void TbxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ResetInput()
        {
            TbxEmail.Text = string.Empty;
            TbxName.Text = string.Empty;
            TbxPhone.Text = string.Empty;
            TbxSubject.Text = string.Empty;
            LblFileName.Content = string.Empty;
            RTBBody.Document.Blocks.Clear();
        }

    }
}
