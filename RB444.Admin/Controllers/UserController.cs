using Microsoft.AspNetCore.Mvc;
using RB444.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Users()
        {
            return View();
        }
       
    }
}
