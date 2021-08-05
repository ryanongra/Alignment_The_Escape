using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public TextMesh interactSign;
    public GameObject player;
    public bool interacted = false;
    public float yoffset = 1.5f;
    public float finalYValue;
    private float finalXValue = 44.5f;
    public int interpolationFramesCount = 45000;
    int elapsedFrames = 0;
    public bool inInteractZone = false;

    public GameObject darkness;

    public Canvas HUD;

    public int nextScene = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && inInteractZone)
        {
            interacted = true;
            finalYValue = player.transform.position.y + yoffset;
            interactSign.gameObject.SetActive(false);
        }

        if (interacted)
        {
            HUD.gameObject.SetActive(false);
            player.transform.position = new Vector3(finalXValue, finalYValue, player.transform.position.z);
            if (!darkness.gameObject.transform.GetComponent<SpriteRenderer>().color.Equals(Color.black))
            {
                float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
                player.transform.localScale = Vector3.Lerp(new Vector3(1, 1), new Vector3(0, 0), interpolationRatio);
                darkness.gameObject.transform.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.clear, Color.black, interpolationRatio);

                elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
            } else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        interactSign.gameObject.SetActive(true);
        inInteractZone = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        interactSign.gameObject.SetActive(false);
        inInteractZone = false;
    }
}
