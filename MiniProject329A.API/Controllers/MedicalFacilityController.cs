using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
    public class MedicalFacilityController : Controller
    {
        private DAMedicalFacility medicalFacility;

        public MedicalFacilityController(MiniProject329AContext _db)
        {
            medicalFacility = new DAMedicalFacility(_db);
        }
        [HttpGet()]
        public VMResponse GetAll() => medicalFacility.GetByName();
    }
}
