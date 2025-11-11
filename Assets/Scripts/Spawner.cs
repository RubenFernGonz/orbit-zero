using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemiesPrefab;

    private bool _canSpawn = true;
    // Start is called before the first frame update
    [SerializeField] private float timeToSpawn = 2f;
    void Start()
    {
        if(enemiesPrefab != null)
        {
            _canSpawn = true;
        } else
        {
            Debug.LogError("Enemies Prefab is not assigned in the Spawner script attached to " + gameObject.name);
            _canSpawn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canSpawn)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    
    private IEnumerator SpawnEnemies()
    {
        _canSpawn = false;
        Instantiate(enemiesPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(timeToSpawn);
        _canSpawn = true;
    }
}
