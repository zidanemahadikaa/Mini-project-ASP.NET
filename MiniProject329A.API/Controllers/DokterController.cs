using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DokterController : Controller
    {
        private DADoctor doctor;

        public DokterController(MiniProject329AContext _db)
        {
            doctor = new DADoctor(_db);
        }

        //Get All Data
        [HttpGet]
        public VMResponse GetAll()
        {
            return doctor.GetAll();
        }
        [HttpGet("[action]")]
        public VMResponse GetByid(long id)
        {
            return doctor.GetById(id);
        }
        [HttpGet("[action]")]
        public VMResponse GetBiodataId(long id)
        {
            return doctor.GetBiodataId(id);
        }
    }
}
