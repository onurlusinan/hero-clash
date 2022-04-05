using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using HeroClash.HeroSystem;

namespace HeroClash.PersistentData
{
    /// <summary>
    /// Saves/Loads data with Binary Serialization/Deserialization
    /// </summary>
    public static class SaveSystem
    {
        public static void SaveHero(Hero hero)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/hero" + hero.GetID() + ".save";
            FileStream stream = new FileStream(path, FileMode.Create);

            HeroSaveData data = new HeroSaveData(hero);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static HeroSaveData LoadHero(Hero hero)
        {
            string path = Application.persistentDataPath + "/hero" + hero.GetID() + ".save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                HeroSaveData data = formatter.Deserialize(stream) as HeroSaveData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogWarning("A new hero?! There is no previous save file for hero ");
                SaveHero(hero);
                return null;
            }
        }

        public static void SavePlayerData(int battlesFought)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/totalBattles.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerSaveData data = new PlayerSaveData(battlesFought);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerSaveData LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/totalBattles.save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogWarning("A new player! Welcome to the world of Hero Clash!");
                SavePlayerData(0);
                return null;
            }
        }
    }
}