using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    /// <summary>
    /// Restarts the game by reloading the main scene
    /// </summary>
    public class SceneReload : MonoBehaviour
    {
        [SerializeField] GameEvent _sceneReloadResponseEvent;

        void OnEnable()
        {
            _sceneReloadResponseEvent.AddListener(Reload);
        }

        void OnDisable()
        {
            _sceneReloadResponseEvent.RemoveListener(Reload);
        }

        void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
