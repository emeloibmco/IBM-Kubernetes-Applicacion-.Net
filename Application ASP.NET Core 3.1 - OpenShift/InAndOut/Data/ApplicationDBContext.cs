using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InAndOut.Models;


namespace InAndOut.Data
{
    public class ApplicationDBContext :DbContext
    {
        // Creamos un constructor
        // Importante para usar la base de datos y los datos almcenados en la misma
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) //Pasamos opciones a la clase base
        {

        }

        // Crear una nueva DB de Items, Expenses y ExpenseTypes
        public DbSet<Item> Items { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
    }
}
