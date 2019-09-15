namespace Api.Controllers
{
	using Core.Entities;
	using Microsoft.AspNetCore.Mvc;
	using Models.Jobs;
	using ServiceLayer.Interfaces;
	using System.Collections.Generic;

	/// <summary>
	///     The controller that handles processing of jobs
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	[Route("api/[controller]")]
	[ApiController]
	public class JobsController : ControllerBase
	{
		/// <summary>
		///     The jobs DAO
		/// </summary>
		private readonly IBaseService<Job> _jobsService;

		/// <summary>
		/// The items DAO
		/// </summary>
		private readonly IBaseService<Item> _itemsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="JobsController" /> class.
		/// </summary>
		/// <param name="jobsService">The jobs service.</param>
		/// <param name="itemsService">The items service.</param>
		public JobsController(IBaseService<Job> jobsService, IBaseService<Item> itemsService)
		{
			_jobsService = jobsService;
			_itemsService = itemsService;
		}

		/// <summary>
		/// Creates a random job.
		/// </summary>
		/// <returns></returns>
		[HttpPost("Create")]
		public JsonResult Create()
		{
			var items = _itemsService.GetList();

			var job = Job.CreateRandom(items);

			_jobsService.Add(job);
			var jobViewModel = new JobViewModel(job, items);

			return new JsonResult(jobViewModel);
		}

		/// <summary>
		/// Gets the list of jobs.
		/// </summary>
		/// <returns></returns>
		[HttpGet("GetList")]
		public JsonResult GetList()
		{
			var jobs = _jobsService.GetList();

			//Convert the entites into view models
			var jobViewModels = new List<JobViewModel>();
			var items = _itemsService.GetList();

			foreach (var job in jobs)
			{
				var jobViewModel = new JobViewModel(job, items);
				jobViewModels.Add(jobViewModel);
			}

			return new JsonResult(jobViewModels);
		}

		/// <summary>
		/// Gets the specified job by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpGet("Get/{id:int}")]
		public JsonResult Get(int id)
		{
			var job = _jobsService.Get(id);

			return new JsonResult(job);
		}

		/// <summary>
		/// Deletes the specified job by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		[HttpPost("Delete/{id:int}")]
		public JsonResult Delete(int id)
		{
			_jobsService.Delete(id);

			return new JsonResult(new {isSucess = true});
		}
	}
}