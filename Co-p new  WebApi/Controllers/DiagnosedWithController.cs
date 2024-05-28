using Co_P_Library.Models;
using Co_p_new__WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Co_p_new_WebApi.Controllers
{
    public class DiagnosedWithController : Controller
    {

        CoPNewContext db = new CoPNewContext();

        [HttpGet]
        [Route("GetAll")]
        public dynamic GetChildHealthProblemsByKindergarten()
        {
            var diagnosed = db.DiagnosedWiths.Select(d => new DiagnosedWith()
            {
                ChildId = d.ChildId,
                HealthProblemsNumber = d.HealthProblemsNumber,
                Severity = d.Severity,
                Care = d.Care
            });

            return diagnosed;
            
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
