﻿using Co_P_Library.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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
        [Route("AddChildrenByExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid Excel file.");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context

            var children = new List<Child>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    if (worksheet.Dimension == null)
                    {
                        return BadRequest("The Excel file is empty.");
                    }
                    var rowCount = worksheet.Dimension.Rows;
                    Console.WriteLine($"Row count: {rowCount}");

                    for (int row = 2; row <= rowCount; row++) // Assuming the first row is the header
                    {
                        var childId = worksheet.Cells[row, 1].Text;
                        var childFirstName = worksheet.Cells[row, 2].Text; // Change from int to string
                        var childSurname = worksheet.Cells[row, 3].Text;  // Change from int to string
                        var childBirthDate = DateTime.Parse(worksheet.Cells[row, 4].Text); // Parse DateTime
                        var childGender = worksheet.Cells[row, 5].Text;
                        var parent1 = worksheet.Cells[row, 6].Text;
                        var parent2 = worksheet.Cells[row, 7].Text;
                        var childPhotoName = worksheet.Cells[row, 8].Text;

                        // Retrieve or create related entities as needed
                        var child = db.Children.FirstOrDefault(c => c.ChildId == childId);
                        if (child == null)
                        {
                            var newChild = new Child
                            {
                                ChildId = childId,
                                ChildFirstName = childFirstName,
                                ChildSurname = childSurname,
                                ChildBirthDate = childBirthDate,
                                ChildGender = childGender,
                                Parent1 = parent1,
                                Parent2 = parent2,
                                ChildPhotoName = childPhotoName
                            };

                            children.Add(newChild);
                        }
                    }
                }
            }

            db.Children.AddRange(children);
            await db.SaveChangesAsync();

            return Ok(new { Message = "Data imported successfully." });
        }
        [HttpPost]
        [Route("AddChildren")]
        public dynamic addChild(string ID, string childFMame, string chilsSName, DateTime chilsBdate, string gender, string parent1, string parent2)
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
