using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        LoadData();
    }

    void Update()
    {
        int maxHealth = Manager.ninjas.Sum(x => x.maxHealth);
        int currentHealth = Manager.ninjas.Sum(x => x.health);
        healthBar.value = (float)currentHealth / maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
    }

    /// <summary>
    /// Load all data from JSON files: ninjas.json, levels.json
    /// </summary>
    void LoadData()
    {
        if (!Directory.Exists(Application.dataPath + "/game_data/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/game_data/");
            Manager.ninjas = null;
            Manager.stages = null;
        }
        else
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/game_data/" + Manager.NINJAS_FILE);
            Manager.ninjas = JsonConvert.DeserializeObject<Ninja[]>(jsonString);

            jsonString = File.ReadAllText(Application.dataPath + "/game_data/" + Manager.LEVELS_FILE);
            Manager.stages = JsonConvert.DeserializeObject<Stage[]>(jsonString);
        }
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Heal ninjas whenever they are injured.
    /// </summary>
    /// <param name="interval">Timer interval for healing (heal all players every X seconds)</param>
    public void HealAll(float interval)
    {
        StartCoroutine(ActualHeal(interval));
    }

    IEnumerator ActualHeal(float interval)
    {
        bool notFullHealthExists = true;
        while (notFullHealthExists)
        {
            yield return new WaitForSeconds(interval);

            Manager.ninjas = Manager.ninjas.Select(x => {
                if(!x.HasFullHealth())
                    x.health++; 
                return x;
            }).ToArray();

            notFullHealthExists = (Manager.ninjas.Any(x => !x.HasFullHealth()));
        }
    }
}
