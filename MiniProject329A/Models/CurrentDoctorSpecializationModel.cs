using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Models
{
    public class CurrentDoctorSpecializationModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;
        public CurrentDoctorSpecializationModel(IConfiguration _config)
        {
            apiurl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }
        public async Task<VMTblMLocation?> GetById(long id)
        {
            VMTblMLocation? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await
                    httpClient.GetStringAsync(apiurl + $"api/CurrentDoctorSpecializationModel/{id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK ||
                        apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMLocation>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("API Lokasi tidak ada");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }
        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    httpClient.GetStringAsync(apiurl + "api/CurrentDoctorSpecialization"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode ==
                        HttpStatusCode.NoContent)
                    {

                        apiResponse.data = JsonConvert.DeserializeObject<
                            List<VMTblTCurrentDoctorSpecialization>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Spesialis tidak bisa diakses");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $" {ex.Message}";
                apiResponse.data = new List<VMTblTCurrentDoctorSpecialization>();

            }
            return apiResponse;
        }

        public async Task<VMResponse> GetByNameSpecialization(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                httpClient.GetStringAsync(apiurl + $"api/CurrentDoctorSpecialization/GetByName/{name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode
                        == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblTCurrentDoctorSpecialization>>
                            (apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception("Lokasi Level Tidak Bisa Diakses");
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblTCurrentDoctorSpecialization>();
            }
            return apiResponse;
        }
    }
}
