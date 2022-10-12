using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHL : MonoBehaviour, ISelectHandler
{
    public MainMenu characterList;
    public int myindex;
    public string myname;
    public bool player = true;
    public bool stage = false;
    
 

    public void OnSelect(BaseEventData eventData)
    {
        if (player == true)
        {
            characterList.selectedName.text = myname;
            characterList.Plindex = myindex;
            characterList.selectCharacter();
        }
        
        if(stage == true)
        {

        }

    }


}