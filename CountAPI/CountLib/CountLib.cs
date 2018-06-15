using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CountAPI.Common;
using Miki.Rest;

namespace CountAPI
{
    public class CountLib
    {
		RestClient client;

		string _authCode = "";

		public CountLib(string baseUrl, string authCode)
		{
			client = new RestClient(baseUrl);
			_authCode = authCode;
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
			=> await client.SetAuthorization(_authCode)
				.PostAsync("/count", $"{{\"id\":{id}, \"count\":{count}}}");
	}
}
