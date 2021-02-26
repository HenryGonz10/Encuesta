using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace encuestas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Models.usuario usuario = (Models.usuario)HttpContext.Session["User"];
            Models.Dinamico.ModeloCompuesto encuesta = new Models.Dinamico.ModeloCompuesto();
            if(usuario!=null)
                encuesta.data = GetEntrevistas(usuario.idUser);
            return View(encuesta);
        }

        public List<Models.Dinamico.entrevista> GetEntrevistas(int? id)
        {
            if (id == null)
                return null;

            try
            {
                using (Models.encuestaEntities db = new Models.encuestaEntities())
                {
                    var lst = (from e in db.encuesta
                               where e.idusuario == id && e.Estado == true
                               select new Models.Dinamico.entrevista
                               {
                                   id = e.idencuesta,
                                   titulo = e.Titulo,
                                   desc = e.Descripcion,
                                   fecha = e.Fecha
                               }).OrderByDescending(x=>x.id).ToList();

                    return lst;
                }
            }
            catch 
            {
                return null;
            }
        }
    }
}