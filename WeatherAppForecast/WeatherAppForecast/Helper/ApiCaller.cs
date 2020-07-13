using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppForecast.Helper
{
    public class ApiCaller
    {
        public static async Task<ApiResponse> Get (string url ,string authId = null)
        {
            using(var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(authId))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", authId);
                }

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new ApiResponse { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                {
                    return new ApiResponse { ErrorMesage = request.ReasonPhrase };
                }

            }
        }


    }

    public class ApiResponse
    {
        public bool Successfull => ErrorMesage == null;
        public string ErrorMesage { get; set; }
        public string Response { get; set; }

    }
}
