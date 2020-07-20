using System;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManager.Models
{
    public class ConfigurationManagerContext : DbContext
    {
        public ConfigurationManagerContext(DbContextOptions<ConfigurationManagerContext> options) : base(options)
        {
        }

        public DbSet<Submodule> Submodule { get; set; }
        public DbSet<SubmoduleParameter> SubmoduleParameter { get; set; }
    }
}
