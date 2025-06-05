using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Transmision { get; set; }
        public int CapacidadPersonas { get; set; }
        public string CapacidadMaletero { get; set; }
        //public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public byte[] Foto { get; set; }

    }
}
