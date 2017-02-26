using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Challenge.Data.Interfaces;
using Challenge.Data.Mapping;
using Challenge.Model;

namespace Challenge.Data
{
    public interface IContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : EntityInt;
        int SaveChanges();
    }
    public class DBContext: DbContext, IContext
    {
        public DBContext() : base("TestsExamDB")
        {           
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
          .Where(type => !String.IsNullOrEmpty(type.Namespace))
          .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
          type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, Migrations.Configuration>(true));

        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : EntityInt
        {
            return base.Set<TEntity>();
        }
    }
}
