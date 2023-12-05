using MiniProject329A.DataAccess;
using MiniProject329A.DataModel;
using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class EducationLevelModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;

        public EducationLevelModel(IConfiguration _config)
        {
            apiurl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }

        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl + "api/EducationLevel"));

                if(apiResponse != null)
                {
                    if(apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMEducationLevel?>>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception("Education Level does not exist");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Education Level API cannot be reached");
                }
            }
            catch(Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse?> GetByName(string? name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl + $"api/EducationLevel/GetByName/{name}"));

                if(apiResponse != null)
                {
                    if(apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMEducationLevel>>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception (apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception(apiResponse.message);
                }
            }
            catch(Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblMEducationLevel>();
            }
            return apiResponse;
        }

        public async Task<VMResponse?> Create(VMTblMEducationLevel data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);

                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiurl + "api/EducationLevel", content)).Content.ReadAsStringAsync());

                if(apiResponse == null)
                {
                    throw new Exception("Educational API cannot be reach!");
                }
            }
            catch( Exception ex )
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMTblMEducationLevel?> FindById(long id)
        {
            VMTblMEducationLevel? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl + $"api/EducationLevel/FindById/{id}"));

                if(apiResponse != null)
                {
                    if(apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMEducationLevel>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Educational Level cannot be reach");
                }
            }
            catch(Exception ex)
            {
                apiResponse.message += $"{ex.Message}";
            }
            return data;
        }

        public async Task<VMResponse> Edit(VMTblMEducationLevel data)
        {
            //Get New Data
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PutAsync(apiurl + "api/EducationLevel", content)).Content.ReadAsStringAsync());

                if(apiResponse == null)
                {
                    throw new Exception("Educational Level API cannot be reach!");
                }
            }
            catch(Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse> Delete(long id, int userId)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.DeleteAsync(apiurl + $"api/EducationLevel/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

                if(apiResponse == null)
                {
                    throw new Exception("Educational Level API cannot be reach!");
                }
            }
            catch( Exception ex)
            {
                apiResponse.message += $" {ex.Message}";
            }
            return apiResponse;
        }

        //Validasi
        public async Task<VMResponse> ValidasiIfExist(string name)
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl + $"api/EducationLevel/NameCheck/?name={name}"));

                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMEducationLevel>>(JsonConvert.SerializeObject(apiResponse.data));
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception(apiResponse.message);
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblMEducationLevel>();
            }
            return apiResponse;
        }

    }
}
