using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System.Xml.Linq;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private DALocation location;
        public LocationController(MiniProject329AContext _db)
        {
            location = new DALocation(_db);
        }
        [HttpGet()]
        public VMResponse GetAll() => location.GetByName();

        [HttpGet("{id?}")]
        public VMResponse Get(long? id = null) => location.GetById(id);

        [HttpGet("[action]/{id?}")]
        public VMResponse GetByParent(long? id = null) => location.GetByParent(id);

        [HttpPost]
        public VMResponse Add(VMTblMLocation dataInput) => location.Add(dataInput);


        [HttpPut]
        public VMResponse Edit(VMTblMLocation dataInput) => location.Edit(dataInput);
        
        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null) => location.GetByName(name);


        [HttpDelete]
        public VMResponse Delete(long id, int userId) => location.Delete(id, userId);
    }
}
