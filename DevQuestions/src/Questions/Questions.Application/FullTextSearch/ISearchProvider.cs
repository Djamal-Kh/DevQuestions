using Questions.Domain;

namespace Questions.Application.FullTextSearch;

public interface ISearchProvider
{
    Task<List<Guid>> SearchAsync(string query);

    Task IndexQuestionAsync(Question questions);
}