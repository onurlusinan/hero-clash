using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using HeroClash.PersistentData;

namespace HeroClash.HeroSystem
{
    /// <summary>
    /// Hero Manager handles core hero features like Level Up, SaveLoad, Locking
    /// </summary>
    public class HeroManager : MonoBehaviour
    {
        public static HeroManager Instance;

        [Header("Hero Leveling")]
        public int experiencePerLevel;
        public int battlesPerNewHero;
        public int initialHeroAmount;
        internal int totalBattles;

        internal List<int> selectedHeroes;

        private Dictionary<int, Hero> _heroDict;
        internal List<int> winnerIDs;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            _heroDict = new Dictionary<int, Hero>();
            selectedHeroes = new List<int>();
            winnerIDs = new List<int>();
        }

        public Hero GetHero(int id) => _heroDict[id];

        /// <summary>
        /// Checks hero locks and turns them either on or off based on battles fought
        /// </summary>
        public void CheckLocks()
        {
            PlayerSaveData playerSaveData = SaveSystem.LoadPlayerData();

            if (playerSaveData == null)
                totalBattles = 0;
            else
                totalBattles = playerSaveData.battlesFought;

            int heroesToUnlock = totalBattles / battlesPerNewHero;
            if (heroesToUnlock == 0)
                heroesToUnlock = initialHeroAmount;
            else
                heroesToUnlock += initialHeroAmount;

            List<Hero> lockList = _heroDict.Values.ToList();
            for (int i = 0; i < lockList.Count; i++)
            {
                lockList[i].SetLock(!(i < heroesToUnlock));
                SelectionManager.Instance.GetHeroSelectionCard(lockList[i].GetID()).RefreshHeroCard();
            }
        }

        public void LoadAllHeroes()
        {
            foreach (KeyValuePair<int, Hero> hero in _heroDict)
                hero.Value.LoadHero();

            CheckLocks();
        }

        private void SaveAllHeroes()
        {
            foreach (KeyValuePair<int, Hero> hero in _heroDict)
                hero.Value.SaveHero();
        }

        /// <summary>
        /// Clears the necessary data structures of Hero Manager
        /// Since it's the same singleton all session, needs empty data structures to fill them back
        /// </summary>
        public void ClearHeroManager()
        {
            _heroDict.Clear();
            selectedHeroes.Clear();
            winnerIDs.Clear();
        }

        /// <summary>
        /// Adds hero to the hero dictionary _heroDict
        /// </summary>
        /// <param name="hero"></param>
        public void AddToHeroes(Hero hero)
        {
            _heroDict.Add(hero.GetID(), hero);
        }

        /// <summary>
        /// Levels up the winner heroes
        /// </summary>
        public void LevelUpWinnerHeroes(List<int> winnerIDs)
        {
            this.winnerIDs = winnerIDs;

            foreach (int id in winnerIDs)
                _heroDict[id].LevelUp();

            SaveAllHeroes();
        }
    }
}