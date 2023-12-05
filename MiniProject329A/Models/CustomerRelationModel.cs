using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class CustomerRelationModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiUrl;
        private readonly HttpClient httpClient;

        public CustomerRelationModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<VMResponse?> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + "api/CustomerRelation"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMCustomerRelation>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Hubungan Relasi tidak dapat ditemukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = new List<VMTblMCustomerRelation>();
            }
            return apiResponse;
        }
        public async Task<VMTblMCustomerRelation?> GetById(long id)
        {
            VMTblMCustomerRelation? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/CustomerRelation/GetById/{id}"));

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMCustomerRelation>(apiResponse.data.ToString());
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
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }
        public async Task<VMResponse> GetByName(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(
                    apiUrl + $"api/CustomerRelation/GetByName/{name}"));

                if (apiResponse != null)
                {
                    apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMCustomerRelation>?>
                        (apiResponse.data.ToString());
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = new List<VMTblMCustomerRelation>();
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> Create(VMTblMCustomerRelation data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiUrl + "api/CustomerRelation", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("....");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomerRelation>(JsonConvert.SerializeObject(apiResponse.data));
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> Edit(VMTblMCustomerRelation data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PutAsync(apiUrl + "api/CustomerRelation", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("...");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomerRelation>(JsonConvert.SerializeObject(apiResponse.data));
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
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.DeleteAsync(apiUrl + $"api/CustomerRelation/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

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
