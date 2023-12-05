using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MiniProject329A.AddOns;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;

namespace MiniProject329A.Controllers
{
    //public class CurrentDoctorSpecializationController : Controller
    //{
    //    private readonly CurrentDoctorSpecializationModel currentDoctorSpecialization;
    //    private readonly SpecializationModel specialization;
    //    //biodata blm
    //    private readonly int pageSize;
    //    private VMResponse? response;
    //    private ISession Session;

    //    public CurrentDoctorSpecializationController(IConfiguration _config)
    //    {
    //        currentDoctorSpecialization = new CurrentDoctorSpecializationModel(_config);
    //        specialization = new SpecializationModel(_config);
    //        pageSize = int.Parse(_config["PageSize"]);
    //    }
    //    //public async Task<IActionResult> Index(VMPageParam pageParam)
    //    //{
    //    //    VMResponse CuDoSpec = await currentDoctorSpecialization.GetAll();
    //    //    VMResponse Spec = await specialization.GetAll();
    //    //    if (pageParam.filter == null)
    //    //    {
    //    //        response = await specialization.GetAll();
    //    //    }
    //    //    else
    //    //        response = await specialization.GetByName(pageParam.filter);
    //    //    List<VMTblMSpecialization> data = (List<VMTblMSpecialization>)response.data;
    //    //    switch (pageParam.orderBy)
    //    //    {
    //    //        case "name_desc":
    //    //            data = data.OrderByDescending(pm => pm.Name).ToList();
    //    //            break;
    //    //        default:
    //    //            data = data.OrderBy(pm => pm.Name).ToList();
    //    //            break;
    //    //    }
    //    //    ViewBag.CuDoSpec = CuDoSpec.data;
    //    //    ViewBag.Spec = Spec;
    //    //    ViewBag.Title = "Current Doctor Specialization";
    //    //    ViewBag.Filter = pageParam.filter;
    //    //    ViewBag.OrderBy = pageParam.orderBy;
    //    //    ViewBag.PageNumber = pageParam.pageNumber;
    //    //    ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
    //    //    return View(Pagination<VMTblMSpecialization>.Create(data, pageParam.pageNumber ?? 1, pageSize));
    //    //}
    //}
}
