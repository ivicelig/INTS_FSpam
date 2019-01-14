using System;
using System.Collections.Generic;
namespace FSpam.classifier
{
    class BayesClassifier<T>
    {
        private int MaxInterestingTokenCount = int.MaxValue;
        private Dictionary<T, double> Probabilities { get; set; }
        TokenLoader tokenLoader = new TokenLoader();
        public double CalculateProbabilityOfTokens(System.Collections.Generic.List<T> Items)
        {
            SortedList<string, double> SortedProbabilities = new SortedList<string, double>();
            for (int x = 0; x < Items.Count; ++x)
            {
                double TokenProbability = 0.5;

                if (Probabilities.ContainsKey(Items[x]))
                {
                    TokenProbability = Probabilities[Items[x]];
                }
                string Difference = ((0.5 - System.Math.Abs(0.5 - TokenProbability))).ToString(".0000000") + Items[x] + x;
                SortedProbabilities.Add(Difference, TokenProbability);
            }
            double TotalProbability = 1;
            double NegativeTotalProbability = 1;
            int Count = 0;
            int MaxCount = Math.Min(SortedProbabilities.Count, MaxInterestingTokenCount);
            foreach (string Probability in SortedProbabilities.Keys)
            {
                double TokenProbability = SortedProbabilities[Probability];
                TotalProbability *= TokenProbability;
                NegativeTotalProbability *= (1 - TokenProbability);
                ++Count;
                if (Count >= MaxCount)

                    break;
            }
            return TotalProbability / (TotalProbability + NegativeTotalProbability);

        }
    }
}