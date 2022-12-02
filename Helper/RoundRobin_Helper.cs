using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SocialNetwork_New.Helper
{
	class RoundRobin_Helper<T>
	{
		private IList<T> _list;
		private int _size;
		private int _position;

		public RoundRobin_Helper()
		{
		}

		public void SetUp(IList<T> list)
		{
			if (!list.Any())
			{
				throw new NullReferenceException("list");
			}

			if (_list.Any())
			{
				_list.Clear();
				_list = null;
			}

			_list = new List<T>(list);
			_size = _list.Count;
			_position = 0;
		}

		public T Next()
		{
			if (_size == 1)
			{
				return _list[0];
			}

			Interlocked.Increment(ref _position);

			if (_position == Int32.MinValue)
			{ 
				Interlocked.Exchange(ref _position, 0); 
			}

			int mod = _position % _size;

			return _list[mod];
		}
	}
}
