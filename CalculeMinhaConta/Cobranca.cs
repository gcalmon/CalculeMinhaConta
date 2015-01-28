using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculeMinhaConta
{
    public class Cobranca
    {
        public Servico Servico { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public Cobranca(Servico servico, int quantidade, decimal valorUnitario)
        {
            Servico = servico;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public decimal ObterTotal()
        {
            return this.Quantidade * this.ValorUnitario;
        }
    }
}
