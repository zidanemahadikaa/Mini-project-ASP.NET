using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class CustomerMemberModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        public CustomerMemberModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMResponse> GetByParentId(long? id)
        {
            //List<VMTblMCustomerMember>? data = new List<VMTblMCustomerMember>();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/CustomerMember/GetByParentId/?parId={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMCustomerMember>>(apiResponse.data.ToString());
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
        public async Task<VMResponse> GetByCustomerName(long? parId, string name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/CustomerMember/GetByCustomerName/?parId={parId}&name={name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMCustomerMember>>(apiResponse.data.ToString());
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
        public async Task<VMResponse> GetById(long? id)
        {
            //VMTblMCustomerMember? data = new VMTblMCustomerMember();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/CustomerMember/GetById/?Id={id}"));
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
            VMTblMCustomerMember? data = new VMTblMCustomerMember();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/CustomerMember/GetByCusId/?cusId={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMCustomerMember>(apiResponse.data.ToString());
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
        public async Task<VMResponse> CreateCustomerMember(VMTblMCustomerMember data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiUrl + "api/CustomerMember", content)).Content.ReadAsStringAsync());

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
        public async Task<VMResponse> EditCustomerMember(VMTblMCustomerMember data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiUrl + "api/CustomerMember", content)).Content.ReadAsStringAsync());

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
        public async Task<VMResponse?> Delete(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.DeleteAsync(
                    apiUrl + $"api/CustomerMember/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

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
