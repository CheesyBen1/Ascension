using UnityEngine;
using UnityEngine.UI;

public class CurrentColor : MonoBehaviour
{
    public Customization customization;

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;

    public Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        customization = GameObject.FindGameObjectWithTag("Customization").GetComponent<Customization>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<RawImage>().color = currentColor;

        switch (customization.color)
        {
            case 1:
                currentColor = color1; 
                return;
            case 2:
                currentColor = color2; 
                return;
            case 3:
                currentColor = color3; 
                return;
            case 4:
                currentColor = color4;
                return;
            case 5:
                currentColor = color5; 
                return;
            case 6:
                currentColor = color6;
                return;
            default:
                currentColor = color1;
                return;
        }
    }
}
