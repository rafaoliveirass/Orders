using FluentValidation;
using Orders.Application.Dtos;

namespace Orders.Application.Validators
{
    public class OrderRequestDtoValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderRequestDtoValidator() { 
	
	  RuleFor(order => order.itens).NotNull().WithMessage("A lista de itens não pode ser nula.");

	  RuleFor(order => order.itens)
            .NotEmpty()
            .WithMessage("A lista de itens do pedido não pode estar vazia.");

 	  RuleForEach(order => order.itens)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("A quantidade do produto deve ser maior que zero.")
                    .Must(q => q % 1 == 0)
                    .WithMessage("A quantidade deve ser um número inteiro.");

                item.RuleFor(x => x.ProductId)
                    .NotEmpty()
                    .WithMessage("O ID do produto é obrigatório.");
            });

            RuleFor(x => x.currency)
                .NotEmpty().WithMessage("Currency é obrigatório")
                .MaximumLength(50).WithMessage("Currency deve ter no máximo 50 caracteres");
        }    
    }
}
