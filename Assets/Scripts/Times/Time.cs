namespace CoinRandomizer.Times
{
    public static class Time
    {
        public const float WRONG_COIN_SELECT_TIME = 0.5f;
        public const float COINBOX_SHAKE_TIME = 1.5f;
        public const float COIN_MOVE_UP_TIME = 0.25f;
        public const float COIN_DROP_TIME = 1f;
        public const float RESTART_TIME = 1f;

        public static int InMilliseconds(this float value) => (int)(value * 1000);
    }
}