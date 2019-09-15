namespace Core.Entities
{
	using System.Collections.Generic;

	/// <summary>
	/// An item that belongs to a job
	/// </summary>
	public class JobItem
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public int JobId { get; set; }

		/// <summary>
		/// Gets or sets the item identifier.
		/// </summary>
		/// <value>
		/// The item identifier.
		/// </value>
		public int ItemId { get; set; }

		/// <summary>
		/// Gets or sets the quantity.
		/// </summary>
		/// <value>
		/// The quantity.
		/// </value>
		public int Quantity { get; set; }
	}
}