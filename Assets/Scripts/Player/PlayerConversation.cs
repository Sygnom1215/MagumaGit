using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConversation : MonoBehaviour
{
    private NpcText npcText; 

    public bool isCanConversation = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Npc")
        {
            isCanConversation = true;
            npcText = other.GetComponent<NpcText>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Npc")
        {
            isCanConversation = false;
        }
    }

    public void Conversation()
    {

    }
}
