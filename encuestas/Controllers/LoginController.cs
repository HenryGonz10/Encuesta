using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace encuestas.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usr, string pass)
        {
            if (usr.Length == 0)
            { ViewBag.error = "Error en usuario"; return View(); }

            if (pass.Length == 0)
            { ViewBag.error = "Error en usuario"; return View(); }

            try
            {
                int? resultado;
                Models.usuario usuario = null;

                using (Models.DataSet1TableAdapters.QueriesTableAdapter db = new Models.DataSet1TableAdapters.QueriesTableAdapter())
                {
                    db.Login(usr.Trim(), pass.Trim(), out resultado);
                }

                if (resultado!=null)
                {
                    usuario = new Models.usuario();
                    usuario.idUser = (int)resultado;
                    usuario.User = usr;
                    Session["User"] = usuario;
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.error = "Usuario o Contraseña incorrectos";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}