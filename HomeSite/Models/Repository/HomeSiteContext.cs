using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HomeSiteDomain.Models.Photos;
using HomeSiteDomain.Models.About;
using HomeSite.Models.Repository;
using HomeSite.Models.BusinessModels.EntityConfigurations;
using HomeSite.Migrations;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeSiteDomain.Models.Home;
using HomeSiteDomain.Models.BaseClasses;

namespace HomeSite.Models
{
    public class HomeSiteContext : DbContext
    {
        public HomeSiteContext() : base("HomeSiteContext") 
        {
            Database.SetInitializer<HomeSiteContext>(new HomeSiteContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new PhotoConfiguration());

            #region Photo Configurations
            modelBuilder.Entity<Photo>().Property(p => p.PhotoName).IsUnicode(false).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Photo>().Property(p => p.Description).IsUnicode(false).HasMaxLength(200).IsRequired();
            #endregion

            #region UserPhotoLikeInfo Configurations
            modelBuilder.Entity<UserPhotoLikeInfo>().Property(p => p.Vote).IsRequired();
            modelBuilder.Entity<UserPhotoLikeInfo>().Property(p => p.PhotoId).IsRequired();
            modelBuilder.Entity<UserPhotoLikeInfo>().Property(p => p.PhotoUserId).IsRequired();
            modelBuilder.Entity<UserPhotoLikeInfo>().HasKey(p => new { p.PhotoUserId, p.PhotoId });
            #endregion

            #region PhotoUserConfiguration

            modelBuilder.Entity<PhotoUser>().Property(p => p.PhotoUserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<PhotoUser>().Property(p => p.FirstName).HasMaxLength(128).IsUnicode(false).IsRequired();

            #endregion

            #region ContentUpdate Configurations

            modelBuilder.Entity<ContentUpdate>().Property(x => x.Name).IsUnicode(false).HasMaxLength(120).IsRequired();
            modelBuilder.Entity<ContentUpdate>().Property(x => x.Type).IsRequired();
            modelBuilder.Entity<ContentUpdate>().Property(x => x.Content).HasMaxLength(300).IsRequired();
            modelBuilder.Entity<ContentUpdate>().Property(x => x.RouteValue).IsUnicode(false).IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<PhotoComment> PhotoComments { get; set; }
        public DbSet<PhotoUser> Users { get; set; }
        public DbSet<UserPhotoLikeInfo> PhotoLikes { get; set; }
        public DbSet<ContentUpdate> ContentUpdates { get; set; }
        public DbSet<ErrorReport> Errors { get; set; }
   
    }
}