using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Collections.Generic;
using RentCar.Models; 

public class VehicleService : IVehicleService
{

    //estamos declarando una variable privada para llamr el HTTPCLIENT
    private readonly HttpClient _httpClient;


    //ese es el constructor que me crea una nueva instancia del httpclient y obtener el api desde el appsetting
    public VehicleService(IConfiguration config)
    {
        _httpClient = new HttpClient();
        string apiKey = config["ApiNinjas:ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
    }

    //aqui estamos definiendo el metodo para hacer la solicitudes
    public async Task<List<VehicleInfo>> GetVehicleData(string make, string model)
    {
        var url = $"https://api.api-ninjas.com/v1/cars?make={make}&model={model}";
        var response = await _httpClient.GetAsync(url); //este es la llamada a la pai despues que el cliente le de click a bsucar
        response.EnsureSuccessStatusCode(); //manejo de errores
        var json = await response.Content.ReadAsStringAsync(); 


        //converit jquery a objecto
        return JsonSerializer.Deserialize<List<VehicleInfo>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

}
