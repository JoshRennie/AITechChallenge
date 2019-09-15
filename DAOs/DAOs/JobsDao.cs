namespace DataLayer.DAOs
{
	using System.Collections.Generic;
	using System.Linq;
	using Core.Entities;
	using Interfaces;

	/// <summary>
	/// Handles the storing/accessing of jobs
	/// Jobs are stored in a list, but ordinarily this would connect to a DB
	/// </summary>
	/// <seealso cref="IBaseDao{T}" />
	public class JobsDao : IBaseDao<Job>
	{
		/// <summary>
		/// The jobs. This mimicks what would be stored in a DB
		/// </summary>
		private static readonly List<Job> Jobs = new List<Job>();

		/// <summary>
		/// Adds the specified job.
		/// </summary>
		/// <param name="job">The job.</param>
		public void Add(Job job)
		{
			//Set the Ids of the job and job items to mimck a DB
			var currentId = 1;
			var currentJobItemId = 1;

			if (Jobs.Any())
			{
				currentId = Jobs.Last().Id + 1;

				currentJobItemId = Jobs.Last().Items.Max(x => x.Id) + 1;
			}

			job.Id = currentId;
			job.Items.ForEach(x => x.JobId = currentId);

			foreach (var jobItem in job.Items)
			{
				jobItem.Id = currentJobItemId;
				currentJobItemId++;
			}

			Jobs.Add(job);
		}

		/// <summary>
		/// Gets the list of jobs
		/// </summary>
		/// <returns></returns>
		public List<Job> GetList()
		{
			return Jobs;
		}

		/// <summary>
		/// Gets a specific job by Id
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public Job Get(int id)
		{
			return Jobs.FirstOrDefault(x => x.Id == id);
		}

		/// <summary>
		/// Deletes the specified job.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public void Delete(int id)
		{
			Jobs.RemoveAll(x => x.Id == id);
		}
	}
}