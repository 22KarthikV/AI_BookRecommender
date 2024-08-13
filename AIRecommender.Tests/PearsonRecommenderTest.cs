using AIRecommender.Core.Implementations;
using AIRecommender.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AIRecommender.Tests
{
    [TestClass]
    public class PearsonRecommenderTests
    {
        private IRecommender _recommender;

        [TestInitialize]
        public void Setup()
        {
            _recommender = new PearsonRecommender();
        }

        [TestMethod]
        public void GetCorrelation_EqualArrays_ReturnsOne()
        {
            int[] baseData = new int[] { 1, 2, 3, 4, 5 };
            int[] otherData = new int[] { 1, 2, 3, 4, 5 };

            double result = _recommender.GetCorrelation(baseData, otherData);

            Assert.AreEqual(1.0, result, 0.0001);
        }

        [TestMethod]
        public void GetCorrelation_InverselyCorrelatedArrays_ReturnsMinusOne()
        {
            int[] baseData = new int[] { 1, 2, 3, 4, 5 };
            int[] otherData = new int[] { 5, 4, 3, 2, 1 };

            double result = _recommender.GetCorrelation(baseData, otherData);

            Assert.AreEqual(-1.0, result, 0.0001);
        }

        [TestMethod]
        public void GetCorrelation_UncorrelatedArrays_ReturnsZero()
        {
            int[] baseData = new int[] { 1, 2, 3, 4, 5 };
            int[] otherData = new int[] { 2, 2, 2, 2, 2 };

            double result = _recommender.GetCorrelation(baseData, otherData);

            Assert.AreEqual(0.0, result, 0.0001);
        }

        [TestMethod]
        public void GetCorrelation_DifferentLengthArrays_AdjustsAndCalculates()
        {
            int[] baseData = new int[] { 1, 2, 3 };
            int[] otherData = new int[] { 1, 2, 3, 4, 5 };

            double result = _recommender.GetCorrelation(baseData, otherData);

            Assert.AreEqual(0.9258200997725514, result, 0.0001);
        }

        [TestMethod]
        public void GetCorrelation_MaxRatingAndZero_AdjustsCorrectly()
        {
            int[] baseData = new int[] { 10, 5, 3, 8 };
            int[] otherData = new int[] { 0, 5, 3, 8 };

            double result = _recommender.GetCorrelation(baseData, otherData);

            Assert.AreEqual(0.9055217489117404, result, 0.0001);
        }

        [TestMethod]
        public void GetCorrelation_ArraysWithZeros_AdjustsAndCalculates()
        {
            int[] baseData = new int[] { 0, 2, 3, 4, 5 };
            int[] otherData = new int[] { 1, 0, 3, 4, 5 };

            double result = _recommender.GetCorrelation(baseData, otherData);

            Assert.AreEqual(0.9722718241315029, result, 0.0001);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCorrelation_NullArrays_ThrowsException()
        {
            _recommender.GetCorrelation(null, null);
        }
    }
}