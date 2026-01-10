using Questions.Application.FullTextSearch;
using Questions.Domain;

namespace Infrastructure.ElasticSearch;

public class ElasticSeachProvider : ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();
    public Task IndexQuestionAsync(Question questions) => throw new NotImplementedException();
}