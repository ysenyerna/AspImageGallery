using Microsoft.AspNetCore.Mvc.RazorPages;

public class GalleryModel : PageModel
{
	public List<ImageRecord> Images = [];

	public void OnGet()
	{
		Images = JsonHandler.GetImages();
	}
}