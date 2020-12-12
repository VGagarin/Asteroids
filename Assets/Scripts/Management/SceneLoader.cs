using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    internal sealed class SceneLoader : MonoBehaviour
    {
        public void LoadScene(int index) => SceneManager.LoadScene(index);
    }
}