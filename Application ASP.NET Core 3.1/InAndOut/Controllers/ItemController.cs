using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;


namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDBContext _db;

        // Creamos un constructor
        public ItemController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Traemos los Items de la base de datos
            IEnumerable<Item> objList = _db.Items;
            return View(objList);
        }

        // Una función que nos permite crear nuevos items - obtener los datos
        // GET Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Create
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token
        //Entry for database
        public IActionResult Create(Item Obj)
        {
            if(ModelState.IsValid)
            {
                _db.Items.Add(Obj); //Agregamos un nuevo item
                _db.SaveChanges(); //Agregamos un nuevo item en Microsoft SQL Server
                return RedirectToAction("Index"); //Especificamos la acción del controlador
            }
            return View(Obj);
            
        }
    }
}

