using NUnit.Framework;

namespace Tests
{
	using System.Collections.Generic;
	using Core.Entities;
	using Core.Enums;
	using Core.Utilities;

	public class JobValidationTests
	{
		/// <summary>
		/// The items used for testing
		/// </summary>
		private static readonly List<Item> Items = new List<Item>
		{
			new Item(1, "Tyres", 30, 200, maxQuantity: 4, validQuantities: new List<int>{2, 4}),
			new Item(2, "Brake Discs", 90, 100, new List<int>{3}),
			new Item(3, "Brake Pads", 60, 50, new List<int>{2}),
			new Item(4, "Oil Change", 30, 20, maxQuantity: 1),
			new Item(5, "Exhausts", 240, 175, maxQuantity: 1)
		};

		[SetUp]
		public void Setup()
		{
			AppSettings.LabourCost = 45;
		}

		/// <summary>
		/// Checks the price validation
		/// </summary>
		[Test]
		public void PriceValidationCheck()
		{
			var job = CreateJobForPriceValidation(310);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Approved, "Approval status not set to approved when provided price is the same as reference price");

			job = CreateJobForPriceValidation(320);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Approved, "Approval status not set to approved when provided price is within 10% of reference price");

			job = CreateJobForPriceValidation(345);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Referred, "Approval status not set to referred when provided price is between 10 and 15% of reference price");

			job = CreateJobForPriceValidation(600);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status not set to declined when provided price is greater than 15% of reference price");
		}

		/// <summary>
		/// Creates the job for price validation.
		/// </summary>
		/// <param name="price">The price.</param>
		/// <returns></returns>
		private Job CreateJobForPriceValidation(decimal price)
		{
			var job = new Job
			{
				LabourHours = 3,
				Price = price,
				Items = new List<JobItem>()
			};

			job.Items.Add(new JobItem
			{
				ItemId = 5,
				Quantity = 1
			});

			return job;
		}

		/// <summary>
		/// Checks the labour hours validation
		/// </summary>
		[Test]
		public void LabourHoursValidationCheck()
		{
			var job = CreateJobForLabourHoursValidation(4);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus != ApprovalStatus.Declined, "Approval status not set to approved or referred when provided labour hours is the same as reference labour hours");

			job = CreateJobForLabourHoursValidation(5);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status not set to declined when provided labour hours is greater than reference labour hours");

			job = CreateJobForLabourHoursValidation(3);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus != ApprovalStatus.Declined, "Approval status not set to approved or referred when provided labour hours is less than reference labour hours");
		}

		/// <summary>
		/// Creates the job for labour hours validation.
		/// </summary>
		/// <param name="labourHours">The labour hours.</param>
		/// <returns></returns>
		private Job CreateJobForLabourHoursValidation(int labourHours)
		{
			var job = new Job
			{
				Items = new List<JobItem>(),
				LabourHours = labourHours,
				Price = 175 + (AppSettings.LabourCost * labourHours)
			};

			job.Items.Add(new JobItem
			{
				ItemId = 5,
				Quantity = 1
			});

			return job;
		}

		/// <summary>
		/// Checks the exhaust validation
		/// </summary>
		[Test]
		public void ExhaustValidationCheck()
		{
			var job = CreateJobForExhaustValidation(1);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Approved, "Approval status not set to approved for 1 exhaust");

			job = CreateJobForExhaustValidation(2);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status not set to declined for 2 exhausts");

		}

		/// <summary>
		/// Creates the job for exhaust validation.
		/// </summary>
		/// <param name="numberOfExhausts">The number of exhausts.</param>
		/// <returns></returns>
		private Job CreateJobForExhaustValidation(int numberOfExhausts)
		{
			var job = new Job
			{
				Price = numberOfExhausts * 175,
				LabourHours = numberOfExhausts * (240 / 60m),
				Items = new List<JobItem>()
			};

			job.Price = (numberOfExhausts * 175) + (job.LabourHours * AppSettings.LabourCost);

			job.Items.Add(new JobItem
			{
				ItemId = 5,
				Quantity = numberOfExhausts
			});

			return job;
		}

		/// <summary>
		/// Checks the tyre validtion
		/// </summary>
		[Test]
		public void TyreValidationCheck()
		{
			var job = CreateJobForTyreValidation(1);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status not set to declined for 1 tyre");

			job = CreateJobForTyreValidation(2);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Approved, "Approval status not set to approved for 2 tyres");

			job = CreateJobForTyreValidation(3);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status not set to declined for 3 tyres");

			job = CreateJobForTyreValidation(4);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Approved, "Approval status not set to approved for 4 tyres");

			job = CreateJobForTyreValidation(5);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status not set to declined for 5 tyres");
		}

		/// <summary>
		/// Creates the job for tyre validation.
		/// </summary>
		/// <param name="numberOfTyres">The number of tyres.</param>
		/// <returns></returns>
		private Job CreateJobForTyreValidation(int numberOfTyres)
		{
			var job = new Job
			{
				LabourHours = 0.5m * numberOfTyres,
				Items = new List<JobItem>()
			};

			job.Price = (numberOfTyres * 200) + (job.LabourHours * AppSettings.LabourCost);

			job.Items.Add(new JobItem
			{
				ItemId = 1,
				Quantity = numberOfTyres
			});

			return job;
		}

		/// <summary>
		/// Checks the brake disc and brake pad validation
		/// </summary>
		[Test]
		public void BrakePadDiscsValidationCheck()
		{
			var job = CreateJobForBrakeDiscPadValidation(2, 2);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Approved, "Approval status is not set to approved when there are the same number of brake discs and pads");

			job = CreateJobForBrakeDiscPadValidation(1, 0);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status is not set to declined when there are the no brake pads");

			job = CreateJobForBrakeDiscPadValidation(1, 2);

			job.SetApprovalStatus(Items);

			Assert.IsTrue(job.ApprovalStatus == ApprovalStatus.Declined, "Approval status is not set to declined when there are the different number of brake discs and pads");
		}

		/// <summary>
		/// Creates the job for brake disc & pad validation.
		/// </summary>
		/// <param name="numberOfDiscs">The number of discs.</param>
		/// <param name="numberOfPads">The number of pads.</param>
		/// <returns></returns>
		private static Job CreateJobForBrakeDiscPadValidation(int numberOfDiscs, int numberOfPads)
		{
			var job = new Job
			{
				Items = new List<JobItem>(),
				LabourHours = (numberOfDiscs * 1.5m) + (numberOfPads * 1)
			};

			job.Price = (numberOfDiscs * 100) + (numberOfPads * 50) + (job.LabourHours * AppSettings.LabourCost);

			job.Items.Add(new JobItem
			{
				ItemId = 2,
				Quantity = numberOfDiscs
			});

			if (numberOfPads > 0)
			{
				job.Items.Add(new JobItem
				{
					ItemId = 3,
					Quantity = numberOfPads
				});
			}

			return job;
		}
	}
}