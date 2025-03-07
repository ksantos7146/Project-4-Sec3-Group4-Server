using Microsoft.EntityFrameworkCore;
using Project_IV.Data;
using Project_IV.Entities;

namespace Project_IV.Service.Impl
{
    public class ImageService : IImageService
    {
        private readonly GitCommitDbContext _dbContext;

        public ImageService(GitCommitDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Image> GetImageByIdAsync(int id) =>
            await _dbContext.Images.FindAsync(id);

        public async Task<IEnumerable<Image>> GetAllImagesAsync() =>
            await _dbContext.Images.ToListAsync();

        public async Task AddImageAsync(Image image)
        {
            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateImageAsync(Image image)
        {
            _dbContext.Images.Update(image);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(int id)
        {
            var image = await GetImageByIdAsync(id);
            if (image != null)
            {
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
