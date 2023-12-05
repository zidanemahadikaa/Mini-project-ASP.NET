using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class LocationModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;

        public LocationModel(IConfiguration _config)
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
                    httpClient.GetStringAsync(apiurl + "api/Location"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == 
                        HttpStatusCode.NoContent)
                    {

                        apiResponse.data = JsonConvert.DeserializeObject<
                            List<VMTblMLocation>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Lokasi tidak bisa diakses");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $" {ex.Message}";
                apiResponse.data = new List<VMTblMLocation>();

            }
            return apiResponse;
        }

        public async Task<VMTblMLocation?> GetById(long id)
        {
            VMTblMLocation? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await
                    httpClient.GetStringAsync(apiurl + $"api/Location/{id}"));
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
        public async Task<VMTblMLocation?> GetByParent(long id)
        {
            VMTblMLocation? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await
                    httpClient.GetStringAsync(apiurl + $"api/Location/GetByParent/{id}"));
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

        //public async Task<VMResponse> Add(VMTblMLocation data)
        //{
        //    try
        //    {
        //        jsonData = JsonConvert.SerializeObject(data);
        //        content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //        apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await 
        //            (await httpClient.PostAsync(apiurl + "api/Location",
        //            content)).Content.ReadAsStringAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        apiResponse.message = $"{ex.Message}";
        //    }
        //    return apiResponse;
        //}

        public async Task<VMResponse> GetByName(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                httpClient.GetStringAsync(apiurl + $"api/Location/GetByName/{name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode 
                        == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMLocation>>
                            (apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception("Lokasi Tidak Bisa Diakses");
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblMLocation>();
            }
            return apiResponse;
        }
        public async Task<VMResponse> GetByNameLevel(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                httpClient.GetStringAsync(apiurl + $"api/Location/GetByNameLevel/{name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode
                        == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMLocation>>
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
                apiResponse.data = new List<VMTblMLocation>();
            }
            return apiResponse;
        }
        public async Task<VMResponse> GetNameLocLevel(int llId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl +
                    $"api/Location/GetByParentId/{llId}"));

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK ||
                        apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMLocation>?>(
                            JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("API Location cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = new List<VMTblMLocation>();
                apiResponse.message = $"{ex.Message}";
            }

            return apiResponse;
        }

        public async Task<VMResponse?> Add(VMTblMLocation data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PostAsync(apiurl + $"api/Location", content)).
                    Content.ReadAsStringAsync());

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.Created)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<VMTblMLocation?>
                            (apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception("Gagal Menambahkan Lokasi");
                    }
                }
                else
                {
                    throw new Exception("Lokasi Tidak Bisa Ditambahkan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = new VMTblMLocation();
                apiResponse.message += $"{ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse> GetByLocationAdd(long Lid)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await 
                    httpClient.GetStringAsync(apiurl + $"api/Location/GetByLocationAdd/{Lid}"));

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMLocation>?>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Vaiant API cannot be reached!");
                }
            }
            catch (Exception ex)
            {
                apiResponse.data = new List<VMTblMLocation>();
                apiResponse.message = $"{ex.Message}";
            }

            return apiResponse;
        }

        public async Task<VMResponse> Edit(VMTblMLocation data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await
                    (await httpClient.PutAsync(apiurl + "api/Location",
                    content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Edit Lokasi Tidak Bisa Dilakukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse> Delete(long id, long userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>
                    (await (await httpClient.DeleteAsync
                    (apiurl + $"api/Location/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

                if (apiResponse == null) throw new Exception("Hapus Lokasi Tidak Bisa Dilakukan!");
            }
            catch (Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }

    }
}
