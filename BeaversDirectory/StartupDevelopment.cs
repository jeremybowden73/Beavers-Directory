﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeaversDirectory.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeaversDirectory
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BeaversDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));
            
            // add the default identity system configuration, passing in the User and Role type that we want to use.
            // BeaversDbContext is used to store the information for a user
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BeaversDbContext>();

            // when an instance of IBeaversRepository is requested via Dependency Injection,
            // a new BeaversRepository will be returned
            services.AddTransient<IBeaversRepository, BeaversRepository>();

            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            services.AddTransient<IEnquireRepository, EnquireRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //set environment to Production for testing
            //env.EnvironmentName = EnvironmentName.Production;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }

            // redirect http to https
            RewriteOptions options = new RewriteOptions().AddRedirectToHttpsPermanent();
            app.UseRewriter(options);

            //app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
