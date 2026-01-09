using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddDtoValidatior : AbstractValidator<AddAnswerDto>
{
    public AddDtoValidatior()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required")
            .MaximumLength(5000).WithMessage("Text cannot exceed 5000 characters");

        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}