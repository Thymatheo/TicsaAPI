using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicsaAPI.DAL.DataProvider.Interface;
using TicsaAPI.DAL.Models;

namespace TicsaAPI.DAL.DataProvider {
    public abstract class BasicDp<T> : IBasicDp<T> where T : BasicElement {
        public DbSet<T> Table { get; set; }
        public TicsaContext Db { get; set; }
        public BasicDp(DbSet<T> table, TicsaContext db) {
            Table = table;
            Db = db;
        }

        public async Task<IEnumerable<T>> GetAll() {
            return await ((IQueryable<T>)Table).ToListAsync();
        }
        public async Task<T> GetById(int id) {
            return (T)(await ((IQueryable<BasicElement>)Table).Where(x => x.Id == id).FirstOrDefaultAsync());
        }
        public async Task<T> Remove(T entity) {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>? result = Table.Remove(entity);
            await Db.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<T> Add(T entity) {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>? result = Table.Add(entity);
            await Db.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<T> Update(T entity) {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>? result = Table.Update(entity);
            await Db.SaveChangesAsync();
            return result.Entity;
        }
        public async Task RemoveRange(IEnumerable<T> entity) {
            Table.RemoveRange(entity);
            await Db.SaveChangesAsync();
        }
        public async Task AddRange(IEnumerable<T> entity) {
            Table.AddRange(entity);
            await Db.SaveChangesAsync();
        }
        public async Task UpdateRange(IEnumerable<T> entity) {
            Table.UpdateRange(entity);
            await Db.SaveChangesAsync();
        }

    }
}
