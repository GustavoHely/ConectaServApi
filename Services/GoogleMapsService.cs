using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ConectaServApi.Services
{
    /// <summary>
    /// Serviço para consumir a API do Google Maps (Geocoding e Distance Matrix)
    /// </summary>
    public class GoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapsService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["GoogleMaps:ApiKey"];
        }

        /// <summary>
        /// Consulta os dados detalhados do endereço com base no CEP informado.
        /// </summary>
        public async Task<CoordenadasDetalhadas> ObterCoordenadasPorCepAsync(string cep)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={cep}&key={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new CoordenadasDetalhadas();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var status = doc.RootElement.GetProperty("status").GetString();
            if (status != "OK")
                return new CoordenadasDetalhadas();

            var resultado = doc.RootElement.GetProperty("results")[0];

            var location = resultado
                .GetProperty("geometry")
                .GetProperty("location");

            var componentes = resultado.GetProperty("address_components");

            string? estado = null, cidade = null, bairro = null, rua = null;

            foreach (var comp in componentes.EnumerateArray())
            {
                var types = comp.GetProperty("types").EnumerateArray().Select(x => x.GetString()).ToList();

                if (types.Contains("administrative_area_level_1"))
                    estado = comp.GetProperty("long_name").GetString();

                if (types.Contains("administrative_area_level_2"))
                    cidade = comp.GetProperty("long_name").GetString();

                if (types.Contains("sublocality") || types.Contains("sublocality_level_1") || types.Contains("neighborhood"))
                    bairro = comp.GetProperty("long_name").GetString();

                if (types.Contains("route"))
                    rua = comp.GetProperty("long_name").GetString();
            }

            return new CoordenadasDetalhadas
            {
                Latitude = location.GetProperty("lat").GetDouble(),
                Longitude = location.GetProperty("lng").GetDouble(),
                Estado = estado,
                Cidade = cidade,
                Bairro = bairro,
                Rua = rua
            };
        }

        /// <summary>
        /// Calcula a distância em quilômetros entre dois pontos (lat/lng).
        /// </summary>
        public async Task<double?> CalcularDistanciaKmAsync(double origemLat, double origemLng, double destinoLat, double destinoLng)
        {
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origemLat},{origemLng}&destinations={destinoLat},{destinoLng}&key={_apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var status = doc.RootElement.GetProperty("status").GetString();
            if (status != "OK") return null;

            var elemento = doc.RootElement
                .GetProperty("rows")[0]
                .GetProperty("elements")[0];

            var elementoStatus = elemento.GetProperty("status").GetString();
            if (elementoStatus != "OK") return null;

            int distanciaMetros = elemento.GetProperty("distance").GetProperty("value").GetInt32();
            return distanciaMetros / 1000.0;
        }

        /// <summary>
        /// Classe auxiliar para retorno dos dados do endereço detalhado.
        /// </summary>
        public class CoordenadasDetalhadas
        {
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public string? Estado { get; set; }
            public string? Cidade { get; set; }
            public string? Bairro { get; set; }
            public string? Rua { get; set; }
        }
    }
}
