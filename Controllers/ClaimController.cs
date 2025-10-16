using Microsoft.AspNetCore.Mvc;
using ClaimSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        // Static list to simulate a database
        private static List<Claim> claims = new List<Claim>();
        private static int nextId = 1;

        // Show the claim submission form
        public IActionResult Submit()
        {
            return View();
        }

        // Handle form submission (POST)
        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.Id = nextId++;   // ✅ fixed property name
                claim.Status = "Pending";
                claims.Add(claim);

                TempData["Message"] = "Claim submitted successfully!";
                return RedirectToAction("Manage");
            }

            return View(claim);
        }

        // Display all submitted claims
        public IActionResult Manage()
        {
            return View(claims);
        }
    }
}
