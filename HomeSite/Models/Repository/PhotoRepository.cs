using HomeSiteDomain.Models.Home;
using HomeSiteDomain.Models.Photos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeSite.Models.Repository
{   //TODO COMMENTS HERE.
    public class PhotoRepository : IPhotoRepository
    {
        private HomeSiteContext context;

        public PhotoRepository()
        {
            this.context = new HomeSiteContext();
        }

        public IEnumerable<Album> GetAllAlbums()
        {
            return context.Albums.ToList();
        }

        public Album GetSingleAlbum(int albumId)
        {
            return context.Albums.Find(albumId);
        }

        public void AddAlbum(Album entity)
        {
            if (entity.Photos == null)
                entity.Photos = new List<Photo>();
            context.Albums.Add(entity);
            context.SaveChanges();
        }

        public void EditAlbum(Album entity)
        {
            Album album = context.Albums.Find(entity.AlbumId);
            if (album != null)
            {
                album.AlbumName = entity.AlbumName;
                if (entity.ThumbnailUrl != null)
                {
                    album.ThumbnailUrl = entity.ThumbnailUrl;
                }
                context.SaveChanges();
            }
            else
                throw new ArgumentNullException();
        }

        public void DeleteAlbum(Album entity)
        {
            Album album = context.Albums.Find(entity.AlbumId);
            if (entity != null && entity.Photos.Count == 0 && album == entity)
            {
                context.Albums.Remove(entity);
                context.SaveChanges();
            }
            else
            {
                if (entity == null)
                {
                    throw new ArgumentNullException();
                }
                else if (!context.Albums.Contains(entity))
                {
                    throw new Exception("Can't find the entity to be deleted.");
                }
                else
                {
                    throw new Exception("Can't delete an Album if it still contains Photographs.");
                }
            }
        }

        public IEnumerable<Photo> GetAllPhotos(int albumId, int start, int number)
        {
            return context.Albums.Find(albumId).Photos.Skip(start).Take(number);
        }

        public IEnumerable<Photo> GetAllPhotos(int albumId)
        {
            return context.Albums.Find(albumId).Photos.ToList();
        }

        public Photo GetSinglePhoto(int photoId)
        {
            return context.Photos.Find(photoId);
        }

        public void AddPhoto(Photo entity)
        {
            context.Photos.Add(entity);
            context.SaveChanges();
            try
            {
                CreateContentUpdate(entity);
                context.SaveChanges();
            }
            catch (Exception)
            {
                context.Photos.Remove(entity);
                context.SaveChanges();
            }
        }

        public void EditPhoto(Photo entity)
        {
            Photo photo = context.Photos.Find(entity.PhotoId);
            if (photo != null)
            {
                photo.PhotoName = entity.PhotoName;
                photo.Description = entity.Description;
                if (entity.PhotoUrl != null)
                {
                    photo.PhotoUrl = entity.PhotoUrl;
                    photo.PhotoThumbnailUrl = entity.PhotoThumbnailUrl;
                }
                context.SaveChanges();
            }
            else
                throw new ArgumentNullException();
        }

        //Deleting a photo, deletes the photo, its content update and the photo like info associated with it.
        public void DeletePhoto(Photo entity)
        {
            Photo photo = context.Albums.Find(entity.AlbumId).Photos.Find(m => m.PhotoId == entity.PhotoId);
            if (entity != null && photo == entity)
            {
                context.Photos.Remove(entity);
                ContentUpdate update = context.ContentUpdates.Where(x => x.RouteValue == entity.AlbumId + "|" + entity.PhotoId).FirstOrDefault();
                if (update != null)
                    DeleteContentUpdate(update);
                UserPhotoLikeInfo photoLike = context.PhotoLikes.Where(x => x.PhotoId == entity.PhotoId).FirstOrDefault();
                if (photoLike != null)                
                    context.PhotoLikes.Remove(photoLike);
                
                context.SaveChanges();
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(entity.PhotoUrl));
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(entity.PhotoThumbnailUrl));
            }
            else if (entity == null)
                throw new ArgumentNullException();
            else
            {
                throw new Exception("We Couldn't find the photo to be deleted. Are you sure you clicked on a link?");
            }
        }

        public IEnumerable<PhotoComment> GetComments(int photoId)
        {
            Photo photo = GetSinglePhoto(photoId);
            IEnumerable<PhotoComment> comments = photo.Comments;
            return comments;
        }

        public string DeleteComment(int commentId)
        {
            PhotoComment comment = context.PhotoComments.Where(m => m.PhotoCommentId == commentId).Single();
            if (comment != null)
            {
                context.PhotoComments.Remove(comment);
                context.SaveChanges();
                return ("success");
            }
            else
                return ("failed");
            //DO A TRY CATCH HERE LATER.
        }

        public string AddComment(int photoId, PhotoComment comment)
        {
            try
            {
                Photo photo = GetSinglePhoto(photoId);
                if (photo.Comments == null)
                {
                    photo.Comments = new List<PhotoComment>();
                }
                photo.Comments.Add(comment);
                context.SaveChanges();
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public PhotoUser GetSingleUser(int userId)
        {
            return context.Users.Find(userId);
        }

        public void AddUser(PhotoUser entity)
        {
            context.Users.Add(entity);
            context.SaveChanges();
        }

        public void EditUser(PhotoUser entity)
        {
            PhotoUser user = context.Users.Find(entity.PhotoUserId);
            if (user != null)
            {
                user.FirstName = entity.FirstName;
            }
            context.SaveChanges();
        }

        public string DeleteUser(int userId)
        {
            PhotoUser user = context.Users.Find(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return ("success");
            }
            else
                return ("failed");

        }

        public UserPhotoLikeInfo GetPhotoLike(string userId, int photoId)
        {
            return context.PhotoLikes.Find(userId, photoId);
        }

        public void AddPhotoLike(UserPhotoLikeInfo entity)
        {
            context.PhotoLikes.Add(entity);
            context.SaveChanges();
        }

        public void EditPhotoLike(UserPhotoLikeInfo entity)
        {
            UserPhotoLikeInfo photoLike = context.PhotoLikes.Find(entity.PhotoUserId, entity.PhotoId);
            if (photoLike != null)
            {
                photoLike.Vote = entity.Vote;
            }
            context.SaveChanges();
        }

        public void UpdatePhotoLikeCount(int photoId, bool vote)
        {
            Photo photo = context.Photos.Find(photoId);
            if (photo != null)
            {
                if (vote == true)
                {
                    photo.LikeCount++;
                }
                else
                {
                    photo.LikeCount--;
                }
            }
            context.SaveChanges();
        }

        private void CreateContentUpdate(Photo photo)
        {
            if (photo != null)
            {
                string routeValue = photo.AlbumId + "|" + photo.PhotoId;
                ContentUpdate update = new ContentUpdate { Type = 0, Name = photo.PhotoName, DateCreated = DateTime.Today, RouteValue = routeValue, Content = photo.PhotoThumbnailUrl.Substring(1) };
                context.ContentUpdates.Add(update);
            }
            else
            {
                throw new NullReferenceException("The photo sent to create to update was null");
            }
        }

        private void DeleteContentUpdate(ContentUpdate update)
        {
            if (update != null)
            {
                context.ContentUpdates.Remove(update);
            }
            else
            {
                throw new NullReferenceException("The content update provided for deletion was null");
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}