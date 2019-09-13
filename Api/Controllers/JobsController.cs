namespace Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Models;

	[Route("api/[controller]")]
	[ApiController]
	public class JobsController : ControllerBase
	{
		[HttpPost]
		public JsonResult CreateRandomJob()
		{
			var job = Job.CreateRandom();
		}
	}
}
