using Microsoft.AspNetCore.Mvc;
using MiniProject329A.AddOns;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;

namespace MiniProject329A.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly SpecializationModel speciallize;
        private readonly int pageSize;
        private VMResponse? response;

        public SpecializationController(IConfiguration _config)
        {
            speciallize = new SpecializationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }
        public async Task<IActionResult> Index(VMPageParam pageParam)
        {

            if (pageParam.filter == null)
            {
                response = await speciallize.GetAll();
            }
            else
                response = await speciallize.GetByName(pageParam.filter);
            List<VMTblMSpecialization> data = (List<VMTblMSpecialization>)response.data;
            switch (pageParam.orderBy)
            {
                case "name_desc":
                    data = data.OrderByDescending(pm => pm.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(pm => pm.Name).ToList();
                    break;
            }
            ViewBag.Title = "Spesialis Dokter";
            ViewBag.Filter = pageParam.filter;
            ViewBag.OrderBy = pageParam.orderBy;
            ViewBag.PageNumber = pageParam.pageNumber;

            ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
            return View(Pagination<VMTblMSpecialization>.Create(data, pageParam.pageNumber ?? 1, pageSize));
        }
    }
}
