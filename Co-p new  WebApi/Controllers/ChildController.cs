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
        public readonly IWebHostEnvironment _environment;
        public ChildController(IWebHostEnvironment environment, CoPNewContext context, ILogger<ChildController> logger)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            db = context ?? throw new ArgumentNullException(nameof(context));
        }

       


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
        [HttpPost("UploadChildPhoto")]
        public async Task<IActionResult> UploadChildPhoto(IFormFile photo, string childId)
        {
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("Invalid photo.");
            }

            if (string.IsNullOrWhiteSpace(childId))
            {
                return BadRequest("Invalid child ID.");
            }

            // Retrieve the child from the database
            var child = await db.Children.FindAsync(childId);
            if (child == null)
            {
                return NotFound("Child not found.");
            }

            // Ensure childId is a valid file name
            string sanitizedChildId = Path.GetInvalidFileNameChars()
                                          .Aggregate(childId, (current, c) => current.Replace(c, '_'));

            string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

            string uniqueFileName = $"{sanitizedChildId}_{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                // Update child entity with photo name
                child.PhotoName = uniqueFileName;
                db.Update(child);
                await db.SaveChangesAsync();

                return Ok("Photo uploaded successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        //[HttpPost]
        //[Route("uploadPhoto")]
        //public async Task<IActionResult> UploadChildPhoto(IFormFile photo, string childId)
        //{
        //    if (photo != null && photo.Length > 0 && !string.IsNullOrWhiteSpace(childId))
        //    {
        //        // Retrieve the child from the database
        //        var child = await db.Children.FindAsync(childId);
        //        if (child == null)
        //        {
        //            return NotFound("Child not found.");
        //        }

        //        // Ensure childId is a valid file name
        //        string sanitizedChildId = Path.GetInvalidFileNameChars()
        //                                      .Aggregate(childId, (current, c) => current.Replace(c, '_'));

        //        string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        //        Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

        //        string uniqueFileName = $"{sanitizedChildId}_{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await photo.CopyToAsync(fileStream);
        //        }

        //        // Update child entity with photo name
        //        child.PhotoName = uniqueFileName;
        //        db.Update(child);
        //        await db.SaveChangesAsync();

        //        return Ok("Photo uploaded successfully!");
        //    }

        //    return BadRequest("Invalid photo or child ID.");
        //}



    }
}
