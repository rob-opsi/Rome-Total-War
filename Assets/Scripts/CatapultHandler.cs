using UnityEngine;
using System.Collections;

/// <summary>
/// This class handles the catapult calculations for trajectories
/// </summary>
public class CatapultHandler : MonoBehaviour {

    private Vector3 angleBase;
    public GameObject target;
    public GameObject projectilePrefab;
    public GameObject catapultRestPosition;

	// Use this for initialization
	void Start () {
        angleBase = catapultRestPosition.transform.position;
        angleBase.y += 100f;
        Debug.Log(angleBase);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTarget(GameObject target)
    {

    }

    void OnReleaseProjectile(GameObject projectile)
    {
        //
        Vector3 newDir = this.transform.position - target.transform.position;
        //projectile.transform.rotation = Quaternion.RotateTowards(newDir);
    }


}
