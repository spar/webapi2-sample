using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Http;
using Auth0.Owin;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Newtonsoft.Json.Serialization;
using Owin;
using Product.Repositories;
using Product.Services;
using Swashbuckle.Application;

namespace Product.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            app.UseCors(CorsOptions.AllowAll);
            if (bool.Parse(ConfigurationManager.AppSettings["AuthenticationEnabled"]))
                ConfigureAuth0(app);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.Register(p => new ProductInMemoryRepository(GetDemoModels())).As<IProductRepository>().SingleInstance();

            builder.RegisterType<ProductService>().As<IProductService>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Product.Api");
                    c.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\bin\Product.Api.XML");
                })
                .EnableSwaggerUi();
            app.UseWebApi(config);
        }

        private void ConfigureAuth0(IAppBuilder app)
        {
            var domain = $"https://{ConfigurationManager.AppSettings["Auth0Domain"]}/";
            var apiIdentifier = ConfigurationManager.AppSettings["Auth0ApiIdentifier"];
            var keyResolver = new OpenIdConnectSigningKeyResolver(domain);
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = apiIdentifier,
                        ValidIssuer = domain,
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) => keyResolver.GetSigningKey(kid)
                    }
                });
        }

        private static List<Models.Product> GetDemoModels()
        {
            return new List<Models.Product>
            {
                new Models.Product(Guid.NewGuid().ToString(), "Microsoft's Surface Pro 6", "Surface Pro 6", "Microsoft Surface"),
                new Models.Product(Guid.NewGuid().ToString(), "Microsoft's Surface Pro 4", "Surface Pro 4", "Microsoft Surface"),
                new Models.Product(Guid.NewGuid().ToString(), "Microsoft's Surface Book 2", "Surface Book 2", "Microsoft Surface"),
                new Models.Product(Guid.NewGuid().ToString(), "Microsoft's Surface Studio", "Surface Studio", "Microsoft Surface")
            };
        }
    }
}