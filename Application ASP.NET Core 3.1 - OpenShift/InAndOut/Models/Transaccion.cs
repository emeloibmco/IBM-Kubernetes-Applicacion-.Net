using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Transaccion
    {
        [Key]
        //Creamos las variables que necesitamos para crear la pestaña Transacciones
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Ciudad")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage ="Debe completar el campo Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Cédula")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor de Cédula debe ser mayor a 0")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Fecha y Hora")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Valor ($)")]
        [Range(1, int.MaxValue, ErrorMessage = "El Valor ($) debe ser mayor a 0")]
        public int Valor { get; set; }

        [Required(ErrorMessage = "Debe completar el campo Tipo de Transacción")]
        public string Tipo { get; set; }
    }
}
