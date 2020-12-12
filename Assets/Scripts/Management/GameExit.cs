using UnityEngine;

namespace Management
{
    internal sealed class GameExit : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}