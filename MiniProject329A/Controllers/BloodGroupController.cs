using Microsoft.AspNetCore.Mvc;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;

namespace MiniProject329A.Controllers
{
    public class BloodGroupController : Controller
    {
        private readonly BloodGroupModel bloodGroup;
        private VMResponse? response;
        public BloodGroupController(IConfiguration _config)
        {
            bloodGroup = new BloodGroupModel(_config);
        }
        public async Task<IActionResult> Index(string? filter)
        {
            VMResponse? response;            
            
            if(filter == null)
            {
                response = await bloodGroup.GetAll();
            }
            else
            {
                response = await bloodGroup.GetByCode(filter);
            }
            
            ViewBag.Title = "Golongan Darah";
            return View(response.data);
        }
        public IActionResult Add()
        {
            ViewBag.Title = "Tambah Golongan Darah baru";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse?> Add(VMTblMBloodGroup formData)
        {
            VMResponse response = await bloodGroup.Create(formData);
            return response;
        }
        public async Task<IActionResult> Edit(long id)
        {
            VMTblMBloodGroup? data = await bloodGroup.GetById(id);

            ViewBag.Title = "Edit Golongan Darah";

            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse?> Edit(VMTblMBloodGroup formData)
        {
            VMResponse response = await bloodGroup.Edit(formData);
            return response;
        }
        public async Task<IActionResult> Delete(long id)
        {
            VMTblMBloodGroup response = await bloodGroup.GetById(id);

            ViewBag.Name = response.Code;
            ViewBag.Title = "Hapus Golongan Darah";
            return View(id);
        }
        [HttpPost]
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            VMResponse response = await bloodGroup.Delete(id, userId);

            return response;
        } 
    }
}