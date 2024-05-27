using Co_P_Library.Models;
using Co_p_new__WebApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Co_p_new__WebApi.Controllers
{
    public class SufferingFromController : Controller
    {
        CoPNewContext db = new CoPNewContext();

        [HttpPut]
        [Route("AddHealthProblemtouser")]
        public dynamic AddHealthProblem(string ID, int healthproblem, int Severity, string care)
        {
            User? u = db.Users.Where(x => x.UserId == ID).FirstOrDefault();
            if (u == null)
            {
                return NotFound(new { message = "User not found" });
            }
            else
            {
                SufferingFrom s = new SufferingFrom();
                s.UserId = u.UserId;
                s.HealthProblemsNumber = healthproblem;
                s.Severity = Severity;
                s.Care = care;

                db.SufferingFrom.Add(s);
                db.SaveChanges();
                return Ok(s);
            }
        }

    }
}
