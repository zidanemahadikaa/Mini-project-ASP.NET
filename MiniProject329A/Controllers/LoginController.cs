using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;

namespace MiniProject329A.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserModel user;
        private VMResponse? response;
        private readonly BiodataModel biodata;
        public LoginController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            user = new UserModel(_config, _webHostEnv);
            biodata = new BiodataModel(_config);
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Login";
            return View();
        }
        public IActionResult Button()
        {
            return View();
        }
        [HttpGet]
        public async Task<VMResponse> Login(string email, string password, VMTblMUser data)
        {
            response = await user.Login(email, password);

            VMTblMUser dataUser = (VMTblMUser)response.data;
            VMResponse bioResponse = await biodata.GetByLongId(dataUser.BiodataId);

            if(response.statusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetInt32("UserId", (int)dataUser.Id);
                HttpContext.Session.SetString("UserEmail", data.Email);
                HttpContext.Session.SetString("UserNama", ((VMTblMBiodata)bioResponse.data).Fullname);
                HttpContext.Session.SetInt32("UserRole", (int)dataUser.RoleId);
                HttpContext.Session.SetString("infoMsg", "Login berhasil");

                if (((VMTblMUser)response.data).LoginAttempt > 0)
                {
                    VMResponse resetResponse = await user.ResetLoginAttemp(data);
                }
                else if(((VMTblMUser)response.data).LoginAttempt == 0) 
                {
                    response.statusCode = HttpStatusCode.NoContent;
                    HttpContext.Session.Remove("infoMsg");
                    HttpContext.Session.SetString("errMsg", "Akun terkunci, silahkan hubungi admin");
                }
            }
            else
            {
                VMTblMUser? dataLA = await user.EmailLogin(email);
                VMResponse LAresponse = await user.LoginAttemp(dataLA);
                HttpContext.Session.SetString("errMsg", "Login gagal!!!  Email dan Password tidak valid");
            }
            return response;
        }
        public IActionResult KonfirmKeluar()
        {
            return View();
        }
        public VMResponse Logout()
        {
            HttpContext.Session.Clear();

            response = new VMResponse();
            response.statusCode = HttpStatusCode.OK;
            response.message = "Logout berhasil";
            HttpContext.Session.SetString("infoMsg", response.message);

            return response;
        }


    }
}
