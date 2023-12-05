using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;

namespace MiniProject329A.Controllers
{
    public class DokterController : Controller
    {
        private readonly DokterModel _dokter;
        private readonly UserModel _user;

        private readonly string imageFolder;
        private VMResponse? response;

        public DokterController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            _dokter = new DokterModel(_config, _webHostEnv);
            imageFolder = _config["ImageFolder"];

            _user = new UserModel(_config, _webHostEnv);
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> DokterProfile(int id)
        {
            response = await _user.GetById(id);
            
            VMResponse DokterResponse = await _dokter.GetBiodataId(((VMTblMUser)response.data).BiodataId);
            return View(DokterResponse.data);
        }
        //public async Task<IActionResult> DokterProfile(long id)
        //{
        //    try
        //    {
        //        response = await _dokter.GetById(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.data = new VMTblMDoctor();
        //        response.message = ex.Message;
        //    }

        //    ViewBag.Title = "Profile";
        //    ViewBag.imgFolder = imageFolder;

        //    return View();
        //}
        //public async Task<IActionResult> GantiFoto(long id)
        //{
        //    response = await _dokter.GetById(id);

        //    ViewBag.Title = "Edit Foto";
        //    ViewBag.imgFolder = imageFolder;

        //    return View(response.data);
        //}
        //[HttpPost]
        //public async Task<VMResponse?> GantiFoto(VMTblMBiodata formData)
        //{
        //    try
        //    {

        //        if (formData.ImageFile != null)
        //        {
        //            if (formData.ImagePath != null)
        //            {
        //                _dokter.DeleteOldImage(formData.ImagePath);
        //            }

        //            formData.ImagePath = _dokter.UploadFile(formData.ImageFile);
        //            formData.ImageFile = null;
        //        }

        //        response = await _dokter.Edit(formData);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);

        //    }
        //    return response;
        //}
    }
}
