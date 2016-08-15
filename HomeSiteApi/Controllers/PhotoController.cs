using HomeSite.Models.Repository;
using HomeSiteDomain.Models.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HomeSiteApi.Controllers
{
    public class PhotoController : ApiController
    {
        private IPhotoRepositoryAlt context;

        public PhotoController(IPhotoRepositoryAlt repository)
        {
            this.context = repository;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Photo>))]
        public List<Photo> GetAll(int albumId)
        {
            return context.GetSingle(albumId).Photos.ToList();
        }

        [HttpGet]
        [ResponseType(typeof(Photo))]
        public Photo GetPhoto(int albumId, int photoId)
        {
            var photo = context.GetSingle(albumId).Photos.Find(m => m.PhotoId == photoId);
            return photo;
        }

        //[HttpPut]
        //public bool EditPhoto(int albumId, int photoId, Photo photo)
        //{
        //    Photo original = context.GetSingle(albumId).Photos.Where(m => m.PhotoId == photoId).SingleOrDefault();
        //    if (original != null && photo != null)
        //    {
        //        if (photo.PhotoName != "")
        //        {
        //            original.PhotoName = photo.PhotoName;
        //        }
        //        else if (photo.Description != "")
        //        {
        //            original.Description = photo.Description;
        //        }
        //        //NEED TO SAVE FILE TO DISK.
        //    }
        //}

        //[HttpPost]
        //[ResponseType(typeof(void))]
        //public IHttpActionResult AddPhoto(CreatePhotoViewModel model, int albumId)
        //{


        //    //Save files to Disk and get their Url.

        //}

        //[HttpDelete]
        //[ResponseType(typeof(void))]
        //public IHttpActionResult DeletePhoto(int albumId, int photoId)
        //{
        //    var album = context.GetSingle(albumId);
        //    var photo = album.Photos.Where(m => m.PhotoId == photoId).FirstOrDefault();
        //    album.Photos.Remove(photo);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}
    }
}
