using Domain.Size.Entities;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Size.Context;

namespace AntecipacaoRecebiveis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpresaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost ("/CadastrarEmpresa")]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            return Ok(empresa);
        }

        [HttpPost("{empresaId}/CadrastrarNotas")]
        public async Task<IActionResult> AdicionarNota(int empresaId, [FromBody] NotaFiscal nota)
        {
            var empresa = await _context.Empresas.FindAsync(empresaId);
            if (empresa == null) return NotFound();

            if (!nota.DataVencimento.HasValue || nota.DataVencimento.Value.Date <= DateTime.Now.Date)
            {
                return BadRequest("A data de vencimento deve ser maior que a data de hoje.");
            }

            nota.EmpresaId = empresaId;
            _context.NotasFiscais.Add(nota);
            await _context.SaveChangesAsync();
            return Ok(nota);
        }
    }
}
