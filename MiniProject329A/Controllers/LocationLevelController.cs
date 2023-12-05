using Microsoft.AspNetCore.Mvc;
using MiniProject329A.AddOns;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;

namespace MiniProject329A.Controllers
{
    public class LocationLevelController : Controller
    {
        private readonly LocationLevelModel locationLevel;
        private readonly int pageSize;
        private VMResponse? response;

        public LocationLevelController(IConfiguration _config)
        {
            locationLevel = new LocationLevelModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }

        public async Task<IActionResult> Index(VMPageParam pageParam)
        {

            if (pageParam.filter == null)
            {
                response = await locationLevel.GetAll();
            }
            else
                response = await locationLevel.GetByName(pageParam.filter);
            List<VMTblMLocationLevel> data = (List<VMTblMLocationLevel>)response.data;
            switch (pageParam.orderBy)
            {
                case "name_desc":
                    data = data.OrderByDescending(pm => pm.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(pm => pm.Name).ToList();
                    break;
            }
            ViewBag.Title = "Level Lokasi";
            ViewBag.Filter = pageParam.filter;
            ViewBag.OrderBy = pageParam.orderBy;
            ViewBag.PageNumber = pageParam.pageNumber;

            ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
            return View(Pagination<VMTblMLocationLevel>.Create(data, pageParam.pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<VMResponse> Add(VMTblMLocationLevel formData)
        {
            VMResponse response = await locationLevel.Add(formData);
            return response;
        }

    }
}
