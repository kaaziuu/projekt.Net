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

        public ClassesController(IZajeciaRepository repository, IProwadzacyRepository prowadzacyRepository)
        {
            _zajeciaRepository = repository;
            _prowadzacyRepository = prowadzacyRepository;
        }
        public async Task<ActionResult> Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var  activities = await _zajeciaRepository.GetZajeciaAsync(id.Value);
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
            
            
            return View();
        }
    }
}