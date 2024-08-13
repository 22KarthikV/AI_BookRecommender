using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AIRecommender.Core.Implementations;
using AIRecommender.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace AIRecommender.Tests
{
    [TestClass]
    public class CSVDataLoaderTests
    {
        private IDataLoader _dataLoader;
        private string _testDataPath;

        [TestInitialize]
        public void Setup()
        {
            _testDataPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData");
            Directory.CreateDirectory(_testDataPath);

            CreateTestCsvFiles();

            _dataLoader = new CSVDataLoader(
                Path.Combine(_testDataPath, "books.csv"),
                Path.Combine(_testDataPath, "ratings.csv"),
                Path.Combine(_testDataPath, "users.csv")
            );
        }

        [TestCleanup]
        public void Cleanup()
        {
            Directory.Delete(_testDataPath, true);
        }

        [TestMethod]
        public void Load_ValidCSVFiles_ReturnsCorrectBookDetails()
        {
            var result = _dataLoader.Load();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Book1", result[0].Book.BookTitle);
            Assert.AreEqual("Book2", result[1].Book.BookTitle);
            Assert.AreEqual(2, result[0].UserRatings.Count);
            Assert.AreEqual(1, result[1].UserRatings.Count);
            Assert.AreEqual(2, result[0].Users.Count);
            Assert.AreEqual(1, result[1].Users.Count);
        }

        [TestMethod]
        public void Load_EmptyCSVFiles_ReturnsEmptyList()
        {
            File.WriteAllText(Path.Combine(_testDataPath, "books.csv"), "ISBN,BookTitle,BookAuthor,YearOfPublication,Publisher,ImageUrlSmall,ImageUrlMedium,ImageUrlLarge");
            File.WriteAllText(Path.Combine(_testDataPath, "ratings.csv"), "UserID,ISBN,Rating");
            File.WriteAllText(Path.Combine(_testDataPath, "users.csv"), "UserID,Age,City,State,Country");

            var result = _dataLoader.Load();

            Assert.AreEqual(0, result.Count);
        }

        private void CreateTestCsvFiles()
        {
            File.WriteAllText(Path.Combine(_testDataPath, "books.csv"),
                "ISBN,BookTitle,BookAuthor,YearOfPublication,Publisher,ImageUrlSmall,ImageUrlMedium,ImageUrlLarge\n" +
                "1234,Book1,Author1,2021,Publisher1,url1,url2,url3\n" +
                "5678,Book2,Author2,2022,Publisher2,url4,url5,url6");

            File.WriteAllText(Path.Combine(_testDataPath, "ratings.csv"),
                "UserID,ISBN,Rating\n" +
                "user1,1234,4\n" +
                "user2,1234,5\n" +
                "user2,5678,3");

            File.WriteAllText(Path.Combine(_testDataPath, "users.csv"),
                "UserID,Age,City,State,Country\n" +
                "user1,25,City1,State1,Country1\n" +
                "user2,30,City2,State2,Country2");
        }
    }
}
