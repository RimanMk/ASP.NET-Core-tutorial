using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using DI.Services;

namespace DI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IMessageSender, EmailMessageSender>();
            //services.AddTransient<MessageService>();

            ////services.AddTransient<TimeService>();
            //services.AddTimeService();
            ////------------------------
            //services.AddSingleton<ICounter, RandomCounter>();
            //services.AddSingleton<CounterService>();
            //--------------------------
            services.AddTransient<RandomCounter>();
            services.AddTransient<ICounter>(provider =>
            {
                var counter = provider.GetService<RandomCounter>();
                return counter;
            });
            services.AddTransient<CounterService>();

            services.AddTransient<IMessageSender>(provider => {

                if (DateTime.Now.Hour >= 12) return new EmailMessageSender();
                else return new SmsMessageSender();
            });
            services.AddTransient<TimeService>();
        }
        public void Configure(IApplicationBuilder app)
        {
            //app.UseMiddleware<MessageMiddleware>();
            //app.Run(async (context) =>
            //{
            //    //IMessageSender messageSender = context.RequestServices.GetService<IMessageSender>();
            //    IMessageSender messageSender = app.ApplicationServices.GetService<IMessageSender>();
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync(messageSender.Send());
            //});
            //-----
            //app.UseMiddleware<CounterMiddleware>();
            //-----
            app.UseMiddleware<TimerMiddleware>();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
