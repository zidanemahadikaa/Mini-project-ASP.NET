using Microsoft.AspNetCore.Mvc;
using MiniProject329A.AddOns;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Controllers
{
    public class LocationController : Controller
    {
        private readonly LocationModel location;
        private readonly LocationLevelModel locationLevel;
        private readonly BiodataAddressModel biodataAddress;
        private readonly MedicalFacilityModel medicalFacility;
        private readonly int pageSize;
        private VMResponse? response;
        private ISession Session;

        public LocationController(IConfiguration _config)
        {
            location = new LocationModel(_config);
            locationLevel = new LocationLevelModel(_config);
            biodataAddress = new BiodataAddressModel(_config);
            medicalFacility = new MedicalFacilityModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }
        
        public async Task<IActionResult> Index(VMPageParam pageParam)
        {
            VMResponse levellocationresponse = await locationLevel.GetAll();
            VMResponse locationResponse = await location.GetAll();
            VMResponse biodataAddressResponse = await biodataAddress.GetAll();
            VMResponse medFac = await medicalFacility.GetAll();
            //GetByNameLevel Belum Ditambahkan
            if (pageParam.filter == null)
            {
                response = await location.GetAll();
            }
            else
            response = await location.GetByName(pageParam.filter);
            List<VMTblMLocation> data = (List<VMTblMLocation>)response.data;
            switch (pageParam.orderBy)
            {
                case "name_desc":
                    data = data.OrderByDescending(pm => pm.Name).ToList();
                    break;
                default:
                    data = data.OrderBy(pm => pm.Name).ToList();
                    break;
            }
            ViewBag.MedicalFacility = medFac.data;
            ViewBag.BiodataAddress = biodataAddressResponse.data;
            ViewBag.LocationLevel = levellocationresponse.data;
            ViewBag.Location = locationResponse.data;
            ViewBag.Title = "Lokasi";
            ViewBag.Filter = pageParam.filter;
            ViewBag.OrderBy = pageParam.orderBy;
            ViewBag.PageNumber = pageParam.pageNumber;
            ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
            return View(Pagination<VMTblMLocation>.Create(data, pageParam.pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Add()
        {
            VMResponse levellocationresponse = await locationLevel.GetAll();
            VMResponse locationResponse = await location.GetAll();
            ViewBag.LocationLevel = levellocationresponse.data;
            ViewBag.Location = locationResponse.data;
            ViewBag.Title = "Tambah Lokasi";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> Add(VMTblMLocation formData)
        {
            response = await location.Add(formData);
            return response;
        }
        public async Task<IActionResult> Edit(long id)
        {
            VMTblMLocation? data = await location.GetById(id);
            VMResponse levellocationresponse = await locationLevel.GetAll();
            VMResponse locationResponse = await location.GetAll();
            ViewBag.LocationLevel = levellocationresponse.data;
            ViewBag.Location = locationResponse.data;
            ViewBag.Title = "Edit";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse?> Edit(VMTblMLocation data)
        {
            VMResponse response = await location.Edit(data);
            return response;
        }


        public async Task<VMResponse> GetLocationsBylocationLevel(long getId)
        {
            try
            {
                response = await location.GetByLocationAdd(getId);

                if (response.statusCode == HttpStatusCode.OK)
                {
                    string jsonData = JsonConvert.SerializeObject(response.data);
                    response.data = JsonConvert.DeserializeObject<List<VMTblMLocation>>(jsonData);

                }
                else
                {
                    throw new Exception(response.message);
                }
            }
            catch (Exception ex)
            {
                response.data = new List<VMTblMLocation>();
            }
            return response;
        }

        public async Task<IActionResult> Delete(long id)
        {
            VMTblMLocation? data = await location.GetById(id);
            VMResponse levellocationresponse = await locationLevel.GetAll();
            VMResponse locationResponse = await location.GetAll();
            VMResponse medFac = await medicalFacility.GetAll();
            VMResponse biodataAddressResponse = await biodataAddress.GetAll();
            ViewBag.MedicalFacility = medFac.data;
            ViewBag.BiodataAddress = biodataAddressResponse.data;
            ViewBag.LocationLevel = levellocationresponse.data;
            ViewBag.Location = locationResponse.data;
            ViewBag.Title = "Hapus Lokasi";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            VMResponse response = await location.Delete(id, userId);
            return response;
        }

    }
}
