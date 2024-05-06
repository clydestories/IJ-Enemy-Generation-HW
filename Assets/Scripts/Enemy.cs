using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.LookAt(_target);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
