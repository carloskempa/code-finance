using System;

namespace CodeFinance.Application.Dtos
{
    public class SaldoDto
    {
        public decimal SaldoAtual { get; set; }
        public DateTime? DataUltimaAlteracaoSaldo { get; set; }
    }
}
