using CodeFinance.Data.MongoDb.Config;
using CodeFinance.Domain.Entidades;
using MongoDB.Driver;

namespace CodeFinance.Data.MongoDb
{
    public class MongoDBContext : IMongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoDbSettings settings)
        {
            _database = new MongoClient(settings.ConnectionString).GetDatabase(settings.Database);
        }

        public IMongoCollection<Categoria> Categorias => _database.GetCollection<Categoria>(nameof(Categoria));
        public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>(nameof(Usuario));
        public IMongoCollection<Saldo> Saldos => _database.GetCollection<Saldo>(nameof(Saldo));
        public IMongoCollection<Movimentacao> Movimentacoes => _database.GetCollection<Movimentacao>(nameof(Movimentacao));
        public IMongoCollection<Meta> Metas => _database.GetCollection<Meta>(nameof(Meta));
        public IMongoCollection<Orcamento> Orcamento => _database.GetCollection<Orcamento>(nameof(Orcamento));

        public static class MongoDBContextFactory
        {
            public static MongoDBContext Create(MongoDbSettings settings) => new(settings);
        }
    }
}
