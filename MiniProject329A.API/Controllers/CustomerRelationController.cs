using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerRelationController : Controller
    {
        private DACustomerRelation customerRelation;
        public CustomerRelationController(MiniProject329AContext _db)
        {
            customerRelation = new DACustomerRelation(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return customerRelation.GetByName();
        }
        [HttpGet("[action]/{id?}")]
        public VMResponse GetById(long? id = null)
        {
            return customerRelation.GetById(id);
        }
        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null)
        {
            return customerRelation.GetByName(name);
        }
        [HttpPost]
        public VMResponse Create(VMTblMCustomerRelation formData)
        {
            return customerRelation.Create(formData);
        }
        [HttpPut]
        public VMResponse Update(VMTblMCustomerRelation formData)
        {
            return customerRelation.Update(formData);
        }
        [HttpDelete]
        public VMResponse Delete(long? id, long userId)
        {
            return customerRelation.Delete(id, userId);
        }
    }
}
