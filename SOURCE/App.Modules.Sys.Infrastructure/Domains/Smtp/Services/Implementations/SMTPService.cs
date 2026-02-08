//using System;
//using System.Collections.Generic;
//using System.Text;

//using System.Net.Mail;
//using App.Base.Infrastructure.Services.Configuration.Implementations;
//using App.Modules.Sys.Infrastructure.Domains.Notifications.Services;

//namespace App.Modules.Sys.Infrastructure.Domains.Notifications.Services.Implementations
//{
//    public class SMTPService: ISmtpService
//    {
 
//    /// <summary>
//    ///     Implementation of the
//    ///     <see cref="ISmtpService" />
//    ///     Infrastructure Service Contract
//    /// </summary>
//    /// <seealso cref="ISmtpService" />
//    public class SmtpService : AppCoreServiceBase, ISmtpService
//    {
//        private SMTPServiceConfiguger SmtpServiceConfiguration { get; }

//        private SmtpClient SmtpClient { get; }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="SmtpService"/> class.
//        /// </summary>
//        /// <param name="smtpServiceConfiguration">The SMTP service configuration.</param>
//        public SmtpService(SMTPServiceConfiguger smtpServiceConfiguration)
//        {
//            SmtpServiceConfiguration = smtpServiceConfiguration;
//            // configure the smtp server
//            SmtpClient = new SmtpClient(smtpServiceConfiguration.Configuration.BaseUri);
//            SmtpClient.Port = smtpServiceConfiguration.Configuration.Port ?? 587;
//            SmtpClient.EnableSsl = true;
//            SmtpClient.Credentials =
//                new System.Net.NetworkCredential(
//                    smtpServiceConfiguration.Configuration.Key,
//                    smtpServiceConfiguration.Configuration.Secret);
//        }

//        /// <inheritdoc/>
//        public void SendMessage(string toAddress, string subject, string body)
//        {
//            var msg = new MailMessage();

//            msg.From = new MailAddress(SmtpServiceConfiguration.Configuration.From);
//            msg.To.Add(toAddress);
//            msg.Subject = subject;
//            msg.IsBodyHtml = true;
//            msg.Body = body;

//            // send the message
//            SmtpClient.Send(msg);
//        }
//    }
//}