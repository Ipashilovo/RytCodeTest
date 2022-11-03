using UnityEngine.SceneManagement;

namespace Core
{
    public class EndGameProvider
    {
        public void Restart()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}