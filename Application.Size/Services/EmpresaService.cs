using Domain.Size.Entities;
using Domain.Size.Enumerations;

namespace Application.Size.Services
{
    public class EmpresaService
    {
        public static decimal CalcularLimite(Empresa empresa)
        {
            var faturamento = empresa.FaturamentoMensal ?? 0;

            if (faturamento >= 100_001)
                return (decimal)(empresa.Ramo == RamoEmpresa.Servicos ? faturamento * 0.60m : faturamento * 0.65m);
            if (faturamento > 50_000)
                return (decimal)(empresa.Ramo == RamoEmpresa.Servicos ? faturamento * 0.55m : faturamento * 0.60m);
            if (faturamento >= 10_000)
                return (decimal)(faturamento * 0.50m);

            return 0;
        }
    }
}
