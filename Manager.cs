using System.Collections;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class Manager : MonoBehaviour
{
    public const string NINJAS_FILE = "ninjas.json";
    public const string LEVELS_FILE = "stages.json";
    public const int MAX_NINJAS = 25;
    public static int level, xp;
    public static int currentStage;

    public static int karma, gold;
    public static int ninjaCount;
    public static Ninja[] ninjas;
    public static Stage[] stages;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static void SaveData()
    {
        File.WriteAllText(Application.dataPath + "/game_data/" + NINJAS_FILE, JsonConvert.SerializeObject(ninjas));
        //File.WriteAllText(Application.dataPath + "/game_data/" + LEVELS_FILE, JsonConvert.SerializeObject(stages));
    }

    /// <summary>
    /// Add ninja to the dojo.
    /// </summary>
    /// <param name="n">Ninja object to add.</param>
    public static void AddNinja(Ninja n)
    {
        ninjas[ninjaCount++] = n;
    }

    public static void AddXP(int amount) 
    { 
        xp += amount; 
    }

    public static void AddMoney(int amount, bool isGold = true)
    {
        if (isGold)
            gold += amount;
        else
            karma += amount;
    }

    /// <summary>
    /// Reduces money per transaction. Returns true if the payment was successful, false otherwise.
    /// </summary>
    /// <param name="amount">Amount of gold/karma to reduce when purchasing.</param>
    /// <param name="isGold">True for using gold as paying method, false otherwise.</param>
    public static bool BuyItem(int amount, bool isGold = true) 
    {
        if (isGold && gold >= amount)
        {
            gold -= amount;
            return true;
        }

        if (!isGold && karma >= amount)
        {
            karma -= amount;
            return true;
        }

        return false;
    }
}
