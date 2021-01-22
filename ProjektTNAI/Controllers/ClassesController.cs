using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.Entities.Models;

namespace ProjektTNAI.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IZajeciaRepository _zajeciaRepository;
        private readonly IProwadzacyRepository _prowadzacyRepository;
        private readonly IPlanRepository _planRepository;

        public ClassesController(IZajeciaRepository repository, IProwadzacyRepository prowadzacyRepository,
            IPlanRepository planRepository)
        {
            _zajeciaRepository = repository;
            _prowadzacyRepository = prowadzacyRepository;
            _planRepository = planRepository;
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var activities = await _zajeciaRepository.GetZajeciaAsync(id.Value);
            if (activities == null)
            {
                return HttpNotFound();
            }

            return View(activities);
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.planId = Request["planId"];
            ViewBag.name = Request["name"];
            ViewData["lecturers"] = await _prowadzacyRepository.GetWieluProwadzacychAsync();

            var daysOfWeek = new Dictionary<int, string>
            {
                {1, "Poniedziałek"},
                {2, "Wtorek"},
                {3, "Środa"},
                {4, "Czwartek"},
                {5, "Piątek"},
            };

            ViewBag.daysOfWeek = new SelectList(daysOfWeek, "Key", "Value");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nazwa")] Zajecia zajecia)
        {
            var lectures = Request["lectures"].Split(',');
            // var startAt = DateTime.ParseExact(Request["startAt"], "HH:mm", CultureInfo.InvariantCulture);
            // var finishAt = DateTime.ParseExact(Request["finishAt"], "HH:mm", CultureInfo.InvariantCulture);
            zajecia.PlanId = Int32.Parse(Request["planId"]);
            var tmp =
                (int) TimeSpan.ParseExact(Request["startAt"], "HH:mm", CultureInfo.InvariantCulture).TotalMinutes;
            var x = "XD";
            zajecia.Godzina = tmp;
            zajecia.CzasTrwania =
                (int) (TimeSpan.ParseExact(Request["finishAt"], "HH:mm", CultureInfo.InvariantCulture).TotalMinutes -
                       (int) TimeSpan.ParseExact(Request["startAt"], "HH:mm", CultureInfo.InvariantCulture)
                           .TotalMinutes);

            foreach (var lecture in lectures)
            {
                var lectureObj = await _prowadzacyRepository.GetProwadzacyAsync(Int32.Parse(lecture));
                zajecia.Prowadzacy.Add(lectureObj);
            }


            // zajecia.Prowadzacy.Add();
            return View("Details");
        }
    }
}