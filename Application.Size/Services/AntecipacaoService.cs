using Domain.Size.Entities;

namespace Application.Size.Services
{
    public class AntecipacaoService
    {
        private const decimal TAXA_MENSAL = 0.0465m;

        public static decimal CalcularDesagio(decimal valorBruto, int diasPrazo)
        {
            var fator = (decimal)Math.Pow((double)(1 + TAXA_MENSAL), diasPrazo / 30.0);
            return valorBruto - (valorBruto / fator);
        }

        public AntecipacaoResultado CalcularAntecipacao(Empresa empresa, List<NotaFiscal> carrinho)
        {
            var limite = EmpresaService.CalcularLimite(empresa);
            var totalBruto = carrinho.Sum(n => n.Valor);
            if (totalBruto > limite)
                throw new InvalidOperationException("Total excede o limite disponível.");

            var hoje = DateTime.Today;

            var resultado = new AntecipacaoResultado
            {
                Empresa = empresa.Nome,
                CNPJ = empresa.CNPJ,
                Limite = limite,
                NotasFiscais = new List<NotaFiscalResultado>()
            };

            foreach (var nota in carrinho)
            {                
                int diasPrazo = nota.DataVencimento.HasValue
                    ? (nota.DataVencimento.Value - hoje).Days
                    : 0;

                var desagio = CalcularDesagio(nota.Valor, diasPrazo);
                var valorLiquido = nota.Valor - desagio;

                resultado.NotasFiscais.Add(new NotaFiscalResultado
                {
                    Numero = nota.Numero,
                    ValorBruto = nota.Valor,
                    ValorLiquido = Math.Round(valorLiquido, 2)
                });
            }

            resultado.TotalBruto = resultado.NotasFiscais.Sum(n => n.ValorBruto);
            resultado.TotalLiquido = resultado.NotasFiscais.Sum(n => n.ValorLiquido);

            return resultado;
        }
    }
}
