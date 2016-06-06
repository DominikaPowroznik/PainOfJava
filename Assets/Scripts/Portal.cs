using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField]
    private GameObject summaryUI;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            summaryUI.SetActive(true);
        }
    }
}
