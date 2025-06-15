using Microsoft.AspNetCore.Mvc;

[Area("Cliente")]
public class VehiculosController : Controller
{
    private readonly IVehicleService _vehicleService;

    public VehiculosController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    //Entrar en la vista del buscador
    [HttpGet]
    public IActionResult BuscarVehiculo()
    {
        return View();
    }

    //metodo para traer informacion de la api a la vista
    [HttpPost]
    public async Task<IActionResult> BuscarVehiculo(string marca, string modelo)
    {
        //sentencia para verificar que el usuario entre los datos correctos
        if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo))
        {
            ModelState.AddModelError("", "Debe ingresar marca y modelo.");
            return View();
        }

        //resultado de la busqieda en el servidio de vehiculo
        var resultado = await _vehicleService.GetVehicleData(marca, modelo);
        return View(resultado);
    }



}
