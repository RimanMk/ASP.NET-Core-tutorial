using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace HelloApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
            //options.DefaultFileNames.Add("hello.html"); // добавляем новое имя файла
            //app.UseDirectoryBrowser();
            //app.UseDefaultFiles(options);
            //app.UseStaticFiles();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            ///------------------------
            //app.UseToken("555555");

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});
            //-------------------------
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseMiddleware<RoutingMiddleware>();
            //----------------------
            //app.UseOwin(pipeline =>
            //{
            //    pipeline(next => SendResponseAsync);
            //});
            //--------------------
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");

            app.Run(async (context) =>
            {
                logger.LogInformation("Processing request {0}", context.Request.Path);
                await context.Response.WriteAsync("Hello World!");
            });
        }
        public Task SendResponseAsync(IDictionary<string, object> environment)
        {
            // получаем заголовки запроса
            var requestHeaders = (IDictionary<string, string[]>)environment["owin.RequestHeaders"];
            // получаем данные по User-Agent
            string responseText = requestHeaders["User-Agent"][0];
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

            var responseStream = (Stream)environment["owin.ResponseBody"];

            return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
