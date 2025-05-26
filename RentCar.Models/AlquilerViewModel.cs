using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class AlquilerViewModel
    {
        public string VehicleModel { get; set; }
        public decimal PrecioPorDia { get; set; }
        public int Dias { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
    }
}
