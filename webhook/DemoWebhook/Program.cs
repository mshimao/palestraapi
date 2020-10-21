using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoWebhook
{
    public class Cliente {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
    }
class Program
    {
        static HttpClient _client = new HttpClient();
        static async Task Main(string[] args)
        {
            _client.BaseAddress = new Uri("https://webhook.site/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int n = 0;
            do
            {
                await PostCliente();
                n++;
                await Task.Delay(5000);
            } while (n < 100);
        }

        private static async Task PostCliente()
        {
                        var cliente = new StringContent(
                                JsonSerializer.Serialize(GetCliente()),
                                Encoding.UTF8,
                                "application/json");
            using var httpResponse = await _client.PostAsync("3a9d42b6-cc69-4de9-b895-dd08e09cebcd", cliente);
            httpResponse.EnsureSuccessStatusCode();
            Console.WriteLine("Enviado");
           
        }

        static Cliente GetCliente()
        {
            return new Cliente()
            {
                Id = DateTime.Now.Second,
                Nome = "Cliente " + DateTime.Now.Second,
                Status = "Ativo"
            };
        }
    }
}
