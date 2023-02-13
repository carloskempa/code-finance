using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Data.MongoDb.Repositories
{
    public class CategoriaMongoRepository : ICategoriaMongoRepository
    {
        private readonly MongoDBContext _context;
        public CategoriaMongoRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<Categoria> ObterPorId(Guid id)
        {
            var filter = Builders<Categoria>.Filter.Eq(c => c.Id, id);
            return await _context.Categorias.Find(filter).FirstOrDefaultAsync();
        }

        public void Adicionar(Categoria categoria)
        {
            categoria.DataCadastro = DateTime.Now;
            _context.Categorias.InsertOne(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            var filter = Builders<Categoria>.Filter.Eq(c => c.Id, categoria.Id);
            _context.Categorias.ReplaceOne(filter, categoria);
        }

        public void Deletar(Guid id)
        {
            var filter = Builders<Categoria>.Filter.Eq(c => c.Id, id);
            _context.Categorias.DeleteOne(filter);
        }
    }
}
