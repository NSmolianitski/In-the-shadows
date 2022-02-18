namespace InTheShadows
{
    public class LevelData
    {
        public int Index { get; }
        public bool IsSolved { get; set; }

        public LevelData(int index, bool isSolved)
        {
            Index = index;
            IsSolved = isSolved;
        }
    }
}