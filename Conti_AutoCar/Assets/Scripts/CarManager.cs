using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private List<Material> screenMaterial;
    [SerializeField]
    private GameObject gameHead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleGame()
    {
        gameHead.SetActive(!gameHead.activeSelf);
    }
}
