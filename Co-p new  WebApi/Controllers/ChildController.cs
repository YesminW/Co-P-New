using Co_P_Library.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Co_p_new__WebApi.Controllers
{
    [EnableCors]
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
            Child c = new Child
            {
                ChildId = ID,
                ChildFirstName = childFMame,
                ChildSurname = chilsSName,
                ChildBirthDate = chilsBdate,
                ChildGender = gender,
                Parent1 = parent1,
                Parent2 = parent2
            };

            db.Children.Add(c);
            db.SaveChanges();
            return Ok(c);
        }
     


    }
}
