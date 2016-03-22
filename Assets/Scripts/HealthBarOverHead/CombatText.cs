using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

    float speed;
    Vector3 direction;
    float fadeTime;
    public AnimationClip critAnim;
    private bool crit;


	
	// Update is called once per frame
	void Update () {
        if (!crit)
        {
            float translation = speed * Time.deltaTime;

            transform.Translate(direction * translation);
        }
        
	}

    public void Initialize(float speed, Vector3 direction, float fadeTime, bool crit)
    {//--Legt die Richtung und das Tempo fest!

        this.speed = speed;
        this.fadeTime = fadeTime;
        this.direction = direction;
        this.crit = crit;
        if (crit)
        {
            GetComponent<Animator>().SetTrigger("Critical");
            
            StartCoroutine(Critical());
        }
        else
        {
            StartCoroutine(FadeOut());
        }

        
    }
    IEnumerator Critical()
    {
        //yield return new WaitForSeconds(critAnim.length);
        crit = false;
        StartCoroutine(FadeOut());
        yield return null;
    }
    private IEnumerator FadeOut()
    {
        float startAlpha = GetComponent<Text>().color.a;
        float rate = 1.0f / fadeTime;
        float progress = 0.0f;
        while(progress < 1.0)
        {
            Color tmpColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(startAlpha, 0, progress)); //änder die Farbe, r,g,b, a
            progress += rate * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
