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

            var lesson = await _zajeciaRepository.GetZajeciaAsync(id.Value);
            if (lesson == null)
            {
                return HttpNotFound();
            }

            var lecutres = await _prowadzacyRepository.getLectures(id.Value);
            ViewData["lecturers"] = lecutres;
            return View(lesson);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Nazwa,LinkDoZajec")]
            Zajecia zajecia)
        {
            int planId = Int32.Parse(Request["planId"]);
            var keyIsValid = await _planRepository.KeyIsValid(planId, Request["key"]);


            if (!keyIsValid)
            {
                @ViewBag.id = planId;
                return View("keyError");
            }

            if (Request["lectures"] == null)
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
                PlanId = planId,
                Godzina = StartAt,
                DzienTygodnia = Int32.Parse(Request["dayOfWeek"]),
                CzasTrwania = finishAt - StartAt,
                Prowadzacy = new List<Prowadzacy>(),
                LinkDoZajec = zajecia.LinkDoZajec
            };

            foreach (var lecture in lectures)
            {
                var lectureObj = await _prowadzacyRepository.GetProwadzacyAsync(Int32.Parse(lecture));
                lesson.Prowadzacy.Add(lectureObj);
            }

            var ok = await _zajeciaRepository.ZapiszZajeciaAsync(lesson, null);
            if (!ok)
            {
                return View("Error");
            }
            // await _zajeciaRepository.ZapiszZajeciaAsync(lesson);


            // zajecia.Prowadzacy.Add();
            return RedirectToAction("Details", "Plans", new {id = lesson.PlanId});
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lesson = await _zajeciaRepository.GetZajeciaAsync(id.Value);


            if (lesson == null)
            {
                return HttpNotFound();
            }

            ViewBag.planId = Request["planId"];
            ViewBag.name = Request["name"];
            var lectures = await _prowadzacyRepository.GetWieluProwadzacychAsync();
            var currentLectures = await _prowadzacyRepository.getLectures(id.Value);
            foreach (var current in currentLectures)
            {
                lectures.Remove(current);
            }

            ViewData["lecturers"] = lectures;
            ViewData["currentLecturers"] = currentLectures;


            var daysOfWeek = new Dictionary<int, string>
            {
                {1, "Poniedziałek"},
                {2, "Wtorek"},
                {3, "Środa"},
                {4, "Czwartek"},
                {5, "Piątek"},
            };

            ViewBag.daysOfWeek = new SelectList(daysOfWeek, "Key", "Value");
            return View(lesson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nazwa,LinkDoZajec")]
            Zajecia zajecia)
        {
            int planId = Int32.Parse(Request["planId"]);
            var keyIsValid = await _planRepository.KeyIsValid(planId, Request["key"]);
            if (!keyIsValid)
            {
                @ViewBag.planId = planId;
                return View("keyError");
            }

            List<Prowadzacy> toRemoveLectures = null;
            zajecia.Prowadzacy = new List<Prowadzacy>();
            if (Request["currentLecturers"] != null)
            {
                toRemoveLectures = new List<Prowadzacy>();
                zajecia.Prowadzacy = new List<Prowadzacy>();
                var toRemove = new List<string>(Request["currentLecturers"].Split(','));
                foreach (var id in toRemove)
                {
                    var lectureObj = await _prowadzacyRepository.GetProwadzacyAsync(Int32.Parse(id));
                    toRemoveLectures.Add(lectureObj);
                }
            }

            var currentLectures = await _prowadzacyRepository.getLectures(zajecia.Id);

            foreach (var current in currentLectures)
            {
                zajecia.Prowadzacy.Add(current);
            }


            if (Request["lectures"] != null)
            {
                // var tt = Request["lectures"];
                var newLectures = new List<string>(Request["lectures"].Split(','));
                foreach (var id in newLectures)
                {
                    var lectureObj = await _prowadzacyRepository.GetProwadzacyAsync(Int32.Parse(id));
                    zajecia.Prowadzacy.Add(lectureObj);
                }
            }

            var tmp = TimeSpan.Parse(Request["startAt"]);
            var StartAt = tmp.Hours * 3600 + tmp.Minutes * 60;
            tmp = TimeSpan.Parse(Request["finishAt"]);
            var finishAt = tmp.Hours * 3600 + tmp.Minutes * 60;
            zajecia.PlanId = planId;
            zajecia.Godzina = StartAt;
            zajecia.DzienTygodnia = Int32.Parse(Request["dayOfWeek"]);
            zajecia.CzasTrwania = finishAt - StartAt;

            await _zajeciaRepository.ZapiszZajeciaAsync(zajecia, toRemoveLectures);

            return RedirectToAction("Details", "Plans", new {id = planId});
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lesson = await _zajeciaRepository.GetZajeciaAsync(id.Value);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.planId = Request["planId"];
            ViewBag.name = Request["name"];
            ViewData["lecturers"] = await  _prowadzacyRepository.getLectures(id.Value);
            

            var daysOfWeek = new Dictionary<int, string>
            {
                {1, "Poniedziałek"},
                {2, "Wtorek"},
                {3, "Środa"},
                {4, "Czwartek"},
                {5, "Piątek"},
            };

            return View(lesson);

        }
        
        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var key = Request["key"];
            var lesson = await _zajeciaRepository.GetZajeciaAsync(id);
            if (lesson == null)
                return HttpNotFound();
            var ok = await _planRepository.KeyIsValid(lesson.PlanId, key);
            
            if (!ok)
            {
                @ViewBag.Id = lesson.PlanId;
                return View("keyError");
            }

            int planId = lesson.PlanId;

            await _zajeciaRepository.UsunZajeciaAsync(lesson);
            return RedirectToAction("Details", "Plans", new {id = planId});

        }
    }
}