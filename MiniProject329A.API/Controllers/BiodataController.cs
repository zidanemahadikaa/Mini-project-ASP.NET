using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BiodataController : Controller
    {
        private DABiodata biodata;
        public BiodataController(MiniProject329AContext _db)
        {
            biodata = new DABiodata(_db);
        }
        [HttpGet]
        public VMResponse GetAll()
        {
            return biodata.GetAll();
        }
        [HttpPost("[action]")]
        public VMResponse CreateBiodata(VMTblMBiodata formData)
        {
            return biodata.CreateBiodata(formData);
        }

        [HttpGet("{id?}")]
        public VMResponse GetById(int id)
        {
            return biodata.GetById(id);
        }
        [HttpGet("[action]")]
        public VMResponse GetByName(string? name)
        {
            return biodata.GetByName(name);
        }
        [HttpGet("[action]")]
        public VMResponse GetByLongId(long id)
        {
            return biodata.GetByLongId(id);
        }
        [HttpPut("[action]")]
        public VMResponse Update(VMTblMBiodata formData) => biodata.UpdateBiodata(formData);

    }
}
