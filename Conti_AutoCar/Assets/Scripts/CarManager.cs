using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer displayMeshRenderer, leftMeshRenderer, rightMeshRenderer;
    [SerializeField]
    private ToggleGroup themeChoice;
    private int themeNumber;
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
    public void SaveTheme()
    {
        foreach (var toggle in themeChoice.ActiveToggles())
        {
            if (toggle.isOn)
            {
                switch (toggle.name)
                {
                    case "sand":
                        themeNumber = 0;
                        break;
                    case "futuristic":
                        themeNumber = 1;
                        break;
                    case "honeycomb":
                        themeNumber = 2;
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }
    public void SetTheme()
    {
        displayMeshRenderer.material = screenMaterial[themeNumber];
    }
}
