using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private static List<Claim> claims = new List<Claim>();
        private static int nextId = 1;

        // GET: Claim/Submit
        public IActionResult Submit()
        {
            return View();
        }

        // POST: Claim/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Claim claim, IFormFile? supportingDocument)
        {
            if (!ModelState.IsValid)
                return View(claim);

            claim.Id = nextId++;
            claim.Status = "Pending";

            // ✅ Handle optional file upload
            if (supportingDocument != null && supportingDocument.Length > 0)
            {
                var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
                var fileExtension = Path.GetExtension(supportingDocument.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    TempData["Message"] = "Invalid file type. Please upload a PDF or Word document.";
                    return View(claim);
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                var filePath = Path.Combine(uploadFolder, supportingDocument.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    supportingDocument.CopyTo(stream);
                }

                claim.SupportingDocumentPath = "/uploads/" + supportingDocument.FileName;
            }

            claims.Add(claim);

            TempData["Message"] = "Claim submitted successfully!";
            return RedirectToAction("Manage");
        }

        // GET: Claim/Manage
        public IActionResult Manage()
        {
            return View(claims);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
                claim.Status = "Approved";

            TempData["Message"] = $"Claim #{id} approved successfully!";
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
                claim.Status = "Rejected";

            TempData["Message"] = $"Claim #{id} rejected successfully!";
            return RedirectToAction("Manage");
        }
    }
}