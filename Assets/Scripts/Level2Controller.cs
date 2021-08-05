using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public GameObject levelDialog;
    public int enemiesRemaining = 6;

    // Start is called before the first frame update
    void Start()
    {
        levelDialog.gameObject.SetActive(true);
        Destroy(levelDialog, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
