using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialoguePopUp : MonoBehaviour {
    [Header("-------Inputs:-------")]
    public string[] grannyDialogue0_25;
    public string[] grannyDialogue25_50;
    public string[] grannyDialogue50_75;
    public string[] grannyDialogue75_100;
    public string[] grannyDialogueMear;
    public string[] barmanDialogues;

    [Space]
    [Header("-------References:-------")]
    public Text grannyTextHolder;
    public Text barmanTextHolder;
    public GameObject grannyDialogueBox;
    public GameObject barmanDialogueBox;
    public AlcoholController alcoholScript;

    [Space]
    [Header("-------DEBUGGING:-------")]
    [SerializeField] int barmanWarningsNumber;
    [SerializeField] IEnumerator barmanDialogueWrite;
    [SerializeField] IEnumerator grannyDialogueWrite;
    public bool hasToPee;
    // Use this for initialization
    void Start () {
        hasToPee = false;
        grannyDialogueBox.SetActive(false);
        barmanDialogueBox.SetActive(false);
        Invoke("OpenGrannyDialogue",5);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenGrannyDialogue()
    {
        if (!alcoholScript.isPeing && !hasToPee)
        {
            string randomDialogue = "";
            if (alcoholScript.drunkLevel < 25)
            {
                Debug.Log("<25");
                randomDialogue = grannyDialogue0_25[Random.Range(0, grannyDialogue0_25.Length)];
            }
            else if (alcoholScript.drunkLevel < 50)
            {
                Debug.Log("<50");
                randomDialogue = grannyDialogue25_50[Random.Range(0, grannyDialogue25_50.Length)];
            }
            else if (alcoholScript.drunkLevel < 75)
            {
                Debug.Log("<75");
                randomDialogue = grannyDialogue50_75[Random.Range(0, grannyDialogue50_75.Length)];
            }
            else
            {
                Debug.Log("<100");
                randomDialogue = grannyDialogue75_100[Random.Range(0, grannyDialogue75_100.Length)];
            }
            grannyDialogueWrite = WriteDialogue(randomDialogue, "granny");
            StartCoroutine(grannyDialogueWrite);
        }
        else if(!alcoholScript.isPeing && hasToPee)
        {
            string randomDialogue = "";
            randomDialogue = grannyDialogueMear[Random.Range(0, grannyDialogueMear.Length)];
            StopCoroutine(grannyDialogueWrite);
            grannyDialogueWrite = WriteDialogue(randomDialogue, "granny");
            StartCoroutine(grannyDialogueWrite);
        }
    }

    public IEnumerator WriteDialogue(string fullText, string who)
    {
        
        int i = 0;
        if (who == "granny")
        {
            grannyDialogueBox.SetActive(true);
            while (fullText!= grannyTextHolder.text)
            {
                grannyTextHolder.text = fullText.Substring(0,i);
                yield return new WaitForSeconds(0.03f);
                i++;
            }
                
        }
        else if (who == "barman")
        {
            barmanDialogueBox.SetActive(true);
            while (fullText != barmanTextHolder.text)
            {
                barmanTextHolder.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(0.03f);
                i++;
            }
        }
        yield return new WaitForSeconds(2);

        if (who == "granny")
        {
            if (hasToPee)
                hasToPee = false;
            grannyDialogueBox.SetActive(false);
            yield return new WaitForSeconds(5);
            OpenGrannyDialogue();
        }
           
        else if (who == "barman")
            barmanDialogueBox.SetActive(false);
    }

    public void OpenBarmanDialogue()
    {
        if(barmanWarningsNumber >= barmanDialogues.Length)
        {
            EndGame();
        }
        else
        {
            if (barmanDialogueWrite != null) 
                StopCoroutine(barmanDialogueWrite);
            barmanDialogueWrite = WriteDialogue(barmanDialogues[barmanWarningsNumber], "barman");
            StartCoroutine(barmanDialogueWrite);
            barmanWarningsNumber++;
        }
    }
    void EndGame()
    {
        SceneManager.LoadScene("EndSceneBarman");
    }
}
