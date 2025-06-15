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

        
>>>>>>> 702075e0e8f84faae4213dd633f4c3bbd795753a

    }
}
