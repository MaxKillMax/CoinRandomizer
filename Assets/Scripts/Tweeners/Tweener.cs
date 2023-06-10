using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CoinRandomizer.Tweeners
{
    public class Tweener
    {
        public async Task ShakeAsync(Transform transform, float time)
        {
            await TryStop(transform);
            transform.DOShakePosition(time, 1, 15);
            await Task.Delay(TimeInMilliseconds(time));
        }

        public async void Jump(Transform transform, float time)
        {
            await TryStop(transform);
            transform.DOJump(transform.position, 1, 2, time);
            await Task.Delay(TimeInMilliseconds(time));
        }

        private async Task TryStop(Transform transform)
        {
            if (DOTween.IsTweening(transform))
            {
                DOTween.Complete(transform);
                await Task.Yield();
            }
        }

        private int TimeInMilliseconds(float value) => (int)(value * 1000);
    }
}