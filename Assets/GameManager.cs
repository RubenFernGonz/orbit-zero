using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform miguel;
    [SerializeField]
    GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(miguel.position.y);
        if (miguel.position.y <= -6)
        {
            text.SetActive(true) ;
        }
    }
}
