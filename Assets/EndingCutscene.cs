using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    public TypeDialogue typeDialogue;
    public Animator myAnimator;
    public Button button_restartGame;
    
    void Start()
    {
        StartCoroutine("CountdownEnding");
        button_restartGame.gameObject.SetActive(false);
    }

    private IEnumerator CountdownEnding() {
        GameManager.reduceMusicVolumeHotlineMiami = true;
        myAnimator.SetTrigger("StartEnding");
        yield return new WaitForSeconds(11f);
        typeDialogue.CreateNewText(4);
        yield return new WaitForSeconds(8f);
        button_restartGame.gameObject.SetActive(true);
    }
}
