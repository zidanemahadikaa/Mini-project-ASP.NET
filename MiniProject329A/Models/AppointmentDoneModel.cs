using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Models
{
    public class AppointmentDoneModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiUrl;
        private readonly HttpClient httpClient;

        public AppointmentDoneModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<List<VMTblTAppointmentDone?>> GetById(long id)
        {
            List<VMTblTAppointmentDone?> data = new List<VMTblTAppointmentDone?>();
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/AppointmentDone/GetById/?id={id}"));

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTblTAppointmentDone>>(apiResponse.data.ToString());
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
        public async Task<VMResponse?> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiUrl + "api/AppointmentDone"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblTAppointmentDone>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("tidak dapat ditemukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
                apiResponse.data = new List<VMTblTAppointmentDone>();
            }
            return apiResponse;
        }
    }
}
