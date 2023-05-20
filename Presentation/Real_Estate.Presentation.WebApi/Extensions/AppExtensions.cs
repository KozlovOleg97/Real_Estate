using Swashbuckle.AspNetCore.SwaggerUI;

namespace Real_Estate.Presentation.WebApi.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class AppExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		public static void UseSwaggerExtension(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "RealsEstate API");

                options.DefaultModelRendering(ModelRendering.Model);
            });
		}
	}
}
