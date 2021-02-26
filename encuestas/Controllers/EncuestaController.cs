using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace encuestas.Controllers
{
    public class EncuestaController : Controller
    {
        // GET: Encuesta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encuesta/Create
        [HttpPost]
        public ActionResult Create(Models.Dinamico.entrevista obj)
        {
            using (Models.encuestaEntities db = new Models.encuestaEntities())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            Models.usuario usuario = (Models.usuario)HttpContext.Session["User"];

                            if (usuario == null)
                                return View();

                            var _encuesta = new Models.encuesta();
                            _encuesta.idusuario = usuario.idUser;
                            _encuesta.Titulo = obj.titulo;
                            _encuesta.Descripcion = obj.desc;
                            _encuesta.Estado = true;
                            db.encuesta.Add(_encuesta);

                            foreach (var i in obj.detalles)
                            {
                                var aux = new Models.detalle_encuesta();
                                aux.idencuesta = _encuesta.idencuesta;
                                aux.Nombre = i.nombre;
                                aux.Titulo = i.titulo;
                                aux.Requerido = i.requerido;
                                aux.Tipo = i.tipo;
                                db.detalle_encuesta.Add(aux);
                            }

                            db.SaveChanges();
                            dbContext.Commit();

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            dbContext.Rollback();
                            return View(obj);
                        }
                    }
                    catch
                    {
                        dbContext.Rollback();
                        return View(obj);
                    }
                }
            }
        }

        // GET: Encuesta/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var obj = new Models.Dinamico.entrevista();
                using (Models.encuestaEntities db = new Models.encuestaEntities())
                {
                    var aux = db.encuesta.Find(id);
                    obj.id = aux.idencuesta;
                    obj.titulo = aux.Titulo;
                    obj.desc = aux.Descripcion;
                    obj.fecha = aux.Fecha;
                    return View(obj);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Encuesta/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Dinamico.entrevista obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Models.encuestaEntities db = new Models.encuestaEntities())
                    {
                        var aux = db.encuesta.Find(obj.id);
                        aux.Titulo = obj.titulo;
                        aux.Descripcion = obj.desc;
                        db.Entry(aux).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(obj);
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                using (Models.encuestaEntities db = new Models.encuestaEntities())
                {
                    var aux = db.encuesta.Find(id);
                    ViewBag.Titulo = aux.Titulo;
                    ViewBag.ID = id;
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }


        // POST: Encuesta/Delete/5
        [HttpPost]
        public ActionResult _Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Models.encuestaEntities db = new Models.encuestaEntities())
                    {
                        var aux = db.encuesta.Find(id);
                        aux.Estado = false;
                        db.Entry(aux).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
