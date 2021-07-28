using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Expense
    {
        // Primary Key es un valor identificable para una entrada en tu base de datos
        // Aumenta de for automática a medida que se agrega una nueva entrada a la tabla
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; } //Identificador
        [DisplayName("Expense")]
        [Required(ErrorMessage = "Debe completar el campo Gasto")] //Importante para evitar campos vacíos
        public string ExpenseName { get; set; } //Gasto
        [Required(ErrorMessage = "Debe completar el campo Valor ($)")] //Importante para evitar campos vacíos
        [Range(1, int.MaxValue, ErrorMessage = "El Valor ($) debe ser mayor a 0")] //Cantidad debe ser positiva y > a 0
        public int Amount { get; set; } //Cantidad


        /*---------------------------------------------------------------------------------------*/
        //Relación con variable de modelo ExpenseType
        public int ExpenseTypeId { get; set; }
        //Conexión entre las 2 tablas
        //Foreign Key
        [ForeignKey("ExpenseTypeId")]
        [DisplayName("Expense Type")]
        public virtual ExpenseType ExpenseType { get; set; }
    }
}
