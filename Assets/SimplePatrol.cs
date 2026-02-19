using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    public float speed = 5.0f;
    private bool moveForward = true;
    private float timer=0.0f;
    private float switchDirectionTime=5.0f;
    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;
        if(timer == switchDirectionTime)
        {
            moveForward = !moveForward;
            timer=0.0f;
        }
        if (moveForward)
        {
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back*speed*Time.deltaTime);
        }
    }
}
