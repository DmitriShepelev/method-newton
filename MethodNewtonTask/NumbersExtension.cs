using System;

namespace MethodNewtonTask
{
    public static class NumbersExtension
    {
        /// <summary>
        /// Initializes static members of the <see cref="NumbersExtension"/> class.
        /// </summary>
        public static readonly AppSettings AppSettings = new AppSettings { Epsilon = double.Epsilon };

        /// <summary>
        /// Find n-th root of number with the given accuracy.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <param name="degree">Root degree.</param>
        /// <param name="accuracy">Accuracy value.</param>
        /// <returns> n-th root of number.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when number is negative and n degree is even.
        /// -or- 
        /// degree is less than zero
        /// -or-
        /// number is NaN, double.NegativeInfinity, double.PositiveInfinity.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when accuracy is less than zero.
        /// -or- 
        /// accuracy is more than `Epsilon`.
        /// </exception>
        public static double FindNthRoot(double number, int degree, double accuracy)
        {
            if (number < 0 && (degree & 1) == 0)
            {
                throw new ArgumentException("Number A cannot be negative when the n degree is even.");
            }

            if (degree <= 0)
            {
                throw new ArgumentException("Degree can not be less or equal zero.");
            }

            if (accuracy < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(accuracy)} cannot be less than zero.");
            }

            if (accuracy > NumbersExtension.AppSettings.Epsilon)
            {
                throw new ArgumentOutOfRangeException($"Accuracy should be less than {NumbersExtension.AppSettings.Epsilon}");
            }

            if (number is double.NegativeInfinity || number is double.PositiveInfinity || number is double.NaN)
            {
                throw new ArgumentException($"{nameof(number)} is not a finite value");
            }

            var prev = number;
            double next;
            int k = 1;
            while (true)
            {
                next = 1.0 / degree * (((degree - 1) * prev) + (number / Math.Pow(prev, degree - 1)));
                k++;
                if (Math.Abs(prev - next) < accuracy)
                {
                    break;
                }

                prev = next;
            }

            return prev;
        }
    }
}
