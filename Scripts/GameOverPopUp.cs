using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPopUp : MonoBehaviour {

    public Text textHolder;
    public string deadText;
    public string who; //granny or barman

	// Use this for initialization
	void Start () {
        StartCoroutine(WriteDialogue(deadText, who));
    }
	
    public IEnumerator WriteDialogue(string fullText, string who)
    {
        int i = 0;
        if (who == "granny")
        {
            while (fullText != textHolder.text)
            {
                textHolder.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(0.08f);
                i++;
            }

        }
        else if (who == "barman")
        {
            while (fullText != textHolder.text)
            {
                textHolder.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(0.08f);
                i++;
            }
        }
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FinalScene");
    }
}
