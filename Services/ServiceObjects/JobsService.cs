namespace ServiceLayer.ServiceObjects
{
	using System.Collections.Generic;
	using Core.Entities;
	using Core.Enums;
	using DataLayer.Interfaces;
	using Interfaces;

	/// <summary>
	/// Used for executing business logic relating to jobs as well as calling the jobs dao
	/// </summary>
	/// <seealso cref="ServiceLayer.Interfaces.IBaseService{Job}" />
	public class JobsService : IBaseService<Job>
	{
		/// <summary>
		/// The jobs DAO
		/// </summary>
		private readonly IBaseDao<Job> _jobsDao;

		/// <summary>
		/// The items DAO
		/// </summary>
		private readonly IBaseDao<Item> _itemsDao;

		/// <summary>
		/// Initializes a new instance of the <see cref="JobsService" /> class.
		/// </summary>
		/// <param name="jobsDao">The jobs DAO.</param>
		/// <param name="itemsDao">The items DAO.</param>
		public JobsService(IBaseDao<Job> jobsDao, IBaseDao<Item> itemsDao)
		{
			_jobsDao = jobsDao;
			_itemsDao = itemsDao;
		}

		/// <summary>
		/// Adds the specified job.
		/// </summary>
		/// <param name="job">The job.</param>
		public void Add(Job job)
		{
			var items = _itemsDao.GetList();
			job.SetApprovalStatus(items);
			_jobsDao.Add(job);
		}

		/// <summary>
		/// Gets the list of jobs
		/// </summary>
		/// <returns></returns>
		public List<Job> GetList()
		{
			return _jobsDao.GetList();
		}

		/// <summary>
		/// Gets the specified job by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public Job Get(int id)
		{
			return _jobsDao.Get(id);
		}

		/// <summary>
		/// Deletes the specified job by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public void Delete(int id)
		{
			_jobsDao.Delete(id);
		}
	}
}