using MessagePack;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MiniProject329A.Models
{
    public class DokterModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient;
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        private readonly IWebHostEnvironment webHostEnv;

        private readonly UserModel user;

        private readonly string imageFolder;

        public DokterModel(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            apiUrl = _config["ApiUrl"];
            imageFolder = _config["ImageFolder"];
            webHostEnv = _webHostEnv;
            httpClient = new HttpClient();
        }

        public async Task<VMResponse?> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + $"api/Dokter"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMDoctor>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception("Dokter tidak di temukan");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Product API cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = new VMTblMDoctor();
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse?> GetById(long? id)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + $"api/User/{id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMUser>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception("Dokter tidak di temukan");
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
        public async Task<VMResponse?> GetBiodataId(long? id)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + $"api/Dokter/GetBiodataId/?id={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMDoctor>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception("Dokter tidak di temukan");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Product API cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = new VMTblMDoctor();
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
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
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
        public async Task<VMResponse?> Edit(VMTblMBiodata formData)
        {

            try
            {
                jsonData = JsonConvert.SerializeObject(formData);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await httpClient.PutAsync(apiUrl + "api/Biodata", content);
                string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(jsonResponse);

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception("Failed to Update!");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Product API cannot be reached!");
                }

                httpResponse.Dispose();
                jsonResponse = null;
            }
            catch (Exception ex)
            {
                apiResponse.data = new VMTblMBiodata();
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }
    }
}
