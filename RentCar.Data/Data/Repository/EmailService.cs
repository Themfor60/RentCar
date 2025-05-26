using MailKit.Net.Smtp;
using MimeKit;
using RentCar.Data.Data.Repository.IRepository;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    public async Task SendRentalConfirmationEmailAsync(string toEmail, string userName, string vehicleModel, int rentalDays, decimal totalPrice, string userAddress)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("RentCar", "themfor60@gmail.com")); 
        message.To.Add(new MailboxAddress(userName, toEmail));
        message.Subject = "Confirmación de alquiler de vehículo";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <h2>Gracias por alquilar con RentCar, {userName}!</h2>
                <p><strong>Vehículo:</strong> {vehicleModel}</p>
                <p><strong>Días de alquiler:</strong> {rentalDays}</p>
                <p><strong>Precio total:</strong> ${totalPrice}</p>
                <p><strong>Dirección:</strong> {userAddress}</p>
                <p>¡Esperamos que disfrutes tu experiencia!</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync("themfor60@gmail.com", "abcdefg...");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
