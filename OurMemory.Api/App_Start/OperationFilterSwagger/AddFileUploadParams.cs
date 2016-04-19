using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace OurMemory.OperationFilterSwagger
{
    public class AddFileUploadParams : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId.Contains("Files"))
            {

                if (operation.operationId.Contains("Post"))
                {
                    operation.consumes.Add("multipart/form-data");
                    operation.parameters = new[]
                    {
                        new Parameter
                        {
                            name = "file",
                            @in = "formData",
                            required = true,
                            type = "file",

                        }
                    };
                }
            }

        }
    }
}
