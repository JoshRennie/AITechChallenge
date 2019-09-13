namespace Api.Models
{
	using System;

	/// <summary>
	/// A maintenance job that needs to be performed.
	/// Holds all the parts that need replcaing
	/// </summary>
	public class Job
	{
		/// <summary>
		/// Gets or sets the number of tyres that need replacing
		/// </summary>
		/// <value>
		/// The tyres.
		/// </value>
		public int Tyres { get; set; }

		/// <summary>
		/// Gets or sets the number of brake discs that need replacing
		/// </summary>
		/// <value>
		/// The brake discs.
		/// </value>
		public int BrakeDiscs { get; set; }

		/// <summary>
		/// Gets or sets the number of brake pads that need replacing
		/// </summary>
		/// <value>
		/// The brake pads.
		/// </value>
		public int BrakePads { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the oil needs changing.
		/// </summary>
		/// <value>
		///   <c>true</c> if [change oil]; otherwise, <c>false</c>.
		/// </value>
		public bool ChangeOil { get; set; }

		/// <summary>
		/// Gets or sets the number of exhausts that need changing
		/// </summary>
		/// <value>
		/// The exhaust.
		/// </value>
		public int Exhausts { get; set; }

		/// <summary>
		/// Creates a random job
		/// </summary>
		/// <returns></returns>
		public static Job CreateRandom()
		{
			var rand = new Random();

			return new Job
			{
				Tyres = rand.Next(6),
				BrakeDiscs = rand.Next(10),
				BrakePads = rand.Next(10),
				Exhausts = rand.Next(2),
				ChangeOil = rand.Next(1) == 1
			};
		}
	}
}