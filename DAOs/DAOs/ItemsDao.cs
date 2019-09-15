namespace DataLayer.DAOs
{
	using System.Collections.Generic;
	using Core.Entities;
	using Interfaces;

	/// <summary>
	/// Handles the storing/accessing of items
	/// Items are stored in a list, but ordinarily this would connect to a DB
	/// </summary>
	/// <seealso cref="IBaseDao{T}" />
	public class ItemsDao : IBaseDao<Item>
	{
		/// <summary>
		/// The items. This mimicks what would be stored in the DB
		/// </summary>
		private static readonly List<Item> Items = new List<Item>
		{
			new Item(1, "Tyres", 30, 200, maxQuantity: 4, validQuantities: new List<int>{2, 4}),
			new Item(2, "Brake Discs", 90, 100, new List<int>{3}),
			new Item(3, "Brake Pads", 60, 50, new List<int>{2}),
			new Item(4, "Oil Change", 30, 20, maxQuantity: 1),
			new Item(5, "Exhausts", 240, 175, maxQuantity: 1)
		};

		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Add(Item entity)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the list of items
		/// </summary>
		/// <returns></returns>
		public List<Item> GetList()
		{
			return Items;
		}

		/// <summary>
		/// Gets the specified entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public Item Get(int id)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Delete(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}