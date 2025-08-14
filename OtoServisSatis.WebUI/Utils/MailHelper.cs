using System.Net.Mail;
using System.Net;
using OtoServisSatis.Entities;
using Azure.Core;

namespace OtoServisSatis.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Customer customer, Vehicle vehicle, HttpRequest request)
        {
            var baseUrl = $"{request.Scheme}://{request.Host}";
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("autoserviceapp8@gmail.com", "xnvt ejwg ammy jxom");
            smtpClient.EnableSsl = true;

            // Site sahibine gelen mail
            MailMessage adminMessage = new MailMessage();
            adminMessage.From = new MailAddress("autoserviceapp8@gmail.com");
            adminMessage.To.Add("autoserviceapp8@gmail.com");
            adminMessage.Subject = "Siteden mesaj geldi";
            adminMessage.Body = $@"
              <b>Mail Bilgileri</b> <hr/>
              <b>Ad Soyad:</b> {customer.Name} {customer.Surname} <br/>
              <b>İlgilendiği Araç Id:</b> {customer.VehicleId} <br/>
              <b>Email:</b> {customer.Email} <br/>
              <b>Telefon:</b> {customer.Phone} <br/>
              <b>Notlar:</b> {customer.Notes}";
            adminMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync(adminMessage);

            // Müşteriye gönderilecek onay maili
            string imageUrl = $"{baseUrl}/Img/Vehicles/{vehicle?.Photo1}";
            MailMessage customerMessage = new MailMessage();
            customerMessage.From = new MailAddress("autoserviceapp8@gmail.com");
            customerMessage.To.Add(customer.Email);
            customerMessage.Subject = "Talebiniz Alındı";
            customerMessage.Body = $@"
              <p>Sayın {customer.Name} {customer.Surname},</p>
              <p>Talebiniz işleme alınmıştır. En kısa sürede sizinle iletişime geçeceğiz.</p>
              <img src='{imageUrl}' alt='Araç Resmi' width='400' />";
              customerMessage.IsBodyHtml = true;
              await smtpClient.SendMailAsync(customerMessage);
        }
    }
}