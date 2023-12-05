using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Models
{
    public class BiodataAddressModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;

        public BiodataAddressModel(IConfiguration _config)
        {
            apiurl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    httpClient.GetStringAsync(apiurl + "api/BiodataAddress"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode ==
                        HttpStatusCode.NoContent)
                    {

                        apiResponse.data = JsonConvert.DeserializeObject<
                            List<VMTblMBiodataAddress>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Biodata Address tidak bisa diakses");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $" {ex.Message}";
                apiResponse.data = new List<VMTblMBiodataAddress>();

            }
            return apiResponse;
        }
    }
}
