using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSpam.classifier
{
    class TokenLoader
    {
        public Dictionary<string, int> SetA { get; set; }
        public Dictionary<string, int> SetB { get; set; }

        private Dictionary<string, Double> Probabilities;

        public double Total  { get; set; }
        public double TotalA { get; set; }
        public double TotalB { get; set; }

        public TokenLoader()
        {
            SetA = new Dictionary<string, int>();
            SetB = new Dictionary<string, int>();
            TotalA = 0;
            TotalB = 0;
            Total = 0;
        }

        public void LoadTokens(List<string> SetATokens, List<string> SetBTokens)
        {
            foreach (string TokenA in SetATokens)

            {
                insertElementInDictionary(SetA, TokenA);
            }

            foreach (string TokenB in SetBTokens)

            {
                insertElementInDictionary(SetB, TokenB);
            }

            foreach (var item in SetA)
            {
                TotalA += item.Value;
            }
            foreach (var item in SetB)
            {
                TotalB += item.Value;
            }
        }
        private void insertElementInDictionary(Dictionary<string, int> dict, string token)
        {
            if (!dict.ContainsKey(token))
            {
                dict.Add(token, 1);
            }
            else
            {
                dict[token] = ++dict[token];
            }
        }
    }

}
