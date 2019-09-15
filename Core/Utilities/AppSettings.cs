namespace Core.Utilities
{
	/// <summary>
	/// Stores the app settings from appsettings.json so they can be used from other projects
	/// </summary>
	public static class AppSettings
	{
		/// <summary>
		/// Gets or sets the labour cost.
		/// </summary>
		/// <value>
		/// The labour cost.
		/// </value>
		public static decimal LabourCost { get; set; }
	}
}