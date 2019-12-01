using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailsTextController : MonoBehaviour
{
    private TextAsset dialogueTextRef;
    private string[] dialogueText;
    private int currentLine, lastLine;
    private float textSpeed = 0.005f;
    private bool isTyping;
    private bool isActive;

    public GameObject Text;
    TextMeshProUGUI textContainer;

    // Start is called before the first frame update
    void Start()
    {
        textContainer = Text.GetComponent<TextMeshProUGUI>();
        dialogueTextRef = GetComponentInParent<InstantiateDetails>().detailsText;

        //Process text file for use
        if (dialogueTextRef != null)
        {
            dialogueText = (dialogueTextRef.text.Split('\n')); //splits the text doc up per line
        }
        if (lastLine == 0)
        {
            lastLine = dialogueText.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) //if the player is inside the trigger but hasn't yet to activate the NPC
        {
            StartCoroutine(Dialogue(dialogueText[currentLine]));
            isActive = true;
        }

        if (!isTyping & Input.GetKeyDown(KeyCode.Mouse0)) //If the dialogue is paused, and player presses 'E', run next line or exit dialogue
        {
            currentLine += 1;
            if (currentLine > lastLine) //if current line if past number of lines, terminate
            {
                isActive = false;
                currentLine = 0;
                CloseText();
            }
            else //or else start the next line
            {
                StartCoroutine(Dialogue(dialogueText[currentLine]));
            }
        }
    }

    //Controls the dialogue being printed to scene
    IEnumerator Dialogue(string Text)
    {
        int letter = 0;
        isTyping = true;
        textContainer.text = ""; //clear text box before passing dialogue
        while (isTyping && letter < Text.Length - 1)
        {
            WriteDialogue(Text[letter]);

            letter += 1;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
        isTyping = false;
    }

    void WriteDialogue(char dialogue)
    {
        textContainer.text += dialogue;
    }

    public void CloseText()
    {
        GetComponentInParent<InstantiateDetails>().AlternateCamerasCheck();
        GetComponentInParent<InstantiateDetails>().InstanceInScene = false;
        Time.timeScale = 1;
        Debug.Log("Getting ready to destroy");
        Destroy(this.gameObject);
    }
}
