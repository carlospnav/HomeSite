using HomeSiteDomain.Models.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSite.Models.Repository
{
    public interface IPhotoRepository : IDisposable
    {
        IEnumerable<Album>GetAllAlbums();

        Album GetSingleAlbum(int albumId);

        void AddAlbum(Album entity);

        void EditAlbum(Album entity);

        void DeleteAlbum(Album entity);

        IEnumerable<Photo> GetAllPhotos(int albumId, int start, int number);

        IEnumerable<Photo> GetAllPhotos(int albumId);

        Photo GetSinglePhoto(int photoId);

        void AddPhoto(Photo entity);

        void EditPhoto(Photo entity);

        void DeletePhoto(Photo entity);

        IEnumerable<PhotoComment> GetComments(int photoId);

        string AddComment(int photoId, PhotoComment comment);

        string DeleteComment(int commentId);

        PhotoUser GetSingleUser(int userId);

        void AddUser(PhotoUser entity);

        void EditUser(PhotoUser entity);

        string DeleteUser(int userId);

        UserPhotoLikeInfo GetPhotoLike(string userId, int photoId);

        void AddPhotoLike(UserPhotoLikeInfo entity);

        void EditPhotoLike(UserPhotoLikeInfo entity);

        void UpdatePhotoLikeCount(int photoId, bool vote);

    }
}
