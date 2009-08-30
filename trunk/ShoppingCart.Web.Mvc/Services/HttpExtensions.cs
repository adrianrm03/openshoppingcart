using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Services
{
	public static class CacheExtensions
	{
		/// <summary>
		/// Return the first the or default cached item.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache">The cache.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static T FirstOrDefault<T>(this System.Web.Caching.Cache cache, Func<T, bool> predicate)
		{
			var list = System.Web.HttpContext.Current.Cache.Cast<System.Collections.DictionaryEntry>()
				.Where(i => i.Value.GetType() == typeof(T))
				.Select(i => i.Value).Cast<T>();

			return list.FirstOrDefault(predicate);
		}

		/// <summary>
		/// Gets the list of.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache">The cache.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		public static IQueryable<T> GetListOf<T>(this System.Web.Caching.Cache cache)
		{
			var list = System.Web.HttpContext.Current.Cache.Cast<System.Collections.DictionaryEntry>()
						.Where(i => i.Value.GetType() == typeof(T))
						.Select(i => i.Value).Cast<T>();

			return list.AsQueryable();
		}
	}
}
