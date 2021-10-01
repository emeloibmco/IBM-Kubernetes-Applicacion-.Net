using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View(); //Aqui vamos a mostrar lo que sale en la Vista appointment/index
            /*string todaysDate = DateTime.Now.ToShortDateString(); //Obtener la fecha
            return Ok(todaysDate); //Retorna un resultado OK*/
        }

        public IActionResult Details(int id)
        {
            return Ok("Tienes que ingresar el ID = " + id);
        }
    }
}
