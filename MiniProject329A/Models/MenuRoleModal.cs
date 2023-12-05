using MessagePack;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MiniProject329A.Models
{
    public class MenuRoleModal
    {
        private VMResponse? apiResponse;
        private readonly HttpClient httpClient;
        private readonly string apiUrl;
        private string jsonData;
        private HttpContent? content;
        private readonly IWebHostEnvironment webHostEnv;

        public MenuRoleModal(IConfiguration _config)
        {
            apiUrl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }

        public async Task<List<VMTblMMenuRole>?> GetByRoleId(long Id)
        {
            List<VMTblMMenuRole>? data= new List<VMTblMMenuRole>();

            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await httpClient.GetStringAsync(apiUrl + $"api/MenuRole/{Id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<List<VMTblMMenuRole>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    };
                }
                else
                {
                    throw new Exception("MenuRole API cannot be reach!");
                }
            }
            catch  (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }

            return data;
        }
    }
}
