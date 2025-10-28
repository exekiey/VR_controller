using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class LoadingCircle : MonoBehaviour
{


    [SerializeField] float radius;
    [SerializeField] int numberOfPoints;

    [SerializeField] LineRenderer lineRenderer;

    Vector3[] circlePoints;

    int circleCompletionCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        

        circlePoints = new Vector3[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {

            float iLerped = i / (float) (numberOfPoints - 1);

            float angle = Mathf.Lerp(0, Mathf.PI * 2, iLerped);

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;


            circlePoints[i] = new Vector3 (x, y, 0);

        }

    }

    public void UpdateCircle(float currentCount, int counterLimit)
    {

        float lerpedCounter = currentCount / (float)counterLimit;

        Debug.Log(lerpedCounter);

        int progress = (int) Mathf.Lerp(0, numberOfPoints, lerpedCounter);

        if (progress > circleCompletionCount)
        {
            AddNewPoint();
        }

    }
    public void ResetCircle()
    {
        circleCompletionCount = 0;
        lineRenderer.positionCount = 0;
    }
    void AddNewPoint()
    {
        lineRenderer.positionCount = circleCompletionCount;

        lineRenderer.SetPosition(circleCompletionCount - 1, circlePoints[circleCompletionCount]);
        circleCompletionCount++;
    }
}
