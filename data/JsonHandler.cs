using System.Text.Json;

public static class JsonHandler
{
	
	private static readonly string filePath = "data/images.json";

	public static List<ImageRecord> GetImages()
	{
		if (!File.Exists(filePath))
			return [];

		var json = File.ReadAllText(filePath);
		return JsonSerializer.Deserialize<List<ImageRecord>>(json) ?? [];
	}

	public static void AddImage(ImageRecord record)
	{
		var records = GetImages();
		records.Add(record);

		var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
		{
			WriteIndented = true
		});

		File.WriteAllText(filePath, json);
	}
}