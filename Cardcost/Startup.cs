using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cardcost.Core.Services;
using Cardcost.Core.Services.interfaces;
using Cardcost.Core.ValidationRules;
using Cardcost.Core.ValidationRules.Interfaces;
using Cardcost.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Cardcost
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
            services.AddControllers();
            services.AddHttpClient<ICardService, CardService>();
            services.AddScoped<ApiExceptionFilter>();
            services.AddScoped<IValidateCardNumber, ValidateCardNumber>();
            //services.AddSingleton<RedisService>();

            //Redis configuration
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, RedisService redisService*/)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //redisService.Connect();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
