using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class LocationLevelModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;

        public LocationLevelModel(IConfiguration _config)
        {
            apiurl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }

        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl + "api/LocationLevel"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {

                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMLocationLevel>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Level Lokasi tidak bisa diproses");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $" {ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMTblMLocationLevel?> GetById(long id)
        {
            VMTblMLocationLevel? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await
                    httpClient.GetStringAsync(apiurl + $"api/LocationLevel/{id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMLocationLevel>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("API Level Lokasi tidak ada");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }

        public async Task<VMResponse> Add(VMTblMLocationLevel data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await 
                    (await httpClient.PostAsync(apiurl + "api/LocationLevel",
                    content)).Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse> Edit(VMTblMLocationLevel data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PutAsync(apiurl + "api/LocationLevel",
                    content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Edit Level Lokasi Tidak Bisa Dilakukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse> GetByName(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                httpClient.GetStringAsync(apiurl + $"api/LocationLevel/GetByName/{name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMLocationLevel>>
                            (apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception("Level Lokasi Tidak...");
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblMLocationLevel>();
            }
            return apiResponse;
        }

        public async Task<VMResponse> Delete(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.DeleteAsync
                    (apiurl + $"api/LevelLokasi/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

                if (apiResponse == null) throw new Exception("Level Lokasi Tidak Bisa Dilakukan!");
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }
    }
}
