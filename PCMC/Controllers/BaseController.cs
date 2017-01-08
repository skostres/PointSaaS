using PCMC.Entities;
using PCMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace PCMC.Controllers
{
    public class BaseController : ApiController
    {
        protected ModelADO db = new ModelADO();

        // GET: Base
        public virtual IHttpActionResult Index()
        {
            return Ok();
        }

    }
}