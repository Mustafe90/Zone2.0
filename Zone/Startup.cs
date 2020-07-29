using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Spotify.OAuth;
using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Zone.Domain;
using Zone.Services;

namespace Zone
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
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddTransient<SpotifyClientDomain>(); 
            
            //Utilises IHttpClientFactory to manage the life cycle of our http client
            services.AddHttpClient<SpotifyHttpClientService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(80);
                client.BaseAddress = new Uri("https://api.spotify.com");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddAuthentication(option =>
                    {
                        option.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                        option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                .AddOpenIdConnect(options =>
                {
                    options.ClientId = "4c753863-092d-44d9-943c-9c27e28b5099";
                    options.Authority = "https://login.microsoftonline.com/74c6bfb9-08e2-4a30-a7d1-a8ef361cd197";
                    options.SignedOutRedirectUri = "/";
                    options.CallbackPath = "/auth/signin-callback";
                    options.ResponseType = OpenIdConnectResponseType.IdToken;
                })
                .AddSpotify(options =>
                {
                    options.CallbackPath = "/auth/spotify-callback";
                    options.ClientId = "943ddecea1524a8e8c632c8b588e7c5d";
                    options.ClientSecret = "72b649a893574643a7f5ac6b43275be7";
                    options.Scope.Add("user-library-read");
                    options.Scope.Add("user-top-read");
                    options.Scope.Add("user-read-recently-played");
                    options.Scope.Add("user-read-currently-playing");
                    options.Scope.Add("user-read-email");
                    options.Scope.Add("user-read-private");
                    options.SaveTokens = true;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
               
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
