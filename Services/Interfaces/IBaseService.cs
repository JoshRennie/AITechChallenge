namespace ServiceLayer.Interfaces
{
	using System.Collections.Generic;

	public interface IBaseService<T>
	{
		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		void Add(T entity);

		/// <summary>
		/// Gets the list of entities
		/// </summary>
		/// <returns></returns>
		List<T> GetList();

		/// <summary>
		/// Gets the specified entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		T Get(int id);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		void Delete(int id);
	}
}