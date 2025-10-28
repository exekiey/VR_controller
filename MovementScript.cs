using TMPro;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] LineRenderer pointerLine;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float teleportCounter;
    [SerializeField] int teleportCounterLimit;

    [SerializeField] int numberOfPoints;
    [SerializeField] float landSlope;
    [SerializeField] Vector3 lineOriginOffset;

    [SerializeField] GameObject playerFeet;

    [SerializeField] FadeScript fadeScript;
    [SerializeField] LoadingCircle loadingCircle;

    Vector3 gazePos;

    Camera mainCam;


    private void OnEnable()
    {
        fadeScript.FadeFinished += OnFadeFinished;
    }

    private void Start()
    {
        mainCam = Camera.main;
        textMeshProUGUI.text = "0";
    }

    (float A, float B, float C) GetQuadraticSpline(float x0, float y0, float x1, float y1, float slope0)
    {
        float dx = x1 - x0;
        float B = slope0;
        float C = y0;
        float A = (y1 - C - B * dx) / (dx * dx);
        return (A, B, C);
    }

    bool GetGazePosition(out Vector3 gazePoint)
    {
        int layerMask = LayerMask.GetMask("Teleportable");
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var hit in hits)
        {
            gazePoint = hit.point;
            return true;
        }

        gazePoint = Vector3.zero;
        return false;
    }


    private void Update()
    {
        if (GetGazePosition(out gazePos))
        {
            DrawPointer(gazePos);

            DrawLine(gazePos);
            Debug.Log("gazed");
            teleportCounter += Time.deltaTime;
            if (teleportCounter > teleportCounterLimit)
            {
                Debug.Log("counterStarted");
                teleportCounter = 0;
                loadingCircle.ResetCircle();
                fadeScript.StartFade();
            }
            loadingCircle.UpdateCircle(teleportCounter, teleportCounterLimit);
            textMeshProUGUI.text = teleportCounter.ToString("0.#####");

            Vector3 watchDirection = gazePos - playerFeet.transform.position;

            pointer.transform.forward = watchDirection;

        }
        else
        {
            loadingCircle.ResetCircle();
            teleportCounter = 0;
            pointer.SetActive(false);
            pointerLine.enabled = false;
            textMeshProUGUI.text = "";
        }
    }

    void OnFadeFinished()
    {

        float prevHeight = playerFeet.transform.position.y;

        playerFeet.transform.position = gazePos;

        playerFeet.transform.position = new Vector3(playerFeet.transform.position.x, prevHeight, playerFeet.transform.position.z);
        
    }

    private void DrawLine(Vector3 gazePos)
    {
        Vector3 lineFrom = mainCam.transform.position + lineOriginOffset;
        pointerLine.positionCount = numberOfPoints;

        var (A, B, C) = GetQuadraticSpline(lineFrom.x, lineFrom.y, gazePos.x, gazePos.y, landSlope);

        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = i / (float)(numberOfPoints - 1);
            float x = Mathf.Lerp(lineFrom.x, gazePos.x, t);
            float z = Mathf.Lerp(lineFrom.z, gazePos.z, t);
            float dx = x - lineFrom.x;
            float y = A * dx * dx + B * dx + C;

            pointerLine.SetPosition(i, new Vector3(x, y, z));
        }
    }

    private void DrawPointer(Vector3 gazePos)
    {

        Debug.Log(pointer.name);

        pointer.SetActive(true);
        pointer.transform.position = gazePos;
        pointerLine.enabled = true;
    }

    public void OnPointerEnter() { }
    public void OnPointerExit() { Debug.Log("exit"); }
}
