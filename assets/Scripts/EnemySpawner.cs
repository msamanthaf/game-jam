using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemies;
    public float initialSpawnInterval = 5f;
    public float spawnCurveBase = 1.01f;
    public float spawnCurveExp = 150f;
    public float spawnRadius = 10f;

    [Header("Change this to change the initial grace period.")]
    public float spawnTimer = 10;

    private float totalTimeElapsed = 0f;

    private void Update()
    {
        totalTimeElapsed += Time.deltaTime;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = Mathf.Pow(spawnCurveBase, -(totalTimeElapsed - spawnCurveExp));
        }
    }

    private void SpawnEnemy()
    {
        Enemy e = enemies[Random.Range(0, enemies.Length)];
        Instantiate(e.gameObject, GetUnitOnCircle(Random.Range(0f, Mathf.PI * 2f), spawnRadius), Quaternion.identity, transform);
    }

    private Vector2 GetUnitOnCircle(float angle, float radius)
    {
        // initialize calculation variables
        float _x = 0;
        float _y = 0;
        float angleRadians = 0;
        Vector2 _returnVector;

        // convert degrees to radians
        angleRadians = angle;

        // get the 2D dimensional coordinates
        _x = radius * Mathf.Cos(angleRadians);
        _y = radius * Mathf.Sin(angleRadians);

        // derive the 2D vector
        _returnVector = new Vector2(_x, _y);

        // return the vector info
        return _returnVector;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
