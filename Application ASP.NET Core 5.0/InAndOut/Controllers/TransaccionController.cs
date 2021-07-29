using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly ApplicationDBContext _db;

        // Creamos un constructor
        public TransaccionController(ApplicationDBContext db)
        {
            _db = db;
        }


        //---------------------------------------------------------------------------------------------
        public IActionResult Index()
        {
            // Traemos los Items de la base de datos
            IEnumerable<Transaccion> objList = _db.Transacciones;
            return View(objList);
        }


        //---------------------------------------------------------------------------------------------
        // Una función que nos permite crear nuevos items - obtener los datos
        // GET Create
        public IActionResult Create()
        {
            return View();
        }


        //---------------------------------------------------------------------------------------------
        // POST Create
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token
        //Entry for database
        public IActionResult Create(Transaccion Obj)
        {
            _db.Transacciones.Add(Obj); //Agregamos un nuevo item
            _db.SaveChanges(); //Agregamos un nuevo item en Microsoft SQL Server
            return RedirectToAction("Index"); //Especificamos la acción del controlador
        }


        //-------------------------------------------------------------------------------------------
        // GET Delete
        public IActionResult Delete(int? id) //Un ID opcional puede ser pasado o no 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var Obj = _db.Transacciones.Find(id);
            if (Obj == null)
            {
                return NotFound();
            }
            return View(Obj);
        }


        //-----------------------------------------------------------------------------------------
        // POST Delete
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token
        public IActionResult DeletePost(int? id) //Un ID opcional puede ser pasado o no 
        {
            var Obj = _db.Transacciones.Find(id);
            if (Obj == null)
            {
                return NotFound();
            }

            _db.Transacciones.Remove(Obj); //Eliminamos el objeto
            _db.SaveChanges(); //Guardamos cambios del  objeto eliminado en Microsoft SQL Server
            return RedirectToAction("Index"); //Especificamos la acción del controlador            
        }


        //-------------------------------------------------------------------------------------------
        // GET Update
        public IActionResult Update(int? id) //Un ID opcional puede ser pasado o no 
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var Obj = _db.Transacciones.Find(id);
            if (Obj == null)
            {
                return NotFound();
            }
            return View(Obj);
        }


        //-------------------------------------------------------------------------------------------
        // POST Update 
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token
        //Entry for database
        public IActionResult Update(Transaccion Obj)
        {
            //Nos permite hacer una validación del lado del servidor
            if (ModelState.IsValid)
            {
                _db.Transacciones.Update(Obj); //Editamos un objeto
                _db.SaveChanges(); //Guardamos cambios de modificación en Microsoft SQL Server
                return RedirectToAction("Index"); //Especificamos la acción del controlador
            }
            return View(Obj);
        }

    }
}
