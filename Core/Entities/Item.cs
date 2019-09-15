namespace Core.Entities
{
	using System.Collections.Generic;

	/// <summary>
	/// An item on a vehicle that may need changing.
	/// Holds the associated cost and time to replace.
	/// </summary>
	public class Item
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Item" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="name">The name.</param>
		/// <param name="unitTime">The unit time.</param>
		/// <param name="unitCost">The unit cost.</param>
		/// <param name="relatedItems">The related items.</param>
		/// <param name="maxQuantity">The maximum quantity.</param>
		/// <param name="validQuantities">The valid quantities.</param>
		public Item(int id, string name, int unitTime, decimal unitCost, List<int> relatedItems = null, int? maxQuantity = null, List<int> validQuantities = null)
		{
			Id = id;
			Name = name;
			UnitTime = unitTime;
			UnitCost = unitCost;
			RelatedItems = relatedItems ?? new List<int>();
			MaxQuantity = maxQuantity;
			ValidQuantities = validQuantities ?? new List<int>();
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
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the time it takes to replace an item (in minutes)
		/// </summary>
		/// <value>
		/// The unit time.
		/// </value>
		public int UnitTime { get; set; }

		/// <summary>
		/// Gets or sets the cost of replacing an item (in £)
		/// </summary>
		/// <value>
		/// The unit cost.
		/// </value>
		public decimal UnitCost { get; set; }

		/// <summary>
		/// Gets or sets the related item ids.
		/// When validting a job, there will need to be the same quantity of each of the related items and this item
		/// </summary>
		/// <value>
		/// The related item ids.
		/// </value>
		public List<int> RelatedItems { get; set; }

		/// <summary>
		/// Gets or sets the valid quantities for an item.
		/// </summary>
		/// <value>
		/// The valid quantities.
		/// </value>
		public List<int> ValidQuantities { get; set; }

		/// <summary>
		/// Gets or sets the maximum quantity of the item allowed in a job
		/// </summary>
		/// <value>
		/// The maximum quantity.
		/// </value>
		public int? MaxQuantity { get; set; }

		/// <summary>
		/// Calclates the total cost of replacing this item
		/// </summary>
		/// <param name="labourCharge">The labour charge.</param>
		/// <returns>The total cost</returns>
		public decimal TotalCost(decimal labourCharge)
		{
			//Labour is charged hourly, so divide the unit time by 60
			var labourCost = (UnitTime / 60m) * labourCharge;
			return labourCost + UnitCost;
		}
	}
}