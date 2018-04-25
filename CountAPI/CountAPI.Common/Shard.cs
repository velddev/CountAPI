using System;

namespace CountAPI.Common
{
	public class Shard
	{
		public int Id { get; set; }
		public int Count { get; set; }

		public Shard() { }
		public Shard(int id, int count)
		{
			Id = id;
			Count = count;
		}
	}
}