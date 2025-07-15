using Newtonsoft.Json;

namespace Fit_Track_API.External {
	public class FoodApiClient {
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _config;

		public FoodApiClient(HttpClient httpClient, IConfiguration configuration) {
			_httpClient = httpClient;
			_config = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}
		public async Task<string> GetFoodsDataAsync(string foodName) {
			var response = await _httpClient.GetAsync($"https://api.nal.usda.gov/fdc/v1/foods/search" +
				$"?query={foodName}&dataType=Survey%20%28FNDDS%29" +
				$"&requireAllWords=true&api_key={_config["Food_Api_Key"]}");

			response.EnsureSuccessStatusCode(); //Throws an HttpRequestException if the status code is not successful
			var body = await response.Content.ReadAsStringAsync();
			return body;
		}
	}
}
