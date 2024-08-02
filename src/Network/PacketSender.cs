namespace io.radston12.datatransmitter
{

    using System;
    using System.Text;

    using System.Net.Http;
    using System.Net.Http.Headers;

    using Exiled.API.Enums;
    using Exiled.API.Features;

    using Utf8Json;

    public class PacketSender
    {
        private static Uri endpointUri = null;
        static readonly HttpClient client = new HttpClient();
        public static void setEndpoint(string endpoint)
        {
            if (endpoint == null)
                throw new ArgumentNullException("Endpoint URL can't be null!");

            Uri uriResult;
            bool result = Uri.TryCreate(endpoint, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!result)
                throw new ArgumentException("Endpoint URL \"" + endpoint + "\" is not a valid HTTP(S) url!");

            endpointUri = uriResult;
        }

        public async static void send(Object obj)
        {
            if (endpointUri == null)
                throw new ArgumentNullException("You must set an endpoint before sending!");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonSerializer.ToJsonString(obj);

            StringContent jsonContent = new(
                json,
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                using HttpResponseMessage response = await client.PostAsync(endpointUri, jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Log.Info(e.Message);
            }

        }
    }

}