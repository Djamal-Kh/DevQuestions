using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.Validators;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required")
            .MaximumLength(500).WithMessage("Title cannot exceed 500 characters");

        RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required")
            .MaximumLength(5000).WithMessage("Text cannot exceed 5000 characters");

        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}