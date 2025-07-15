using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class MensajeDeContactoModels
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
       public string Nombre {  get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string Correo { get; set; }


        [Required(ErrorMessage = "El asunto es obligatorio")]
        public string Asunto { get; set; }


        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string Mensaje { get; set; }
    }
}
