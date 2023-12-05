using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerMemberController : Controller
    {
        private DACustomerMember customerMember;
        public CustomerMemberController(MiniProject329AContext _db)
        {
            customerMember = new DACustomerMember(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return customerMember.GetAll();
        }
        [HttpGet("[action]")]
        public VMResponse GetById(long? id)
        {
            return customerMember.GetById(id);
        }
        [HttpGet("[action]")]
        public VMResponse GetByCusId(long? cusId)
        {
            return customerMember.GetByCusId(cusId);
        }
        [HttpGet("[action]")]
        public VMResponse GetByParentId(long? parId)
        {
            return customerMember.GetByParentId(parId);
        }
        [HttpGet("[action]")]
        public VMResponse GetByCustomerName(long parId, string? name)
        {
            return customerMember.GetByCustomerName(parId, name);
        }
        [HttpPost]
        public VMResponse Create(VMTblMCustomerMember formData)
        {
            return customerMember.CreateUpdate(formData);
        }
        [HttpPut]
        public VMResponse Update(VMTblMCustomerMember formData)
        {
            return customerMember.CreateUpdate(formData);
        }
        [HttpDelete]
        public VMResponse Delete(long? id, long userId)
        {
            return customerMember.Delete(id, userId);
        }
    }
}
