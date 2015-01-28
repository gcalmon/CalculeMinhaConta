using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculeMinhaConta.Tests
{
    [TestClass]
    public class PlanoTest
    {
        [TestMethod]
        public void FuncionamentoBasicoDoPlano()
        {
            //Arrange
            var minutosFixos = 1;
            var minutosCel = 2;
            var minutosInter = 3;

            var consumo = new Consumo("Andre",
                                       minutosFixos,
                                       minutosCel,
                                       minutosInter);

            var plano = new Plano("Smart", 0.10m, 0.35m, 0.75m, 30m);

            var total = (consumo.MinutosParaFixo * plano.ValorParaFixo) +
                        (consumo.MinutosParaCelular * plano.ValorParaCelular) +
                        (consumo.MinutosParaInterurbano * plano.ValorParaInterurbano) +
                        plano.ValorAssinatura;

            //Act
            var conta = plano.CalcularConta(consumo);

            //Assert
            Assert.AreEqual(total, conta.Total);
        }

        [TestMethod]
        public void FuncionamentoBasicoDaPromocao()
        {
            //Arrange
            var minutosFixos = 1;
            var minutosCel = 2;
            var minutosInter = 3;

            var consumo = new Consumo("Andre",
                                       minutosFixos,
                                       minutosCel,
                                       minutosInter);

            var plano = new Plano("Smart", 0.10m, 0.35m, 0.75m, 30m);

            var promocao = new DescontoSobreConsumo("Fale 30", 30, 0.05m, 0.25m, 0.60m);

            plano.AdicionarPromocao(promocao);

            var total = (consumo.MinutosParaFixo * plano.ValorParaFixo) +
                        (consumo.MinutosParaCelular * plano.ValorParaCelular) +
                        (consumo.MinutosParaInterurbano * plano.ValorParaInterurbano) +
                        (1 * plano.ValorParaFixo) +
                        (1 * plano.ValorParaCelular) +
                        (1 * plano.ValorParaInterurbano) +
                        plano.ValorAssinatura;

            //Act
            var conta = plano.CalcularConta(consumo);

            //Assert
            Assert.AreEqual(total, conta.Total);
        }
    }
}
