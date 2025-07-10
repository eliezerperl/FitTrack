namespace Fit_Track_API.External {
	public class WorkoutApiClient {
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _config;

		public WorkoutApiClient(HttpClient httpClient, IConfiguration configuration) {
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://exercisedb.p.rapidapi.com");
			_config = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}


		//Get a list of equipment
		public async Task<string> GetEquipmentListAsync() {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync("equipmentList");
			return body;
		}

		// Get exercises for a specified equipment
		public async Task<string> GetEquipmentWorkoutsAsync(string equipment) {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync($"equipment/{equipment}");
			return body;
		}



		// Get exercises for a specified body part
		public async Task<string> GetBodyPartWorkoutsAsync(string bodyPart) {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync($"bodyPart/{bodyPart}");
			return body;
		}

		// Get list for all body parts (e.g. chest, back, legs, etc.)
		public async Task<string> GetBodyPartListAsync() {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync("bodyPartList");
			return body;
		}



		// Get exercises for a specific target muscle
		public async Task<string> GetTargetMuscleWorkoutsAsync(string muscle) {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync($"target/{muscle}");
			return body;
		}

		// Get a list of all target muscles
		public async Task<string> GetTargetMuscleListAsync() {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync("targetList");
			return body;
		}



		// Get Workout image Resolution - 180 (option for  360, 720, 1080)
		public async Task<(byte[] ImageData, string ContentType)> GetExerciseImageAsync(string exerciseId) {
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/image?resolution=180&exerciseId={exerciseId}", UriKind.Relative),
				Headers =
				{
					{ "x-rapidapi-key", _config["Workout_Api_Key"] }
				}
			};

			using var response = await _httpClient.SendAsync(request);
			response.EnsureSuccessStatusCode();

			var contentType = response.Content.Headers.ContentType?.ToString() ?? "image/gif";
			var imageBytes = await response.Content.ReadAsByteArrayAsync();

			return (imageBytes, contentType);
		}




		// Helper Method to avoid redundancy making the request and returning the body
		private async Task<string> RequestExerciseResourceAndGetResponseBodyAsync(string slug) {
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/exercises/{slug}", UriKind.Relative),
				Headers =
				{
					{ "x-rapidapi-key", _config["Workout_Api_Key"] },
				},
			};
			using (var response = await _httpClient.SendAsync(request)) {
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsStringAsync();
			}
		}

	}
}
