using TMPro;
using UnityEngine;

public class logEntry : MonoBehaviour
{
    public bool randomizedName = true;
    public string[] logNames = { "Mark", "bob" };
    public TMP_Text text;
    public GameObject callIcon;

    // Start is called before the first frame update
    void Start()
    {
        if (randomizedName)
        {
            int randomNumber = Random.Range(0, logNames.Length);
            text.text = logNames[randomNumber];

            int randomnumber2 = Random.Range(0, 2);
            callIcon.SetActive(randomnumber2 == 1);
        }
    }

}
