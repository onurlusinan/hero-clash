namespace HeroClash.PersistentData
{
    /// <summary>
    /// Class for saving any player data, gets serialized / unserialized by the binary formatter
    /// </summary>
    [System.Serializable]
    public class PlayerSaveData
    {
        public int battlesFought;

        public PlayerSaveData(int battlesFought)
        {
            this.battlesFought = battlesFought;
        }
    }
}