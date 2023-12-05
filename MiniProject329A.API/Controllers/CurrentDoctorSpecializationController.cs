using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrentDoctorSpecializationController : Controller
    {
        private DACurrentDoctorSpecialization currentDoctorSpec;
        public CurrentDoctorSpecializationController(MiniProject329AContext _db)
        {
            currentDoctorSpec = new DACurrentDoctorSpecialization(_db);
        }

        [HttpGet()]
        public VMResponse GetAll() => currentDoctorSpec.GetByNameSpecialization();

        [HttpGet("{id?}")]
        public VMResponse Get(long? id = null) => currentDoctorSpec.GetById(id);
        
        [HttpGet("[action]/{id?}")]
        public VMResponse GetDoctorId(long? id = null) => currentDoctorSpec.GetDoctorId(id);
        
        [HttpGet("[action]/{id?}")]
        public VMResponse GetSpecializationId(long? id = null) => currentDoctorSpec.GetSpecializationId(id);
        [HttpPost]
        public VMResponse Add(VMTblTCurrentDoctorSpecialization dataInput) => currentDoctorSpec.Add(dataInput);

        [HttpPut]
        public VMResponse Edit(VMTblTCurrentDoctorSpecialization dataInput) => currentDoctorSpec.Edit(dataInput);
        [HttpDelete]
        public VMResponse Delete(long id, int userId) => currentDoctorSpec.Delete(id, userId);
        
        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null) => currentDoctorSpec.GetByNameSpecialization(name);
    }
}
