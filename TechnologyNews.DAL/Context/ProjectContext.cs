using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TechnologyNews.Core.Entity;
using TechnologyNews.Map.Option;
using TechnologyNews.Model.Option;
using TechnologyNews.Utility;

namespace TechnologyNews.DAL.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=.;Database=TechnologyNews;Uid=sa;Pwd=sG9194**";
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new LikeMap());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Article>Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        public override int SaveChanges()// DBContext’in save işleminin yapıldığı yer burasıdır. Önce değişen tablolar bulunur ve değiştiği alanların önceki ve sonraki halleri kaydedilir.
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            //“ChangeTracker.Entries()” :Tüm entry’ler alınır.
            //“.Where(p => p.State == EntityState.Modified).ToList()” : Update olanlar bir listeye atılır.

            string identity = WindowsIdentity.GetCurrent().Name;//Geçerli Windows kullanıcısını temsil eden bir WindowsIdentity nesnesini döndürür.
            string computerName = Environment.MachineName;//Bu yerel bilgisayarın NetBIOS adını alır.
            DateTime dateTime = DateTime.Now;
            string GetIp = RemoteIP.IpAddress;

            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity;

                if (item.State==EntityState.Added)
                {
                    entity.CreatedUserName = identity;
                    entity.CreatedComputerName = computerName;
                    entity.CreatedDate = dateTime;
                    entity.CreatedIP = GetIp;
                }
                else if (item.State == EntityState.Modified)
                {
                    entity.ModifiedUserName = identity;
                    entity.ModifiedComputerName = computerName;
                    entity.ModifiedDate = dateTime;
                    entity.ModifiedIP = GetIp;
                }

            }

            return base.SaveChanges();
        }

    }
}
