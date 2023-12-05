using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class PasienUpdateModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        public PasienUpdateModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMResponse> GetById(long? id)
        {
            //VMTblMCustomerMember? data = new VMTblMCustomerMember();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/updatebiocus/GetCusmemberId/?Id={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomerMember>(apiResponse.data.ToString());
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
        public async Task<VMResponse> GetByCusId(long? id)
        {
            //VMTblMCustomer? data = new VMTblMCustomer();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/updatebiocus/GetCusId/?Id={id}"));
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
        public async Task<VMResponse> GetByBioId(long? id)
        {
            //VMTblMBiodata? data = new VMTblMBiodata();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/updatebiocus/GetIdBio/?Id={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata>(apiResponse.data.ToString());
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
        public async Task<VMResponse> UpdateName(VMTblMBiodata data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PutAsync(apiUrl + "api/updatebiocus/Updatename", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("....");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMBiodata>(JsonConvert.SerializeObject(apiResponse.data));
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMResponse> UpdateCustomer(VMTblMCustomer data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PutAsync(apiUrl + "api/updatebiocus/UpdateCustomer", content)).Content.ReadAsStringAsync());

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
        public async Task<VMResponse> UpdateCustomerMember(VMTblMCustomerMember data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PutAsync(apiUrl + "api/updatebiocus/UpdateCustomerMember", content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("....");
                }
                else
                {
                    apiResponse.data = JsonConvert.DeserializeObject<VMTblMCustomerMember>(JsonConvert.SerializeObject(apiResponse.data));
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
