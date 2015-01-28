using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculeMinhaConta.Tests
{
    [TestClass]
    public class PlanoTests
    {
        [TestMethod]
        public void FuncionamentoBasicoDoPlano()
        {
            // Arrange
            var fixo = 1;
            var cel = 2;
            var inter = 3;

            var valfixo = 0.10m;
            var valcel = 0.35m;
            var valinter = 0.75m;
            var valassinatura = 30m;

            var consumo = new Consumo("Denis", fixo, cel, inter);
            var plano = new Plano("Vivo Smart", valfixo, valcel, valinter, valassinatura);

            var total = (fixo * valfixo) +
                        (cel * valcel) +
                        (inter * valinter) +
                        valassinatura;

            // Act
            var conta = plano.CalcularConta(consumo);

            // Assert
            Assert.AreEqual(total, conta.Total);
        }

        [TestMethod]
        public void FuncionamentoBasicoDoPlano3()
        {
            // Arrange
            var fixo = 31;
            var cel = 31;
            var inter = 31;

            var valfixo = 0.10m;
            var valcel = 0.35m;
            var valinter = 0.75m;
            var valassinatura = 30m;

            var consumo = new Consumo("Denis", fixo, cel, inter);
            var plano = new Plano("Vivo Smart", valfixo, valcel, valinter, valassinatura);

            var total = (fixo * valfixo) +
                        (cel * valcel) +
                        (inter * valinter) +
                        valassinatura;

            // Act
            var conta = plano.CalcularConta(consumo);

            // Assert
            Assert.AreEqual(total, conta.Total);
        }

        [TestMethod]
        public void FuncionamentoBasicoDaPromocao()
        {
            // Arrange
            var fixo = 30;
            var cel = 30;
            var inter = 30;

            var valfixo = 0.10m;
            var valcel = 0.35m;
            var valinter = 0.75m;
            var valassinatura = 30m;

            var consumo = new Consumo("Denis", fixo, cel, inter);
            var plano = new Plano("Vivo Smart", valfixo, valcel, valinter, valassinatura);

            var promocao = new PromocaoDescontoSobreComsumo("Fale 30", 30, 0.05m, 0.25m, 0.60m);

            plano.AdicionarPromocao(promocao);

            var total = (fixo * valfixo) +
                        (cel * valcel) +
                        (inter * valinter) +
                        valassinatura;

            // Act
            var conta = plano.CalcularConta(consumo);

            // Assert
            Assert.AreEqual(total, conta.Total);
        }

        [TestMethod]
        public void FuncionamentoBasicoDaPromocao2()
        {
            // Arrange
            var fixo = 31;
            var cel = 31;
            var inter = 31;

            var valfixo = 0.10m;
            var valcel = 0.35m;
            var valinter = 0.75m;
            var valassinatura = 30m;

            var consumo = new Consumo("Denis", fixo, cel, inter);
            var plano = new Plano("Vivo Smart", valfixo, valcel, valinter, valassinatura);

            var promocao = new PromocaoDescontoSobreComsumo("Fale 30", 30, 0.05m, 0.25m, 0.60m);

            plano.AdicionarPromocao(promocao);

            var total = (promocao.ConsumoReferencia * valfixo) +
                        (promocao.ConsumoReferencia * valcel) +
                        (promocao.ConsumoReferencia * valinter) +
                        (1 * promocao.ValorParaFixo) +
                        (1 * promocao.ValorParaCelular) +
                        (1 * promocao.ValorParaInterurbano) +
                        valassinatura;

            // Act
            var conta = plano.CalcularConta(consumo);

            // Assert
            Assert.AreEqual(total, conta.Total);
        }

        [TestMethod]
        public void PromocaoNaoPagaNada()
        {
            // Arrange
            var fixo = 31;
            var cel = 31;
            var inter = 31;

            var valfixo = 0.10m;
            var valcel = 0.35m;
            var valinter = 0.75m;
            var valassinatura = 30m;

            var consumo = new Consumo("Denis", fixo, cel, inter);
            var plano = new Plano("Vivo Smart", valfixo, valcel, valinter, valassinatura);

            var promocao = new PromocaoNaoPagaNada();

            plano.AdicionarPromocao(promocao);

            var total = 0m;

            // Act
            var conta = plano.CalcularConta(consumo);

            // Assert
            Assert.AreEqual(total, conta.Total);
        }

        [TestMethod]
        public void PromocaoAcumulativa()
        {
            // Arrange
            var fixo = 31;
            var cel = 31;
            var inter = 31;

            var valfixo = 0.10m;
            var valcel = 0.35m;
            var valinter = 0.75m;
            var valassinatura = 30m;

            var consumo = new Consumo("Denis", fixo, cel, inter);
            var plano = new Plano("Vivo Smart", valfixo, valcel, valinter, valassinatura);

            plano.AdicionarPromocao(new PromocaoDezPorcento());
            plano.AdicionarPromocao(new PromocaoDezPorcento());


            var total = ((fixo * valfixo) +
                        (cel * valcel) +
                        (inter * valinter) +
                        valassinatura) * 0.8m;

            // Act
            var conta = plano.CalcularConta(consumo);

            // Assert
            Assert.AreEqual(total, conta.Total);
        }
    }
}
