using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StPrintQueue.Db;
using StPrintQueue.Print;
using System;

namespace StPrintQueue.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private QueueManager _queue;
        public void ConfigureServices(IServiceCollection services)
        {
            _queue = new QueueManager();
            try
            {
                //restore data from previous session.
                _queue.LoadFrom("queue.json");
            }
            catch(Exception ex)
            {
                //better to to log this.. something terrible happened.
            }

            services.AddMvc();
            services.AddSingleton<QueueManager>(_queue);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);
            applicationLifetime.ApplicationStarted.Register(() => PrintService.BackgroundService(ref _queue, applicationLifetime.ApplicationStopping));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        protected void OnShutdown()
        {
            //this code is called when the application stops to save current queue state
            try
            {
                _queue.WriteTo("queue.json");
            }
            catch(Exception ex)
            {
                //better to to log this.. something terrible happened.

            }
        }
    }
}
