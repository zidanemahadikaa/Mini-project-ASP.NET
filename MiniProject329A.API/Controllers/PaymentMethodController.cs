using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PaymentMethodController : Controller
    {
        private DAPaymentMethod paymentmethod;
        public PaymentMethodController(MiniProject329AContext _db)
        {
            paymentmethod = new DAPaymentMethod(_db);
        }
        [HttpGet()]
        public VMResponse GetAll() => paymentmethod.GetByName();

        [HttpGet("{id?}")]
        public VMResponse Get(long? id = null) => paymentmethod.GetById(id);

        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null) => paymentmethod.GetByName(name);

        [HttpPost]
        public VMResponse Add(VMTblMPaymentMethod dataInput)=> paymentmethod.Add(dataInput);
        

        [HttpPut]
        public VMResponse Edit(VMTblMPaymentMethod dataInput) => paymentmethod.Edit(dataInput);


        [HttpDelete]
        public VMResponse Delete(long id, int userId) => paymentmethod.Delete(id, userId);

    }
}
