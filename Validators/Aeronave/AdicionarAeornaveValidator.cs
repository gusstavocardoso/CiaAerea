using CiaAerea.Contexts;
using CiaAerea.ViewModels.Aeronave;
using FluentValidation;

namespace CiaAerea.Validators.Aeronave;

public class AdicionarAeornaveValidator: AbstractValidator<AdicionarAeronaveViewModel>
{
    private readonly CiaAereaContext _context;
    
    public AdicionarAeornaveValidator(CiaAereaContext context)
    {
        _context = context;

        RuleFor(a => a.Fabricante)
            .NotEmpty().WithMessage("É necessário informar o fabricante da aeronave")
            .MaximumLength(50).WithMessage("O nome do fabricante deve ter no máximo 50 caracteres.");
        
        RuleFor(a => a.Modelo)
            .NotEmpty().WithMessage("É necessário informar o modelo da aeronave")
            .MaximumLength(50).WithMessage("O nome do modelo deve ter no máximo 50 caracteres.");

        RuleFor(a => a.Codigo)
            .NotEmpty().WithMessage("É necessário informar o código da aeronave")
            .MaximumLength(50).WithMessage("O código deve ter no máximo 10 caracteres.")
            .Must(codigo => !_context.Aeronaves.Any(aeronave => aeronave.Codigo == codigo))
            .WithMessage("Já existe uma aeronave com esse código.");
    }
}