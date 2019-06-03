using Product.Api.Attributes;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace Product.Api.Swagger
{
    public class SwaggerAuthenticationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var toBeAuthorize = apiDescription.GetControllerAndActionAttributes<ScopeAuthorizeAttribute>().Any();
            if (!toBeAuthorize) return;
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "Bearer Token",
                required = true,
                type = "string"
            });
        }
    }
}