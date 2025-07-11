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


		// Get list for all body parts (e.g. chest, back, legs, etc.)
		public async Task<string> GetBodyPartListAsync() {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync("bodyPartList");
			return body;
		}

		// Get exercises for a specified body part
		public async Task<string> GetBodyPartWorkoutsAsync(string bodyPart) {
			string body = await RequestExerciseResourceAndGetResponseBodyAsync($"bodyPart/{bodyPart}");
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



		// Get Workout image Resolution - 180px
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
			//var request = new HttpRequestMessage
			//{
			//	Method = HttpMethod.Get,
			//	RequestUri = new Uri($"/exercises/{slug}", UriKind.Relative),
			//	Headers =
			//	{
			//		{ "x-rapidapi-key", _config["Workout_Api_Key"] },
			//	},
			//};
			//using (var response = await _httpClient.SendAsync(request)) {
			//	response.EnsureSuccessStatusCode();
			//	return await response.Content.ReadAsStringAsync();
			//}
			switch (slug) {
				case "equipmentList":
					return @"[
				""dumbbell"",
				""barbell"",
				""kettlebell"",
				""resistance band"",
				""body weight"",
				""machine""
			]";

				case "bodyPartList":
					return @"[
				""chest"",
				""back"",
				""legs"",
				""arms"",
				""shoulders"",
				""core""
			]";

				case "targetList":
					return @"[
				""biceps"",
				""triceps"",
				""pectorals"",
				""quadriceps"",
				""hamstrings"",
				""deltoids"",
				""abs"",
				""glutes""
			]";

				case var s when s.StartsWith("equipment/"):
					return @"[
				{
					""id"": ""fa7e4211-760d-4b28-a6ee-1e77e263a521"",
					""name"": ""Dumbbell Bench Press"",
					""bodyPart"": ""chest"",
					""target"": ""pectorals"",
					""equipment"": ""dumbbell"",
					""description"": ""A chest-focused pressing movement using dumbbells on a flat bench."",
					""difficulty"": ""intermediate"",
					""category"": ""strength"",
					""secondaryMuscles"": [""triceps"", ""deltoids""],
					""instructions"": [
						""Lie down on a flat bench with a dumbbell in each hand."",
						""Press the weights above your chest by extending your elbows."",
						""Slowly lower them back after a short pause.""
					]
				},
				{
					""id"": ""c3b74e33-1c90-4e18-88f7-5797c65704d0"",
					""name"": ""Dumbbell Bicep Curl"",
					""bodyPart"": ""arms"",
					""target"": ""biceps"",
					""equipment"": ""dumbbell"",
					""description"": ""Isolates the biceps by curling dumbbells with controlled form."",
					""difficulty"": ""beginner"",
					""category"": ""strength"",
					""secondaryMuscles"": [""forearms""],
					""instructions"": [
						""Stand upright with a dumbbell in each hand."",
						""Curl the weights while keeping your elbows stationary."",
						""Lower back down slowly.""
					]
				}
			]";

				case var s when s.StartsWith("bodyPart/"):
					return @"[
				{
					""id"": ""6a38eaf9-3974-4561-94cc-f10ae0c10b64"",
					""name"": ""Push-Up"",
					""bodyPart"": ""chest"",
					""target"": ""pectorals"",
					""equipment"": ""body weight"",
					""description"": ""A bodyweight movement that strengthens the chest and arms."",
					""difficulty"": ""beginner"",
					""category"": ""bodyweight"",
					""secondaryMuscles"": [""triceps"", ""core""],
					""instructions"": [
						""Place your hands slightly wider than shoulder-width."",
						""Lower your body until your chest nearly touches the floor."",
						""Push yourself back up.""
					]
				},
				{
					""id"": ""a3f059f2-27bb-4fc9-9375-4db8cc3e2b2c"",
					""name"": ""Incline Dumbbell Press"",
					""bodyPart"": ""chest"",
					""target"": ""pectorals"",
					""equipment"": ""dumbbell"",
					""description"": ""Targets upper chest using dumbbells on an incline bench."",
					""difficulty"": ""intermediate"",
					""category"": ""strength"",
					""secondaryMuscles"": [""shoulders"", ""triceps""],
					""instructions"": [
						""Lie on an incline bench with dumbbells."",
						""Press weights upward then slowly lower them.""
					]
				}
			]";

				case var s when s.StartsWith("target/"):
					return @"[
				{
					""id"": ""8ebf04d0-69b1-42ea-948e-92b7124ad4f9"",
					""name"": ""Bicep Curl"",
					""bodyPart"": ""arms"",
					""target"": ""biceps"",
					""equipment"": ""dumbbell"",
					""description"": ""Focuses on isolating the bicep with a curling motion."",
					""difficulty"": ""beginner"",
					""category"": ""strength"",
					""secondaryMuscles"": [""forearms""],
					""instructions"": [
						""Stand upright holding a dumbbell in each hand."",
						""Curl both weights up simultaneously."",
						""Lower them back down after a brief hold.""
					]
				},
				{
					""id"": ""6e0d9c01-40e6-4b93-a876-dad0f7ef7e32"",
					""name"": ""Barbell Curl"",
					""bodyPart"": ""arms"",
					""target"": ""biceps"",
					""equipment"": ""barbell"",
					""description"": ""Performed with a barbell to develop arm strength and size."",
					""difficulty"": ""intermediate"",
					""category"": ""strength"",
					""secondaryMuscles"": [""forearms""],
					""instructions"": [
						""Stand with a shoulder-width grip on the barbell."",
						""Keep your elbows close to your body and curl up."",
						""Control the bar as you lower it back down.""
					]
				}
			]";

				default:
					throw new InvalidOperationException($"Unknown slug: {slug}");
			}
		}

	}

}

