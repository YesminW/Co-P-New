using Co_P_Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Co_p_new_WebApi.Controllers
{
    public class DiagnosedWithController : Controller
    {

        CoPNewContext db = new CoPNewContext();

        [HttpGet]
        [Route("GetAllDiagnosed")]
        public IActionResult GetChildrenWithHealthProblems()
        {
            var childrenWithHealthProblems = db.DiagnosedWiths
                .Select(dw => new
                {
                    dw.ChildId,
                    ChildName = dw.Child.ChildFirstName + " " + dw.Child.ChildSurname,
                    dw.Child.RegisterdTos.FirstOrDefault().KindergartenNumberNavigation.KindergartenName,
                    dw.Child.RegisterdTos.FirstOrDefault().CurrentAcademicYear,
                    dw.HealthProblem.HealthProblemName,
                    dw.Severity,
                    dw.Care
                })
                .ToList();

            return Ok(childrenWithHealthProblems);
        }

        [HttpGet]
        [Route("GetChildHealthProblemsbyKindergarten")]
        public IActionResult GetByKindergartenName(string kindergartenName, int year)
        {
            var diagnosedChildren = db.RegisterdTos
                .Where(r => r.KindergartenNumberNavigation.KindergartenName == kindergartenName && r.CurrentAcademicYear == year)
                .SelectMany(r => r.Child.DiagnosedWiths.Select(dw => new
                {
                    ChildId = r.ChildId,
                    ChildName = r.Child.ChildFirstName + " " + r.Child.ChildSurname,
                    HealthProblemsName = dw.HealthProblem.HealthProblemName,
                    Severity = dw.Severity,
                    Care = dw.Care
                }))
                .ToList();

            return Ok(diagnosedChildren);
        }

        [HttpPost]
        [Route("Add")]
        public bool AddHealthProblemToChild(string parentId, int healthProblemsNumber, int severity, string care)
        {
            // Step 1: Retrieve the Child ID
            var childId = db.Children
                .Where(c => c.Parent1 == parentId || c.Parent2 == parentId)
                .Select(c => c.ChildId)
                .FirstOrDefault();

            // Step 2: Check for the Child
            if (childId == null)
            {
                // Child with the provided Parent ID does not exist
                return false;
            }

            try
            {
                // Step 3: Add the Health Problem
                var diagnosedWith = new DiagnosedWith
                {
                    ChildId = childId,
                    HealthProblemsNumber = healthProblemsNumber,
                    Severity = severity,
                    Care = care
                };

                db.DiagnosedWiths.Add(diagnosedWith);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions, such as database errors
                Console.WriteLine($"Error adding health problem: {ex.Message}");
                return false;
            }



        }
    }
}
