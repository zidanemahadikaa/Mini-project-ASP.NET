using Microsoft.AspNetCore.Mvc;
using MiniProject329A.Models;
using MiniProject329A.ViewModel;
using System.Net;

namespace MiniProject329A.Controllers
{
    public class LupaPasswordController : Controller
    {
        private readonly UserModel user;
        private readonly PasswordModel syaratPassword;
        private readonly ResetPasswordModel resetPassword;
        private readonly TokenModel token;
        private VMResponse? response;
        public LupaPasswordController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            user = new UserModel(_config, _webHostEnv);
            resetPassword = new ResetPasswordModel(_config);
            token = new TokenModel(_config);
            syaratPassword = new PasswordModel();
        }

        //Email Verification
        public IActionResult EmailVerif()
        {
            ViewBag.Title = "Lupa Password";
            return View();
        }
        public async Task<bool> CheckYourEmail(string email)
        {
            bool result = false;
            response = await user.GetEmail(email);
            if(response.message == "Email sudah digunakan")
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
            string usedFor = "Lupa Password";
            response = await token.AddToken(email, otp, usedFor);
            ViewBag.OtpCode = otp;
            ViewBag.Title = "Verifikasi E-Mail";

            DateTime dateNow = DateTime.Now;
            VMResponse dataResponse = await token.GetByEmail(email, otp);
            VMTblTToken data = (VMTblTToken)dataResponse.data;
            DateTime? dateEx = data.ExpiredOn;

            if (dateNow >= dateEx)
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

        public async Task<IActionResult> SetPassword(string email, string OTP, VMTblTToken dataToken)
        {
            response = await token.UpdateByOtpCode(dataToken);
            ViewBag.Email = email;
            ViewBag.OtpCode = OTP;
            ViewBag.Title = "Atur Password Baru";
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


        public async Task<VMResponse> InputData(VMTblMUser dataUser, string password)
        {
            VMResponse userResponse = await user.GetEmail(dataUser.Email);
            response = await user.UpdatePassword(dataUser);

            VMTblTResetPassword dataRP = new VMTblTResetPassword();
            dataRP.OldPassword = ((VMTblMUser)userResponse.data).Password;
            dataRP.NewPassword = password;
            dataRP.CreatedBy = ((VMTblMUser)userResponse.data).Id;
            dataRP.ResetFor = "Lupa Password";

            response = await resetPassword.ResetOldPassword(dataRP);
            if (response.statusCode == HttpStatusCode.Created)
            {
                HttpContext.Session.SetString("infoMsg", "Reset password berhasil");
            }
            else
            {
                HttpContext.Session.SetString("errMsg", "Reset Password gagal");
            }

            VMResponse? updateID = await token.UpdateUserID(dataUser.Email);
            return response;
        }
    }
}
