using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetTNAI.Entities;
using ProjetTNAI.Entities.Models;
using ProjetTNAI.DataAccessLayer.Repositories.Abstract;

namespace ProjektTNAI.Controllers
{
    public class ProwadzacyController : Controller
    {
        private readonly IProwadzacyRepository _prowadzacyRepository;

        public ProwadzacyController(IProwadzacyRepository prowadzacyRepository)
        {
            _prowadzacyRepository = prowadzacyRepository;
        }

        // GET: Prowadzacy
        public async Task<ActionResult> Index(int? planId, string editKey)
        {
            if(planId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var prowadzacy = await _prowadzacyRepository.GetProwadzacyWPlanieAsync(planId.Value);

            if (await _prowadzacyRepository.KeyIsValid(planId, editKey))
                return View(prowadzacy);
            
            return View(nameof(Index)+"Readonly", prowadzacy);
        }

        // GET: Prowadzacy/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prowadzacy prowadzacy = await _prowadzacyRepository.GetProwadzacyAsync(id.Value);
            if (prowadzacy == null)
            {
                return HttpNotFound();
            }
            return View(prowadzacy);
        }

        // GET: Prowadzacy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prowadzacy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Imie,Nazwisko,Email")] Prowadzacy prowadzacy)
        {
            if (ModelState.IsValid)
            {
                await _prowadzacyRepository.ZapiszProwadzacyAsync(prowadzacy);
                return RedirectToAction("Index");
            }

            return View(prowadzacy);
        }

        // GET: Prowadzacy/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prowadzacy prowadzacy = await _prowadzacyRepository.GetProwadzacyAsync(id.Value);
            if (prowadzacy == null)
            {
                return HttpNotFound();
            }
            return View(prowadzacy);
        }

        // POST: Prowadzacy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Imie,Nazwisko,Email")] Prowadzacy prowadzacy, int? planId, string editKey)
        {
            if (!await _prowadzacyRepository.KeyIsValid(planId, editKey))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                await _prowadzacyRepository.ZapiszProwadzacyAsync(prowadzacy);
                return RedirectToAction("Index");
            }
            return View(prowadzacy);
        }

        // GET: Prowadzacy/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prowadzacy prowadzacy = await _prowadzacyRepository.GetProwadzacyAsync(id.Value);
            if (prowadzacy == null)
            {
                return HttpNotFound();
            }
            return View(prowadzacy);
        }

        // POST: Prowadzacy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int? planId, string editKey)
        {
            if (!await _prowadzacyRepository.KeyIsValid(planId, editKey))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Prowadzacy prowadzacy = await _prowadzacyRepository.GetProwadzacyAsync(id);
            await _prowadzacyRepository.UsunProwadzacyAsync(prowadzacy);
            return RedirectToAction("Index");
        }
    }
}
