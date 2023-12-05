using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SpecializationController : Controller
    {
        private DASpecialization specialization;
        public SpecializationController(MiniProject329AContext _db)
        {
            specialization = new DASpecialization(_db);
        }
        [HttpGet()]
        public VMResponse GetAll() => specialization.GetByName();

        [HttpGet("{id?}")]
        public VMResponse Get(long? id = null) => specialization.GetById(id);

        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null) => specialization.GetByName(name);

        [HttpPost]
        public VMResponse Add(VMTblMSpecialization dataInput) => specialization.Add(dataInput);


        [HttpPut]
        public VMResponse Edit(VMTblMSpecialization dataInput) => specialization.Edit(dataInput);


        [HttpDelete]
        public VMResponse Delete(long id, int userId) => specialization.Delete(id, userId);

    }
}
