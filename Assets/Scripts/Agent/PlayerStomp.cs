using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStomp : MonoBehaviour
{
    [field: SerializeField] private UnityEvent OnStomp { get; set; }
    private bool stomp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stomp)
            return;

        stomp = true;
        IHitable hitable = collision.GetComponent<IHitable>();
        hitable?.GetHit(1, gameObject);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            OnStomp?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        stomp = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stomp = false;
    }
}
