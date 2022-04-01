using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveHero(Hero hero)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/hero" + hero.GetID() +".save";
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
            Debug.LogWarning("A new hero?! There is no previous save file for hero " + hero.GetID() + ". Saving...");
            SaveHero(hero);
            return null;
        }
    }
}
