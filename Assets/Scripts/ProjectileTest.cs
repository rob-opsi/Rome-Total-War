using UnityEngine;
using System.Collections;

public class ProjectileTest : MonoBehaviour {

    public float yOffset;
    public float rotateSpeed;
    public bool fire;
    public float angle; //angles should be between 
    public float force;
    public GameObject prefab;
    private bool rotating;

	// Use this for initialization
	void Start () {
        StartCoroutine(fireProjectile());
        InvokeRepeating("startFiring", 5f, 5f);
	}

    void Update()
    {
        //Debug.DrawRay(this.transform.position, this.transform.forward, Color.cyan);
    }

    private void startFiring()
    {
        StartCoroutine(fireProjectile());
    }

    IEnumerator fireProjectile()
    {
        Debug.Log("Startign to fire a projectiel");
        Vector3 SpawnPoint = new Vector3(this.transform.position.x, this.transform.position.y + yOffset, this.transform.position.z);
        GameObject clone = Instantiate(prefab, SpawnPoint, Quaternion.identity) as GameObject;
        clone.transform.eulerAngles = this.transform.forward;
        yield return new WaitForSeconds(1f);
        //while the angle difference between the absolute values of the current angles are greater than 1 degree
        /*
        while (Mathf.Abs(clone.transform.eulerAngles.x - angle) >= .5f)
        {
            Debug.DrawLine(clone.transform.position, clone.transform.localEulerAngles * 10f, Color.red);
            Debug.Log("Rotating: " + (clone.transform.eulerAngles.x - angle));
            float tmpAngle = Mathf.MoveTowardsAngle(clone.transform.eulerAngles.x, angle, rotateSpeed * Time.deltaTime);
            clone.transform.eulerAngles = new Vector3(tmpAngle, 0, 0);
            yield return null;
        }
        // */


        Debug.Log("Adding a force");
        Vector3 dir = Quaternion.AngleAxis(angle, this.transform.forward) * this.transform.right;
        Rigidbody rigid = clone.GetComponent<Rigidbody>();
        rigid.AddForce(dir * force);

        //StartCoroutine(fireProjectile());
        yield return null;
    }
}
