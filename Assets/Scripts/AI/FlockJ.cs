using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlockJ : MonoBehaviour
{
    public GameObject SpawnPoints;
    public FlockAgent agentPrefab;
    public FlockAgent groundPrefab;
    public static List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    public int startingCount = 10;
    const float AgentDensity = 0.04f;

    [Range(1f, 100f)]
    public float driveFactor = 10;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.8f;
    public float spawnDelay = 0.5f;
    public float attackDelay = 5f;


    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;

    int spawnCount = 0;
    Vector3[] spawnPoints;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        // Get random spawn point
        var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var newAgent = Instantiate(agentPrefab, pos, Quaternion.identity);
        newAgent.SetCollider();
        newAgent.name = "Flying Duck " + agents.Count() + 1;
        agents.Add(newAgent);
        spawnCount++;
        if (spawnCount < startingCount)
        {
            StartCoroutine(Spawn());
        }
    }

    //private IEnumerator Spawn()
    //{
    //    yield return new WaitForSeconds(spawnDelay);
    //    // Get random spawn point
    //    var randomSpawn = Random.Range(0, 2);

    //    if (randomSpawn == 0)
    //    {
    //        FlyingSpawn();
    //    }
    //    else if (randomSpawn == 1)
    //    {
    //        GroundSpawn();
    //    }

    //}

    //private IEnumerator FlyingSpawn()
    //{
    //    var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
    //    var newAgent = Instantiate(agentPrefab, pos, Quaternion.identity);
    //    newAgent.SetCollider();
    //    newAgent.name = "Flying Duck " + agents.Count() + 1;
    //    agents.Add(newAgent);
    //    spawnCount++;
    //    if (spawnCount < startingCount)
    //    {
    //        StartCoroutine(Spawn());
    //    }
    //    yield return null;
    //}
    //private IEnumerator GroundSpawn()
    //{
    //    var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
    //    var newAgent = Instantiate(groundPrefab, pos, Quaternion.identity);
    //    newAgent.SetCollider();
    //    newAgent.name = "Ground Duck " + agents.Count() + 1;
    //    agents.Add(newAgent);
    //    spawnCount++;
    //    if (spawnCount < startingCount)
    //    {
    //        StartCoroutine(Spawn());
    //    }
    //    yield return null;
    //}

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        if (agents.Count() > 0)
        {
            var randomDuck = agents.FirstOrDefault(x => !x.lockHealthDamage && x.transform.position.y > 6);
            if (randomDuck != null)
            {
                randomDuck.stayInRadius = false;
                randomDuck.allign = false;
                randomDuck.attack = true;
            }
        }
        // set different timing
        attackDelay = Random.Range(1f, 7f);
        StartCoroutine(Attack());
    }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        // Populate spawn points
        var tempList = new List<Vector3>();
        foreach (Transform child in SpawnPoints.transform)
        {
            tempList.Add(child.position);
        }
        spawnPoints = tempList.ToArray(); // convert to array to improve perfomrance
        StartCoroutine(Spawn());
        StartCoroutine(Attack());
    }

    private bool IsCollidingWithOthers(Bounds b)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i].AgentCollider.bounds.Intersects(b))
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector3 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach (var c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    public void NextWave()
    {
        spawnCount = 0;
        startingCount += (int)Random.Range(4, 8);
        StartCoroutine(Spawn());
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class Flock : MonoBehaviour
//{
//    public GameObject SpawnPoints;
//    public FlockAgent agentPrefab;
//    public FlockAgent groundPrefab;
//    public static List<FlockAgent> agents = new List<FlockAgent>();
//    public static List<FlockAgent> agentsGround = new List<FlockAgent>();
//    public FlockBehaviour behaviour;

//    public int startingCount = 10;
//    const float AgentDensity = 0.04f;

//    [Range(1f, 100f)]
//    public float driveFactor = 10;
//    [Range(1f, 100f)]
//    public float maxSpeed = 5f;
//    [Range(1f, 10f)]
//    public float neighbourRadius = 1.5f;
//    [Range(0f, 1f)]
//    public float avoidanceRadiusMultiplier = 0.8f;
//    public float spawnDelay = 0.5f;
//    public float attackDelay = 5f;


//    float squareMaxSpeed;
//    float squareNeighbourRadius;
//    float squareAvoidanceRadius;

//    GameObject[] Enemy;
//    float[] probability = { 0.1f, 0.9f };

//    float[] cumulative;

//    void MakeCumulative()
//    {
//        float current = 0f;
//        int itemCount = probability.Length;

//        for (int i = 0; i <= itemCount; i++)
//        {
//            current += probability[i];
//            cumulative[i] = current;
//        }

//        if (current > 1.0f)
//        {
//            Debug.Log("Probabilty exceeds 100%");
//        }
//    }

//    int spawnCount = 0;
//    Vector3[] spawnPoints;
//    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

//    private IEnumerator Spawn()
//    {
//        yield return new WaitForSeconds(spawnDelay);

//        float rnd = Random.Range(0, 1.0f);
//        int itemCount = cumulative.Length;

//        for (int i = 0; i <= itemCount; i++)
//        {
//            if (rnd <= cumulative[i])
//            {
//                yield return Enemy[i];
//            }
//        }

//        var randomSpawn = Random.Range(0, 2);

//        if (randomSpawn == 0)
//        {
//            StartCoroutine(FlyingSpawn());
//        }
//        else if (randomSpawn == 1)
//        {
//            StartCoroutine(GroundSpawn());
//        }
//    }

//    private IEnumerator FlyingSpawn()
//    {
//        // Get random spawn point
//        var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
//        var newAgent = Instantiate(agentPrefab, pos, Quaternion.identity);
//        newAgent.SetCollider();
//        newAgent.name = "Flying Duck " + agents.Count() + 1;
//        agents.Add(newAgent);
//        spawnCount++;
//        if (spawnCount < startingCount)
//        {
//            StartCoroutine(Spawn());
//        }
//        yield return null;
//    }
//    private IEnumerator GroundSpawn()
//    {
//        // Get random spawn point
//        var pos = spawnPoints[Random.Range(0, spawnPoints.Length)];
//        var newAgent = Instantiate(groundPrefab, pos, Quaternion.identity);
//        newAgent.SetCollider();
//        newAgent.name = "Ground Duck " + agentsGround.Count() + 1;
//        agentsGround.Add(newAgent);
//        spawnCount++;
//        if (spawnCount < startingCount)
//        {
//            StartCoroutine(Spawn());
//        }
//        yield return null;
//    }

//    private IEnumerator Attack()
//    {
//        yield return new WaitForSeconds(attackDelay);
//        if (agents.Count() > 0)
//        {
//            var randomDuck = agents.FirstOrDefault(x => !x.lockHealthDamage && x.transform.position.y > 6);
//            if (randomDuck != null)
//            {
//                randomDuck.stayInRadius = false;
//                randomDuck.allign = false;
//                randomDuck.attack = true;
//            }
//        }
//        // set different timing
//        attackDelay = Random.Range(1f, 7f);
//        StartCoroutine(Attack());
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        squareMaxSpeed = maxSpeed * maxSpeed;
//        squareNeighbourRadius = neighbourRadius * neighbourRadius;
//        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
//        // Populate spawn points
//        var tempList = new List<Vector3>();
//        foreach (Transform child in SpawnPoints.transform)
//        {
//            tempList.Add(child.position);
//        }
//        spawnPoints = tempList.ToArray(); // convert to array to improve perfomrance
//        StartCoroutine(Spawn());
//        StartCoroutine(Attack());
//    }

//    private bool IsCollidingWithOthers(Bounds b)
//    {
//        for (int i = 0; i < agents.Count; i++)
//        {
//            if (agents[i].AgentCollider.bounds.Intersects(b))
//            {
//                return true;
//            }
//        }
//        return false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        foreach (FlockAgent agent in agents)
//        {
//            List<Transform> context = GetNearbyObjects(agent);

//            Vector3 move = behaviour.CalculateMove(agent, context, this);
//            move *= driveFactor;
//            if (move.sqrMagnitude > squareMaxSpeed)
//            {
//                move = move.normalized * maxSpeed;
//            }
//            agent.Move(move);
//        }
//    }

//    private List<Transform> GetNearbyObjects(FlockAgent agent)
//    {
//        List<Transform> context = new List<Transform>();
//        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
//        foreach (var c in contextColliders)
//        {
//            if (c != agent.AgentCollider)
//            {
//                context.Add(c.transform);
//            }
//        }
//        return context;
//    }

//    public void NextWave()
//    {
//        spawnCount = 0;
//        startingCount += (int)Random.Range(4, 8);
//        StartCoroutine(Spawn());
//    }
//}
