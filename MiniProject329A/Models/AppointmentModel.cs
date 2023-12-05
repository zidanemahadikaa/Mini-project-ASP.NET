using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Models
{
    public class AppointmentModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiUrl;
        private readonly HttpClient httpClient;

        public AppointmentModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Appointment/GetAllAppointment"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblTAppointment>>(apiResponse.data.ToString());
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
                apiResponse.data = new List<VMTblTAppointment>();

            }
            return apiResponse;
        }
        public async Task<VMResponse> GetByParentId(long? id)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Appointment/GetAllAppointment/?id={id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblTAppointment>>(apiResponse.data.ToString());
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
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/Appointment/GetCusDocName/?parId={parId}&name={name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblTAppointment>>(apiResponse.data.ToString());
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
    }
}
