using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Models
{
    public class DoctorOfficeModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiUrl;
        private readonly HttpClient httpClient;

        public DoctorOfficeModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/DoctorOffice"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblTDoctorOffice>>(apiResponse.data.ToString());
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
                apiResponse.data = new List<VMTblTDoctorOffice>();

            }
            return apiResponse;
        }
    }
}
