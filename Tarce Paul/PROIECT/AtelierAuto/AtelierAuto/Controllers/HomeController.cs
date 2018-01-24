using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AtelierAuto.Repository;
using AtelierAuto.Models;
using AtelierAuto.Commands;

namespace AtelierAuto.Controllers
{
    public class HomeController : Controller
    {

        List<Comanda> comandaRepo = new List<Comanda>();
        List<ComandaMvc> comandaMcv = new List<ComandaMvc>();
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ManipulareComenzi()
        {

            ViewBag.Message = "Ce doriti sa faceti?";

            return View();
        }

        public ActionResult AfisareComandaPlasata()
        {

            ViewBag.Message = "Comanda plasata:";
            return View();
        }

        public ActionResult ManipulareComenziPlasare()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    // return RedirectToAction("AfisareComandaPlasata", "Home");
                    return View("HomePage");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View("HomePage");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


      /*  public ActionResult AdaugaComanda1()
        {
            return View("AdaugaComanda");
        }
        */

        [HttpPost]
        public ActionResult CautaComanda()
        {
            var repo = new ReadRepository();
            comandaRepo = ReadRepository.IncarcaDinListaDeComenzi();
            var eventRepo = ReadRepository.IncarcaDinListaDeEvenimente();
            List<ComandaMvc> comanda = new List<ComandaMvc>();
            foreach (Comanda c in comandaRepo)
            {
               // string idCom = c.iDComanda.ToString();
                /*  var mVC = new ComandaMvc(
                                          x.CIV.ToString(), x.Tip, x.Marca.ToString(), x.Model.ToString(), x.An.ToString(), x.Pret.ToString(), x.Kilometraj.ToString(),
                                          x.Descriere.ToString(), x.Motorizare.ToString(), x.Culoare.ToString(), x.Putere.ToString(),
                                          x.CapacitateCilindrica.ToString()
                                          ); */
                var mvc = new ComandaMvc(c.mecanic.nume, c.mecanic.idMecanic, c.client.nume, c.client.idClient,
                    c.iDComanda, c.masina.Model, c.masina.anFabricatie, c.masina.civ, c.masina.serieSasiu,
                    c.cerereClient);
                                            

                comanda.Add(mvc);
            }

            ViewBag.Model = comanda;
            return View(comanda);
        }

        public ActionResult CautaComanda2()
        {
            return View("CautaComanda");
        }

        public ActionResult PlasareComanda2()
        {
            return View("PlasareComanda");
        }
        [HttpPost]
        public ActionResult PlasareComanda(ComandaMvc mvc)
        {
            //MagistralaComenzi.Instanta.Value.InregistreazaProcesatoareStandard();
            //MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
            //MagistralaEvenimente.Instanta.Value.InchideInregistrarea();
            //Berilna
            var commandPlasareComanda = new CommandPlasareComanda();
            /* Masina m = new Masina(new PlainText(mVC.CIV), mVC.Tip, new PlainText(mVC.Marca.ToString()), new PlainText(mVC.Model.ToString()),
                 new PlainText(mVC.An), new PlainText(mVC.Pret.ToString()), new PlainText(mVC.Kilometraj), new PlainText(mVC.Motorizare),
                 new PlainText(mVC.CapacitateCilindrica), new PlainText(mVC.Putere),
                 new PlainText(mVC.Culoare), new PlainText(mVC.Descriere));
             */

            Comanda c = new Comanda(new Mecanic(mvc.mecanic.nume, mvc.mecanic.idMecanic), new Client(mvc.client.nume, mvc.client.idClient), mvc.iDComanda,
                new Masina(mvc.masina.Model, mvc.masina.anFabricatie, mvc.masina.civ, mvc.masina.serieSasiu), mvc.cerereClient);
            commandPlasareComanda.Comanda = c;

            MagistralaCommands.Instance.Value.Trimite(commandPlasareComanda); // in comanda asta am o masina
            return View("AfisareComenzi");

        }

        public ActionResult AfisareComenzi()
        {
            //var repo = new ReadRepository();
           // comandaRepo = ReadRepository.IncarcaDinListaDeComenzi();
            var comandaRepo = ReadRepository.IncarcaDinListaDeEvenimente();
            // List<ComandaMvc> comanda = new List<ComandaMvc>();
            //  foreach (Comanda c in comandaRepo)
            //{
            // string idCom = c.iDComanda.ToString();
            /*  var mVC = new ComandaMvc(
                                      x.CIV.ToString(), x.Tip, x.Marca.ToString(), x.Model.ToString(), x.An.ToString(), x.Pret.ToString(), x.Kilometraj.ToString(),
                                      x.Descriere.ToString(), x.Motorizare.ToString(), x.Culoare.ToString(), x.Putere.ToString(),
                                      x.CapacitateCilindrica.ToString()
                                      ); */
            /* var mvc = new ComandaMvc(c.mecanic.nume, c.mecanic.idMecanic, c.client.nume, c.client.idClient,
                 c.iDComanda, c.masina.Model, c.masina.anFabricatie, c.masina.civ, c.masina.serieSasiu,
                 c.cerereClient); 
                 */
            comandaMcv = comandaRepo.Select(c => new ComandaMvc{
               mecanic = c.mecanic , client = c.client , iDComanda = c.iDComanda, stareComanda = c.stareComanda,
               masina = c.masina , cerereClient = c.cerereClient , cost = c.cost , evaluareMecanic = c.evaluareMecanic 
                }).ToList();



                //comanda.Add(mvc);
         //   }
            ViewBag.Model = comandaMcv;
            return View(comandaMcv);
        }
    }
}