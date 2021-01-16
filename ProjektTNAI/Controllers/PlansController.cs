using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ProjetTNAI.DataAccessLayer.Repositories.Abstract;
using ProjetTNAI.DataAccessLayer.Repositories.Concrete;
using ProjetTNAI.Entities;
using ProjetTNAI.Entities.Models;

namespace ProjektTNAI.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanRepository _planRepository;
        private Random _random = new Random();

        public PlansController(IPlanRepository planRepository, HttpClient httpClient)
        {
            _planRepository = planRepository;
        }

        // GET: Plans
        public async Task<ActionResult> Index()
        {
            var plans = await _planRepository.GetWielePlanowAsync();
            return View(plans);
        }

        // GET: Plans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var plan = await _planRepository.GetPlanAsync(id.Value);
            if (plan == null)
            {
                return HttpNotFound();
            }

            return View(plan);
        }

        // GET: Plans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nazwa,Rok,Grupa")] Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return View(plan);
            }

            var token = "";
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            for (var i = 0; i < 5; i++)
            {
                token += chars[_random.Next(chars.Length)];
            }

            plan.KluczEdycji = token;

            var result = await _planRepository.ZapiszPlanAsync(plan);
            if (!result)
                return View("Error");

            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var plan = await _planRepository.GetPlanAsync(id.Value);


            if (plan == null)
            {
                return HttpNotFound();
            }

            plan.KluczEdycji = "";

            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nazwa,Rok,Grupa,KluczEdycji")]
            Plan plan)
        {
            var chekKey = await _planRepository.CheckKey(plan);
            if (ModelState.IsValid && chekKey)
            {
                await _planRepository.ZapiszPlanAsync(plan);
            }
            else
            {
                return View(plan);
            }

            return RedirectToAction("Index");
        }

        // GET: Plans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var plan = await _planRepository.GetPlanAsync(id.Value);
            if (plan == null)
            {
                return HttpNotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var key = Request["key"];

            Plan plan = await _planRepository.GetPlanAsync(id);
            if (plan.KluczEdycji != key)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _planRepository.UsunPlanAsync(plan);

            return RedirectToAction("Index");
        }
    }
}