using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {

    public float timer;
	// Use this for initialization
	void Start () {
        StartCoroutine(destroyInSeconds());
	}
	
    IEnumerator destroyInSeconds()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
