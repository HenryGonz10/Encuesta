using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace encuestas.Filters
{
    public class VerifiSession:ActionFilterAttribute
    {
        private Models.usuario usuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                usuario = (Models.usuario)HttpContext.Current.Session["User"];
                if (usuario == null)
                {
                    if (filterContext.Controller is Controllers.LoginController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Login/Index");
                    }
                }
                else
                {
                    if (filterContext.Controller is Controllers.LoginController == true)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Home/Index");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
            }
        }
    }
}