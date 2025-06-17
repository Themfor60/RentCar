using Microsoft.Extensions.Configuration;
using RentCar.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class WhatsAppService
{
    private readonly string token;
    private readonly string phoneNumberId;
    private readonly string numeroRentCar;

    public WhatsAppService(IConfiguration config)
    {
        token = config["WhatsApp:Token"];
        phoneNumberId = config["WhatsApp:PhoneNumberId"];
        numeroRentCar = config["WhatsApp:NumeroRentCar"];
    }

    public async Task EnviarNotificacionReservaAsync(ReservaRequest request)
    {
        string mensaje = $@"🚗 NUEVA RESERVA RECIBIDA

            Nombre: {request.Nombre}
            Email: {request.EmailCliente}
            Teléfono: {request.Telefono}
            Vehículo:
            Recogida: {request.FechaRecogida:yyyy-MM-dd} a las {request.HoraRecogida}
            Entrega: {request.FechaEntrega:yyyy-MM-dd} a las {request.HoraEntrega}
            Dirección: {request.CiudadCodigo}

            ✅ Procede a gestionar esta reserva.";

        var payload = new
        {
            messaging_product = "whatsapp",
            to = numeroRentCar,
            type = "text",
            text = new { body = mensaje }
        };

        var url = $"https://graph.facebook.com/v18.0/{phoneNumberId}/messages";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        
    }
}
