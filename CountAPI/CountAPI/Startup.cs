using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CountAPI.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Miki.Rest;

namespace CountAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			Console.WriteLine(Configuration.GetValue<ulong>("DiscordBotsUserId"));

			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			CountController.OnGuildChange += async (c) =>
			{
				var client = new RestClient("https://discordbots.org/api/");

				var x = await client
					.SetAuthorization(Configuration.GetValue<string>("DiscordBotsToken"))
					.PostAsync("bots/160105994217586689/stats", $"{{\"server_count\": {c}}}");
			};
			CountController.OnGuildChange += async (c) =>
			{
				using (var client = new HttpClient())
				{
					var values = new Dictionary<string, string>
				{
				   { "key", Configuration.GetValue<string>("CarbonitexToken") },
				   { "servercount", c.ToString() }
				};

					FormUrlEncodedContent content = new FormUrlEncodedContent(values);
					HttpResponseMessage response = await client.PostAsync("https://www.carbonitex.net/discord/data/botdata.php", content);
					string responseString = await response.Content.ReadAsStringAsync();
				}
			};
			CountController.OnGuildChange += async (c) =>
			{
				var client = new RestClient("https://bots.discord.pw/api/bots/160105994217586689/stats");

				await client
					.SetAuthorization(Configuration.GetValue<string>("DiscordPwToken"))
					.PostAsync<string>("", $"{{\"server_count\": {c}}}");
			};

			app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc();
        }
    }
}
