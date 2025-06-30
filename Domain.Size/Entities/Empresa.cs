using Domain.Size.Enumerations;

namespace Domain.Size.Entities
{
    public class Empresa
    {        
        public int? Id { get; set; }
        public string? CNPJ { get; set; }
        public string? Nome { get; set; }
        public decimal? FaturamentoMensal { get; set; }
        public RamoEmpresa Ramo { get; set; }
      
        public List<NotaFiscal>? NotasFiscais { get; set; } = new();
    }
}
