using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private float _delay;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _delay, _delay);
    }

    private void Spawn()
    {
        _spawners[Random.Range(0, _spawners.Count)].Spawn();
    }
}
