using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CountAPI.Common;
using Miki.Rest;

namespace CountLib
{
    public class CountLib
    {
		RestClient client;

		public CountLib()
		{
			client = new RestClient("https://servers.miki.ai/api");
		}

		// GET ALL
		public async Task<List<Shard>> GetAllShards()
			=> (await client.GetAsync<List<Shard>>("/count/shards")).Data;

		// GET COUNT
		public async Task<List<Shard>> GetCount()
			=> (await client.GetAsync<List<Shard>>("/count")).Data;

		// GET ID
		public async Task<List<Shard>> GetId(int id)
			=> (await client.GetAsync<List<Shard>>($"/count/{id}")).Data;

		// SEND STATS
		public async Task PostStats(int id, int count)
			=> await client.PostAsync("/count", $"{{\"id\":{id}, \"count\":{count}}}");
	}
}
