namespace ServiceLayer.ServiceObjects
{
	using System.Collections.Generic;
	using Core.Entities;
	using DataLayer.Interfaces;
	using Interfaces;

	/// <summary>
	/// Used for executing business logic relating to items as well as calling the items dao
	/// </summary>
	/// <seealso cref="ServiceLayer.Interfaces.IBaseService{Item}" />
	public class ItemsService : IBaseService<Item>
	{
		/// <summary>
		/// The items DAO
		/// </summary>
		private readonly IBaseDao<Item> _itemsDao;

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemsService"/> class.
		/// </summary>
		/// <param name="itemsDao">The items DAO.</param>
		public ItemsService(IBaseDao<Item> itemsDao)
		{
			_itemsDao = itemsDao;
		}

		/// <summary>
		/// Adds the specified item.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Add(Item item)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the list of items
		/// </summary>
		/// <returns></returns>
		public List<Item> GetList()
		{
			return _itemsDao.GetList();
		}

		/// <summary>
		/// Gets the specified item by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public Item Get(int id)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Deletes the specified item by id.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}