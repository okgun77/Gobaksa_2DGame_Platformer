using UnityEngine;

public class PropsSign : MonoBehaviour
{
    [SerializeField]
    private GameObject guideObject;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            guideObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            guideObject.SetActive(false);
        }
    }
}
