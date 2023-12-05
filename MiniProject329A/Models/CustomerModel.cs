using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class CustomerModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        public CustomerModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMTblMCustomer> GetByBioId(long? id)
        {
            VMTblMCustomer? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Customer/GetByBioId/?bioId={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMCustomer>(apiResponse.data.ToString());
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
                apiResponse.message = ex.Message;
            }
            return data;
        }
        public async Task<VMResponse> GetById(long? id)
        {
            //VMTblMCustomer? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Customer/GetById/?Id={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomer>(apiResponse.data.ToString());
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
                apiResponse.message = ex.Message;
            }
            return apiResponse;
        }
        public async Task<VMTblMCustomer> GetByName(string? name)
        {
            VMTblMCustomer? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Customer/GetByName"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMCustomer>(apiResponse.data.ToString());
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
                apiResponse.message = ex.Message;
            }
            return data;
        }
        public async Task<VMResponse> UpdateProfileCus(VMTblMCustomer data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PutAsync(apiUrl + "api/Customer/UpdateProfile", content)).Content.ReadAsStringAsync());

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
        public async Task<VMResponse> GetAll()
        {            
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + "api/Customer/"));
                if(apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMCustomer>>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception(".....");
                }
            }
            catch(Exception ex)
            {
                apiResponse.message = $"{ ex.Message}";
                apiResponse.data = new List<VMTblMCustomer>();

            }
            return apiResponse;
        }
        public async Task<VMResponse> CreateCustomer(VMTblMCustomer dataCustomer)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(dataCustomer);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiUrl + "api/Customer",
                    content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("....");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomer>(JsonConvert.SerializeObject(apiResponse.data));
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> EditCustomer(VMTblMCustomer dataCustomer)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(dataCustomer);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiUrl + "api/Customer",
                    content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("....");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomer>(JsonConvert.SerializeObject(apiResponse.data));
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
    }
}
