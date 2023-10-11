
using TodoList.Domain.Entities;
using TodoList.Domain.Validation;

namespace TodoList.Test.Domain
{
    [TestClass]
    public class TestDomainJob
    {


   

        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_IfhNameIsNameNullOrEmpty_Should_RetunDomainExceptionValidation()
        {

            // Arrange
            var id = 1;
            string? emptyName = null;
            var description = "Some description";
            var executionDate = DateTime.Now;

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, emptyName, description, executionDate, null);
            });

        }

        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_IfNameWhiteSpace_Should_ReturnDomainExceptionValidation()
        {

            // Arrange
            var id = 1;
            var emptyName = " ";
            var description = "Some description";
            var executionDate = DateTime.Now;

            // Act & Assert
            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, emptyName, description, executionDate, null);
            });

        }

        //[TestMethod]
        //[TestCategory("Domain")]
        //public void CreateJob_IfDateIsSmallerDateCurrent_Sould_ReturnDomainExceptionValidation()
        //{
        //    // Arrange
        //    var id = 1;
        //    var emptyName = " ";
        //    var description = "Some description";
        //    var executionDate = DateTime.Now.AddMinutes(2); ;

        //    // Act & Assert
        //    Assert.ThrowsException<DomainExceptionValidation>(() =>
        //    {
        //        new Job(id, emptyName, description, executionDate, null);
        //    });

        //}


        [TestMethod]
        [TestCategory("Domain")]
        public void CreatedJob_IfNameIsnNotSmaller_WhatThree_Should_ReturnDomainExceptionValidation()
        {
            var id = 1;
            var Name = "Tes";
            var description = "Some description";
            var executionDate = DateTime.Now;

            Assert.ThrowsException<DomainExceptionValidation>(() =>
            {
                new Job(id, Name, description, executionDate, null);
            });

        }

        

    }
}


