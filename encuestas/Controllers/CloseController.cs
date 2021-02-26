using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace encuestas.Controllers
{
    public class CloseController : Controller
    {
        public ActionResult Logoff()
        {
            try
            {
                Session["User"] = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
