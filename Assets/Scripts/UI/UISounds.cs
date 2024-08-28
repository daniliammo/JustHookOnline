using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UISounds : MonoBehaviour, ISelectHandler, IPointerEnterHandler
    {

        public ButtonSounds buttonSounds;
        
        
        public void OnSelect(BaseEventData eventData)
        {
            buttonSounds.PlayHoverSound2();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonSounds.PlayHoverSound3();
        }
        
    }
}
