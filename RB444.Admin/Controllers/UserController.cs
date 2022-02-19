using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }
    }
}
