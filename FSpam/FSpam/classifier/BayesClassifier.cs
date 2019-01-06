using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace FSpam.classifier
{
    class BayesClassifier
    {
        public void LoadTokens(System.Collections.Generic.List SetATokens, System.Collections.Generic.List SetBTokens)
          {
              foreach (T TokenA in SetATokens)
              {
 119:                 SetA.Add(TokenA);
 120:             }
 121:             foreach (T TokenB in SetBTokens)
 122:             {
 123:                 SetB.Add(TokenB);
 124:             }
 125:             TotalA = 0;
 126:             TotalB = 0;
 127:             foreach (T Token in SetA)
 128:             {
 129:                 TotalA += SetA[Token];
 130:             }
 131:             foreach (T Token in SetB)
 132:             {
 133:                 TotalB += SetB[Token];
 134:             }
 135:             Total = TotalA + TotalB;
 136:             Probabilities = new Dictionary<T, double>();
 137:             foreach (T Token in SetA)
 138:             {
 139:                 Probabilities.Add(Token, CalculateProbabilityOfToken(Token));
 140:             }
 141:             foreach (T Token in SetB)
 142:             {
 143:                 if (!Probabilities.ContainsKey(Token))
 144:                 {
 145:                     Probabilities.Add(Token, CalculateProbabilityOfToken(Token));
 146:                 }
 147:             }
 148:         }
    }
}
