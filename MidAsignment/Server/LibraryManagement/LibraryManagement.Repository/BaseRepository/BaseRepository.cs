using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic.Core;

namespace LibraryManagement.Repository.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected LibraryManagementContext _context = null;
        protected DbSet<TEntity> _table = null;
        public BaseRepository(LibraryManagementContext dbContext)
        {
            _context = dbContext;
            _table = _context.Set<TEntity>();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteAsync(int Id)
        {
            var t = _table.Find(Id);
            if (t != null)
            {
                _table.Remove(t);
                Save();
            }
        }

        /// <summary>
        /// Delete all by fields
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public async Task DeleteByFieldAsync(string fieldName, string fieldValue)
        {
            var recordsToDelete = await GetByFieldsAsync($"{{ '{fieldName}': '{fieldValue}' }}");

            foreach (var record in recordsToDelete)
            {
                _table.Remove(record);
            }

            Save();
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return _table.ToList();
        }

        /// <summary>
        /// Get by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetByIdAsync(int Id)
        {
            return await _table.FindAsync(Id);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="entity"></param>
        public void CreateAsync(TEntity entity)
        {
            _table.Add(entity);
            Save();
        }

        /// <summary>
        /// Get by fields
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetByFieldsAsync(string json)
        {
            JObject jObject = JObject.Parse(json);
            var query = _table.AsQueryable();

            foreach (var property in jObject.Properties())
            {
                string propertyName = property.Name;
                string propertyValue = property.Value.ToString();

                // Construct the query dynamically using System.Linq.Dynamic.Core
                query = query.Where($"{propertyName} == @0", propertyValue);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Search by fields
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetByFieldsLikeAsync(string json)
        {
            JObject jObject = JObject.Parse(json);
            var query = _table.AsQueryable();

            foreach (var property in jObject.Properties())
            {
                string propertyName = property.Name;
                string propertyValue = property.Value.ToString();

                // Construct the query dynamically using System.Linq.Dynamic.Core
                query = query.Where($"{propertyName}.Contains(@0)", propertyValue);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAsync(TEntity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
