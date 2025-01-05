using minimal_api.Dominio.Entidades;

namespace Test.Domain.Entidades
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            // Arrange
            var adm = new Veiculo
            {
                // Act
                Id = 1,
                Nome = "Fusion",
                Marca = "Ford",
                Ano = 2021,
            };

            // Assert
            Assert.AreEqual(1, adm.Id);
            Assert.AreEqual("Fusion", adm.Nome);
            Assert.AreEqual("Ford", adm.Marca);
            Assert.AreEqual(2021, adm.Ano);
        }
    }
}
