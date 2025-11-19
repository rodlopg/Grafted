using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCLogic : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    // Booleanos de estado
    [SerializeField] private TMP_Text tipBubble;
    [SerializeField] private GameObject interactionIcon;

    // Posibles comentarios o tips que puede hacer
    [SerializeField] private string tip;
    [SerializeField] private bool canInteract;

    private void Start()
    {
        tipBubble.gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tipBubble.text = tip;

        /*
            if (canInteract)
            {
                interactionIcon.SetActive(false);
                tipBubble.gameObject.SetActive(true);
            }
            else
            {
                tipBubble.gameObject.SetActive(true);
            }
        */

        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            tipBubble.gameObject.SetActive(true);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        tipBubble.gameObject.SetActive(false);
        interactionIcon.SetActive(false);
        
    }

}