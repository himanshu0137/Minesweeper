using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models.Grid;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Minesweeper.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        GridDetails GD;
        // GET: api/values/<num>?<q>=
        [HttpGet("{data}")]
        public IActionResult Get(string data, int q) {
            switch (q) {
                case 1: {
                        GD = new GridDetails();
                        GD.config_set(Convert.ToInt32(data));
                        return Json(GD);
                    }
                case 2: { return Json(GD.cell_click(data)); }
                case 3: {/* If won store scores*/ break; }
            }
            return Json(new string[] {data, q.ToString()});
        }
    }
}
