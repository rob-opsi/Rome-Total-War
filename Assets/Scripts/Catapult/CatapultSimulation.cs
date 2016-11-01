using UnityEngine;
using System.Collections;

public class CatapultSimulation : MonoBehaviour {
    public Transform Target;
    public float timeBetweenShots;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public float moveSpeed;
    public float rotateSpeed;
    public float maxAngle;
    public float maxDist;

    public Transform Projectile;
    private Transform myTransform;

    public float timer;
    private float curMoveSpeed;
    private float curRotateSpeed;

    void Awake()
    {
        curMoveSpeed = moveSpeed;
        curRotateSpeed = rotateSpeed;

        myTransform = transform;
        timer = timeBetweenShots;
    }

    void Start()
    {
        //StartCoroutine(SimulateProjectile());
    }

    void Update()
    {
        float dist = Vector3.Distance(myTransform.position, Target.transform.position);

        if (!Rotate() || dist > maxDist)
        {
            
            //Debug.Log(string.Format("angle: {0}dist: {1}", angle, dist));
            Rotate();
            if(dist > maxDist)
            {
                Move();
            }
        }else {
            Debug.Log("ready to shoot");
            Shoot();
        }
    }

    private void Shoot()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = timeBetweenShots;
            StartCoroutine(SimulateProjectile());
        }
    }

    private bool Rotate()
    {
        //Debug.DrawLine(this.transform.position, Target.transform.position, Color.red);
        //Debug.DrawLine(this.transform.position, (this.transform.position + this.transform.forward * 10f), Color.cyan);

        Vector3 targetDir = (new Vector3(Target.position.x, 0, Target.position.z) - new Vector3(this.transform.position.x, 0, this.transform.position.z)).normalized;
        float step = curRotateSpeed * Time.deltaTime;
        Quaternion lookRot = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, lookRot, step);

        float angle = Quaternion.Angle(lookRot, this.transform.rotation);

        if (angle <= maxAngle)
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        myTransform.position = Vector3.MoveTowards(this.transform.position, Target.transform.position, curMoveSpeed * Time.deltaTime);
    }


    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
