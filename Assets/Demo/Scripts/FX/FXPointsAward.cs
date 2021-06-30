using Demo.Utilities.Pooling;
using TMPro;
using UnityEngine;

namespace Demo.FX
{
    /// <summary>
    /// Shows the number of points awarded when the player earns points
    /// </summary>
    [RequireComponent(typeof(Pool))]
    public class FXPointsAward : MonoBehaviour
    {
        private Pool pool;

        private void Awake()
        {
            pool = GetComponent<Pool>();

            // add our listener
            EventDispatcher.AddListener<PointsAwardedEvent>(Points_Awarded);
        }

        // remove our listener when we're destroyed
        private void OnDestroy()
        {
            EventDispatcher.RemoveListener<PointsAwardedEvent>(Points_Awarded);
        }

        private void Points_Awarded(PointsAwardedEvent e)
        {
            // spawn an effect from the pool
            if (!pool.TryGet(out var go)) return;

            var text = go.GetComponentInChildren<TMP_Text>();
            var t = go.transform;
            
            // set text to award
            text.text = e.Award.ToString();

            // set position to brick position
            t.position = e.Source.transform.position;
        }
    }
}