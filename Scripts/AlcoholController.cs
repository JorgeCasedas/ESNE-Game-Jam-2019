using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlcoholController : MonoBehaviour {

    [Header("-------Inputs:-------")]
    [SerializeField] float headMovementSpeed;
    [SerializeField] float limitDownSpeed;
    [SerializeField] float limitUpSpeed;
    [SerializeField] Vector2 AlcoholGreaterLimits;
    [SerializeField] Vector2 AlcoholLimits;
    [SerializeField] GameObject[] spawnAreas;
    [SerializeField] int maxActiveBottles;
    [SerializeField] int maxLimitWidth = 15;
    [SerializeField] float alcoholLoseSpeed;
    [SerializeField] float waitSecs;

    [Space]
    [Header("-------References:-------")]
    [SerializeField] GameObject UIHead;
    [SerializeField] GameObject limitsCenter;
    [SerializeField] GameObject rightLimit;
    [SerializeField] GameObject leftLimit;
    [SerializeField] GameObject alcoholPrefab;
    [SerializeField] GameObject character;
    [SerializeField] SpriteRenderer grannyGfx;
    [SerializeField] ParticleSystem grannyParticles;
    [SerializeField] DialoguePopUp dialogueScript;

    [Space]
    [Header("-------DEBUGGING:-------")]
    [Range(0,100)] public float drunkLevel;
    public int numberOfActiveBottles;
    public bool isPeing;
    [SerializeField] bool changingPos;
    byte grannyAlpha;
    bool moduleEnabled;
    [SerializeField] float distanceToLimits;

    RectTransform headTransform;
    [SerializeField] float actualHeadPosition;
    [SerializeField] [Range(-44.3f, 44.3f)] float nextHeadPosition;

    RectTransform limitsTransform;
    [SerializeField] float actualLimitCenterPos;
    [SerializeField] [Range(-44.3f, 44.3f)] float nextLimitCenterPos;

    // Use this for initialization
    void Start () {
        changingPos = false;
        headTransform = UIHead.GetComponent<RectTransform>();
        limitsTransform = limitsCenter.GetComponent<RectTransform>();
        nextHeadPosition = Mathf.Lerp(AlcoholGreaterLimits.x, AlcoholGreaterLimits.y, drunkLevel / 100);
        MoveLimitsCenter();
    }
	
	// Update is called once per frame
	void Update () {
        if (!changingPos)
        {
            if (nextLimitCenterPos >= Mathf.Lerp(-44.3f, 44.3f, 0.85f) && actualHeadPosition > actualLimitCenterPos + 15)
            {
                changingPos = true;
                dialogueScript.hasToPee = true;
                StartCoroutine(WaitSecs());
            }
            else if (actualLimitCenterPos >= Mathf.Lerp(-44.3f, 44.3f, 0.85f))
            {
                changingPos = true;
                StartCoroutine(WaitSecs());
            }
            else if (actualLimitCenterPos <= Mathf.Lerp(-44.3f, 44.3f, 0.15f))
            {
                changingPos = true;
                StartCoroutine(WaitSecs());
            }
        }
        
        if (isPeing)
        {
            drunkLevel -= alcoholLoseSpeed*Time.deltaTime;
            drunkLevel = Mathf.Clamp(drunkLevel, 0, 100);
            nextHeadPosition = Mathf.Lerp(AlcoholGreaterLimits.x, AlcoholGreaterLimits.y, (float)drunkLevel / 100);
            character.GetComponent<CharacterMovment>().changeSpeed(drunkLevel);
            character.GetComponent<CharacterMovment>().anim.SetFloat("SpeedModifier", Mathf.Lerp(1, 4, drunkLevel / 100));
        }
        else
        {

            distanceToLimits = Mathf.Abs(Mathf.Abs(headTransform.position.x) - Mathf.Abs(limitsTransform.position.x));
            grannyAlpha =(byte)Mathf.Lerp(255, 0, distanceToLimits/300);   

            if (actualHeadPosition>actualLimitCenterPos+15 || actualHeadPosition < actualLimitCenterPos - 15)
            {
                moduleEnabled = false;
                var emission = grannyParticles.emission;
                emission.enabled = moduleEnabled;
                grannyGfx.color = new Color32(255, 255, 255, grannyAlpha);
                if(grannyAlpha <= 1)
                {
                    SceneManager.LoadScene("EndScreenGranny");  
                }
            }
            else
            {
                moduleEnabled = true;
                var emission = grannyParticles.emission;
                emission.enabled = moduleEnabled;
                grannyGfx.color = new Color(255, 255, 255, 255);
            }
        }
        if(numberOfActiveBottles < maxActiveBottles)
        {
            StartCoroutine(SpawnAlcohol());
            numberOfActiveBottles++;
        }

        actualHeadPosition = headTransform.anchoredPosition.x;
        actualLimitCenterPos = limitsTransform.anchoredPosition.x;

        if (actualHeadPosition < nextHeadPosition - 0.5f || actualHeadPosition > nextHeadPosition + 0.5f)
        {
            if(actualHeadPosition > nextHeadPosition)
                headTransform.position -= new Vector3(headMovementSpeed * Time.deltaTime, 0);
            else
                headTransform.position += new Vector3(headMovementSpeed * Time.deltaTime, 0);
        }

        if (actualLimitCenterPos > nextLimitCenterPos && !changingPos)
            limitsTransform.position -= new Vector3(limitDownSpeed * Time.deltaTime, 0);
        else if(!changingPos)
            limitsTransform.position += new Vector3(limitUpSpeed * Time.deltaTime, 0);
        
    }

    public void AddAlcohol(int addition)
    {
        drunkLevel += addition;
        drunkLevel = Mathf.Clamp(drunkLevel, 0, 100);
        nextHeadPosition = Mathf.Lerp(AlcoholGreaterLimits.x, AlcoholGreaterLimits.y, (float)drunkLevel / 100);
        character.GetComponent<CharacterMovment>().changeSpeed(drunkLevel);
        character.GetComponent<CharacterMovment>().anim.SetFloat("SpeedModifier", Mathf.Lerp(1, 4, drunkLevel / 100));
    }
    public IEnumerator SpawnAlcohol()
    {
        float randomSeconds=Random.Range(1f,2f);
        yield return new WaitForSeconds(randomSeconds);
        int randomArea = Random.Range(0, spawnAreas.Length);
        Vector3 randomPos = spawnAreas[randomArea].GetComponent<GetRandomCoordinate>().GetCoordinate();
        Instantiate(alcoholPrefab, randomPos, Quaternion.Euler(Vector3.zero));         
    }
    public IEnumerator WaitSecs()
    {
        yield return new WaitForSeconds(waitSecs);
        MoveLimitsCenter();
        yield return new WaitForSeconds(0.1f);
        changingPos = false;
    }
    void MoveLimitsCenter()
    {
        float nextPos = nextLimitCenterPos >= Mathf.Lerp(-44.3f, 44.3f, 0.85f) ? Mathf.Lerp(-44.3f, 44.3f, 0.15f) : Mathf.Lerp(-44.3f, 44.3f, 0.85f); //Goes up and down
        nextLimitCenterPos = nextPos;
    }
}
