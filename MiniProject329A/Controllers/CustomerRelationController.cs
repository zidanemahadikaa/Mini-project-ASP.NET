using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;

namespace MiniProject329A.Controllers
{
    public class CustomerRelationController : Controller
    {
        private readonly CustomerRelationModel customerRelation;
        private VMResponse? response;
        public CustomerRelationController(IConfiguration _config)
        {
            customerRelation = new CustomerRelationModel(_config);
        }
        public async Task<IActionResult> Index(string? filter)
        {
            VMResponse? response;
            if(filter == null)
            {
                response = await customerRelation.GetAll();
            }
            else
            {
                response = await customerRelation.GetByName(filter);
            }

            ViewBag.Title = "Hubungan Relasi";
            return View(response.data);
        }
        public IActionResult Add()
        {
            ViewBag.Title = "Tambah Hubungan Relasi";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse?> Add(VMTblMCustomerRelation formData)
        {
            VMResponse response = await customerRelation.Create(formData);
            return response;
        }
        public async Task<IActionResult> Edit(long id)
        {
            VMTblMCustomerRelation? data = await customerRelation.GetById(id);

            ViewBag.Name = data.Name;
            ViewBag.Title = "Edit Hubungan Relasi";
            return View(id);
        }
        [HttpPost]
        public async Task<VMResponse?> Edit(VMTblMCustomerRelation formData)
        {
            VMResponse response = await customerRelation.Edit(formData);
            return response;
        }
        public async Task<IActionResult> Delete(long id)
        {
            VMTblMCustomerRelation response = await customerRelation.GetById(id);

            ViewBag.Name = response.Name;
            ViewBag.Title = "Hapus Hubungan Relasi";
            return View(id);
        }
        [HttpPost]
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            VMResponse response = await customerRelation.Delete(id, userId);

            return response;
        }
    }
}
