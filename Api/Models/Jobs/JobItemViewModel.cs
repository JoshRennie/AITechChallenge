namespace Api.Models.Jobs
{
	using Core.Entities;

	/// <summary>
	/// A job item in a more useful form for the website
	/// </summary>
	public class JobItemViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JobItemViewModel" /> class.
		/// </summary>
		/// <param name="jobItem">The job item.</param>
		/// <param name="item">The item.</param>
		public JobItemViewModel(JobItem jobItem, Item item)
		{
			Id = jobItem.Id;
			Quantity = jobItem.Quantity;
			ItemName = item.Name;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the item.
		/// </summary>
		/// <value>
		/// The name of the item.
		/// </value>
		public string ItemName { get; set; }

		/// <summary>
		/// Gets or sets the quantity of the item
		/// </summary>
		/// <value>
		/// The quantity.
		/// </value>
		public int Quantity { get; set; }
	}
}