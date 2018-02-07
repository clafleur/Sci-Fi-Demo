using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null) {
                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(audioClip, transform.position, 1.0f);
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

                    if (uiManager != null)
                    {
                        uiManager.CollectedCoin();
                    }

                    Destroy(this.gameObject);
                }
            }
        }
    }
}
