using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class updatebiocusController : Controller
    {
        private DaUpdateName name;
        public updatebiocusController(MiniProject329AContext _db)
        {
            name = new DaUpdateName(_db);
        }


        [HttpGet("[action]")]
        public VMResponse GetCusmemberId(long id)=> name.GetById(id);

        [HttpGet("[action]")]
        public VMResponse GetCusId(long id) => name.GetByIdcus(id);

        [HttpGet("[action]")]
        public VMResponse GetIdBio(long id) => name.GetByIdbio(id);

        [HttpPut("[action]")]
        public VMResponse Updatename(VMTblMBiodata data) => name.UpdateBiodata(data);
        [HttpPut("[action]")]
        public VMResponse UpdateCustomer(VMTblMCustomer data) => name.UpdateCustomer(data);
        [HttpPut("[action]")]
        public VMResponse UpdateCustomerMember(VMTblMCustomerMember data) => name.UpdateCustomerMember(data);
    }
}
