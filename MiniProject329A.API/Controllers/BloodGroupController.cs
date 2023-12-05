using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodGroupController : Controller
    {
        private DABloodGroup bloodGroup;
        public BloodGroupController(MiniProject329AContext _db)
        {
            bloodGroup = new DABloodGroup(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return bloodGroup.GetByCode();
        }
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long? id = null)
        {
            return bloodGroup.Get(id);
        }
        [HttpGet("[action]/{code?}")]
        public VMResponse GetByCode(string? code = null)
        {
            return bloodGroup.GetByCode(code);
        }
        [HttpPost]
        public VMResponse Create(VMTblMBloodGroup formData)
        {
            return bloodGroup.Create(formData);
        }
        [HttpPut]
        public VMResponse Update(VMTblMBloodGroup formData)
        {
            return bloodGroup.Update(formData);
        }
        [HttpDelete]
        public VMResponse Delete(long? id, long userId)
        {
            return bloodGroup.Delete(id, userId);
        }
    }
}
