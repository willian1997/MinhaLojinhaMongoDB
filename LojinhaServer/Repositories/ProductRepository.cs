using LojinhaServer.Collections;
using MongoDB.Driver;


namespace LojinhaServer.Repositories;

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<Product>("products");
        }
        public async Task<List<Product>> GetAllAsync() =>
        await _collection.Find(_ => true) .ToListAsync();

        public async Task<Product> GetByIdAsync(string id) =>
        await _collection.Find(_ => _.Id == id) .FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
        await _collection.InsertOneAsync(product);

        public async Task UpdateAsync(Product product) =>
        await _collection.ReplaceOneAsync(x => x.Id == product.Id, product);

        public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
