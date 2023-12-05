using MessagePack;
using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MiniProject329A.Models
{
    public class UserModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        private readonly IWebHostEnvironment webHostEnv;
        private readonly string imageFolder;
        public UserModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            apiUrl = _config["ApiUrl"];
            webHostEnv = _webHostEnv;
            imageFolder = _config["ImageFolder"];
        }
        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + "api/User"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMUser?>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
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
        public async Task<List<VMTblMRole>> GetAllRole()
        {
            List<VMTblMRole>? data = new List<VMTblMRole>();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + "api/Role"));

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTblMRole>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("API cannot be reached");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }
        public async Task<VMResponse> Register(VMTblMUser data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PostAsync(apiUrl + "api/User", content)).Content.ReadAsStringAsync());

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
        
        public async Task<VMResponse> GetEmail(string email)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/User/EmailCheck/?email={email}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMUser?>(apiResponse.data.ToString());
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
        public string PassValid(string password)
        {
            string hurufK = "abcdefghijklmnopqrstuvwxyz";
            string hurufB = hurufK.ToUpper();
            string simbol = "~!@#$%^&*";
            string angka = "1234567890";
            string komen;

            bool isHurufK = false, isHurufB = false, isSimbol = false, isAngka = false, isKurangPanjang = false;

            isHurufK = !hurufK.Any(a => password.Any(b => a == b));
            isHurufB = !hurufB.Any(a => password.Any(b => a == b));
            isSimbol = !simbol.Any(a => password.Any(b => a == b));
            isAngka = !angka.Any(a => password.Any(b => a == b));
            isKurangPanjang = (password.Length < 6);

            if (isHurufK || isHurufB || isSimbol || isAngka || isKurangPanjang)
            {
                komen = "tidak valid";
            }
            else { komen="valid"; }

            return komen;
        }
        public async Task<VMResponse> Login(string email, string password)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(
                    await httpClient.GetStringAsync(apiUrl + $"api/User/Login/?email={email}&password={password}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMUser?>(apiResponse.data.ToString());
                    }
                    else
                    {
                        VMTblMUser emailData = new VMTblMUser();
                        emailData.Email = email;
                        apiResponse.data = JsonConvert.SerializeObject(emailData);
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMUser?>(apiResponse.data.ToString());
                    }
                }
                else
                {
                    throw new Exception("Tidak bisa terhubung ke API user");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = ex.Message;
            }
            return apiResponse;
        }
        public async Task<VMTblMUser> EmailLogin(string email)
        {
            VMTblMUser? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/User/EmailCheck/?email={email}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMUser?>(apiResponse.data.ToString());
                    }
                    else
                    {
                        VMTblMUser emailData = new VMTblMUser();
                        emailData.Email = null;
                        apiResponse.data = JsonConvert.SerializeObject(emailData);
                        data = JsonConvert.DeserializeObject<VMTblMUser?>(apiResponse.data.ToString());
                    }
                }
                else
                {

                    data = null;
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = ex.Message;
            }
            return data;
        }
        public async Task<VMResponse> LoginAttemp(VMTblMUser data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/User/LoginAttemp", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Variants API cannot be reached");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> ResetLoginAttemp(VMTblMUser data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/User/ResetLoginAttemp", content)).Content.ReadAsStringAsync());

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
        public async Task<VMResponse> UpdatePassword(VMTblMUser data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/User/UpdatePassword", content)).Content.ReadAsStringAsync());

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

        public async Task<VMResponse?> GetById(int id)
        {
            
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + $"api/User/{id}"));
                if(apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMUser>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception("User tidak di temukan");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Product API cannot be reached!");
                }

            }
            catch (Exception ex)
            {
                apiResponse.data = new VMTblMUser();
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        

        public string UploadFile(IFormFile imageFile)
        {
            string uniqueFileName = string.Empty;
            string uploadFolder = string.Empty;

            if (imageFile != null)
            {
                uniqueFileName =Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                uploadFolder = webHostEnv.WebRootPath + "\\" + imageFolder + "\\" + uniqueFileName;

                using (FileStream fileStream = new FileStream(uploadFolder, FileMode.CreateNew))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public void DeleteOldImage(string oldImageFIleName)
        {
            try
            {
                oldImageFIleName = webHostEnv.WebRootPath + "\\" + imageFolder + "\\" + oldImageFIleName;

                //check if image exist then delete
                if (File.Exists(oldImageFIleName))
                {
                    File.Delete(oldImageFIleName);
                }
                else
                {
                    throw new ArgumentNullException("File do not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }


}
