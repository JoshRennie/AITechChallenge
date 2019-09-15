namespace Api.Models.Jobs
{
	using System.Collections.Generic;
	using System.Linq;
	using Core.Entities;
	using Core.Enums;

	/// <summary>
	/// A job in a more useful format for the website
	/// </summary>
	public class JobViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JobViewModel"/> class.
		/// </summary>
		/// <param name="job">The job.</param>
		/// <param name="items">The items.</param>
		public JobViewModel(Job job, IReadOnlyCollection<Item> items)
		{
			//Usually, rather than getting a list of items seperately to get the item name, I would do a join in the DB and return a custom object
			//As a trade off, I'm just getting the name here instead

			Id = job.Id;
			ApprovalStatus = job.ApprovalStatus;
			ApprovalMessage = job.ApprovalMessage;
			Items = new List<JobItemViewModel>();

			Price = job.Price;
			LabourHours = job.LabourHours;

			foreach (var jobItem in job.Items)
			{
				var item = items.First(x => x.Id == jobItem.ItemId);
				var itemViewModel = new JobItemViewModel(jobItem, item);
				Items.Add(itemViewModel);
			}
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string Name => $"Job {Id}";

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public List<JobItemViewModel> Items { get; set; }

		/// <summary>
		/// Gets or sets the approval status.
		/// </summary>
		/// <value>
		/// The approval status.
		/// </value>
		public ApprovalStatus ApprovalStatus { get; set; }

		/// <summary>
		/// Gets or sets the approval message.
		/// </summary>
		/// <value>
		/// The approval message.
		/// </value>
		public string ApprovalMessage { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>
		/// The price.
		/// </value>
		public decimal Price {get;set;}

		/// <summary>
		/// Gets or sets the labour hours.
		/// </summary>
		/// <value>
		/// The labour hours.
		/// </value>
		public decimal LabourHours { get; set; }
	}
}