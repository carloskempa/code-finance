using CodeFinance.Domain.Entidades;
using MongoDB.Driver;

namespace CodeFinance.Data.MongoDb.Config
{
    public interface IMongoDBContext
    {
        IMongoCollection<Categoria> Categorias { get; }
        IMongoCollection<Usuario> Usuarios { get; }
        IMongoCollection<Saldo> Saldos { get; }
        IMongoCollection<Movimentacao> Movimentacoes { get; }
        IMongoCollection<Meta> Metas { get; }
        IMongoCollection<Orcamento> Orcamento { get; }
    }
}
