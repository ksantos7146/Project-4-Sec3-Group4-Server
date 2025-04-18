﻿using Project_IV.Entities;

namespace Project_IV.Service
{
    public interface ILikeService
    {
        Task<Like> GetLikeByIdAsync(int id);
        Task<IEnumerable<Like>> GetAllLikesAsync();
        Task AddLikeAsync(Like like);
        Task RemoveLikeAsync(int id);                                                                                   
        Task<IEnumerable<Like>> GetLikesByLikedIdAsync(string likedId);
        Task<IEnumerable<Like>> GetLikesByLikerIdAsync(string likerId);
    }
}
