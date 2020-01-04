using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Model.DTO.Validations
{
    public class ToDoItemDTOValidator : AbstractValidator<ToDoItemDTO>
    {
        public ToDoItemDTOValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

            RuleFor(x => x.Text)
            .MaximumLength(1000);

            RuleFor(x => x.Deadline)
              .Must(x => x > DateTime.Now)
              .WithMessage("Deadline date must be greater than today");
        }
    }
}
