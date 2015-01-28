using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valores = System.Collections.Generic.Dictionary<CalculeMinhaConta.Servico, decimal>;

namespace CalculeMinhaConta
{
    public class Plano
    {
        public string Nome { get; set; }

        public Valores Valores { get; set; }

        protected IList<IPromocao> Promocoes { get; set; }

        public Plano()
        {
            this.Valores = new Valores();
            this.Promocoes = new List<IPromocao>();
        }

        public Plano(   string nome, 
                        decimal valorParaFixo, 
                        decimal valorParaCelular, 
                        decimal valorParaInterurbano,
                        decimal valorAssinatura)
            :this()
        {
            this.Nome = nome;

            this.AdicionarValor(Servico.Assinatura, valorAssinatura);
            this.AdicionarValor(Servico.Fixo, valorParaFixo);
            this.AdicionarValor(Servico.Celular, valorParaCelular);
            this.AdicionarValor(Servico.Interurbano, valorParaInterurbano);
        }

        public void AdicionarValor(Servico tipo, decimal valor)
        {
            this.Valores.Add(tipo, valor);
        }

        public Valores ObterValores()
        {
            return this.Valores; // TODO: Clonar!
        }

        public Conta CalcularConta(Consumo consumo)
        {
            var conta = new Conta();
            var consumos = consumo.ObterConsumos();
            var valores = this.ObterValores();

            var cobrancas = new List<Cobranca>();

            foreach (var item in consumos)
            {
                var valor = valores.First(c => c.Key == item.Key).Value;

                cobrancas.Add(new Cobranca(item.Key, item.Value, valor));
            }

            var desconto = this.Promocoes.Sum(p => p.CalcularDesconto(cobrancas));

            conta.Total = cobrancas.Sum(s => s.ObterTotal()) - desconto;

            return conta;
        }

        public void AdicionarPromocao(IPromocao promocao)
        {
            this.Promocoes.Add(promocao);
        }
    }
}
