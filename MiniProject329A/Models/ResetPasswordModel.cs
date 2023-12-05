using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject329A.Models
{
    public class ResetPasswordModel
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        public ResetPasswordModel(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
        }
        public async Task<VMResponse> ResetOldPassword(VMTblTResetPassword data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.PostAsync(apiUrl + "api/ResetPassword/ResetOldPassword", content)).Content.ReadAsStringAsync());

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

    }
}
