using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class ExpenseType
    {
        [Key]
        public int Id { get; set; } //Identificador
        [Required(ErrorMessage = "Debe completar el campo Tipo de Gasto")]
        public string Name { get; set; } //Tipo de gasto
    }
}
