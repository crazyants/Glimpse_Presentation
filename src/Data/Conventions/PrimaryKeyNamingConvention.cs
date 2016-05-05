using System.Data.Entity.ModelConfiguration.Conventions;

namespace Kiehl.App.Data.Conventions
{
    public class PrimaryKeyNamingConvention : Convention
    {
        public PrimaryKeyNamingConvention()
        {
            Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey().HasColumnName(p.ClrPropertyInfo.ReflectedType.Name + "Id"));
        }
    }
}