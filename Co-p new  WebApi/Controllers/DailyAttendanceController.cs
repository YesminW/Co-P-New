using Co_P_Library.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Co_p_new__WebApi.Controllers
{
    public class DailyAttendanceController
    {
        CoPNewContext db = new CoPNewContext();

        [HttpGet]
        [Route("GetDailyAttendance")]
        public dynamic GetAttendance(DateTime date)
        {
            var attendace = db.DailyAttendances.Where(x => x.AttendanceDate == date).Select(a => new
            {
                date = a.AttendanceDate,
                ChildName = a.Child.ChildFirstName + " " + a.Child.ChildSurname,
                MorningPresence = a.MorningPresenceNavigation.AttendanceCodeName,
                AfternoonPresence = a.AfternoonPresenceNavigation.AttendanceCodeName

            });

            return attendace;
        }

        [HttpPost]
        [Route("AddMorningPresence")]
        public dynamic addMorningPresence(string childID, int MorningP)
        {
            DailyAttendance DA = new DailyAttendance();
            DA.AttendanceDate = DateTime.Now;
            DA.ChildId = childID;
            DA.MorningPresence = MorningP;

            db.DailyAttendances.Add(DA);
            db.SaveChanges();
            return DA; 

        }

        [HttpPost]
        [Route("AddAfternoonPresence")]
        public dynamic addAfternoonPresence(string ID, int AfternoonP)
        {
            var dateNow = DateTime.Today;
            DailyAttendance? DA = db.DailyAttendances.Where(x => x.ChildId == ID && x.AttendanceDate == dateNow).FirstOrDefault();
        
              DA.AfternoonPresence = AfternoonP;

            db.DailyAttendances.Update(DA);
            db.SaveChanges();
            return DA;

        }


    }
}
