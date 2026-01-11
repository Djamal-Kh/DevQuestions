using System.Data;
using Shared.Database;

namespace Questions.Infrastructure.Postgres;

public class PostgreTransactionManager : ITransactionManager
{
    public Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}