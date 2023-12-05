using MiniProject329A.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MiniProject329A.Models
{
    public class PaymentMethodModel
    {
        private string jsonData;
        private HttpContent content;
        private VMResponse? apiResponse;
        private readonly string apiurl;
        private readonly HttpClient httpClient;

        public PaymentMethodModel(IConfiguration _config)
        {
            apiurl = _config["ApiUrl"];
            apiResponse = new VMResponse();
            httpClient = new HttpClient();
        }

        public async Task<VMResponse> GetAll()
        {
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await httpClient.GetStringAsync(apiurl + "api/PaymentMethod"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMPaymentMethod>>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("Metode Pembayaran tidak bisa dilakukan");
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $" {ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMTblMPaymentMethod?> GetById(long id)
        {
            VMTblMPaymentMethod? data = null;
            try
            {
                apiResponse = JsonConvert.DeserializeObject<VMResponse>(await
                    httpClient.GetStringAsync(apiurl + $"api/PaymentMethod/{id}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == 
                        HttpStatusCode.NoContent)
                    {
                        data = JsonConvert.DeserializeObject<VMTblMPaymentMethod>(apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception(apiResponse.message);
                    }
                }
                else
                {
                    throw new Exception("API Metode Pembayaran tidak ada");
                }

            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return data;
        }


        public async Task<VMResponse> Add(VMTblMPaymentMethod data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await (await httpClient.PostAsync(apiurl + "api/PaymentMethod",
                    content)).Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
            }
            return apiResponse;
        }

        public async Task<VMResponse> Edit(VMTblMPaymentMethod data)
        {
            try
            {
                jsonData = JsonConvert.SerializeObject(data);
                content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                apiResponse = JsonConvert.DeserializeObject<VMResponse?>(await 
                    (await httpClient.PutAsync(apiurl + "api/PaymentMethod",
                    content)).Content.ReadAsStringAsync());

                if (apiResponse == null)
                {
                    throw new Exception("Edit Metode Pembayaran Tidak Bisa Dilakukan");
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
                    (apiurl + $"api/PaymentMethod/?id={id}&userId={userId}")).Content.ReadAsStringAsync());

                if (apiResponse == null) throw new Exception("Metode Pembayaran Tidak Bisa Dilakukan!");
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
                httpClient.GetStringAsync(apiurl + $"api/PaymentMethod/GetByName/{name}"));
                if (apiResponse != null)
                {
                    if (apiResponse.statusCode == HttpStatusCode.OK || apiResponse.statusCode == HttpStatusCode.NoContent)
                    {
                        apiResponse.data = JsonConvert.DeserializeObject<List<VMTblMPaymentMethod>>
                            (apiResponse.data.ToString());
                    }
                    else
                    {
                        throw new Exception("Metode Pembayaran Tidak Bisa Dilakukan");
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.message = $"{ex.Message}";
                apiResponse.data = new List<VMTblMPaymentMethod>();
            }
            return apiResponse;
        }
    }
}
