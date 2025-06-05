using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class ReservaRequest
    {
        [Key]
        public int IdReserva { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string EmailCliente { get; set; }
        public string Telefono { get; set; }
        public string Vehiculo { get; set; }
        public decimal Precio { get; set; }

        
        public virtual RentaFormularioViewModel RentaFormulario { get; set; }
    }

}
