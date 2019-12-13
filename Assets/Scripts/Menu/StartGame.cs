using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void Game()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(1);
    }
}
