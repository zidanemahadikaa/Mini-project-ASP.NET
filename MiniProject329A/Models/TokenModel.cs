using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MiniProject329A.Models
{
    public class TokenModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        public TokenModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public string OtpCode(string email)
        {
            Random otp = new Random();
            string otpCode = otp.Next(1000, 9999).ToString();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ahmadzulamalzaini@gmail.com");
            mailMessage.To.Add(email);
            mailMessage.Subject = "OTP Verification Code";
            mailMessage.Body = $"Your otp verification code : {otpCode}";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 25;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("ahmadzulamalzaini@gmail.com", "kuit bgvs zkww sspy");
            smtpClient.EnableSsl = true;

            try
            {
                //smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                otpCode = "gagal";
            }


            return otpCode;
        }
        public async Task<VMResponse> AddToken(string email, string otp, string usedFor)
        {
            try
            {
                VMTblTToken data = new VMTblTToken();
                data.Email = email;
                data.Token = otp;
                data.UsedFor = usedFor;
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PostAsync(apiUrl + "api/Token/AddToken", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("API cannot be reached");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> UpdateByOtpCode(VMTblTToken data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/Token/UpdateByOtpCode", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("API cannot be reached");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> UpdateOtpEx(VMTblTToken data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/Token/UpdateOtpEx", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("API cannot be reached");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> GetByEmail(string email, string otp)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Token/GetByEmail/?email={email}&otp={otp}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblTToken?>(apiResponse.data.ToString());
                    }
                    else
                    {
                        apiResponse.data = null;
                    }
                }
                else
                {
                    throw new Exception("User API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = ex.Message;
            }
            return apiResponse;
        }
        public async Task<VMResponse> UpdateUserID(string email)
        {
            try
            {
                VMTblTToken data = new VMTblTToken();
                data.Email = email;
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/Token/UpdateUserID", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("API cannot be reached");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
    }
}
