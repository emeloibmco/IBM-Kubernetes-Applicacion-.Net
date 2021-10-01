using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDBContext _db;

        // Creamos un constructor
        public ExpenseController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Traemos los Items de la base de datos
            IEnumerable<Expense> objList = _db.Expenses;

            //Nos permite pasar los objetos --> Ej: Name
            foreach (var obj in objList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }

            return View(objList);
        }

        //------------------------------------------------------------------------------------
        // Una función que nos permite crear nuevos items - obtener los datos
        // GET Create
        public IActionResult Create()
        {
            //USAMOS VIEWBAG
            //Opción para seleccionar datos - con ExpenseTypes
            /*IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }) ;

            //Pasamos los datos del Controller a View
            ViewBag.TypeDropDown = TypeDropDown; 

            // Normal - lo que teniamos antes de relacionar con ExpenseTypes
            return View(); */


            //--------------------------------------------------------------------------------
            //USAMOS VIEWMODELS
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            // Normal - lo que teniamos antes de relacionar con ExpenseTypes
            return View(expenseVM);

        }

        //------------------------------------------------------------------------------------
        // POST Create
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token

        // Con ViewBag
        //Entry for database
        /*public IActionResult Create(Expense Obj)
        {
            //Nos permite hacer una validación del lado del servidor
            if (ModelState.IsValid)
            {
                //Obj.ExpenseTypeId = 6; // Lo asignamos para relacionar ExpenseType - Asignamos 6 porque previamente ya hemos creado tipos de gatos y tiene este id.
                _db.Expenses.Add(Obj); //Agregamos un nuevo item
                _db.SaveChanges(); //Agregamos un nuevo item en Microsoft SQL Server
                return RedirectToAction("Index"); //Especificamos la acción del controlador
            }
            return View(Obj);
        }*/

        //------------------------------------------------------------------------------------
        // Con ViewModel
        //Entry for database
        public IActionResult Create(ExpenseVM Obj)
        {
            //Nos permite hacer una validación del lado del servidor
            if (ModelState.IsValid)
            {
                //Obj.ExpenseTypeId = 6; // Lo asignamos para relacionar ExpenseType - Asignamos 6 porque previamente ya hemos creado tipos de gatos y tiene este id.
                _db.Expenses.Add(Obj.Expense); //Agregamos un nuevo item
                _db.SaveChanges(); //Agregamos un nuevo item en Microsoft SQL Server
                return RedirectToAction("Index"); //Especificamos la acción del controlador
            }
            return View(Obj);
        }

        //------------------------------------------------------------------------------------
        // GET Delete
        public IActionResult Delete(int? id) //Un ID opcional puede ser pasado o no 
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var Obj = _db.Expenses.Find(id);
            if (Obj == null)
            {
                return NotFound();
            }
            return View(Obj); 
        }

        //------------------------------------------------------------------------------------
        // POST Delete
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token
        public IActionResult DeletePost(int? id) //Un ID opcional puede ser pasado o no 
        {
            var Obj = _db.Expenses.Find(id);
            if(Obj == null)
            {
                return NotFound();
            }

            _db.Expenses.Remove(Obj); //Eliminamos el objeto
            _db.SaveChanges(); //Guardamos cambios del  objeto eliminado en Microsoft SQL Server
            return RedirectToAction("Index"); //Especificamos la acción del controlador            
        }

        //------------------------------------------------------------------------------------
        // GET Update
        public IActionResult Update(int? id) //Un ID opcional puede ser pasado o no 
        {
            //USAMOS VIEWBAG
            /*if (id == null || id == 0)
            {
                return NotFound();
            }

            var Obj = _db.Expenses.Find(id);
            if (Obj == null)
            {
                return NotFound();
            }
            return View(Obj);*/


            //------------------------------------------------------------------------------
            //USAMOS VIEWMODELS
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            expenseVM.Expense = _db.Expenses.Find(id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);

        }

        //------------------------------------------------------------------------------------
        // POST Update 
        [HttpPost] //Debemos especificar el estado
        [ValidateAntiForgeryToken] //Verificamos si todavía tenemos un token

        //Con ViewBag
        //Entry for database
        /*public IActionResult Update(Expense Obj)
        {
            //Nos permite hacer una validación del lado del servidor
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(Obj); //Editamos un objeto
                _db.SaveChanges(); //Guardamos cambios de modificación en Microsoft SQL Server
                return RedirectToAction("Index"); //Especificamos la acción del controlador
            }
            return View(Obj);
        }*/


        //------------------------------------------------------------------------------------
        // Con ViewModel
        public IActionResult Update(ExpenseVM Obj)
        {
            //Nos permite hacer una validación del lado del servidor
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(Obj.Expense); //Editamos un objeto
                _db.SaveChanges(); //Guardamos cambios de modificación en Microsoft SQL Server
                return RedirectToAction("Index"); //Especificamos la acción del controlador
            }
            return View(Obj);
        }

    }
}
