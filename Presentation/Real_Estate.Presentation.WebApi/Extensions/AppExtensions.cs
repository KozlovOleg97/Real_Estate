using Swashbuckle.AspNetCore.SwaggerUI;

namespace Real_Estate.Presentation.WebApi.Extensions
{
	public static class AppExtensions
	{
		public static void UseSwaggerExtension(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant Api");

                options.DefaultModelRendering(ModelRendering.Model);
            });
		}
	}
}
