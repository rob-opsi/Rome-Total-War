using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    public float mSpeed;
    public float interpolation;
    public float mDelta;

    private Vector3 newPos;

	// Use this for initialization
	void Start () {
        newPos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        CheckMousePosition();

        Move();
    }

    /// <summary>
    /// This method is in charge of adjusting the target position to move towards when the mouse touches or gets near to the edge of the screen
    /// </summary>
    private void CheckMousePosition()
    {
        // Move the camera right
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            newPos.x += mSpeed;
        }

        // Move the camera left
        if (Input.mousePosition.x <= 0 + mDelta)
        {
            newPos.x -= mSpeed;
        }

        // Move the camera forward
        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            newPos.z += mSpeed;
        }

        // Move the camera backwards
        if (Input.mousePosition.y <= 0 + mDelta)
        {
            newPos.z -= mSpeed;
        }
    }

    /// <summary>
    /// We move the camera here with lerping it
    /// </summary>
    private void Move()
    {
        //transform.position = Vector3.Lerp(this.transform.position, newPos, interpolation * Time.deltaTime);
        transform.position = Vector3.Lerp(this.transform.position, newPos, interpolation);
    }
}
