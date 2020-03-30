using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecomendationService.Controllers;
using RecommendationService.Models;
using System;
using System.Collections.Generic;

namespace RecommendationServiceTest
{
    [TestClass]
    public class RecommendationServiceControllerTest
    {
        RecomendationServiceController rscMock; 

        [TestInitialize]
        public void start()
        {
            rscMock = new RecomendationServiceController(true);
        }

        [TestMethod]
        public void GetRecomendation_ValidProductId_NotNullPageDTO()
        {
            //Arrange
            string validProductId = "xDgnEowe4u";
            List<ProductDTO> actual = new List<ProductDTO>();

            //Act
            try
            {
                actual = (List<ProductDTO>)((OkObjectResult)rscMock.GetRecomendation(validProductId)).Value;
            }
            catch (Exception e)
            {
                throw e;
            }

            //Assert
            Assert.IsNotNull(actual);
        }
    }
}
