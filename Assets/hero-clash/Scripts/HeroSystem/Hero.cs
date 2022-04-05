using UnityEngine;

using HeroClash.PersistentData;

namespace HeroClash.HeroSystem
{
    /// <summary>
    /// The main hero class
    /// </summary>
    public class Hero
    {
        #region VARIABLES & REFS

        [Header("Attributes")]
        [SerializeField] private int _heroID;
        [SerializeField] private string characterName;
        public float health;
        public float attackPower;

        [Header("Hero Experience-Related")]
        public int level;
        [SerializeField] private int _experience;
        [SerializeField] private bool _isLocked;

        [Header("Sprites")]
        internal Sprite defaultAvatarSprite;

        #endregion

        #region GETTERS - SETTERS
        public int GetID() => _heroID;
        public string GetName() => characterName;
        public int GetExperience() => _experience;
        public bool IsLocked() => _isLocked;
        public void SetID(int id) => _heroID = id;
        public void SetHeroName(string name) => characterName = name;
        public void SetExperience(int experience)
        {
            _experience = experience;
            level = CalculateLevel(_experience);
        }
        public void SetLock(bool locked) => _isLocked = locked;

        #endregion

        #region LEVELING 
        /// <summary>
        /// Calculates the user level through experience
        /// </summary>
        private int CalculateLevel(int experience)
        {
            level = experience / HeroManager.Instance.experiencePerLevel;
            return level + 1;
        }

        /// <summary>
        /// Main level up (exp gain) method
        /// </summary>
        public void LevelUp()
        {
            _experience = _experience + 1;

            int previousLevel = level;
            int newLevel = CalculateLevel(_experience);

            if (newLevel > previousLevel)
            {
                health = health * 1.1f;
                attackPower = attackPower * 1.1f;
                SaveHero();

                Debug.Log(characterName + " has leveled up! " + previousLevel + " -> " + newLevel);
            }

            level = newLevel;
        }
        #endregion

        #region SAVE-SYSTEM

        /// <summary>
        /// Saves the hero
        /// </summary>
        public void SaveHero()
        {
            SaveSystem.SaveHero(this);
        }

        /// <summary>
        /// Loads the hero
        /// </summary>
        public void LoadHero()
        {
            HeroSaveData data = SaveSystem.LoadHero(this);

            if (data == null)
                return;

            SetID(data.id);
            SetHeroName(data.heroName);
            SetExperience(data.experience);
            health = data.health;
            attackPower = data.attackPower;
            SetLock(data.isLocked);


        }

        public void LoadBaseData(HeroBaseData baseData)
        {
            SetID(baseData.GetID());
            SetHeroName(baseData.GetHeroName());

            health = baseData.GetHealth();
            attackPower = baseData.GetAttackPower();
            SetExperience(0);

            defaultAvatarSprite = baseData.GetDefaultAvatar();
        }

        #endregion
    }
}