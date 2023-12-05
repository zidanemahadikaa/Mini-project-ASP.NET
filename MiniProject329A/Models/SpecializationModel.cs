using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;

namespace MiniProject329A.Models
{
    public class SpecializationModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;

        public SpecializationModel(IConfiguration _config)
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
                    httpClient.GetStringAsync(apiurl + "api/Specialization"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {

                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMSpecialization>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Spesialis tidak bisa dilakukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $" {ex.Message}";
            }
            return apiResponse;
        }
        public async Task<VMTblMSpecialization?> GetById(long id)
        {
            VMTblMSpecialization? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await
                    httpClient.GetStringAsync(apiurl + $"api/Specialization/{id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode ==
                        HttpStatusCode.NoContent)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMSpecialization>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("API Specialization tidak ada");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }
        public async Task<VMResponse> GetByName(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                httpClient.GetStringAsync(apiurl + $"api/Specialization/GetByName/{name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMSpecialization>>
                            (apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception("Spesialis Tidak Bisa Dilakukan");
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblMSpecialization>();
            }
            return apiResponse;
        }
    }
}
