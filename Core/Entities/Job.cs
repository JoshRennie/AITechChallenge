namespace Core.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Enums;
	using Utilities;

	/// <summary>
	///     A maintenance job that needs to be performed.
	///     Holds all the parts that need replcaing
	/// </summary>
	public class Job
	{
		/// <summary>
		///     Gets or sets the identifier.
		/// </summary>
		/// <value>
		///     The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>
		/// The items.
		/// </value>
		public List<JobItem> Items { get; set; }

		/// <summary>
		/// Gets or sets the approval status of the job.
		/// </summary>
		/// <value>
		/// The approval status.
		/// </value>
		public ApprovalStatus ApprovalStatus { get; private set; }

		/// <summary>
		/// Gets or sets the approval message.
		/// </summary>
		/// <value>
		/// The approval message.
		/// </value>
		public string ApprovalMessage { get; set; }

		/// <summary>
		/// Gets or sets the labour hours.
		/// </summary>
		/// <value>
		/// The labour hours.
		/// </value>
		public decimal LabourHours { get; set; }

		/// <summary>
		/// Gets or sets the price.
		/// </summary>
		/// <value>
		/// The price.
		/// </value>
		public decimal Price { get; set; }

		/// <summary>
		///     Creates a random job
		/// </summary>
		/// <returns></returns>
		public static Job CreateRandom(List<Item> availableItems)
		{
			var rand = new Random();
			var jobItems = new List<JobItem>();
			var itemsAdded = new List<Item>();

			//Limit the number of items to reduce the chance of there being validation errors as otherwise they happen all the time
			var numberOfItemsToAdd = rand.Next(1, 3);

			//Need to make a copy of the list so as not to remove items from the orignal
			var availableItemsCopy = new List<Item>();
			availableItemsCopy.AddRange(availableItems);

			for(var i = 1; i <= numberOfItemsToAdd; i++)
			{
				//Get the index of the item to add and get the item
				var itemIndex = rand.Next(availableItemsCopy.Count - 1);
				var itemToAdd = availableItemsCopy[itemIndex];

				itemsAdded.Add(itemToAdd);

				//Remove the item so we don't have duplicate items
				availableItemsCopy.Remove(itemToAdd);

				var quantity = rand.Next(1,3);

				//This just ensures there's a chance that an invalid amount of tyres is created
				if (itemToAdd.Name == "Tyre")
				{
					quantity = rand.Next(6);
				}

				//Oil quantity will always be one.
				if(itemToAdd.Name == "Oil Change")
				{
					quantity = 1;
				}

				var jobItem = new JobItem
				{
					ItemId = itemToAdd.Id,
					Quantity = quantity
				};

				jobItems.Add(jobItem);
			}

			//Calculate the expected total labour
			var expectedTotalLabour = 0m;

			foreach (var item in itemsAdded)
			{
				var jobItem = jobItems.First(x => x.ItemId == item.Id);
				var labourHours = jobItem.Quantity * (item.UnitTime / 60m);
				expectedTotalLabour += labourHours;
			}
			
			//Calculate the expected price
			var expectedPrice = 0m;

			foreach (var item in itemsAdded)
			{
				//Get the item from the db that relates to the job item
				var jobItem = jobItems.First(x => x.ItemId == item.Id);
				expectedPrice += (item.UnitCost * jobItem.Quantity);
			}
			
			//Add the cost of labour to the expected price
			expectedPrice += (expectedTotalLabour * AppSettings.LabourCost);

			//Create a chance of exceeding total labour
			var exceedTotalLabour = rand.Next(0, 4) == 4;

			var job = new Job
			{
				Items = jobItems
			};

			if (exceedTotalLabour)
			{
				job.LabourHours = expectedTotalLabour + 5;
			}
			else
			{
				job.LabourHours = expectedTotalLabour;
			}

			//Create the chance of the cost being incorrect
			var totalCostDecider = rand.Next(0, 5);

			if(totalCostDecider < 3)
			{
				//Set the total cost to reference price - 5% (Should be approved)
				var percentageOfPrice = (expectedPrice / 100) * 5;
				job.Price = expectedPrice - percentageOfPrice;
			}
			else if(totalCostDecider == 4)
			{
				//Set the total cost to reference price - 12% (Should be referred)
				var percentageOfPrice = (expectedPrice / 100) * 12;
				job.Price = expectedPrice - percentageOfPrice;
			}
			else if (totalCostDecider == 5)
			{
				//Set the total cost to reference price - 20% (Should be declined)
				var percentageOfPrice = (expectedPrice / 100) * 20;
				job.Price = expectedPrice - percentageOfPrice;
			}

			return job;
		}

		/// <summary>
		/// Validates the job.
		/// Sets the approval status
		/// </summary>
		/// <param name="items">The items.</param>
		public void SetApprovalStatus(List<Item> items)
		{
			ValidateJobItems(items);

			//If the job's already invalid, then just return
			if(ApprovalStatus == ApprovalStatus.Declined)
			{
				return;
			}
			
			var expectedTotalLabour = 0m;

			foreach (var jobItem in Items)
			{
				//Get the item from the db that relates to the job item
				var jobItemItem = items.First(x => x.Id == jobItem.ItemId);
				expectedTotalLabour += jobItem.Quantity * (jobItemItem.UnitTime / 60m);
			}

			if(expectedTotalLabour < LabourHours)
			{
				ApprovalStatus = ApprovalStatus.Declined;
				ApprovalMessage = "Labour hours exceed the expected labour hours";
				return;
			}

			var expectedPrice = 0m;

			foreach (var jobItem in Items)
			{
				//Get the item from the db that relates to the job item
				var jobItemItem = items.First(x => x.Id == jobItem.ItemId);
				expectedPrice += (jobItemItem.UnitCost * jobItem.Quantity);
			}
			
			//Add the cost of labour to the expected price
			expectedPrice += (LabourHours * AppSettings.LabourCost);

			var tenPercentTotalCost = (expectedPrice / 100) * 10;
			var fifteenPercentTotalCost = (expectedPrice / 100) * 15;

			if(expectedPrice + tenPercentTotalCost > Price && expectedPrice - tenPercentTotalCost < Price)
			{
				//Price is within 10 percent of reference price so approve
				ApprovalStatus = ApprovalStatus.Approved;
				ApprovalMessage = "Automatically Approved";
			}
			else if(expectedPrice + fifteenPercentTotalCost > Price && expectedPrice - fifteenPercentTotalCost < Price)
			{
				//Price is between 10/15% of referenced price so refer
				ApprovalStatus = ApprovalStatus.Referred;
				ApprovalMessage = "Sent for referral";
			}
			else
			{
				//Price exceeds 15% of referenced price so decline
				ApprovalStatus = ApprovalStatus.Declined;
				ApprovalMessage = "Provided total cost exceeds the referenced cost";
			}
		}

		/// <summary>
		/// Validates the job items.
		/// </summary>
		/// <param name="items">The items.</param>
		private void ValidateJobItems(IReadOnlyCollection<Item> items)
		{
			foreach (var jobItem in Items)
			{
				//Check the quantity
				var item = items.First(x => x.Id == jobItem.ItemId);
				if(item.MaxQuantity.HasValue && jobItem.Quantity > item.MaxQuantity)
				{
					ApprovalStatus = ApprovalStatus.Declined;
					ApprovalMessage += $"{item.Name} exceeds the maximum quantity of {item.MaxQuantity}. ";
				}
				
				//Check that items that depend on other items have their dependencies.
				//E.g. Check that brake pads/discs are the same quantity
				foreach (var relatedItemId in item.RelatedItems)
				{
					var relatedJobItem = Items.FirstOrDefault(x => x.ItemId == relatedItemId);

					if (relatedJobItem == null || relatedJobItem.Quantity != jobItem.Quantity)
					{
						var relatedItem = items.First(x => x.Id == relatedItemId);
						ApprovalStatus = ApprovalStatus.Declined;
						ApprovalMessage += $"There must be the same number of {item.Name} as {relatedItem.Name}. ";
					}
				}

				//If an item only allows certain values (e.g tyres only allow 2 or 4) thenc check the value
				if (item.ValidQuantities.Any())
				{
					if (!item.ValidQuantities.Contains(jobItem.Quantity))
					{
						ApprovalStatus = ApprovalStatus.Declined;
						ApprovalMessage = $"{jobItem.Quantity} is not a valid quantity for {item.Name}. ";
					}
				}
			}
		}
	}
}