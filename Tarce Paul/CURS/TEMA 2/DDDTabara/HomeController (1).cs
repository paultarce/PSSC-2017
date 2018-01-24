using Proiect_DPO.Comenzi;
using Proiect_DPO.Evenimente;
using Proiect_DPO.Model.Produs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Interfata.App_Start;
using Proiect_DPO.Model;
using Interfata.Models;
using Proiect_DPO;


namespace Interfata.Controllers
{
    public class HomeController : Controller
    {

        List<Produs> produsRepo = new List<Produs>();
        List<ProdusMVC> produsMVC = new List<ProdusMVC>();

        public HomeController()
        {

        }
        public ActionResult Index()
        {
             // return View("Login");
            return View("MainPage");
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

        public ActionResult CautaProduse()
        {
            return View("CautaProdus");
        }

        [HttpPost]
        public ActionResult CautaProdus(ProdusMVC mVC)
        {
            ProdusMVC mVC2 = null;
            var repo = new ReadRepository();
            produsRepo = repo.IncarcaListaDeEvenimente();
            List<ProdusMVC> produs = new List<ProdusMVC>();
            foreach (Produs i in produsRepo)
            {
                if (i.CodBare.ToString().Equals(mVC.CodBare)) //(i.Denumire.ToString().Equals(mVC.Denumire))  //(i.CodBare.ToString().Equals(mVC.CodBare))
                {
                    mVC2 = new ProdusMVC(i.CodBare.ToString(), i.Denumire.ToString(), i.Marca.ToString(), i.Tip, i.Stoc.ToString(), i.Pret.ToString(),
                   i.Descriere.ToString(), i.Garantie.ToString(), i.Furnizor.ToString(), i.Stare);

                    produs.Add(mVC2);
                }
            }

            ViewBag.Model = produs;
            return View("AfisareProduse", produs);
        }

        public ActionResult AdaugaProduse()
        {
            return View("AdaugaProdus");
        }

        [HttpPost]
        public ActionResult AdaugaProdus(ProdusMVC mVC)
        {
            var comandaAdaugaProdus = new ComandaAdaugaProdus();

            Produs p = new Produs(new PlainText(mVC.CodBare.ToString()), new PlainText(mVC.Denumire.ToString()), new PlainText(mVC.Marca.ToString()), mVC.Tip, new PlainText(mVC.Stoc.ToString()),
            new PlainText(mVC.Pret.ToString()), new PlainText(mVC.Descriere.ToString()), new PlainText(mVC.Garantie.ToString()), new PlainText(mVC.Furnizor.ToString()), mVC.Stare);

            comandaAdaugaProdus.Produs1 = p;
            MagistralaComenzi.Instanta.Value.Trimite(comandaAdaugaProdus);
            var r = new Receive();      //////rabbit receive
            var mesaj = r.PrimesteEveiment();
            return View("MainPage");

        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {

                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return View("MainPage");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("MainPage", "Home");
        }

        public ActionResult Delete(string CodBare)
        {
            var comandaStergereProdus = new ComandaStergeProdus();
            comandaStergereProdus.CodBare = CodBare.ToString();
            produsMVC.Remove(produsMVC.Find(_ => _.CodBare.Equals(CodBare)));
            MagistralaComenzi.Instanta.Value.Trimite(comandaStergereProdus);

            return View("MainPage");
        }

        public ActionResult StergereProdus()
        {
            return View("StergereProduse");
        } 
        public ActionResult StergereProduse(string CodBare)
        {

            var comandaStergereProdus = new ComandaStergeProdus();
            comandaStergereProdus.CodBare = CodBare.ToString();
            var repo = new ReadRepository();
            produsRepo = repo.IncarcaListaDeEvenimente();
            List<ProdusMVC> produs = new List<ProdusMVC>();
            foreach (Produs i in produsRepo)
            {
                if (i.CodBare.ToString().Equals(CodBare))
                {
                    produsMVC.Remove(produsMVC.Find(_ => _.CodBare.Equals(CodBare)));
                    MagistralaComenzi.Instanta.Value.Trimite(comandaStergereProdus);
                }
            }

            ViewBag.Model = produs;
            return View("MainPage");
        }


        public ActionResult AfisareProduse()
        {
            var repo = new ReadRepository();
            produsRepo = repo.IncarcaListaDeEvenimente();

            produsMVC = produsRepo.Select(i => new ProdusMVC
            {
                CodBare = i.CodBare.ToString(),
                Denumire = i.Denumire.ToString(),
                Marca = i.Marca.ToString(),
                Tip = i.Tip,
                Stoc = i.Stoc.ToString(),
                Pret = i.Pret.ToString(),
                Descriere = i.Descriere.ToString(),
                Garantie = i.Garantie.ToString(),
                Furnizor = i.Furnizor.ToString(),
                Stare = i.Stare
            }).ToList();
            ViewBag.Model = produsMVC;
            return View(produsMVC);
        }

    }
}