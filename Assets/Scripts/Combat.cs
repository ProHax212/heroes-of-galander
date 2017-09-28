using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour {

	public int health;
	public Image healthBar;

	private int maxHealth;

	void Start(){
		maxHealth = health;
		if (healthBar != null) {
			healthBar.fillAmount = 1f;
		}
	}

	public void TakeDamage(int damage){
		health -= damage;
		updateHealthBar ();
		if (health <= 0) {
			Death ();
		}
	}

	public void Heal(int amount){
		health += amount;
		health = Mathf.Min (health, maxHealth);
		updateHealthBar ();
	}

	private void updateHealthBar(){
		if (healthBar != null) {
			healthBar.fillAmount = (float)health / (float)maxHealth;
		}
	}

	private void Death(){
		Destroy (this.gameObject);
	}

}
