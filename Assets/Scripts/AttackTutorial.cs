using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTutorial : MonoBehaviour
{
    public Text jumpTutorial;
    public Text attackTutorial;
    public GameObject speech;
    public GameObject healthbar;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        jumpTutorial.gameObject.SetActive(false);
        attackTutorial.gameObject.SetActive(true);
        speech.gameObject.SetActive(true);
        healthbar.gameObject.SetActive(true);

        Destroy(speech, 1.5f);
    }
}
