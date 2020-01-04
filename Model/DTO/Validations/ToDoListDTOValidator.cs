using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Model.DTO.Validations
{
    public class ToDoListDTOValidator : AbstractValidator<ToDoListDTO>
    {
        public ToDoListDTOValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty()
               .MaximumLength(200);

            RuleFor(x => x.Color)
               .NotEmpty()
               .MaximumLength(100)
               .Matches("^#[0-9,a-z,A-Z]*$")
               .WithMessage("Only color in hex format is valid");
        }
    }
}
