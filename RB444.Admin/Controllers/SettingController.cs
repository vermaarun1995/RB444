using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult SportsSetting()
        {
            return View();
        }

        public IActionResult SeriesSetting()
        {
            return View();
        }

        public IActionResult MatchSetting()
        {
            return View();
        }
    }
}
