
using TodoList.Domain.Entities;
using TodoList.Domain.Validation;

namespace TodoList.Test.Domain
{
    [TestClass]
    public class TestDomainJob
    {


        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_WithNameIsNameNull_Should_IsValid()
        {

            // Arrange
            var id = 1;
            var Name = "mercado";
            var description = "Some description";
            var executionDate = DateTime.Now;

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, Name, description, executionDate);
            });

        }



        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_WithNameIsNameNull_Should_ReturnErro()
        {

            // Arrange
            var id = 1;
            var emptyName = "";
            var description = "Some description";
            var executionDate = DateTime.Now;

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, emptyName, description, executionDate);
            });

        }

        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_WithNameWhiteSpace_Should_ReturnErro()
        {

            // Arrange
            var id = 1;
            var emptyName = " ";
            var description = "Some description";
            var executionDate = DateTime.Now;

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, emptyName, description, executionDate);
            });

        }


        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_Should_IsValid()
        {

            // Arrange
            var id = 1;
            var emptyName = "Teste";
            var description = "Some description";
            var executionDate = DateTime.Now;

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, emptyName, description, executionDate);
            });

        }


        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_Should_DataExecutionInvalid()
        {

            // Arrange
            var id = 1;
            var emptyName = "Teste";
            var description = "Some description";
            var executionDate = DateTime.Now.AddDays(-3);

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, emptyName, description, executionDate);
            });

        }


    }
}


