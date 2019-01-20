using FSpam.data;
using System;
using System.Collections.Generic;
using System.IO;

namespace FSpam.classifier
{
    class BayesClassifier
    {
        private Dictionary<String, double> Probabilities { get; set; }
        TokenLoader tokenLoader;

        public BayesClassifier()
        {
            tokenLoader = new TokenLoader();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            List<string> tokensA = readFiles.getTokensFromFolder(projectDirectory + @"\DataSet\ham");
            List<string> tokensB = readFiles.getTokensFromFolder(projectDirectory + @"\DataSet\spam");
            tokenLoader.LoadTokens(tokensA, tokensB);
            Probabilities = new Dictionary<string, double>();
            fillProbabilitiesDict();
        }

        public Tuple<double, string> CalculateProbabilityOfTokens(System.Collections.Generic.List<String> Items)
        {
            SortedList<string, double> SortedProbabilities = new SortedList<string, double>();
            for (int x = 0; x < Items.Count; ++x)
            {
                double TokenProbability = 0.5;

                if (Probabilities.ContainsKey(Items[x]))
                {
                    TokenProbability = Probabilities[Items[x]];
                }

                SortedProbabilities.Add(x.ToString(), TokenProbability);
            }
            double TotalProbability = 1;
            double NegativeTotalProbability = 1;
            foreach (string Probability in SortedProbabilities.Keys)
            {
                double TokenProbability = SortedProbabilities[Probability];
                TotalProbability *= TokenProbability;
                NegativeTotalProbability *= (1 - TokenProbability);
            }
            double bayesValue = TotalProbability / (TotalProbability + NegativeTotalProbability);
            string spam = bayesValue > 0.5 ? "DA" : "NE";
            return Tuple.Create(bayesValue, spam);
        }
        private double CalculateProbabilityOfToken(string Item)
        {
            int countItemInA = tokenLoader.SetA.ContainsKey(Item) ? tokenLoader.SetA[Item] : 0;
            int countItemInB = tokenLoader.SetB.ContainsKey(Item) ? tokenLoader.SetB[Item] : 0;

            double aProbability = (double)countItemInA / tokenLoader.TotalA;
            double bProbability = (double)countItemInB / tokenLoader.TotalB;

            return aProbability / (aProbability + bProbability);
        }
        private void fillProbabilitiesDict()
        {
            foreach (var item in tokenLoader.SetA)
            {
                Probabilities.Add(item.Key, CalculateProbabilityOfToken(item.Key));
            }

            foreach (var item in tokenLoader.SetB)
            {
                if (!Probabilities.ContainsKey(item.Key))
                {
                    Probabilities.Add(item.Key, CalculateProbabilityOfToken(item.Key));
                }
            }
        }



    }
}