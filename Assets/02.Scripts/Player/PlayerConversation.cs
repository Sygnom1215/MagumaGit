using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConversation : MonoBehaviour
{
    private NpcText npcText;
    private int idx = 0;

    public bool isCanConversation = false;
    public bool isTalking = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Npc")
        {
            isCanConversation = true;
            npcText = col.GetComponent<NpcText>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Npc")
        {
            isCanConversation = false;
            npcText = null;
        }
    }

    public void Conversation()
    {
        isTalking = true;
        string textData = TextManager.Instance.GetTalk(npcText.npcId, idx);
        idx++;
        if(textData == null)
        {
            idx = 0;
            isTalking = false;
            UIManager.Instance.CloseText();
            return;
        }
        Vector3 pos = npcText.gameObject.transform.position + new Vector3(0f, 1.5f, 0f);
        UIManager.Instance.OpenText(textData, pos);
    }
}
