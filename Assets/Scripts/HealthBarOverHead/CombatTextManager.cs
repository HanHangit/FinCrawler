using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour {

    public float speed;
    public Vector3 direction;
    public float fadeTime;
    public static CombatTextManager instance;
    public GameObject textPrefab;
    public RectTransform canvasTransform;

    public static CombatTextManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }
    }

    public void CreateText(Vector3 position, string text, Color color, bool crit)
    {
        //GameObject Casten damit wir es als Chil für das Canvas speichern können
        GameObject sct = (GameObject) Instantiate(textPrefab, position, Quaternion.identity);

        
        sct.transform.SetParent(canvasTransform);                                               //Als Chil vom Canvas machen

        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);                    //Verändert den ScaleWert
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime, crit);            //Ruft die Funktion in ComBatText auf! -->
        sct.GetComponent<Text>().text = text;                                                   //Legt den Text fest
        sct.GetComponent<Text>().color = color;                                                 //Legt die FARBE fest

    }




}
