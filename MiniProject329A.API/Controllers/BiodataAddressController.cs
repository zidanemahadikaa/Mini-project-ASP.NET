using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BiodataAddressController : Controller
    {
        private DABiodataAddress biodataAddress;

        public BiodataAddressController(MiniProject329AContext _db)
        {
            biodataAddress = new DABiodataAddress(_db);
        }
        [HttpGet()]
        public VMResponse GetAll() => biodataAddress.GetByName();
    }
}
