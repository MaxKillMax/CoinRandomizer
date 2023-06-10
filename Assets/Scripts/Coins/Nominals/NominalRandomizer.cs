using System.Collections.Generic;
using System.Linq;

namespace CoinRandomizer.Coins.Nominals
{
    public class NominalRandomizer
    {
        public List<Nominal> GeUniqueRandomNominals(IEnumerable<Nominal> nominals, int randomCount)
        {
            if (randomCount > nominals.Count())
                randomCount = nominals.Count();
            else if (randomCount < 0)
                return new();

            List<Nominal> allNominal = nominals.ToList();
            List<Nominal> randomNominals = new(randomCount);

            for (int i = 0; i < randomCount; i++)
            {
                int random = UnityEngine.Random.Range(0, allNominal.Count);

                randomNominals.Add(allNominal[random]);
                allNominal.RemoveAt(random);
            }

            return randomNominals;
        }
    }
}