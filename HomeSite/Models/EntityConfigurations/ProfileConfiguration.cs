using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using HomeSiteDomain.Models.About;
using HomeSiteDomain.Models.Photos;


namespace HomeSite.Models.BusinessModels.EntityConfigurations
{
    public class ProfileConfiguration : EntityTypeConfiguration<Profile>
    {
        public ProfileConfiguration()
        {
            this.Property(p => p.WelcomeText).IsUnicode(false).HasMaxLength(250);
            this.Property(p => p.Description).IsUnicode(false).HasMaxLength(2000);
        }
    }

    public class PhotoConfiguration : EntityTypeConfiguration<Photo> 
    {
        public PhotoConfiguration()
        {
            this.Property(p => p.PhotoName).IsUnicode(false).HasMaxLength(20).IsRequired();
            this.Property(p => p.Description).IsUnicode(false).HasMaxLength(200);
            this.HasKey(p => p.PhotoId);
            this.HasMany(p => p.Comments).WithRequired(p => p.Photo).WillCascadeOnDelete(true);
        }


    }

    public class PhotoCommentConfiguration : EntityTypeConfiguration<PhotoComment>
    {
        public PhotoCommentConfiguration()
        {
            this.Property(p => p.Body).IsUnicode(false).HasMaxLength(400).IsRequired();
        }
    }

    public class AlbumConfiguration : EntityTypeConfiguration<Album>
    {
        public AlbumConfiguration()
        {
            this.Property(p => p.AlbumName).IsUnicode(false).HasMaxLength(15).IsRequired();
        }
    }

    public class PhotoViewModelConfiguration : EntityTypeConfiguration<CreatePhotoViewModel>
    {
        public PhotoViewModelConfiguration()
        {
            this.Property(p => p.PhotoName).IsUnicode(false).HasMaxLength(20);
            this.Property(p => p.Description).IsUnicode(false).HasMaxLength(200);
        }
    }
}