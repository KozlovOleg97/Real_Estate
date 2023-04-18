using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_Estate.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Real_Estate.Core.Domain.Common;

namespace Real_Estate.Infrastructure.Persistence.Contexts
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext (DbContextOptions<ApplicationContext> options) : base(options) { }

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries<OverallBaseEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.Created = DateTime.Now;
						entry.Entity.CreatedBy = "DefaultAppUser";
						break;
					case EntityState.Modified:
						entry.Entity.LastModified = DateTime.Now;
						entry.Entity.LastModifiedBy = "DefaultAppUser";
						break;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}
