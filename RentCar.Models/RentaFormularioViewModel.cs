using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class RentaFormularioViewModel
    {
        public string CiudadCodigo { get; set; }
        public DateTime FechaRecogida { get; set; }
        public TimeSpan HoraRecogida { get; set; }
        public DateTime FechaEntrega { get; set; }
        public TimeSpan HoraEntrega { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Tripulantes { get; set; }
        public string EmailDestino { get; set; }
    }

}
