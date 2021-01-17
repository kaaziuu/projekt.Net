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

        public ClassesController(IZajeciaRepository repository)
        {
            _zajeciaRepository = repository;
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
    }
}