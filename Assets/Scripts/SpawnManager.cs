using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(nameof(Spawn));
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            _spawners[Random.Range(0, _spawners.Count)].Spawn();
            yield return wait;
        }
    }
}
