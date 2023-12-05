using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;

namespace MiniProject329A.Controllers
{
    public class FaskesController : Controller
    {
        private readonly ProfileModel _user;

        private readonly string imageFolder;
        private VMResponse? response;

        public FaskesController(IConfiguration _config, IWebHostEnvironment _webHostEnv) //kontraktors
        {
            _user = new ProfileModel(_config, _webHostEnv);
            imageFolder = _config["ImageFolder"];
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminProfile(int id)
        {
            try
            {
                response = await _user.GetById(id);
            }
            catch (Exception ex)
            {
                response.data = new VMTblMUser();
                response.message = ex.Message;

            }

            ViewBag.Title = "Profile";
            ViewBag.imgFolder = imageFolder;

            return View(response.data);
        }
        public async Task<IActionResult> GantiFoto(int id)
        {
            response = await _user.GetById(id);


            ViewBag.Title = "Edit foto";
            ViewBag.ImgFolder = imageFolder;

            return View(response.data);
        }
        [HttpPost]
        public async Task<VMResponse?> GantiFoto(VMTblMBiodata formData)
        {
            try
            {

                if (formData.ImageFile != null)
                {
                    if (formData.ImagePath != null)
                    {
                        _user.DeleteOldImage(formData.ImagePath);
                    }

                    formData.ImagePath = _user.UploadFile(formData.ImageFile);
                    formData.ImageFile = null;
                }

                response = await _user.Edit(formData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return response;
        }
    }
}
