using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Servicos
{
    [TestClass]
    public class VeiculoServicoTest
    {
        private static DbContexto CriarContextoDeTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            return new DbContexto(configuration);
        }

        [TestMethod]
        public void TestandoSalvarVeiculo()
        {
            // Arrange
            var context = CriarContextoDeTest();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos; ALTER TABLE Veiculos AUTO_INCREMENT = 1;");

            var veiculo = new Veiculo
            {
                Nome = "Uno",
                Marca = "Fiat",
                Ano = 2000,
            };

            var veiculoServico = new VeiculoServico(context);

            // Act
            veiculoServico.Incluir(veiculo);

            // Assert
            Assert.AreEqual(1, veiculoServico.Todos(1).Count);
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            // Arrange
            var context = CriarContextoDeTest();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos; ALTER TABLE Veiculos AUTO_INCREMENT = 1;");

            var veiculo = new Veiculo
            {
                Nome = "Uno",
                Marca = "Fiat",
                Ano = 2000,
            };

            var veiculoServico = new VeiculoServico(context);

            // Act
            veiculoServico.Incluir(veiculo);
            var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo.Id);

            // Assert
            Assert.AreEqual(1, veiculoDoBanco.Id);
        }
    }
}
