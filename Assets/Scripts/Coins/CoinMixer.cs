using System.Collections.Generic;
using System.Linq;

namespace CoinRandomizer.Coins
{
    public class CoinMixer
    {
        public List<Coin> Mix(IEnumerable<Coin> coins)
        {
            List<Coin> unmixedCoins = coins.ToList();
            List<Coin> mixedCoins = new(unmixedCoins.Count);

            for (int i = unmixedCoins.Count - 1; i >= 0; i--)
            {
                int random = UnityEngine.Random.Range(0, unmixedCoins.Count);

                mixedCoins.Add(unmixedCoins[random]);
                unmixedCoins.RemoveAt(random);
            }

            return mixedCoins;
        }
    }
}