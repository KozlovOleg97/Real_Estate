﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Real_Estate.Presentation.WebApi.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	public static class ServiceExtension
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public static void AddSwaggerExtension(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
            {
				List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, 
                    "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
				xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "RealsEstate API",
					Description = "This Api will be responsible for overall data distribution",
					Contact = new OpenApiContact
					{
						Name = "Group #5",
						Email = "g.teachmeskills@gmail.com",
						Url = new Uri("https://Easily.com")
					}
				});

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Description = "Input your Bearer token in this format - Bearer {your token here}"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "Bearer",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					},
				});

			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public static void AddApiVersioningExtension(this IServiceCollection services)
		{
			services.AddApiVersioning(config =>
			{
				config.DefaultApiVersion = new ApiVersion(1, 0);
				config.AssumeDefaultVersionWhenUnspecified = true;
				config.ReportApiVersions = true;
			});
		}
	}
}
		
