using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtelierAuto.Models;
using AtelierAuto.Models.Generic;

namespace AtelierAuto.Controllers
{
    public class PlasareComandaControler : Controller
    {

       

        // GET: PlasareComandaControler
        //[HttpPost]
        public ActionResult PlasareComanda()
        {
            return RedirectToAction("AfisareComandaPlasata","User");
        }

        [HttpPost]
        public ActionResult PlasareComandaControlerr()
        {

            //View
           

            var masina = new Masina(new PlainText("WV Passat"), 2005, new CIV("EI309MNN"), new SerieSasiu("ALABALAPR"));
            var mecanic = new Mecanic(new PlainText("Nelutu"), 1);
            var client = new Client(new PlainText("Orlando"), 1);

            var comandaa = new Comanda(mecanic, client, new IDComanada(1), masina, "reparatie turbina");
             return View();
        }
    }
}