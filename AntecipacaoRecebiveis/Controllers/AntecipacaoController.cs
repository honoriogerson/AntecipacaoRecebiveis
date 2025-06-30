using Application.Size.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Size.Context;

namespace AntecipacaoRecebiveis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AntecipacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
                
        public AntecipacaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{empresaId}/checkout")]
        public async Task<IActionResult> Calcular(int empresaId, [FromBody] List<int> notaIds)
        {
            var empresa = await _context.Empresas.Include(e => e.NotasFiscais)
                                                 .FirstOrDefaultAsync(e => e.Id == empresaId);

            if (empresa == null) return NotFound();

            var carrinho = empresa.NotasFiscais.Where(n => notaIds.Contains(n.Id)).ToList();
            var resultado = new AntecipacaoService().CalcularAntecipacao(empresa, carrinho);

            return Ok(resultado);
        }
    }
}
