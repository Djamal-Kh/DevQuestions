using Shared;

namespace DevQuestions.Application.Questions.Fails;

public partial class Errors1
{
    public static class General
    {
        public static Error NotFound(Guid questionId) =>
            Error.Failure("record.not.found", "Запись по id не найдена - {questionId}");
    }

    public static class Questions
    {
        public static Error ToManyQuestions() =>
            Error.Failure("questions.too.many", "Пользователь не может открыть больше 3 вопроса");
    }
}