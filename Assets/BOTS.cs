using System.Collections.Generic;
using UnityEngine;


public enum NodeState { Running, Success, Failure }

public abstract class Node {
    protected NodeState _state;
    public NodeState State => _state;
    public abstract NodeState Evaluate();
}

public class Sequence : Node {
    private List<Node> _children;

    public Sequence(List<Node> children) {
        _children = children;
    }

    public override NodeState Evaluate() {
        bool anyRunning = false;

        foreach (var child in _children) {
            var result = child.Evaluate();
            if (result == NodeState.Failure) {
                _state = NodeState.Failure;
                return _state;
            }
            if (result == NodeState.Running) {
                anyRunning = true;
            }
        }

        _state = anyRunning ? NodeState.Running : NodeState.Success;
        return _state;
    }
}

public class Selector : Node {
    private List<Node> _children;

    public Selector(List<Node> children) {
        _children = children;
    }

    public override NodeState Evaluate() {
        foreach (var child in _children) {
            var result = child.Evaluate();
            if (result == NodeState.Success) {
                _state = NodeState.Success;
                return _state;
            }
            if (result == NodeState.Running) {
                _state = NodeState.Running;
                return _state;
            }
        }
        _state = NodeState.Failure;
        return _state;
    }
}

public class ActionNode : Node {
    public delegate NodeState ActionNodeDelegate();

    private ActionNodeDelegate _action;

    public ActionNode(ActionNodeDelegate action) {
        _action = action;
    }

    public override NodeState Evaluate() {
        return _action();
    }
}


public class BotAI : MonoBehaviour {
    private Node _root;

    [Header("Target Settings")]
    public Transform target;

    [Header("Vision Settings")]
    public float viewDistance = 10f;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float wanderRadius = 5f;
    public float wanderInterval = 3f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;

    private Vector2 _wanderTarget;
    private float _wanderTimer;

    void Start() {
        PickNewWanderTarget();
        _root = SetupTree();
    }

    void Update() {
        _root.Evaluate();
    }

    private Node SetupTree() {
        var detectEnemy = new ActionNode(DetectEnemy);
        var shootEnemy = new ActionNode(ShootEnemy);
        var patrol = new ActionNode(Patrol);

        var attackSequence = new Sequence(new List<Node> { detectEnemy, shootEnemy });
        var rootSelector = new Selector(new List<Node> { attackSequence, patrol });

        return rootSelector;
    }

    private NodeState DetectEnemy() {
        if (target == null) return NodeState.Failure;

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= viewDistance) {
            Debug.Log("Enemy detected!");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    private NodeState ShootEnemy() {
        if (target == null || bulletPrefab == null || bulletSpawnPoint == null)
            return NodeState.Failure;

        Vector2 direction = (target.position - bulletSpawnPoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.linearVelocity = direction * bulletSpeed;
        }

        Debug.Log("Shooting at enemy!");
        return NodeState.Success;
    }

    private NodeState Patrol() {
        _wanderTimer += Time.deltaTime;

        if (_wanderTimer >= wanderInterval || Mathf.Abs(transform.position.x - _wanderTarget.x) < 0.1f) {
            PickNewWanderTarget();
            _wanderTimer = 0f;
        }

        Vector2 targetPosition = new Vector2(_wanderTarget.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (_wanderTarget.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        Debug.Log("Wandering...");
        return NodeState.Running;
    }

    private void PickNewWanderTarget() {
        float randomOffset = Random.Range(-wanderRadius, wanderRadius);
        _wanderTarget = new Vector2(transform.position.x + randomOffset, transform.position.y);
        Debug.Log("New wander target: " + _wanderTarget);
    }
}