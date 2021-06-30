using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo
{
    public class ReloadSceneOnKeypress : MonoBehaviour
    {
        [SerializeField] private KeyCode key = KeyCode.R;
        
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(key))
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex
                );
            }
        }
    }
}