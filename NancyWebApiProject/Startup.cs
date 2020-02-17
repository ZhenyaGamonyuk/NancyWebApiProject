using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;
using NancyWebApiProject.DataProviders;
using NancyWebApiProject.Services;

namespace NancyWebApiProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataProvider, NewYorkTimesDataProvider>();
            services.AddScoped<IArticleService, ArticleService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOwin(b => b.UseNancy());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
