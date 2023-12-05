using Microsoft.AspNetCore.Mvc;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;

namespace MiniProject329A.Controllers
{
    public class EducationLevelController : Controller
    {
        private readonly EducationLevelModel educationLevel;
        private VMResponse? response;

        public EducationLevelController(IConfiguration _config)
        {
            educationLevel = new EducationLevelModel(_config);
        }
        //Read all and read by filter process
        public async Task<IActionResult> Index(string? filter)
        {
            VMResponse? response;
            if(filter == null)
            {
                response = await educationLevel.GetAll();
            }
            else
            {
                response = await educationLevel.GetByName(filter);
            }
            ViewBag.Title = "Education Level List";

            return View(response.data);
        }

        //Add Proses
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<VMResponse?> Add(VMTblMEducationLevel formData)
        {
            response = await educationLevel.Create(formData);

            HttpContext.Session.SetString((response?.statusCode == HttpStatusCode.Created) ? "infoMsg" : "errMsg", response?.message);

            return response;
        }

        //Edit Proses
        public async Task<IActionResult> Edit(long id)
        {
            VMTblMEducationLevel? data = await educationLevel.FindById(id);

            ViewBag.Title = "Education Level Edit";

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse?> Edit(VMTblMEducationLevel data)
        {
            response = await educationLevel.Edit(data);
            if(response.statusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("infoMsg", response.message);
            }
            else
            {
                HttpContext.Session.SetString("errMsg", response.message);
            }
            return response;
        }

        //Delete Proses
        public async Task<IActionResult> Delete(long id)
        {
            VMTblMEducationLevel data = await educationLevel.FindById(id);
            ViewBag.name = data.Name;
            ViewBag.id = data.Id;

            ViewBag.Title = "Menghapus Level Edukasi";

            return View();
        }

        [HttpPost]
        public async Task<VMResponse?> DeleteAsync(long id, int userId)
        {
            VMResponse response = await educationLevel.Delete(id, userId);

            if (response.statusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("infoMsg", response.message);
            }
            else
            {
                HttpContext.Session.SetString("errMsg", response.message);
            }

            return response;
        }

        //Validate education level
        public bool ValidasiPendidikan(string name)
        {
            bool LevelEdukasi = false;

            List<string> Validasi = new List<string> {"TK","PAUD","SD","SMP","SMA","SMK","MAN","D1","D2","D3","D4","S1","S2","S3","tk","paud","sd","smp","sma","smk","man","d1","d2","d3","d4","s1","s2","s3"};

            foreach(string val in Validasi)
            {
                if(val == name)
                {
                    LevelEdukasi = true;
                    break;
                }
                else
                {
                    LevelEdukasi = false;
                }
            }

            return LevelEdukasi;
        }

        //Validasi ketika data sudah tersedia
        public async Task<bool> ValidasiIfExist(string name)
        {
            bool LevelEdukasi = false;

            response = await educationLevel.ValidasiIfExist(name);

            if(response.message == "Jenjang pendidikan belum digunakan")
            {
                LevelEdukasi = true;
            }
            else
            {
                LevelEdukasi = false;
            }

            return LevelEdukasi;
        }
    }
}
