using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    [Range(0.1f,120f)]
    [SerializeField] float secondsBetweenSpawns = 3f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnedEnemies;
    [SerializeField] AudioClip spawnEnemySFX;
    int score = 0;

    // Use this for initialization
    void Start () {
        StartCoroutine(RepeatedSpawnEnemies());
        spawnedEnemies.text = score.ToString();
	}
	

    IEnumerator RepeatedSpawnEnemies()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);

            score++;
            spawnedEnemies.text = score.ToString();
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
