using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public bool fly;
    public bool moveTowardsBread;
    public GameObject Feathers;

    public Vector3 center;
    public Vector3 size;
    public float speed = 2f;

    private bool flightSetUp;
    private Vector3[] path = new Vector3[10];
    int current = 0;
    float waypointRadius = 1;

    private GameObject Bread;

    private float maxSpeed = 30f;
    private float incline = 0.25f;
    private float turnSpeed = 3f;

    Vector3 lastPos; // used to calculate current velocity

    void Start()
    {
        Bread = GameObject.Find("/Bread");
        speed = 2f;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            if (!flightSetUp) { FlightSetUp(); }
            Fly();
        }
        else if(moveTowardsBread)
        {
            transform.position = Vector3.MoveTowards(transform.position, Bread.transform.position, Time.deltaTime * speed);
            FaceTowards();
        }
    }

    void FlightSetUp()
    {
        for (int i = 0; i < 10; i++)
        {
            path[i] =  center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        }
        flightSetUp = true;
    }

    void Fly()
    {
        if (Vector3.Distance(path[current], transform.position) < waypointRadius)
        {
            current++;
            if (current >= path.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, path[current], Time.deltaTime * speed);
        FaceTowards();
    }

    void FaceTowards()
    {
        Vector3 dir = transform.position - lastPos; //calculate displacement per frame
        lastPos = transform.position; //update position
        float dist = dir.magnitude;
        if (dist > 0.001f) //if any movement check
        {
            dir /= dist; //normalise direction
            float vel = dist / Time.deltaTime; //calulate velocity

            Quaternion bankRot = Quaternion.LookRotation(dir + incline * Vector3.down * vel / maxSpeed); //bank in direction of movement based on velocity
            transform.rotation = Quaternion.Slerp(transform.rotation, bankRot, turnSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Destroy bullet too
            Destroy(collision.gameObject);
            //TODO: Increase Score
            CheckForRespawn();
        } 
        else if (collision.gameObject.tag == "Bread")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            moveTowardsBread = false;
            //TODO: Decrease Life
            CheckForRespawn();
        }
    }

    void CheckForRespawn()
    {
        var obj = Instantiate(Feathers, transform.position, transform.rotation);
        obj.GetComponent<ParticleSystem>().Play();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 1)
        {
            Destroy(this.gameObject);
            var script = GameObject.Find("/Spawn_Area_1").GetComponent<SpawnObjects>();
            script.numOfEnemies += (int)Random.Range(4, 8);
            script.InitSpawn();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
