using System.Text.Json;
using System.Text;
using ApiPrueba.Interfases;
using System.Net.Http.Headers;
using ApiPrueba.Dtos;

namespace ApiPrueba.Implementaciones
{
    public class ConsumoWS : IConsumoWS
    {
        private readonly HttpClient _httpClient;

        public ConsumoWS(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync(LoginRequest loginRequest)
        {
            var url = "auth/login";

            var response = await _httpClient.PostAsJsonAsync(url, loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error en la autenticación: {response.StatusCode}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            using JsonDocument jsonDoc = JsonDocument.Parse(responseBody);
            return jsonDoc.RootElement.GetProperty("access_token").GetString();
        }

        public async Task<string> GetNotificaciones(string token, string startDate, string endDate)
        {
            string url = $"notifications?start_date={startDate}&end_date={endDate}";

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error en la solicitud: {response.StatusCode}");
            }

            var respuesta = await response.Content.ReadAsStringAsync();
            GuardarArchivo(respuesta);
            return respuesta;
        }

        private static void GuardarArchivo(string jsonData)
        {
            string filePath = @"G:\notificaciones.txt";

            using JsonDocument doc = JsonDocument.Parse(jsonData);
            JsonElement root = doc.RootElement;
            JsonElement data = root.GetProperty("data");

            using StreamWriter writer = new StreamWriter(filePath, false);

            foreach (JsonElement item in data.EnumerateArray())
            {
                string line = $"ID: {item.GetProperty("id").GetInt32()} | " +
                              $"Teléfono: {item.GetProperty("cell_phone_number").GetString()} | " +
                              $"Número de Solicitud: {item.GetProperty("request_number").GetString()} | " +
                              $"Intermediario: {item.GetProperty("intermediary").GetString()} | " +
                              $"Valor Póliza: {item.GetProperty("policy_value").GetString()} | " +
                              $"Estado Notificación: {item.GetProperty("notification_status").GetString()} | " +
                              $"Estado Mensaje: {item.GetProperty("message_status").GetString()}";

                writer.WriteLine(line);
            }

        }
    }
}
