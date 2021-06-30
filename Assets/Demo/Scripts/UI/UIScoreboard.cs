using TMPro;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// Displays the player's current score
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class UIScoreboard : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            // initialize text display
            text = GetComponent<TMP_Text>();
            text.text = "0";
            
            // listen for PointsAwardedEvent with Points_Awarded method
            EventDispatcher.AddListener<PointsAwardedEvent>(Points_Awarded);
        }

        private void OnDestroy()
        {
            // remove our listener when we're destroyed
            EventDispatcher.RemoveListener<PointsAwardedEvent>(Points_Awarded);
        }

        private void Points_Awarded(PointsAwardedEvent e)
        {
            // update text with new score
            text.text = e.Score.ToString();
        }
    }
}