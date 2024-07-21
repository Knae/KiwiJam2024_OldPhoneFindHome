using TMPro;
using UnityEngine;

public class logEntry : MonoBehaviour
{
    public PopupHint hint;
    public bool randomizedName = true;
    public bool randomizedIcon = true;
    public string[] logNames = { "Mark", "bob", "Asshat" };
    public string[] descriptions = { "Mark's Description", "bob's description", "I don't like this guy"};
    public TMP_Text text;
    public GameObject callIcon;

    // Start is called before the first frame update
    void Start()
    {
        if (randomizedName)
        {
            int randomNumber = Random.Range(0, logNames.Length);
            text.text = logNames[randomNumber];
            hint._hintText = descriptions[randomNumber];
        }

        if (randomizedIcon) 
        {
            int randomnumber2 = Random.Range(0, 2);
            callIcon.SetActive(randomnumber2 == 1);
        }
    }

}
