namespace Application.Size
{
    public class AntecipacaoResultado
    {
        public string? Empresa { get; set; }
        public string? CNPJ { get; set; }
        public decimal Limite { get; set; }

        public List<NotaFiscalResultado> NotasFiscais { get; set; }

        public decimal TotalBruto { get; set; }
        public decimal TotalLiquido { get; set; }
    }

    public class NotaFiscalResultado
    {
        public int Numero { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}

