using BusinessLogic.Extensions;
using ClassLibrary1;
using Data.Extensions;
using Xxf.Service.Exception.Extensions;

namespace violaAPITest
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions();

            services
                .AddCors()
                .AddControllers();

            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()                
                //.AddBusiness(_configuration)
                .AddClass();


            ////DB
            //var dataOptionsSettings = _configuration.GetSection(nameof(DataOptions));
            //services.Configure<DataOptions>(dataOptionsSettings);
            //services
            //    .AddDataContext(opt =>
            //    {
            //        opt.Connectionstring = dataOptionsSettings.GetValue<string>("Connectionstring");
            //        opt.Provider = dataOptionsSettings.GetValue<string>("Provider");
            //    });

            services.AddDataContext2(_configuration);

            //var businessOptions = _configuration.GetSection(nameof(BusinessOptions));
            //services.Configure<BusinessOptions>(businessOptions);
            services
                .AddBusiness(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDomainDataErrorException();
        }

    }
}
