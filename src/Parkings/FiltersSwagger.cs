using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Parkings
{
    public class FiltersSwagger : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.Name.StartsWith("Create")) // Asumiendo que los métodos de creación comienzan con "Create"
            {
                var parameterType = context.MethodInfo.GetParameters().FirstOrDefault()?.ParameterType;
                if(parameterType == typeof(DataAccess.DTO.WorkerDto))
                {
                    var schema = operation.RequestBody?.Content?.FirstOrDefault().Value.Schema;
                    schema?.Properties.Remove("id");
                }
                
            }
        }
    }
}