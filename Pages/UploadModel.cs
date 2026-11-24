using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UploadModel : PageModel
{
	// Set the max file size (2 MB)
	private readonly int maxFileSize = 2 * 1024 * 1024;
	private readonly string imagesDirectory;

	// Image properties
	[BindProperty]
	[Required] public required string Title { get; set; }
	[Required] public required IFormFile Image { get; set; }

	// Custom error message
	public string Message = "";

	public UploadModel(IWebHostEnvironment env)
	{
		imagesDirectory = Path.Combine(env.WebRootPath, "uploads");
		Directory.CreateDirectory(imagesDirectory);
	}

	public IActionResult OnPost()
	{
		// Validate image title
		if (string.IsNullOrWhiteSpace(Title))
		{
			Message = "Please enter a title.";
			return Page();
		}
		// Check if the image is null
		if (Image is null || Image.Length == 0)
		{
			Message = "Choose a valid image file.";
			return Page();
		}
		// Check if the file is under 2 MB
		if (Image.Length >= maxFileSize)
		{
			Message = "File must be under 2 MB.";
			return Page();
		}
		// Check whether the file is PNG or JPG (note, this only checks extension, not MIME type)
		var ext = Path.GetExtension(Image.FileName);
		if (ext != ".png" && ext != "jpg")
		{
			Message = "Only .jpg and .png files are accepted.";
			return Page();
		}

		// Upload the image file to the root directory
		var filePath = Path.Combine(imagesDirectory, Title + ext);
		using var stream = System.IO.File.Create(filePath);
		Image.CopyTo(stream);

		
		Message = "Image uploaded successfully.";
		return Page();

	}


}