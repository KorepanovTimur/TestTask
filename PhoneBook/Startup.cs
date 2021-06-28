using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Data;
using PhoneBook.Data.interfaces;
using PhoneBook.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class Startup
    {
        private IConfigurationRoot _confString;
        public Startup(IHostingEnvironment hostingEnvironment)
        {//устанавливаем связь с базой данных
            _confString = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        //добавляем необходимые сервисы
        public void ConfigureServices(IServiceCollection services)
        {   //устанавливаем строку для всязи с базой данных
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            //связываем интерфейс и моель реализующую её
            services.AddTransient<IContact, ContactRepository>();
            services.AddTransient<IPerson, PersonRepository>();
            //подключаем MVC
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //подключаем возможность использования статических файлов
            app.UseStaticFiles();
            //bcgjkmpetv cnhfybws jib,jr lkz hfphf,jnxbrf
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            //устанавливааем стандартный путь для приложения, контроллер и метод
            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template: "{Controller=home}/{action=Index}/{search?}");
                });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent context = scope.ServiceProvider.GetRequiredService<AppDBContent>();
            }
        }
    }
}
