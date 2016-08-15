using HomeSite.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HomeSiteDomain.Models.Photos;
using System.Web.Http.Description;

namespace HomeSiteApi.Controllers
{
    public class AlbumController : ApiController
    {
        private IPhotoRepositoryAlt context;

        public AlbumController(IPhotoRepositoryAlt repository)
        {
            this.context = repository;
        }

        [ResponseType(typeof(List<Album>))]
        [HttpGet]
        public IEnumerable<Album> GetAll()
        {
            return context.GetAll();
        }

        [HttpGet]
        [ResponseType(typeof(Album))]
        public Album GetSingle(int albumId)
        {
            return context.GetSingle(albumId);
        }

        //[HttpPost]
        //[ResponseType(typeof(void))]
        //public void Post()
        //{

        //    return View();
        //}
    }
}
