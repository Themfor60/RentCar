using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class RentaFormularioViewModel
    {
        [Key]
        public int IdRenta { get; set; }

        [Required]
        public string CiudadCodigo { get; set; }
        public DateTime FechaRecogida { get; set; }
        public TimeSpan HoraRecogida { get; set; }
        public DateTime FechaEntrega { get; set; }
        public TimeSpan HoraEntrega { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Tripulantes { get; set; }

        [Required]
        public string EmailDestino { get; set; }

        
        [ForeignKey("ReservaRequest")]
        public int ReservaRequestId { get; set; }
        public virtual ReservaRequest ReservaRequest { get; set; }
    }

}
