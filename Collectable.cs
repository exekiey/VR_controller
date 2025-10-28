using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Collectable : MonoBehaviour
{

    static Camera mainCam;
    Collider collider;
    private bool doShrinking;
    private Vector3 originalSize;
    private Vector3 originalPosition;



    float counter;
    [SerializeField] float timeToShrink = 0.1f;
    private bool alreadyPickedUp;

    private void Start()
    {

        mainCam = Camera.main;
        collider = GetComponent<Collider>();
        originalSize = transform.lossyScale;

    }

    public void OnPointerEnter()
    {
        if (!doShrinking)
        {
            doShrinking = true;
            Collectables.instance.NotifyCollectableGrabbed(this.gameObject);
        }

    }
    public void OnPointerExit()
    {
    }

    private void Update()
    {

        if (!alreadyPickedUp)
        {
            if (doShrinking)
            {
                counter += Time.deltaTime;

                float t = counter / timeToShrink;

                Vector3 currentScale = Vector3.Lerp(originalSize, Vector3.zero, t);

                transform.localScale = currentScale;

                if (t > 1)
                {
                    gameObject.SetActive(false);
                    alreadyPickedUp = true;
                    transform.localScale = originalSize;
                }
            }
        }

    }

}
