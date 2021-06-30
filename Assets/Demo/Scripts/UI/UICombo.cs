using TMPro;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// Displays the player's current combo
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class UICombo : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            // initialize text display
            text = GetComponent<TMP_Text>();
            text.text = "x1";
            
            // listen for combo changed events
            EventDispatcher.AddListener<ComboChangedEvent>(Combo_Changed);
        }

        private void OnDestroy()
        {
            // remove our listener when we're destroyed
            EventDispatcher.RemoveListener<ComboChangedEvent>(Combo_Changed);
        }
        
        private void Combo_Changed(ComboChangedEvent e)
        {
            // set the text to the new value
            text.text = $"x{e.Value}";
        }

    }
}