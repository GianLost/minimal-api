using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Servicos;
using minimal_api.Infraestrutura.Db;

namespace Test.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
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
        public void TestandoSalvarAdministrador()
        {
            // Arrange
            var context = CriarContextoDeTest();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores; ALTER TABLE Administradores AUTO_INCREMENT = 1;");

            var adm = new Administrador
            {
                Email = "teste@teste.com",
                Senha = "123456",
                Perfil = "Adm",
            };

            var administradorServico = new AdministradorServico(context);

            // Act
            administradorServico.Incluir(adm);

            // Assert
            Assert.AreEqual(1, administradorServico.Todos(1).Count);
        }

        [TestMethod]
        public void TestandoBuscaPorId()
        {
            // Arrange
            var context = CriarContextoDeTest();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores; ALTER TABLE Administradores AUTO_INCREMENT = 1;");

            var adm = new Administrador
            {
                Id = 1,
                Email = "teste@teste.com",
                Senha = "123456",
                Perfil = "Adm",
            };

            var administradorServico = new AdministradorServico(context);

            // Act
            administradorServico.Incluir(adm);
            var admDoBanco = administradorServico.BuscaPorId(adm.Id);

            // Assert
            Assert.AreEqual(1, admDoBanco.Id);
        }
    }
}