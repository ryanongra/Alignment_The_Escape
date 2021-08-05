using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpTutorialTrigger : MonoBehaviour
{

    public Text movementTutorial;
    public Text jumpTutorial;
    public GameObject speech;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        movementTutorial.gameObject.SetActive(false);
        jumpTutorial.gameObject.SetActive(true);
        speech.gameObject.SetActive(true);
        Destroy(speech, 1.5f);
    }
}
