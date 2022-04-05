using HeroClash.HeroSystem;

namespace HeroClash.PersistentData
{
    /// <summary>
    /// Class for saving the heroes, gets serialized / unserialized by the binary formatter
    /// </summary>
    [System.Serializable]
    public class HeroSaveData
    {
        public int id;
        public string heroName;

        public int experience;

        public float health;
        public float attackPower;

        public bool isLocked;

        public HeroSaveData(Hero hero)
        {
            id = hero.GetID();
            heroName = hero.GetName();
            experience = hero.GetExperience();
            health = hero.health;
            attackPower = hero.attackPower;
            isLocked = hero.IsLocked();
        }
    }
}