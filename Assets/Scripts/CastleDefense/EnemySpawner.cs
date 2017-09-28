using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[System.Serializable]
	public struct EnemyAndDifficulty{
		public GameObject enemy;
		public int difficulty;
	}

	public List<Transform> spawnPoints = new List<Transform>();
	public Dictionary<int, List<GameObject>> enemiesDictionary = new Dictionary<int, List<GameObject>>();	// Maps difficulty to list of enemies
	public List<EnemyAndDifficulty> enemies = new List<EnemyAndDifficulty>();
	public float spawnRate;
	public float spawnNumber;
	public int maxDifficulty;
	public float difficultyIncreaseRate;

	private int difficulty;

	// Use this for initialization
	void Start () {
		difficulty = 0;	// Start difficulty at 0

		// Make the dictionary
		foreach (EnemyAndDifficulty ead in enemies) {
			// Create the list if null
			if (!enemiesDictionary.ContainsKey(ead.difficulty)) {
				enemiesDictionary.Add(ead.difficulty, new List<GameObject>());
			}

			enemiesDictionary[ead.difficulty].Add(ead.enemy);
		}

		StartCoroutine(SpawnEnemies());
		StartCoroutine (IncreaseDifficulty ());
	}

	// Spawn enemies at the various spawn locations
	private IEnumerator SpawnEnemies(){
		while (true) {
			// Spawn 'spawnNumber' number of enemies
			for (int i = 0; i < spawnNumber; i++) {
				Transform spawnPoint = getSpawnLocation ();
				GameObject enemy = getEnemy ();
				if (spawnPoint == null || enemy == null) {
					Debug.Log ("Problem spawning enemy: Enemy or spawn point is null");
					continue;
				}

				// Spawn the enemy
				Instantiate (enemy, spawnPoint.position, Quaternion.identity);
			}

			yield return new WaitForSeconds (spawnRate);
		}
	}

	// Increase the difficulty of the enemies that are spawned
	private IEnumerator IncreaseDifficulty(){
		while (true) {
			difficulty += 1;
			difficulty = Mathf.Min (difficulty, maxDifficulty);

			yield return new WaitForSeconds (difficultyIncreaseRate);
		}
	}

	// Get a random enemy based on the difficulty
	private GameObject getEnemy(){
		int currentDifficulty = Random.Range (0, difficulty + 1);
		List<GameObject> enemiesToChooseFrom = enemiesDictionary [currentDifficulty];
		return enemiesToChooseFrom [Random.Range (0, enemiesToChooseFrom.Count)];
	}

	// Get a random spawn location for the new enemy
	private Transform getSpawnLocation(){
		return spawnPoints [Random.Range (0, spawnPoints.Count)];
	}

}
