using ClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private static List<Claim> claims = new List<Claim>();
        private static int nextId = 1;

        // GET: /Claim/Submit
        public IActionResult Submit()
        {
            return View();
        }

        // POST: /Claim/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(Claim claim, IFormFile? supportingDocument)
        {
            if (!ModelState.IsValid)
                return View(claim);

            claim.Id = nextId++;
            claim.Status = "Pending";

            // File upload
            if (supportingDocument != null && supportingDocument.Length > 0)
            {
                var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
                var extension = Path.GetExtension(supportingDocument.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    TempData["Message"] = "Only PDF or Word documents allowed.";
                    return View(claim);
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    supportingDocument.CopyTo(stream);

                claim.SupportingDocumentPath = "/uploads/" + uniqueFileName;
            }

            claims.Add(claim);

            TempData["Message"] = "Claim submitted successfully!";
            return RedirectToAction("Manage");
        }

        // GET: /Claim/Manage
        public IActionResult Manage()
        {
            return View(claims.OrderByDescending(c => c.Id).ToList());
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
                claim.Status = "Approved";

            TempData["Message"] = $"Claim #{id} approved!";
            return RedirectToAction("Manage");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
                claim.Status = "Rejected";

            TempData["Message"] = $"Claim #{id} rejected!";
            return RedirectToAction("Manage");
        }
    }
}