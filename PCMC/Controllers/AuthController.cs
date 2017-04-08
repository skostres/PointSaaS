using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PCMC.Entities;
using PCMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace PCMC.Controllers
{
    public class AuthController : BaseController
    {
        /* [HttpPost]
         public IHttpActionResult Login(LoginViewModel login)
         {

             return Json(new { status = true, id = "Gone with Wind", user = new { role = "admin", id = 1, name = login.UserName } });

         }
         */

        // GET: api/auth/Login
        [ResponseType(typeof(User))]
        [HttpPost]
        [Route("api/Auth/Login")]
        public IHttpActionResult Login(LoginViewModel login)
        {
            IQueryable<User> userDB = db.User.Where(c => c.Username == login.UserName && c.Password == login.Password);
            
            if (userDB == null || userDB.Count() != 1)
            {
                return Unauthorized();
            }

            return Ok(userDB.First());
        }
    }
}
