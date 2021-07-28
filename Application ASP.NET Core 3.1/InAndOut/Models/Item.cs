using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace InAndOut.Models
{
    public class Item
    {
        // Primary Key es un valor identificable para una entrada en tu base de datos
        // Aumenta de for automática a medida que se agrega una nueva entrada a la tabla
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; } //Identificador
        [Required] //Importante para evitar campos vacíos
        public string Borrower { get; set; } //Persona que pide prestado 
        [Required] //Importante para evitar campos vacíos
        public string Lender { get; set; } //Persona que presta

        //Realizamos una anotación para pasar un nombre mejor en la vista al usuario, no ItemName junto
        [DisplayName("Item Name")] //Asi en Interfaz de Usuario vamos a ver Item Name no ItemName
        [Required] //Importante para evitar campos vacíos
        public string ItemName { get; set; } //Articulo que se presta
    }
}
