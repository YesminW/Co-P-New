using Co_P_Library.Models;
using Co_p_new__WebApi.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Co_p_new__WebApi.Controllers
{
    [EnableCors]

    public class DiagnosedWithController : Controller
    {

        CoPNewContext db = new CoPNewContext();

        [HttpPut]
        [Route("AddHealthProblem")]
        public IActionResult AddHealthProblem(string ID, int healthproblem, int Severity, string care)
        {
            var c = db.Children.FirstOrDefault(x => x.Parent1 == ID || x.Parent2 == ID);
            if (c == null)
            {
                return NotFound(new { message = "child not found" });
            }
            else
            {
                DiagnosedWith D = new DiagnosedWith
                {
                    ChildId = c.ChildId,
                    HealthProblemsNumber = healthproblem,
                    Severity = Severity,
                    Care = care
                };

                db.DiagnosedWith.Add(D);
                db.SaveChanges();
                return Ok(D);
            }
        }

    }
}
