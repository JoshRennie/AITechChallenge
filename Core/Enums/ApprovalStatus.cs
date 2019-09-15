namespace Core.Enums
{
	/// <summary>
	/// The approval status of a job
	/// </summary>
	public enum ApprovalStatus
	{
		/// <summary>
		/// The job has been automatically renewed
		/// </summary>
		Approved = 1,

		/// <summary>
		/// The job has been referred
		/// </summary>
		Referred = 2,

		/// <summary>
		/// The job has been declined
		/// </summary>
		Declined = 3
	}
}