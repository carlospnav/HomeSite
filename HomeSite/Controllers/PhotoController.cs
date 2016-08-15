using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeSiteDomain.Models.Photos;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using HomeSiteDomain.Models.BaseClasses;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HomeSite.Models.Repository;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using HomeSiteDomain.Models.Home;
using HomeSite.Infrastructure.Exceptions;

namespace HomeSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class PhotoController : Controller
    {
        private IPhotoRepository context;

        public PhotoController(IPhotoRepository repository)
        {
            this.context = repository;
        }

        [AllowAnonymous]
        public ActionResult Index(string content = null)
        {
            //Gets the roles from the current user, see if the user is an admin and send the information to the index page.
            var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            var authorizationAsAdmin = ((HttpContext.Request.IsAuthenticated) && roles.Contains("admin")) ? true : false;
            ViewBag.IsAuthedAsAdmin = authorizationAsAdmin;
            ViewBag.IsAuthed = User.Identity.IsAuthenticated;
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string FirstName;
            if (!User.Identity.IsAuthenticated)
                FirstName = null;
            else
                FirstName = claimsIdentity.FindFirst("FirstName").Value.ToString();

            ViewBag.UserName = (FirstName != null) ? FirstName : "Anonymous";
            //string userIdValue;
            //if (claimsIdentity != null)
            //{
            //    var userIdClaim = claimsIdentity.Claims
            //        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            //    if (userIdClaim != null)
            //    {
            //        userIdValue = userIdClaim.Value;
            //    }
            //    else
            //        userIdValue = null;
            //}
            //else
            //{
            //    userIdValue = null;
            //}

            ViewBag.Redirected = false;

            if (content != null)
            {

                ViewBag.RouteValue = content;
                ViewBag.Redirected = true;
            }

            List<Album> albums = context.GetAllAlbums().ToList();
            return View(albums);
        }

        public ActionResult CreatePhoto(int albumId)
        {
            ViewBag.albumId = albumId;
            return View(new CreatePhotoViewModel());
        }

        [HttpPost]
        public ActionResult CreatePhoto(CreatePhotoViewModel model, int albumId)
        {
            Album album = context.GetSingleAlbum(albumId);
            if (album != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.albumId = albumId;
                    return View(model);
                }
                Photo photo = FromPhotoViewModelToPhoto(model, albumId);
                if (photo == null)
                {
                    ModelState.AddModelError("", "There was a problem saving the photo. Please try again in a few moments.");
                    return View(model);
                }
                context.AddPhoto(photo);
                return RedirectToAction("Index");
            }
            else
                throw new HttpException(404, "We were not able to find the album you are trying to add a photo to. Please contact an administrator.");
        }

        public ActionResult EditPhoto(int photoId)
        {
            Photo photo = context.GetSinglePhoto(photoId);
            if (photo != null)
            {
                ViewBag.photoId = photoId;
                ViewBag.albumId = photo.AlbumId;
                EditPhotoViewModel model = new EditPhotoViewModel();
                model.PhotoName = photo.PhotoName;
                model.Description = photo.Description;
                return View(model);
            }
            else
                throw new HttpException(404, "We were not able to find the photo you are trying to edit. Please contact an administrator.");
        }

        [HttpPost]
        public ActionResult EditPhoto(EditPhotoViewModel model, int albumId, int photoId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Photo photo = FromPhotoViewModelToPhoto(model, albumId, photoId);
            photo.PhotoId = photoId;
            context.EditPhoto(photo);
            return RedirectToAction("Index");
        }

        public ActionResult DeletePhoto(int photoId)
        {
            Photo photo = context.GetSinglePhoto(photoId);
            if (photo != null)
            {
                context.DeletePhoto(photo);
                var fullPath = Server.MapPath(photo.PhotoUrl);
                var thumbPath = Server.MapPath(photo.PhotoThumbnailUrl);
                System.IO.File.Delete(fullPath);
                System.IO.File.Delete(thumbPath);
                return RedirectToAction("Index");
            }
            else
                throw new HttpException(404, "We were not able to find the photo you are trying to delete. Please contact an administrator.");

        }

        public ActionResult CreateAlbum()
        {
            return View(new CreateAlbumViewModel());
        }

        [HttpPost]
        public ActionResult CreateAlbum(CreateAlbumViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Album album = FromAlbumViewModelToAlbum(model);
            context.AddAlbum(album);
            return RedirectToAction("Index");
        }

        public ActionResult EditAlbum(int albumId)
        {
            Album album = context.GetSingleAlbum(albumId);
            if (album != null)
            {
                ViewBag.albumId = albumId;
                EditAlbumViewModel model = new EditAlbumViewModel();
                model.AlbumName = album.AlbumName;
                return View(model);
            }
            else
                throw new HttpException(404, "We could not find the album you are looking to edit. Please contact an administrator.");
        }

        [HttpPost]
        public ActionResult EditAlbum(EditAlbumViewModel model, int albumId)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Album album = FromAlbumViewModelToAlbum(model);
            album.AlbumId = albumId;
            context.EditAlbum(album);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAlbum(int albumId)
        {
            Album entity = context.GetSingleAlbum(albumId);
            if (entity != null)
            {
                if (entity.Photos.Count > 0)
                {
                    throw new AlbumNotEmptyException("Please delete the album's photos before attempting to delete it.");
                }
                else
                    context.DeleteAlbum(entity);
                var path = Server.MapPath(entity.ThumbnailUrl);
                System.IO.File.Delete(path);
                return RedirectToAction("Index");
            }
            else
                throw new HttpException(404, "We could not find the album you are looking to delete. Please contact an administrator.");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateComment(CreatePhotoCommentViewModel comment)
        {
            if (User.Identity.IsAuthenticated)
            {
                PhotoComment commentToAdd = new PhotoComment()
                {
                    PhotoId = comment.PhotoId,
                    UserName = comment.UserName,
                    Body = comment.Body,
                    CreatedDate = DateTime.Today
                };
                string result = context.AddComment(comment.PhotoId, commentToAdd);
                return Content(result);
            }
            else
                return new HttpUnauthorizedResult();
        }

        public ActionResult DeleteComment(int commentId)
        {
            if (User.Identity.IsAuthenticated)
            {
                string result = context.DeleteComment(commentId);
                return Content(result);
            }
            else
                return new HttpUnauthorizedResult();
        }

        /// <summary>
        /// Takes a PhotoViewModel object and the name of the album and returns a Photo object with the proper optional automatic name generation.
        /// </summary>
        /// <param name="model">A PhotoViewModel object</param>
        /// <param name="albumId">The Primary Key of the Album Table in the Database.</param>
        /// <returns>A Photo object</returns>
        private Photo FromPhotoViewModelToPhoto(PhotoViewModel model, int albumId, int photoId = 0)
        {
            Album album = context.GetSingleAlbum(albumId);
            if (album != null)
            {
                int latestPhotoNumber = Convert.ToInt32(new String(album.Photos.Last().PhotoUrl.Where(Char.IsDigit).ToArray()));
                HttpPostedFileBase file;

                int photoCount = album.Photos.Count;
                var albumName = album.AlbumName;
                string fileName = albumName + (latestPhotoNumber + 1);
                string thumbFileName = "T" + fileName + ".jpg";

                var fullSizedPath = "~/Content/Images/Photography/FullSized/" + albumName + "/";
                var thumbnailPath = "~/Content/Images/Photography/Thumbnail/" + albumName + "/";

                Photo photo;

                if (model is CreatePhotoViewModel)
                {
                    #region FileName Assignment
                    if (model.Photo.ContentType == "image/jpeg")
                    {
                        fileName += ".jpg";
                        fileName = fileName.Insert(0, "F");
                    }
                    else if (model.Photo.ContentType == "image/png")
                    {
                        fileName += ".png";
                        fileName = fileName.Insert(0, "F");
                    }
                    #endregion
                    photo = new Photo();
                    photo.AlbumId = album.AlbumId;
                    photo.Comments = new List<PhotoComment>();
                    photo.CreatedDate = DateTime.Now;
                    file = model.Photo;

                    if (model.PhotoName == null)
                    {
                        photo.PhotoName = "Photo " + photoCount;
                    }
                    else
                    {
                        photo.PhotoName = model.PhotoName;
                    }
                    if (model.Description == null)
                    {
                        photo.Description = "This is a photograph from the album: " + albumName + ".";
                    }
                    else
                    {
                        photo.Description = model.Description;
                    }

                    model.Photo.SaveAs(Path.Combine(Server.MapPath(fullSizedPath), fileName));
                    photo.PhotoUrl = fullSizedPath + fileName;

                    Image image = Image.FromStream(file.InputStream, true, true);
                    if (image.Height == 800)
                    {
                        photo.Orientation = "Horizontal";
                    }
                    else if (image.Height == 1200)
                    {
                        photo.Orientation = "Vertical";
                    }
                    var thumbnail = GetThumbnail(image, 150, 100);
                    thumbnail.Save(Path.Combine(Server.MapPath(thumbnailPath), thumbFileName));
                    photo.PhotoThumbnailUrl = thumbnailPath + thumbFileName;
                    return photo;
                }
                else if (model is EditPhotoViewModel)
                {
                    photo = context.GetSinglePhoto(photoId);
                    photo.PhotoName = model.PhotoName;
                    photo.Description = model.Description;

                    if (model.Photo != null)
                    {
                        file = model.Photo;
                        var newFileName = photo.PhotoUrl;
                        newFileName = newFileName.Substring(0, (newFileName.Length - 3));
                        if (model.Photo.ContentType == "image/jpeg")
                        {
                            newFileName = newFileName + "jpg";
                        }
                        else if (model.Photo.ContentType == "image/png")
                        {
                            newFileName = newFileName + "png";
                        }

                        file.SaveAs(Server.MapPath(newFileName));
                        photo.PhotoUrl = newFileName;
                        Image image = Image.FromStream(file.InputStream, true, true);
                        if (image.Height == 800)
                        {
                            photo.Orientation = "Horizontal";
                        }
                        else if (image.Height == 1200)
                        {
                            photo.Orientation = "Vertical";
                        }
                        var thumbnail = GetThumbnail(image, 150, 100);
                        thumbnail.Save(Server.MapPath(photo.PhotoThumbnailUrl));
                    }
                    return photo;
                }
                else
                    throw new ArgumentException("Argument must be a PhotoViewModel");
            }
            else throw new HttpException(404, "Album could not be found.");

        }

        private Album FromAlbumViewModelToAlbum(AlbumViewModel model)
        {

            Album album = new Album();
            album.AlbumName = model.AlbumName;

            if (model.Thumbnail != null || model is CreateAlbumViewModel)
            {
                var albumFileName = model.AlbumName;
                if (model.Thumbnail.ContentType == "image/jpeg")
                    albumFileName += ".jpg";
                else if (model.Thumbnail.ContentType == "image/png")
                    albumFileName += ".png";

                var webPath = "~/Content/Images/Photography/AlbumThumbs/";
                var thumbnailPath = webPath + albumFileName;
                var savePath = Server.MapPath(Path.Combine(webPath, albumFileName));
                Image albumThumbnail = GetThumbnail(Image.FromStream(model.Thumbnail.InputStream), 150, 100);
                albumThumbnail.Save(savePath);
                album.ThumbnailUrl = thumbnailPath;
                album.Photos = new List<Photo>();
            }
            return album;
        }

        /// <summary>
        /// Takes an image and returns a thumbnail.
        /// </summary>
        /// <param name="image">The image Object.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Returns a Thumbnail image.</returns>
        private Image GetThumbnail(Image image, int width, int height)
        {
            var newRectangle = new Rectangle(0, 0, width, height);
            var newImage = new Bitmap(width, height);

            newImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, newRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return newImage;
        }

        [AllowAnonymous]
        public ActionResult Paginator(int albumId, int pagination)
        {
            Album album = context.GetSingleAlbum(albumId);
            if (album != null)
            {
                int returnPages = GetNumberOfPages(album.Photos.Count, pagination);
                return Json(new { pages = returnPages }, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult GetPaginatedPhotos(int albumId, int currentPagination, int endPagination)
        {
            Album album = context.GetSingleAlbum(albumId);
            List<Photo> photos;

            if (album != null)
            {
                photos = album.Photos;
                if (endPagination > photos.Count)
                    endPagination = photos.Count;
                var numberOfPhotosPerPage = endPagination - currentPagination;
                List<Photo> selectedPhotos = photos.Skip(currentPagination).Take(numberOfPhotosPerPage).ToList();
                List<object> returnPhotos = new List<object>();
                foreach (var photo in selectedPhotos)
                {

                    returnPhotos.Add(new { photoId = photo.PhotoId, thumbnail = photo.PhotoThumbnailUrl, photopath = photo.PhotoUrl, orientation = photo.Orientation, photoName = photo.PhotoName, photoDesc = photo.Description, photoLike = photo.LikeCount });
                }
                returnPhotos.Add(new { authorized = true });
                return Json(returnPhotos, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult CommentsPaginator(int photoId)
        {
            int PhotoId = photoId;
            Photo photo = context.GetSinglePhoto(photoId);
            List<PhotoComment> comments;
            if (photo != null)
            {
                comments = photo.Comments;
                var pagination = 4;
                int numberOfComments = comments.Count;
                int returnPages;
                if (numberOfComments == 0)
                {
                    returnPages = 1;
                }
                else if (numberOfComments % pagination == 0)
                {
                    returnPages = (numberOfComments / pagination);
                }
                else
                    returnPages = (numberOfComments / pagination) + 1;

                return Json(new { pages = returnPages }, JsonRequestBehavior.AllowGet);

            }
            else
                return HttpNotFound();

        }

        [AllowAnonymous]
        public ActionResult GetPaginatedComments(int photoId, int currentPagination)
        {
            Photo photo = context.GetSinglePhoto(photoId);
            List<PhotoComment> comments;
            if (photo != null)
            {
                comments = photo.Comments;
                int endPagination = currentPagination + 4;
                int numberOfCommentsPerPage = 4;
                if (endPagination > comments.Count)
                {
                    endPagination = comments.Count;
                }
                List<PhotoComment> selectedComments = comments.Skip(currentPagination).Take(numberOfCommentsPerPage).ToList();
                List<object> returnComments = new List<object>();
                if (selectedComments.Count != 0)
                {
                    foreach (var comment in selectedComments)
                    {
                        returnComments.Add(new { photoCommentId = comment.PhotoCommentId, photoId = comment.PhotoId, userName = comment.UserName, createdDate = comment.CreatedDate, body = comment.Body });
                    }
                    return Json(returnComments, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();

        }

        [AllowAnonymous]
        public ActionResult GetLastComments(int photoId, int commentsPerPage)
        {
            Photo photo = context.GetSinglePhoto(photoId);
            if (photo != null)
            {


                int totalComments = photo.Comments.Count;
                List<object> returnComments = new List<object>();
                List<PhotoComment> comments = photo.Comments;
                //Get the proper amount of comments. If not perfectly divisable by the number of comments per page, then return only the rest of the division.
                var numberCommentsToTake = totalComments % commentsPerPage;
                if (numberCommentsToTake == 0)
                    numberCommentsToTake = commentsPerPage;
                comments.Reverse();
                List<PhotoComment> lastComments = comments.Take(numberCommentsToTake).ToList();
                lastComments.Reverse();

                if (totalComments != 0)
                {
                    foreach (var comment in lastComments)
                    {
                        returnComments.Add(new { photoCommentId = comment.PhotoCommentId, photoId = comment.PhotoId, userName = comment.UserName, createdDate = comment.CreatedDate, body = comment.Body });
                    }
                    return Json(returnComments, JsonRequestBehavior.AllowGet);

                }
                else
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult LikeOrUnlikeAPhoto(int photoId)
        {
            if (User.Identity.IsAuthenticated)
            {
                Photo photo = context.GetSinglePhoto(photoId);
                if (photo != null)
                {
                    string userId = User.Identity.GetUserId();
                    UserPhotoLikeInfo photoLike = context.GetPhotoLike(userId, photoId);
                    if (photoLike == null)
                    {
                        context.AddPhotoLike(new UserPhotoLikeInfo { PhotoUserId = userId, PhotoId = photoId, Vote = true });
                        context.UpdatePhotoLikeCount(photoId, true);
                    }
                    else
                    {
                        if (photoLike.Vote == true)
                        {
                            photoLike.Vote = false;
                            context.EditPhotoLike(photoLike);
                            context.UpdatePhotoLikeCount(photoId, false);
                        }
                        else
                        {
                            photoLike.Vote = true;
                            context.EditPhotoLike(photoLike);
                            context.UpdatePhotoLikeCount(photoId, true);
                        }

                    }

                    return Json(new { likeCount = photo.LikeCount }, JsonRequestBehavior.AllowGet);
                }
                else
                    return HttpNotFound();
            }
            else
                return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public ActionResult GetPhotoLike(int photoId)
        {

            if (User.Identity.IsAuthenticated)
            {
                Photo photo = context.GetSinglePhoto(photoId);
                if (photo != null)
                {
                    string userId = User.Identity.GetUserId();
                    UserPhotoLikeInfo photoLike = context.GetPhotoLike(userId, photoId);
                    if (photoLike == null)
                    {
                        return Json(new { vote = false }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { vote = photoLike.Vote }, JsonRequestBehavior.AllowGet);
                }
                else
                    return HttpNotFound();
            }
            else
                return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public ActionResult GetLikeCount(int photoId)
        {
            Photo photo = context.GetSinglePhoto(photoId);

            if (photo != null)
            {
                return Json(new { likeCount = photo.LikeCount }, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();
        }

        [AllowAnonymous]
        public JsonResult GetRedirectedPhotoInformation(string content, int pagination)
        {
            string[] contents = content.Split('|');
            int albumId = Convert.ToInt32(contents[0]);
            int photoId = Convert.ToInt32(contents[1]);
            int photoPosition;
            int specificPage;
            int specificPhoto;
            string albumName;
            List<Photo> photos;
            Album album = context.GetSingleAlbum(albumId);
            photos = album.Photos;
            Photo photo = context.GetSinglePhoto(photoId);


            if (album != null && photo != null)
            {
                albumName = album.AlbumName;
                photoPosition = photos.IndexOf(photo);
                specificPage = (photoPosition / pagination) + 1;
                specificPhoto = (photoPosition % pagination) + 1;
                return Json(new { albumId = album.AlbumId, albumName = albumName, specificPage = specificPage, specificPhoto = specificPhoto, photoId = photoId }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { error = "Album or Photo could not be found." }, JsonRequestBehavior.AllowGet);

        }

        [AllowAnonymous]
        public ActionResult GetPhotoDetails(int photoId)
        {
            Photo photo = context.GetSinglePhoto(photoId);

            if (photo != null)
            {
                return Json(new { photoId = photo.PhotoId, thumbnail = photo.PhotoThumbnailUrl, photopath = photo.PhotoUrl, orientation = photo.Orientation, photoName = photo.PhotoName, photoDesc = photo.Description, photoLike = photo.LikeCount }, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();
        }

        private int GetNumberOfPages(int numberOfPhotosParam, int pagination)
        {
            int numberOfPhotos;
            numberOfPhotos = numberOfPhotosParam;
            int returnPages;
            if (numberOfPhotos % pagination == 0)
            {
                returnPages = (numberOfPhotos / pagination);
            }
            else
                returnPages = (numberOfPhotos / pagination) + 1;
            return returnPages;
        }

        protected override void Dispose(bool disposing)
        {
            if (context != null)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}