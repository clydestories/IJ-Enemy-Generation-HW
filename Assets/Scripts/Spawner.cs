using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _target;
    [SerializeField] private int _capacity;
    [SerializeField] private int _maxSize;

    private Target _targetScript;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>
            (
                createFunc: () => CreateFunc(),
                actionOnGet: (enemy) => ActionOnGet(enemy),
                actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
                actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
                collectionCheck: true,
                defaultCapacity: _capacity,
                maxSize: _maxSize
            );
        _targetScript = GetComponentInChildren<Target>();
    }

    private void OnEnable()
    {
        _targetScript.TargetReached.AddListener(Release);
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        enemyRigidbody.velocity = Vector3.zero;
        enemyRigidbody.angularVelocity = Vector3.zero;
        enemy.SetTarget(_target);
        enemy.gameObject.SetActive(true);
    }

    private Enemy CreateFunc()
    {
        Enemy enemy = Instantiate(_prefab, transform.position, Quaternion.identity);
        enemy.SetTarget(_target);
        return enemy;
    }

    public void Spawn()
    {
        _pool.Get();
    }

    public void Release(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private void OnDisable()
    {
        _targetScript.TargetReached.RemoveListener(Release);
    }
}
