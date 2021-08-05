using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{
    public GameObject darkness;

    public int interpolationFramesCount = 45000;
    int elapsedFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        darkness.gameObject.transform.GetComponent<SpriteRenderer>().color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (!darkness.gameObject.transform.GetComponent<SpriteRenderer>().color.Equals(Color.clear))
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            darkness.gameObject.transform.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.black, Color.clear, interpolationRatio);

            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
        }
    }
}
