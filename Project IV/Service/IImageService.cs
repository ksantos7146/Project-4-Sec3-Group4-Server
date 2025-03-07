using Project_IV.Entities;

namespace Project_IV.Service
{
    public interface IImageService
    {
        Task<Image> GetImageByIdAsync(int id);
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task AddImageAsync(Image image);
        Task UpdateImageAsync(Image image);
        Task DeleteImageAsync(int id);
    }
}
