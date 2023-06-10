using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CoinRandomizer.Droppers
{
    public class Dropper
    {
        private Vector3 _origin;
        private Vector3 _destination;

        private List<Transform> _drops;

        public Dropper(Vector3 origin, Vector3 destination)
        {
            _origin = origin;
            _destination = destination;
        }

        public async Task DropAsync<T>(IEnumerable<T> drops, float dropTime) where T : Component
        {
            _drops = drops.Select((c) => c.transform).ToList();

            for (int i = 0; i < _drops.Count; i++)
            {
                _drops[i].position = _origin;
                _drops[i].gameObject.SetActive(true);

                _drops[i].DOMove(_destination, dropTime);

                await Task.Delay((int)(dropTime * 1000));
            }
        }
    }
}