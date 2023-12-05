using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class BiodataModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        public BiodataModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMResponse> CreateBiodata(VMTblMBiodata data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PostAsync(apiUrl + "api/Biodata/CreateBiodata", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Variants API cannot be reached");
                }
                else {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata>(JsonConvert.SerializeObject(apiResponse.data));
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> UpdateBiodata(VMTblMBiodata data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/Biodata/Update", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Biodata API cannot be reached");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata>(JsonConvert.SerializeObject(apiResponse.data));
                }

            }
            catch (Exception ex)
            {
                apiResponse.message += $"  {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> GetByLongId(long? id)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Biodata/GetByLongId/?id={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata?>(apiResponse.data.ToString());
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
        public async Task<VMResponse> GetByName(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Biodata/GetByName"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata?>(apiResponse.data.ToString());
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
    }
}
