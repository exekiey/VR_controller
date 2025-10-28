using UnityEngine;

public class DebugPlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, Time.deltaTime * 10, 0);
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -Time.deltaTime * 10, 0);
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Time.deltaTime * 10, 0, 0);
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-Time.deltaTime * 10, 0, 0);
        }
    }
}
