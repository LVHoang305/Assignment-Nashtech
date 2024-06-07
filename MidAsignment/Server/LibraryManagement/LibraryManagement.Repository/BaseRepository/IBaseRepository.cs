namespace LibraryManagement.Repository.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int Id);
        Task<IEnumerable<TEntity>> GetByFieldsAsync(string json);
        Task<IEnumerable<TEntity>> GetByFieldsLikeAsync(string json);
        void CreateAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(int Id);
        Task DeleteByFieldAsync(string fieldName, string fieldValue);
        void Save();
    }
}
