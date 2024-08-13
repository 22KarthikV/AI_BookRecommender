using AIRecommender.Core.Interfaces;
using System;
using System.Linq;

namespace AIRecommender.Core.Implementations
{
    public class PearsonRecommender : IRecommender
    {
        private const int MAX_RATING = 10;

        public double GetCorrelation(int[] baseData, int[] otherData)
        {
            ValidateInputArrays(baseData, otherData);
            (baseData, otherData) = AdjustArrays(baseData, otherData);

            int n = baseData.Length;
            double sumX = baseData.Sum();
            double sumY = otherData.Sum();
            double sumXY = baseData.Zip(otherData, (x, y) => (double)x * y).Sum();
            double sumX2 = baseData.Sum(x => (double)x * x);
            double sumY2 = otherData.Sum(y => (double)y * y);

            double numerator = n * sumXY - sumX * sumY;
            double denominator = Math.Sqrt((n * sumX2 - sumX * sumX) * (n * sumY2 - sumY * sumY));

            return denominator == 0 ? 0 : numerator / denominator;
        }

        private void ValidateInputArrays(int[] baseData, int[] otherData)
        {
            if (baseData == null || otherData == null)
                throw new ArgumentNullException("Input arrays cannot be null.");
        }

        private (int[], int[]) AdjustArrays(int[] baseData, int[] otherData)
        {
            int maxLength = Math.Max(baseData.Length, otherData.Length);
            var adjustedBase = new int[maxLength];
            var adjustedOther = new int[maxLength];

            for (int i = 0; i < maxLength; i++)
            {
                int baseValue = i < baseData.Length ? baseData[i] : 0;
                int otherValue = i < otherData.Length ? otherData[i] : 0;

                if (baseValue == MAX_RATING && otherValue == 0)
                {
                    adjustedBase[i] = baseValue;
                    adjustedOther[i] = 1;
                }
                else if (baseValue == 0 && otherValue == MAX_RATING)
                {
                    adjustedBase[i] = 1;
                    adjustedOther[i] = otherValue;
                }
                else if (baseValue == 0 || otherValue == 0)
                {
                    adjustedBase[i] = baseValue == 0 ? 1 : baseValue;
                    adjustedOther[i] = otherValue == 0 ? 1 : otherValue;
                }
                else
                {
                    adjustedBase[i] = baseValue;
                    adjustedOther[i] = otherValue;
                }
            }

            return (adjustedBase, adjustedOther);
        }
    }
}