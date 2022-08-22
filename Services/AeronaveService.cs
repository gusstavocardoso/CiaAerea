using CiaAerea.Contexts;
using CiaAerea.Entities;
using CiaAerea.Validators.Aeronave;
using CiaAerea.ViewModels.Aeronave;
using FluentValidation;

namespace CiaAerea.Services;

public class AeronaveService
{
    private readonly CiaAereaContext _context;
    private readonly AdicionarAeornaveValidator _adicionarAeornaveValidator;
    private readonly AtualizarAeronaveValidator _atualizarAeornaveValidator;
    private readonly ExcluirAeronaveValidator _excluirAeronaveValidator;

    public AeronaveService(CiaAereaContext context, AdicionarAeornaveValidator adicionarAeornaveValidator, AtualizarAeronaveValidator atualizarAeornaveValidator, ExcluirAeronaveValidator excluirAeronaveValidator)
    {
        _context = context;
        _adicionarAeornaveValidator = adicionarAeornaveValidator;
        _atualizarAeornaveValidator = atualizarAeornaveValidator;
        _excluirAeronaveValidator = excluirAeronaveValidator;
    }

    public DetalhesAeronaveViewModel AdicionarAeronave(AdicionarAeronaveViewModel dados)
    {
        _adicionarAeornaveValidator.ValidateAndThrow(dados);
        var aeronave = new Aeronave(dados.Fabricante, dados.Modelo, dados.Codigo);

        _context.Add(aeronave);
        _context.SaveChanges();

        return new DetalhesAeronaveViewModel
        (
            aeronave.Id,
            aeronave.Fabricante,
            aeronave.Modelo,
            aeronave.Codigo
        );
    }

    public IEnumerable<ListarAeronaveViewModel> ListarAeronaves()
    {
        return _context.Aeronaves.Select(a => new ListarAeronaveViewModel(a.Id, a.Modelo, a.Codigo));
    }

    public DetalhesAeronaveViewModel? ListarAeronavPeloId(int id)
    {
        var aeronave = _context.Aeronaves.Find(id);

        if (aeronave != null)
        {
            return new DetalhesAeronaveViewModel
            (
                aeronave.Id,
                aeronave.Fabricante,
                aeronave.Modelo,
                aeronave.Codigo
            );
        }

        return null;
    }
    
    public DetalhesAeronaveViewModel? AtualizarAeronave(AtualizarAeronaveViewModel dados)
    {
        _atualizarAeornaveValidator.ValidateAndThrow(dados);
        var aeronave = _context.Aeronaves.Find(dados.Id);

        if (aeronave != null)
        {
            aeronave.Fabricante = dados.Fabricante;
            aeronave.Modelo = dados.Modelo;
            aeronave.Codigo = dados.Codigo;

            _context.Update(aeronave);
            _context.SaveChanges();

            return new DetalhesAeronaveViewModel(aeronave.Id, aeronave.Fabricante, aeronave.Modelo, aeronave.Codigo);
        }

        return null;
    }

    public void ExcluirAeronave(int id)
    {
        _excluirAeronaveValidator.ValidateAndThrow(id);
        var aeronave = _context.Aeronaves.Find(id);
        
        {
            if (aeronave != null)
            {
                _context.Remove(aeronave);
                _context.SaveChanges();
            }
        }
    }
}