using Microsoft.AspNetCore.Mvc;
using MiniProject329A.DataAccess;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;
using static System.Net.WebRequestMethods;

namespace MiniProject329A.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserModel user;
        private readonly PasswordModel syaratPassword;
        private readonly BiodataModel biodata;
        private readonly TokenModel token;
        private VMResponse? response;
        public RegisterController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            user = new UserModel(_config, _webHostEnv);
            biodata = new BiodataModel(_config);
            token = new TokenModel(_config);
            syaratPassword = new PasswordModel();
        }
        public IActionResult Button()
        {
            ViewBag.Title = "Register";

            return View();
        }


        //Email Verification
        public IActionResult EmailVerif()
        {
            ViewBag.Title = "Register";
            return View();
        }
        public async Task<bool> CheckYourEmail(string email)
        {
            bool result = false;
            response = await user.GetEmail(email);
            if (response.message == "Email belum digunakan")
            {
                result = true;
            }
            return result;
        }



        //Send and resend otp code
        public async Task<IActionResult> OtpCode(string email)
        {
            ViewBag.Email = email;
            string otp = token.OtpCode(email);
            string usedFor = "Register";
            response = await token.AddToken(email, otp, usedFor);
            ViewBag.OtpCode = otp;
            ViewBag.Title = "Verifikasi E-Mail";

            DateTime dateNow = DateTime.Now;
            VMResponse dataResponse = await token.GetByEmail(email, otp);
            VMTblTToken data = (VMTblTToken)dataResponse.data;
            DateTime? dateEx = data.ExpiredOn;

            if ( dateNow >= dateEx)
            {
                VMTblTToken dataToken = new VMTblTToken();
                dataToken.Email = email;
                dataToken.Token = otp;
                response = await token.UpdateOtpEx(dataToken);
            }
            return View();
        }
        public async Task<IActionResult> OtpResend(string email, VMTblTToken dataToken)
        {
            response = await token.UpdateByOtpCode(dataToken);

            string otp = token.OtpCode(email);
            ViewBag.OtpCode = otp;
            ViewBag.Email = email;
            ViewBag.Title = "Verifikasi E-Mail";

            DateTime dateNow = DateTime.Now;
            VMResponse dataResponse = await token.GetByEmail(email, otp);
            VMTblTToken data = (VMTblTToken)dataResponse.data;
            DateTime? dateEx = data.ExpiredOn;

            if (dateNow >= dateEx)
            {
                VMTblTToken dataTokenBaru = new VMTblTToken();
                dataTokenBaru.Email = email;
                dataTokenBaru.Token = otp;
                response = await token.UpdateOtpEx(dataTokenBaru);
            }

            return View();
        }



        //SetPassword
        public async Task<IActionResult> SetPassword(string email, string OTP, VMTblTToken dataToken)
        {
            response = await token.UpdateByOtpCode(dataToken);
            ViewBag.Email = email;
            ViewBag.OtpCode = OTP;
            ViewBag.Title = "Set your password";
            return View();
        }
        public bool KurangHurufK(string password)
        {
            bool komen = syaratPassword.KurangHurufKecil(password);
            return komen;
        }
        public bool KurangHurufB(string password)
        {
            bool komen = syaratPassword.KurangHurufBesar(password);
            return komen;
        }
        public bool KurangSimbol(string password)
        {
            bool komen = syaratPassword.KurangSimbol(password);
            return komen;
        }
        public bool KurangAngka(string password)
        {
            bool komen = syaratPassword.KurangAngka(password);
            return komen;
        }


        //Set Biodata
        public async Task<IActionResult> Biodata(string email, string OTP, string password, string passVerif)
        {
            ViewBag.Email = email;
            ViewBag.OtpCode = OTP;
            ViewBag.Password = password;
            ViewBag.Role = await user.GetAllRole();

            ViewBag.Title = "Insert Biodata";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse> InputData(VMTblMUser dataUser, VMTblMBiodata dataBio)
        {
            VMResponse? bioResponse = await biodata.CreateBiodata(dataBio);
            response = await user.Register(dataUser);

            if(response.statusCode == HttpStatusCode.Created)
            {
                HttpContext.Session.SetString("infoMsg", response.message);
            }
            else
            {
                HttpContext.Session.SetString("errMsg", "Pendaftaran gagal");
            }

            VMResponse? updateID = await token.UpdateUserID(dataUser.Email);
            return response;
        }
    }
}
