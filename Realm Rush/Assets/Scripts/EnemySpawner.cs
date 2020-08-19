using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f,120f)] [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;

    [SerializeField] int scorePerEnemySpawned = 5;
    [SerializeField] Text scoreText;

    [SerializeField] AudioClip spawnedEnemySfx;

    int totalScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());
    }

    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySfx);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform.transform;
            UpdateScore();

            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void UpdateScore()
    {
        totalScore += scorePerEnemySpawned;
        scoreText.text = totalScore.ToString();
    }
}
