using Co_P_Library.Models;
using Co_p_new__WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Co_p_new__WebApi.Controllers
{
    public class DiagnosedWithController : Controller
    {
        CoPNewContext db = new CoPNewContext();

        [HttpPut]
        [Route("AddHealthProblem")]
        public dynamic AddHealthProblem(string ID, int healthproblem, int Severity, string care)
        {
            Child? c = db.Children.Where(x => x.Parent1 == ID || x.Parent2 == ID).FirstOrDefault();
            if (c == null)
            {
                return NotFound(new { message = "child not found" });
            }
            else
            {
                DiagnosedWith D = new DiagnosedWith();
                D.ChildId = c.ChildId;
                D.HealthProblemsNumber = healthproblem;
                D.Severity = Severity;
                D.Care = care;

                db.DiagnosedWith.Add(D);
                db.SaveChanges();
                return Ok(D);
            }
        }

    }
}
