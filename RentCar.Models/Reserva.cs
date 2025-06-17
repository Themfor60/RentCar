using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class ReservaRequest
    {
        [Key]
        public int IdReserva { get; set; } 

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debes ingresar un correo electrónico ")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string EmailCliente { get; set; }

        public string Telefono { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La cédula debe tener exactamente 11 dígitos")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "La ciudad o código es obligatorio")]
        public string CiudadCodigo { get; set; }

        


        [DataType(DataType.Date)]
        public DateTime FechaRecogida { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HoraRecogida { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan HoraEntrega { get; set; }

        
        public DateTime FechaHoraRecogidaCompleta { get; set; }
        public DateTime FechaHoraEntregaCompleta { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; } 

        [Range(1, 10, ErrorMessage = "Debes ingresar entre 1 y 10 tripulantes")]
        public int Tripulantes { get; set; }

        [Required(ErrorMessage = "El ID del vehículo es obligatorio.")] 
        public int? VehiculoId { get; set; } 

        [ForeignKey("VehiculoId")] 
        public virtual Vehiculo Vehiculo { get; set; }
    }
}