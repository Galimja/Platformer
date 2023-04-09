using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformerMVC
{
    public class PauseMenuController
    {
        private PauseMenuView _view;

        public PauseMenuController(PauseMenuView view)
        {
            _view = view;
            _view.Init(OpenPauseMenu, ClosePauseMenu, ResetLevel);

        }

        private void OpenPauseMenu()
        {
            _view.PausePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        private void ClosePauseMenu()
        {
            _view.PausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

        private void ResetLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}