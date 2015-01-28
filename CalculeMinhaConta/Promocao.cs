using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculeMinhaConta
{
    public interface IPromocao
    {
        decimal CalcularDesconto(IEnumerable<Cobranca> cobrancas);
    }

    public class PromocaoNaoPagaNada : IPromocao
    {
        public decimal CalcularDesconto(IEnumerable<Cobranca> cobrancas)
        {
            return cobrancas.Sum(c => c.ObterTotal());
        }
    }

    public class PromocaoDezPorcento : IPromocao
    {
        public decimal CalcularDesconto(IEnumerable<Cobranca> cobrancas)
        {
            return cobrancas.Sum(c => c.ObterTotal()) * 0.1m;
        }
    }

    public class PromocaoDescontoSobreComsumo : IPromocao
    {
        public string Nome { get; set; }

        public int ConsumoReferencia { get; set; }
        
        public decimal ValorParaFixo { get; set; }

        public decimal ValorParaCelular { get; set; }

        public decimal ValorParaInterurbano { get; set; }

        public PromocaoDescontoSobreComsumo(string nome,
                                    int consumoReferencia,
                        decimal valorParaFixo, 
                        decimal valorParaCelular, 
                        decimal valorParaInterurbano)
        {
            this.Nome = nome;
            this.ConsumoReferencia = consumoReferencia;
            this.ValorParaFixo = valorParaFixo;
            this.ValorParaCelular = valorParaCelular;
            this.ValorParaInterurbano = valorParaInterurbano;
        }

        public decimal CalcularDesconto(IEnumerable<Cobranca> cobrancas)
        {
            var desconto = 0m;

            // Quanto eu tenho que devolver pelo Fixo!
            {
                var trinta = this.ConsumoReferencia;
                var servico = Servico.Fixo;
                var valorPromocional = this.ValorParaFixo;

                var cobranca = cobrancas.First(c => c.Servico == servico);

                var valorOriginal = cobranca.ValorUnitario;

                if (cobranca.Quantidade > 30)
                {
                    desconto += (cobranca.Quantidade - trinta) * (valorOriginal - valorPromocional);
                }
            }

            {
                var trinta = this.ConsumoReferencia;
                var servico = Servico.Celular;
                var valorPromocional = this.ValorParaCelular;

                var cobranca = cobrancas.First(c => c.Servico == servico);

                var valorOriginal = cobranca.ValorUnitario;

                if (cobranca.Quantidade > 30)
                {
                    desconto += (cobranca.Quantidade - trinta) * (valorOriginal - valorPromocional);
                }
            }

            {
                var trinta = this.ConsumoReferencia;
                var servico = Servico.Interurbano;
                var valorPromocional = this.ValorParaInterurbano;

                var cobranca = cobrancas.First(c => c.Servico == servico);

                var valorOriginal = cobranca.ValorUnitario;

                if (cobranca.Quantidade > 30)
                {
                    desconto += (cobranca.Quantidade - trinta) * (valorOriginal - valorPromocional);
                }
            }

            return desconto;
        }
    }
}
