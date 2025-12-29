using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float speed = 3f;
    // Set in Unity to be the Transform of the GameObject
    // that this CameraRig is following
    // Updates in real time
    public Transform follow;

    // Update is called once per frame
    // var transform is the Transform of this CameraRig
    // accessed from Unity whenever used
    void Update()
    {
        if (follow)
            transform.position =
                Vector3.Lerp(
                    transform.position, follow.position,
                    speed*Time.deltaTime
                );
    }
}
