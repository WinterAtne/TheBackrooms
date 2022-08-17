using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Scene_Manager : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    [SerializeField] UI_Elements ui;
    [SerializeField] Player_Movement player;
    [SerializeField] Player_Look camera;
    [SerializeField] Player_Inventory inventory;




    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false) {
            Pause();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Unpause();
        }
    }





    //Loading Functions
    public void LoadLevel(int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void ReloadLevel() {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);

            yield return null;
        }
    }

    //Pausing & Unpausing Functions
    public void Pause() {
        isPaused = true;
        ui.InventoryEnable();

        camera.enabled = false;
        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
    }

    public void Unpause() {
        isPaused = false;
        ui.InventoryDisable();

        player.enabled = true;
        camera.enabled = true;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit() {
        Application.Quit();
    }

    public bool ReturnSceneState() {
        return(isPaused);
    }
}
