using System.Collections;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Battle : MonoBehaviour
{
    private Stage stage;
    private UnityEvent onDeath;

    void Start()
    {
        stage = Manager.stages[Manager.currentStage];

        onDeath = new UnityEvent();
        onDeath.AddListener(OnNinjaDeath);
        onDeath.Invoke();
    }

    /// <summary>
    /// Triggered when ninja dies.
    /// </summary>
    public void OnNinjaDeath()
    {
        Ninja randomValidPlayer = Manager.ninjas.Where(x => x.ValidForBattle()).Select(x => x).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        Ninja randomValidEnemy = stage.ninjas.Where(x => x.ValidForBattle()).Select(x => x).OrderBy(x => Guid.NewGuid()).FirstOrDefault();

        if (randomValidPlayer == null)
        {
            EndBattle(false);
            return;
        }
        else if (randomValidEnemy == null)
        {
            EndBattle(true);
            return;
        }

        randomValidPlayer.inFight = true;
        randomValidEnemy.inFight = true;
    }

    /// <summary>
    /// Damage ninjas during battle.
    /// </summary>
    /// <param name="attacker">Attacker's ninja object</param>
    /// <param name="victim">Victim's ninja object</param>
    public void ReceiveDamage(Ninja attacker, Ninja victim)
    {
        int amount = attacker.power;
        if (UnityEngine.Random.Range(1, 101) <= attacker.critChance)
            amount *= 2;

        victim.health -= amount;
        if (victim.health <= 0)
        {
            victim.health = 0;
            victim.inFight = false;
            onDeath.Invoke();
        }
    }

    /// <summary>
    /// Ends the battle, reward the player in case he's the winner.
    /// </summary>
    public void EndBattle(bool playerWon)
    {
        if(playerWon) // Player is the winner
        {
            Manager.AddMoney(stage.gold, true);
            Manager.AddMoney(stage.karma, false);
            Manager.AddXP(stage.xp);
        }
    }
}

public class Stage
{
    public Ninja[] ninjas;
    public int gold, karma, xp;
}