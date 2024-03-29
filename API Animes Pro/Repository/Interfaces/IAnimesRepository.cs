﻿using API_Animes_Pro.Models;

namespace API_Animes_Pro.Repository.Interfaces
{
    public interface IAnimesRepository
    {
        Task<List<AnimesModel>> GetAll();
        Task<AnimesModel?> GetById(int id);
        Task<List<AnimesModel>> GetByKey(string key, string filter);
        Task<List<AnimesModel>> Pagination(int page, int pageSize, string key = "", string filter = "Default");
        Task<AnimesModel> Add(AnimesModel anime);
        Task<AnimesModel> Put(AnimesModel anime);
        Task<string> Delete(AnimesModel anime);
    }
}
