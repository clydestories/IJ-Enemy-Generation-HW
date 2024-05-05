using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private float _delay;
    [SerializeField] private List<Transform> _spawnpoints;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _delay, _delay);
    }

    private void Spawn()
    {
        Vector3 spawnPosition = _spawnpoints[Random.Range(0, _spawnpoints.Count)].position;
        Quaternion direction = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Instantiate(_prefab, spawnPosition, Quaternion.identity).transform.rotation = direction;
    }
}
