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
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Transmision { get; set; }
        public int CapacidadPersonas { get; set; }
        public string? CapacidadMaletero { get; set; }
        public decimal Precio { get; set; } 
<<<<<<< HEAD
        public byte[]? Foto { get; set; }
=======
        public byte[]? Foto { get; set; }  
>>>>>>> 1091f47b26bf7de552f4813b80f66c8dfa2210e6

    }
}
