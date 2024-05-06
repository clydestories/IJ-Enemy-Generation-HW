using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _pointsContainer;

    private List<Transform> _points;
    private Transform _patrolPoint;
    private int _patrolPointIndex;

    public UnityEvent<Enemy> TargetReached;

    private void Awake()
    {
        _points = _pointsContainer.GetComponentsInChildren<Transform>().ToList();
    }

    private void Start()
    {
        _patrolPointIndex = 0;
        _patrolPoint = _points[_patrolPointIndex];
    }

    private void Update()
    {
        if (transform.position == _patrolPoint.position)
        {
            ChooseNextPartolPoint();
        }

        Patrol();
    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, _patrolPoint.position, _speed * Time.deltaTime);
    }

    private void ChooseNextPartolPoint()
    {
        _patrolPointIndex = ++_patrolPointIndex % _points.Count;
        _patrolPoint = _points[_patrolPointIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        Debug.Log("qwe");

        if (enemy != null)
        {
            TargetReached?.Invoke(enemy);
        }
    }
}
