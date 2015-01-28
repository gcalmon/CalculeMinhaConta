using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculeMinhaConta
{
    public class Consumo
    {
        public string Usuario { get; set; }

        protected Dictionary<Servico, int> Consumos { get; set; }

        protected Consumo()
        {
            this.Consumos = new Dictionary<Servico, int>();
        }

        public Consumo( string usuario, 
                        int minutosParaFixo, 
                        int minutosParaCelular, 
                        int minutosParaInterurbano)
            : this()
        {
            this.Usuario = usuario;
            this.AdicionarConsumo(Servico.Assinatura, 1);
            this.AdicionarConsumo(Servico.Fixo, minutosParaFixo);
            this.AdicionarConsumo(Servico.Celular, minutosParaCelular);
            this.AdicionarConsumo(Servico.Interurbano, minutosParaInterurbano);

        }

        public Dictionary<Servico, int> ObterConsumos()
        {
            return this.Consumos; // TODO: Copiar!
        }

        public void AdicionarConsumo(Servico tipo, int valor)
        {
            this.Consumos.Add(tipo, valor);
        }
    }
}
