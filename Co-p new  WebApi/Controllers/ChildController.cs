using Co_P_Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Co_p_new__WebApi.Controllers
{
    public class ChildController : Controller
    {
        CoPNewContext db = new CoPNewContext();

        [HttpGet]
        [Route("AllChild")]
        public dynamic GetAllChild()
        {
            var children = db.Children;
            return children;
        }

        [HttpPost]
        [Route("AddChildren")]
        public dynamic addChild (string ID, string childFMame, string chilsSName, DateTime chilsBdate, string gender, string parent1, string parent2)
        {
            Child c = new Child();
            c.ChildId = ID;
            c.ChildFirstName = childFMame;
            c.ChildSurname = chilsSName;
            c.ChildBirthDate = chilsBdate;
            c.ChildGender = gender;
            c.Parent1 = parent1;
            c.Parent2 = parent2;

            db.Children.Add(c);
            db.SaveChanges();
            return Ok(c);

        }

    }
}
