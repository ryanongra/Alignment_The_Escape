using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractTutorial : MonoBehaviour
{
    public Text attackTutorial;
    public Text interactTutorial;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        attackTutorial.gameObject.SetActive(false);
        interactTutorial.gameObject.SetActive(true);
    }
}
