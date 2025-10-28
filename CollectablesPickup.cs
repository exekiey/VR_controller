using UnityEngine;

public class CollectablesPickup : MonoBehaviour
{

    [SerializeField] GameObject player;

    [SerializeField] float speed;

    public void OnPointerEnter()
    {
        foreach (GameObject collectable in Collectables.instance.CollectablesList)
        {
            Debug.Log(collectable.name);
            collectable.SetActive(true);
            Rigidbody rb = collectable.GetComponent<Rigidbody>();
            
            collectable.transform.position = transform.position;

            Vector3 playerDirection = player.transform.position - transform.position;


            if (rb != null)
            {
                rb.AddForce(playerDirection * speed, ForceMode.Impulse);
            }


        }

        Collectables.instance.CollectablesList.Clear();
    }
    public void OnPointerExit()
    {
    }
}
