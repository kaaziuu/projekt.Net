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
using Unity.Injection;

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

            if (Request["lectures"] == "")
            {
                return View("Create");
            }
            var lectures = Request["lectures"].Split(',');

            var tmp = TimeSpan.Parse(Request["startAt"]);
            var StartAt = tmp.Hours * 3600 + tmp.Minutes * 60;
            tmp = TimeSpan.Parse(Request["finishAt"]);
            var finishAt = tmp.Hours * 3600 + tmp.Minutes * 60;
            zajecia.Prowadzacy = new List<Prowadzacy>();
            var lesson = new Zajecia
            {
                Nazwa = zajecia.Nazwa,
                PlanId = Int32.Parse(Request["planId"]),
                Godzina = StartAt,
                DzienTygodnia = Int32.Parse(Request["dayOfWeek"]),
                CzasTrwania = finishAt - StartAt,

            };

            foreach (var lecture in lectures)
            {
                var lectureObj = await _prowadzacyRepository.GetProwadzacyAsync(Int32.Parse(lecture));
                lesson.Prowadzacy.Add(lectureObj);
            }
            await _zajeciaRepository.ZapiszZajeciaAsync(lesson);

            // await _zajeciaRepository.ZapiszZajeciaAsync(lesson);


            // zajecia.Prowadzacy.Add();
            return View("Details");
        }
    }
}