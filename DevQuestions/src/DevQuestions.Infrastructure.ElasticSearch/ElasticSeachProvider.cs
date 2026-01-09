using DevQuestions.Application.FullTextSearch;
using DevQuestions.Domain.Questions;

namespace DevQuestions.Infrastructure.ElasticSearch;

public class ElasticSeachProvider : ISearchProvider
{
    public Task<List<Guid>> SearchAsync(string query) => throw new NotImplementedException();
    public Task IndexQuestionAsync(Question questions) => throw new NotImplementedException();
}