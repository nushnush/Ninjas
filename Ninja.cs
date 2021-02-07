using System.Collections.Generic;
using UnityEngine;

public class Ninja
{
	public Transform healthBar;
	public int power;
	public int health;
	public int maxHealth;
	public int critChance;
	public int upgradeCost;
	public bool inFight;
	public Belt belt;

	public Ninja(int power, int health)
	{
		this.belt = Belt.White;
		this.power = power;
		this.health = health;
		this.maxHealth = health;
		this.critChance = 3;
		this.upgradeCost = 1;
		this.inFight = false;
	}

	public void Upgrade()
	{
		if(!Manager.BuyItem(upgradeCost, false))
			return;

		belt++;
		maxHealth += 50 * (1 + ((int)belt / 5));
		power += (1 + ((int)belt / 5));

		if (System.Enum.IsDefined(typeof(Belt), belt)) // New belt achieved
		{
			upgradeCost++;
			critChance += 2;
		}

		// Manager.Heal();
	}

	public bool ValidForBattle() => (!inFight && health > 0);
	
	/// <summary>
    /// Indicates whether the ninja has full health or not.
    /// </summary>
	public bool HasFullHealth() => (health >= maxHealth);
	
	/// <summary>
    /// Apply damage to victim from the attacker.
    /// </summary>
    /// <param name="amount">Damage amount</param>
	/// <param name="critChance">The attacker's crit chance</param>
}

public enum Belt
{
	White,
	Yellow = 3,
	Orange = 6,
	Green = 10,
	Blue = 15,
	Red = 20,
	Purple = 25,
	Brown = 30,
	Black = 40
};
