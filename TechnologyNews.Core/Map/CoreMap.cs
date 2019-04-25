using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Entity;

namespace TechnologyNews.Core.Map
{
    public class CoreMap<T>:EntityTypeConfiguration<T>where T: CoreEntity//Herhangi bir Entity class'ı için generic bir konfigurasyon tanımlar.Örneğin customer için - T:Generic bir tip
    {
        //Fluent API:Bir API'nin kolayca okunabilir ve kullanılablir şikilde sunulmasını sağlayan, LINQ to Entity veya LINQ to SQL yazarken kullanılan tasarımsal bir prensiptir.(Bir yazım şekli)
        public CoreMap()
        {
            Property(x => x.ID).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);//veritabanı tarafında identity oluşturulsun mu?

            Property(x => x.CreatedUserName).HasColumnName("CreatedUserName").HasMaxLength(50).IsOptional();
            Property(x => x.CreatedComputerName).HasColumnName("CreatedComputerName").HasMaxLength(50).IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedIP).HasColumnName("CreatedIP").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional();

            Property(x => x.ModifiedUserName).HasColumnName("ModifiedUserName").HasMaxLength(50).IsOptional();
            Property(x => x.ModifiedComputerName).HasColumnName("ModifiedComputerName").HasMaxLength(50).IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedIP).HasColumnName("ModifiedIP").IsOptional();
            Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").IsOptional();
        }
    }
}
