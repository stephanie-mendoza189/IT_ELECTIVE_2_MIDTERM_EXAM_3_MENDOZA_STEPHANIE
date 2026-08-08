using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Visitor_Pass_Monitoring_System.Models;
using Visitor_Pass_Monitoring_System.Models.Dtos;
using Visitor_Pass_Monitoring_System.Repositories;

namespace Visitor_Pass_Monitoring_System.Controllers
{
    [Authorize]
    public class VisitorController : Controller
    {
        private VisitorRepository _visitorRepo = new VisitorRepository();

        public IActionResult Index(string searchString)
        {
            List<Visitor> allVisitors = _visitorRepo.GetAll();
            List<Visitor> filteredVisitors = new List<Visitor>();

            if (string.IsNullOrEmpty(searchString))
            {
                filteredVisitors = allVisitors;
            }
            else
            {
                foreach (Visitor v in allVisitors)
                {
                    string pass = v.PassNumber != null ? v.PassNumber : "";
                    string fname = v.FirstName != null ? v.FirstName : "";
                    string lname = v.LastName != null ? v.LastName : "";

                    if (pass.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        fname.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        lname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredVisitors.Add(v);
                    }
                }
            }

            return View(filteredVisitors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VisitorCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VisitorCreateDto visitorDto)
        {
            if (!ModelState.IsValid)
            {
                return View(visitorDto);
            }

            Visitor visitor = new Visitor
            {
                FirstName = visitorDto.FirstName,
                LastName = visitorDto.LastName,
                Company = visitorDto.Company,
                ContactNumber = visitorDto.ContactNumber,
                PersonToVisit = visitorDto.PersonToVisit,
                Department = visitorDto.Department,
                Purpose = visitorDto.Purpose,
                ValidIdPresented = visitorDto.ValidIdPresented,
                Notes = visitorDto.Notes
            };

            _visitorRepo.Add(visitor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Visitor visitor = _visitorRepo.GetById(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Visitor visitor = _visitorRepo.GetById(id);

            if (visitor == null)
            {
                return NotFound();
            }

            VisitorEditDto visitorDto = new VisitorEditDto
            {
                Id = visitor.Id,
                PassNumber = visitor.PassNumber,
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                Company = visitor.Company,
                ContactNumber = visitor.ContactNumber,
                PersonToVisit = visitor.PersonToVisit,
                Department = visitor.Department,
                Purpose = visitor.Purpose,
                ValidIdPresented = visitor.ValidIdPresented,
                Notes = visitor.Notes
            };

            return View(visitorDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VisitorEditDto visitorDto)
        {
            if (!ModelState.IsValid)
            {
                return View(visitorDto);
            }

            Visitor visitor = new Visitor
            {
                Id = visitorDto.Id,
                PassNumber = visitorDto.PassNumber,
                FirstName = visitorDto.FirstName,
                LastName = visitorDto.LastName,
                Company = visitorDto.Company,
                ContactNumber = visitorDto.ContactNumber,
                PersonToVisit = visitorDto.PersonToVisit,
                Department = visitorDto.Department,
                Purpose = visitorDto.Purpose,
                ValidIdPresented = visitorDto.ValidIdPresented,
                Notes = visitorDto.Notes
            };

            _visitorRepo.Update(visitor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Exit(int id)
        {
            Visitor visitor = _visitorRepo.GetById(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        [HttpPost, ActionName("Exit")]
        [ValidateAntiForgeryToken]
        public IActionResult ExitConfirmed(int id)
        {
            _visitorRepo.RecordExit(id);

            return RedirectToAction("Index");
        }
    }
}
