using sistema_AAPM_Senai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistema_AAPM_Senai.Controllers
{
    public class EventosController : Controller
    {
        // GET: Eventos
        public ActionResult Eventos()
        {
            return View();
        }
        public ActionResult listaEventos()
        {
            return View();
        }

        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var viewModel = new Eventos();
            var events = new List<Eventos>();
            start = DateTime.Today.AddDays(-14);
            end = DateTime.Today.AddDays(-11);

            for (var i = 1; i <= 5; i++)
            {
                events.Add(new Eventos()
                {
                    Id_evento = i,
                    Nome_evento = "Event " + i,
                    Comeco = start.ToString(),
                    Final = end.ToString(),
                });

                start = start.AddDays(7);
                end = end.AddDays(7);
            }


            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }

}
