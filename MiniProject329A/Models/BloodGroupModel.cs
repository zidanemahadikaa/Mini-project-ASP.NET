using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class BloodGroupModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiUrl;
        private readonly HttpClient httpClient;

        public BloodGroupModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<VMResponse?> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + "api/BloodGroup"));
                if(apiResponse != null)
                {
                    if(apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMBloodGroup>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Golongan Darah tidak dapat ditemukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = new List<VMTblMBloodGroup>();
            }
            return apiResponse;
        }
        public async Task<VMTblMBloodGroup?> GetById(long id)
        {
            VMTblMBloodGroup? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/BloodGroup/GetById/{id}"));

                if(apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMBloodGroup>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Golongan Darah tidak dapat ditemukan");
                }
            }
            catch(Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }
        public async Task<VMResponse?> GetByCode(string? code)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(
                    apiUrl + $"api/BloodGroup/GetByCode/{code}"));

                if(apiResponse != null)
                {
                    apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMBloodGroup>?>
                        (apiResponse.data.ToString());
                }
            }
            catch(Exception ex)
            {
                apiResponse.data = new List<VMTblMBloodGroup>();
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> Create(VMTblMBloodGroup data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiUrl + "api/BloodGroup", content)).Content.ReadAsStringAsync());

                if(apiResponse == null)
                {
                    throw new Exception("....");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> Edit(VMTblMBloodGroup data)
        {
            try
            {
                jsonData= JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PutAsync(apiUrl + "api/BloodGroup", content)).Content.ReadAsStringAsync());

                if(apiResponse == null)
                {
                    throw new Exception("...");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMBloodGroup>(JsonConvert.SerializeObject(apiResponse.data));
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.DeleteAsync(apiUrl + $"api/BloodGroup/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("....");
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