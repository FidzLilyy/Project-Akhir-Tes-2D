using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{
    public GameObject TextBox, panel;

    int noDialog = 0;
    [SerializeField]
    int LetterPS;
    [SerializeField] TextMeshProUGUI dialogText;
    [TextArea]
    [SerializeField] string text, text1, text2;

    private Coroutine typingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TypeDialog(text));
    }
    public IEnumerator TypeDialog(string dialog){
        dialogText.text =  "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / LetterPS);
        }

        yield return new WaitForSeconds(1f);
        dialogText.text = "";
        if(noDialog < 1){
            StartCoroutine(TypeDialog(text1));
            yield return new WaitForSeconds(2f);
            dialogText.text = "";
            noDialog++;
        }else if(noDialog == 1){
            StartCoroutine(TypeDialog(" Good Luck"));
            yield return new WaitForSeconds(2f);
            dialogText.text = "";
            noDialog++;
        }

        if(noDialog >= 2){
            panel.SetActive(false);
            TextBox.SetActive(false);
        }
    }

    public void NextDialog(){
            dialogText.text = "";
            noDialog++;
    }
}
