using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;
using System.Globalization;
using MiniProject329A.AddOns;

namespace MiniProject329A.Controllers
{
    public class PasienController : Controller
    {
        private readonly ProfileModel _user;
        private readonly BiodataModel _biodata;
        private readonly CustomerModel _customer;
        private readonly CustomerMemberModel _cusMember;
        private readonly BloodGroupModel _bloodGroup;
        private readonly CustomerRelationModel _customerRelation;
        private readonly AppointmentDoneModel _appointmentDone;
        private readonly AppointmentModel _appointment;
        private readonly PasienUpdateModel _pasienUpdate;
        private readonly DoctorOfficeModel _doctorOffice;
        private readonly int pageSize;
        private CultureInfo ID = new CultureInfo("id-ID");

        private readonly string imageFolder;
        private VMResponse? response;
        private MenuRoleModal MenuRole;

        public PasienController(IConfiguration _config, IWebHostEnvironment _webHostEnv) //kontraktors
        {
            _user = new ProfileModel(_config, _webHostEnv);
            _biodata = new BiodataModel(_config);
            _customer = new CustomerModel(_config);
            _cusMember = new CustomerMemberModel(_config);
            _bloodGroup = new BloodGroupModel(_config);
            _customerRelation = new CustomerRelationModel(_config);
            _appointmentDone = new AppointmentDoneModel(_config);
            _appointment = new AppointmentModel(_config);
            _pasienUpdate = new PasienUpdateModel(_config);
            _doctorOffice = new DoctorOfficeModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
            imageFolder = _config["ImageFolder"];
            MenuRole = new MenuRoleModal(_config);
        }
        public async Task<IActionResult> Index(long Id)
        {
            List<VMTblMMenuRole>? data;
            Id = 3;
                data = await MenuRole.GetByRoleId(Id);
            

            ViewBag.Title = "Menu Pasien";
            return View(data);
        }
        public async Task<IActionResult> PasienProfile(int id)
        {
            try
            {
                response = await _user.GetById(id);
                VMResponse bioResponse = await _biodata.GetByLongId(id);
                VMTblMCustomer dataCustomer = await _customer.GetByBioId(id);
                ViewBag.Email = ((VMTblMUser)response.data).Email;
                ViewBag.Password = ((VMTblMUser)response.data).Password;
                ViewBag.HP = ((VMTblMBiodata)bioResponse.data).MobilePhone;
                //ViewBag.DOB = dataCustomer.Dob;
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
        public async Task<IActionResult> AddPasien(int id)
        {
            VMResponse? bloodGroupResponse = await _bloodGroup.GetAll();
            VMResponse? customerRelationResponse = await _customerRelation.GetAll();
            response = await _user.GetById(id);

            ViewBag.ParentId = ((VMTblMUser)response.data).BiodataId;
            ViewBag.UserId = ((VMTblMUser)response.data).Id;
            ViewBag.BloodGroup = bloodGroupResponse.data;
            ViewBag.CustomerRelation = customerRelationResponse.data;

            ViewBag.Title = "Tambah Pasien";

            return View(response.data);
        }
        [HttpPost]
        public async Task<VMResponse?> Add(VMTblMBiodata bio, VMTblMCustomer customerData, VMTblMCustomerMember customerMemberData)
        {
            VMResponse? bioResponse = await _biodata.CreateBiodata(bio);
            long? bioId = ((VMTblMBiodata)bioResponse.data).Id;
            customerData.BiodataId = bioId;
            VMResponse? cusResponse = await _customer.CreateCustomer(customerData);
            long? cusId = ((VMTblMCustomer)cusResponse.data).Id;
            customerMemberData.CustomerId = cusId;
            response = await _cusMember.CreateCustomerMember(customerMemberData);

            if (response.statusCode == HttpStatusCode.Created)
            {
                HttpContext.Session.SetString("infoMsg", response.message);
            }
            else
            {
                HttpContext.Session.SetString("errMsg", "Gagal Tambah Pasien");
            }

            return response;
        }
        public IActionResult TabAlamat()
        {
            return View();
        }
        public async Task<IActionResult> ListPasien(int id, VMPageParam pageParam, int? pageSizeNumber)
        {
            VMResponse response;
            response = await _user.GetById(id);

            if(pageParam.filter == null)
            {
                long? bioId = ((VMTblMUser)response.data).BiodataId;
                if (pageParam.orderBy == null)
                {
                    response = await _cusMember.GetByParentId(bioId);
                }
                else
                {
                    response = await _cusMember.GetByCustomerName(bioId, pageParam.filter);
                }
            }
            else
            {
                long? bioId = ((VMTblMUser)response.data).BiodataId;
                response = await _cusMember.GetByCustomerName(bioId, pageParam.filter);
            }
            VMResponse customer = await _customer.GetAll();
            
            List<VMTblMCustomerMember> data = (List<VMTblMCustomerMember>)response.data;
            List<VMTblMCustomer> dataCustomer = (List<VMTblMCustomer>)customer.data;

            switch (pageParam.orderBy)
            {                
                case "name_desc":
                    data = data.OrderByDescending(n => n.CustomerName).ToList();
                    break;
                case "name":
                    data = data.OrderBy(n => n.CustomerName).ToList();
                    break;
                case "age_desc":
                    data = data.OrderByDescending(a => a.DobCustomer).ToList();
                    break;
                case "age":
                    data = data.OrderBy(a => a.DobCustomer).ToList();
                    break;
                case "cr_desc":
                    data = data.OrderByDescending(cr => cr.CustomerRelationName).ToList();
                    break;
                case "cr":
                    data = data.OrderBy(cr => cr.CustomerRelationName).ToList();
                    break;
                case "add_desc":
                    data = data.OrderByDescending(add => add.CreateOn).ToList();
                    break;
                default:
                    data = data.OrderBy(add => add.CreateOn).ToList();
                    break;
            }
            ViewBag.OrderCR = pageParam.orderBy == "cr" ? "cr_desc" : "cr";
            ViewBag.OrderAdd = pageParam.orderBy == "" ? "add_desc" : "";
            ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
            ViewBag.OrderAge = pageParam.orderBy == "age" ? "age_desc" : "age";
            ViewBag.Name = dataCustomer;
            ViewBag.Dob = customer.data;
            ViewBag.Title = "List Pasien";

            
            
            return View(Pagination<VMTblMCustomerMember>.Create(data, pageParam.pageNumber ?? 1, pageSizeNumber ?? pageSize ));
        }
        public async Task<IActionResult> EditPasien(int id)
        {
            VMResponse? bloodGroupResponse = await _bloodGroup.GetAll();
            VMResponse? customerRelationResponse = await _customerRelation.GetAll();
            VMResponse cusMemberResponse = await _cusMember.GetById(id);
            long? findCusId = ((VMTblMCustomerMember)cusMemberResponse.data).CustomerId;
            long? findParId = ((VMTblMCustomerMember)cusMemberResponse.data).ParentBiodataId;
            VMResponse? customerResponse = await _customer.GetById(findCusId);
            long? findBioId = ((VMTblMCustomer)customerResponse.data).BiodataId;
            VMResponse? bioResponse = await _biodata.GetByLongId(findBioId);

            response = await _cusMember.GetById(id);

            ViewBag.ParentId = findParId;
            ViewBag.CusMemId = ((VMTblMCustomerMember)cusMemberResponse.data).Id;
            ViewBag.BioId = ((VMTblMBiodata)bioResponse.data).Id;
            ViewBag.CustomerName = ((VMTblMBiodata)bioResponse.data).Fullname;
            ViewBag.CustomerId = ((VMTblMCustomer)customerResponse.data).Id;
            ViewBag.CustomerDob = ((VMTblMCustomer)customerResponse.data).Dob;
            ViewBag.CustomerBG = ((VMTblMCustomer)customerResponse.data).BloodGroupId;
            ViewBag.BloodGroup = bloodGroupResponse.data;
            ViewBag.CustomerRelation = customerRelationResponse.data;

            ViewBag.Title = "Edit Pasien";

            return View(response.data);
        }
        [HttpPost]
        public async Task<VMResponse?> Edit(VMTblMBiodata bio, VMTblMCustomer customerData, VMTblMCustomerMember customerMemberData, long id)
        {
            VMResponse? findCusMemId = await _pasienUpdate.GetById(id);
            long? getCusId = ((VMTblMCustomerMember)findCusMemId.data).CustomerId;
            VMResponse? GetCusId = await _pasienUpdate.GetByCusId(getCusId);
            long? getBioId = ((VMTblMCustomer)GetCusId.data).BiodataId;

            bio.Id = getBioId??0;
            customerData.Id = getCusId ?? 0;
            customerMemberData.Id = id;

            VMResponse? bioResponse = await _pasienUpdate.UpdateName(bio);
            long? bioId = ((VMTblMBiodata)bioResponse.data).Id;
            customerData.BiodataId = bioId;
            VMResponse? cusResponse = await _pasienUpdate.UpdateCustomer(customerData);
            long? cusId = ((VMTblMCustomer)cusResponse.data).Id;
            customerMemberData.CustomerId = cusId;
            response = await _pasienUpdate.UpdateCustomerMember(customerMemberData);

            if (response.statusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("infoMsg", "Pasien berhasil ditambah");
            }
            else
            {
                HttpContext.Session.SetString("errMsg", "Gagal Tambah Pasien");
            }

            return response;
        }
        public async Task<IActionResult> DeletePasien(long id)
        {
            VMResponse cusMemberResponse = await _cusMember.GetById(id);
            long? findCusId = ((VMTblMCustomerMember)cusMemberResponse.data).CustomerId;
            VMResponse? customerResponse = await _customer.GetById(findCusId);

            ViewBag.Name = ((VMTblMCustomer)customerResponse.data).Fullname;
            ViewBag.Title = "Hapus Pasien";
            return View(id);
        }
        [HttpPost]
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            VMResponse response = await _cusMember.Delete(id, userId);

            return response;
        }
        public async Task<IActionResult> MultipleDeletePasien(List<int> listId)
        {
            List<string> pasienName = new List<string>();
            foreach (int id in listId)
            {
                VMResponse cusMemberResponse = await _cusMember.GetById(id);
                long? findCusId = ((VMTblMCustomerMember)cusMemberResponse.data).CustomerId;
                VMResponse? customerResponse = await _customer.GetById(findCusId);
                long? findBioId = ((VMTblMCustomer)customerResponse.data).BiodataId;
                VMResponse? bioResponse = await _biodata.GetByLongId(findBioId);
                string? name = ((VMTblMBiodata)bioResponse.data).Fullname;
                pasienName.Add(name);
            }
            ViewBag.Name = pasienName;
            ViewBag.Title = "Hapus Pasien";
            return View(listId);
        }
        [HttpPost]
        public async Task<VMResponse> MultipleDelete(List<int> listId, long userId)
        {
            try
            {
                foreach(int id in listId)
                {
                    response = await _cusMember.Delete(id, userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
        public async Task<IActionResult> AppointmentHistoryPasien(int id, VMPageParam pageParam, int? pageSizeNumber)
        {
            VMResponse response;
            VMResponse userResponse = await _user.GetById(id);

            if(pageParam.filter == null)
            {
                long? bioId = ((VMTblMUser)userResponse.data).BiodataId;
                if(pageParam.orderBy == null)
                {
                    response = await _appointment.GetByParentId(bioId);
                }
                else
                {
                    response = await _appointment.GetByCustomerName(bioId, pageParam.filter);
                }
            }
            else
            {
                long? bioId = ((VMTblMUser)userResponse.data).BiodataId;
                response = await _appointment.GetByCustomerName(bioId, pageParam.filter);
            }

            VMResponse doctorOfficeResponse = await _doctorOffice.GetAll();

            List<VMTblTAppointment> data = (List<VMTblTAppointment>)response.data;

            switch (pageParam.orderBy)
            {
                case "name_desc":
                    data = data.OrderByDescending(n => n.CustomerName).ToList();
                    break;
                case "name":
                    data = data.OrderBy(n => n.CustomerName).ToList();
                    break;
                case "add_desc":
                    data = data.OrderByDescending(add => add.CreatedOn).ToList();
                    break;
                case "add":
                    data = data.OrderBy(add => add.CreatedOn).ToList();
                    break;
                case "app_desc":
                    data = data.OrderByDescending(app => app.AppointmentDate).ToList();
                    break;
                default:
                    data = data.OrderBy(app => app.AppointmentDate).ToList();
                    break;
            }
            ViewBag.OrderApp = pageParam.orderBy == "" ? "app_desc" : "";
            ViewBag.OrderName = pageParam.orderBy == "name" ? "name_desc" : "name";
            ViewBag.OrderAdd = pageParam.orderBy == "add" ? "add_desc" : "add";
            ViewBag.DoctorName = doctorOfficeResponse.data;
            ViewBag.RSName = doctorOfficeResponse.data;
            ViewBag.OrderBy = pageParam.orderBy;
            ViewBag.Filter = pageParam.filter;

            return View(Pagination<VMTblTAppointment>.Create(data, pageParam.pageNumber ?? 1, pageSizeNumber ?? pageSize));
        }
    }
}
               