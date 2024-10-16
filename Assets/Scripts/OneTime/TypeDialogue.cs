using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeDialogue : MonoBehaviour
{

    public AudioClip textAudioClip;
    [TextArea(3,10)]
    public string shipDialogue1;
    [TextArea(3, 10)]
    public string shipDialogue2;
    [TextArea(3, 10)]
    public string shipDialogue3;
    [TextArea(3, 10)]
    public string dialogueEnding;

    public bool endingDialogue = false;
    private string targetText;
    public TextMeshProUGUI myTextUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreateNewText(int textId) {
        myTextUI.text = "";
        gameObject.SetActive(true);
        if(textId == 1) {
            targetText = shipDialogue1;
        } else if (textId == 2) {
            targetText = shipDialogue2;
        } else if (textId == 3) {
            targetText = shipDialogue3;
        } else if (textId == 4) {
            targetText = dialogueEnding;
        }
        StopAllCoroutines();
        StartCoroutine(TypeText(targetText));

        GameManager.SpawnLoudAudio(textAudioClip);
    }
    // Update is called once per frame
    private IEnumerator TypeText(string newDialogue)
    {
        for (int i = 0; i < newDialogue.Length; i++) {
            if(endingDialogue) { // slower
                yield return new WaitForSeconds(0.08f);
            } else {
                yield return new WaitForSeconds(0.01f);
            }
            myTextUI.text += newDialogue[i];
        }
        // deactivate self after 6 seconds
        yield return new WaitForSeconds(6f);
        if (endingDialogue == false) {
            gameObject.SetActive(false);
        }
    }
}
