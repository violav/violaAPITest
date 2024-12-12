using Azure.Core;
using BusinessLogic.Controller;
using BusinessLogic.Options;
using BusinessLogic.Services;
using Data.Data.EF.Context;
using Data.Data.EF.Output;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Web.Mvc;

namespace BusinessLogicTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        private NorthwindContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Northwind;Trusted_Connection=True;Integrated Security=SSPI;TrustServerCertificate=True;").Options;
            var dbContext = new NorthwindContext(options);
            return dbContext;
        }

        [Fact]
        public void It_should_add_a_product_successfully_into_data_store()
        {
            //Arrange
            var dbContext = CreateDbContext();            
            var dati = dbContext
                   .Categories
                   .Take(5)
                   .AsEnumerable();
            //var product = new Product { Description = "Model S - Tesla", ProductName = "Tesla Car - Model S", UnitPrice = 10000 };
            ////Act
            //var result = sut.Add(product);

            //Assert
            Assert.True(dati.Count() == 5);
            //Clean up
            dbContext.Dispose();
        }

        //[Theory]
        //public void It_should_add_a_product_successfully_into_data_store_2(NorthwindContext dbContext)
        //{
        //    //Arrange
        //    //var dbContext = CreateDbContext();
        //    var dati = dbContext
        //           .Categories
        //           .Take(5)
        //           .AsEnumerable();
        //    //var product = new Product { Description = "Model S - Tesla", ProductName = "Tesla Car - Model S", UnitPrice = 10000 };
        //    ////Act
        //    //var result = sut.Add(product);

        //    //Assert
        //    Assert.True(dati.Count() == 5);
        //    //Clean up
        //    dbContext.Dispose();
        //}

        [Fact]
        public async Task It_should_add_a_product_successfully_into_data_store_2()
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Category>>();

            var mockContext = new Mock<NorthwindContext>();
            var options = new Mock<IOptions<BusinessOptions>>();
            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var service = new BusinessServices(mockContext.Object, options.Object);
            var data = await service.GetData();

            Assert.True(data.Count() == 1);
        }
        [Fact]
        public async Task It_should_add_a_product_successfully_into_data_store_3()
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Category>>();

            var mockContext = new Mock<NorthwindContext>();
            var options = new Mock<IOptions<BusinessOptions>>();

            var data = new List<Category>
            {
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object
            }.AsQueryable();

            mockSet.As<IEnumerable<Category>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            mockContext.Setup(m => m.Categories).Returns(mockSet.Object);

            var service = new BusinessServices(mockContext.Object, options.Object);
            var result = await service.GetData();

            Assert.True(result.Count() == 6);
        }



        /**************************+ DAPPER ********************************/
        [Fact]
        public async Task DapperTest()
        {
            var mockSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Category>>();

            var mockContext = new Mock<NorthwindContext>();
            var options = new Mock<IOptions<BusinessOptions>>();
            mockContext.Setup(m => m.Database.GetDbConnection()).Returns((new Mock<DbConnection>()).Object);

            var data = new List<Category>
            {
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object,
                (new Mock<Category>()).Object
            }.AsQueryable();

            mockSet.As<IEnumerable<Category>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var service = new BusinessServices(mockContext.Object, options.Object);
            var result = await service.GetCategories();

            Assert.True(result.Count() == 6);
        }
        /*********************************** FINE DAPPER ********************************/

        [Fact]
        public async Task TestApiRestCall()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            var mockContext = new Mock<NorthwindContext>();
            var options = new Mock<IOptions<BusinessOptions>>();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockMessageHandler.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonSerializer.Serialize(new Category() { CategoryId = 9, CategoryName = "tes", Description = "foo" }))
               });

            var httpClient = new HttpClient(mockMessageHandler.Object);

            var service = new BusinessServices(mockContext.Object, options.Object);
            var result = await service.GetNumberFromApi(httpClient);


        }
    }
}