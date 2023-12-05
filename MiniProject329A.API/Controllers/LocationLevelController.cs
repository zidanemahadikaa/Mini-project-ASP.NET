using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationLevelController : Controller
    {
        private DALocationLevel locationlevel;
        public LocationLevelController(MiniProject329AContext _db)
        {
            locationlevel = new DALocationLevel(_db);
        }
        [HttpGet()]
        public VMResponse GetAll() => locationlevel.GetByName();

        [HttpGet("{id?}")]
        public VMResponse Get(long? id = null) => locationlevel.GetById(id);

        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null) => locationlevel.GetByName(name);

        [HttpPost]
        public VMResponse Add(VMTblMLocationLevel dataInput) => locationlevel.Add(dataInput);


        [HttpPut]
        public VMResponse Edit(VMTblMLocationLevel dataInput) => locationlevel.Edit(dataInput);


        [HttpDelete]
        public VMResponse Delete(long id, int userId) => locationlevel.Delete(id, userId);


    }
}
