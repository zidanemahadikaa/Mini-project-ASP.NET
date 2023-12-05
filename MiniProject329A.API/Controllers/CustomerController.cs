using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private DACustomer customer;
        public CustomerController(MiniProject329AContext _db)
        {
            customer = new DACustomer(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return customer.GetAll();
        }        
        [HttpPost]
        public VMResponse Create(VMTblMCustomer formData)
        {
            return customer.CreateUpdate(formData);
        }
        [HttpPut]
        public VMResponse Update(VMTblMCustomer formData)
        {
            return customer.CreateUpdate(formData);
        }
        [HttpGet("[action]")]
        public VMResponse GetById(long? id)
        {
            return customer.GetById(id);
        }
        [HttpGet("[action]")]
        public VMResponse GetByBioId(long? bioId = null)
        {
            return customer.GetByBioId(bioId);
        }
        [HttpGet("[action]")]
        public VMResponse GetByName(string? name = null)
        {
            return customer.GetByName(name);
        }
    }
}
