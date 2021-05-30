using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus {
    public class MainMenu : MonoBehaviour {
        public string firstLevelName;

        public void StartPressed() {
            SceneManager.LoadScene(firstLevelName, LoadSceneMode.Single);
        }
    }
}
