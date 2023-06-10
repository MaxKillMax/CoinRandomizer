namespace CoinRandomizer.Characters
{
    public class Character
    {
        public Skeleton skeleton { get; set; }
        public Bone[] bones { get; set; }
        public Slot[] slots { get; set; }
        public Ik2[] ik { get; set; }
        public Skin[] skins { get; set; }
        public Animations animations { get; set; }
    }
}