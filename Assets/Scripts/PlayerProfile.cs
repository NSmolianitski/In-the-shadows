namespace InTheShadows
{
    public class PlayerProfile
    {
        public string Name { get; set; }
        public int LastUnlockedLevel { get; set; }

        public PlayerProfile(string name = "Player", int lastUnlockedLevel = 0)
        {
            Name = name;
            LastUnlockedLevel = lastUnlockedLevel;
        }
    }
}