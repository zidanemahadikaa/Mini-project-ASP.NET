using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace MiniProject329A.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationLevelController : Controller
    {
        private DAEducationLevel EducationLevel;

        public EducationLevelController(MiniProject329AContext _db)
        {
            EducationLevel = new DAEducationLevel(_db);
        }

        //Get All Data
        [HttpGet()]
        public VMResponse GetAll()
        {
            return EducationLevel.GetByName();
        }

        //Create Data
        [HttpPost]
        public VMResponse Create(VMTblMEducationLevel dataInput) => EducationLevel.CreateUpdate(dataInput);

        //Read Data by Id
        [HttpGet("[action]/{id?}")]
        public VMResponse FindById(int? id = null) => EducationLevel.GetById(id);

        //Read Data by Name
        [HttpGet("[action]/{name?}")]
        public VMResponse GetByName(string? name = null) => EducationLevel.GetByName(name);

        //Update Data (Masih Diperbaiki)
        [HttpPut]
        public VMResponse Update(VMTblMEducationLevel dataInput) => EducationLevel.CreateUpdate(dataInput);

        //Delete Data
        [HttpDelete]
        public VMResponse Delete(long id, int userId) => EducationLevel.Delete(id, userId);

        //Validation for name if exit
        [HttpGet("[action]")]
        public VMResponse NameCheck(string name) => EducationLevel.NameCheck(name);
    }
}
