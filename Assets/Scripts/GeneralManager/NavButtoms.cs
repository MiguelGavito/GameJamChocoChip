using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NavButtoms : MonoBehaviour
{
    public void CargarPrimerNivel()
    {
        StartCoroutine(CargarEscenaAsync(1));
    }

    public void CargarSegundoNivel()
    {
        StartCoroutine(CargarEscenaAsync(2));
    }

    public void CargarTercerNivel()
    {
        StartCoroutine(CargarEscenaAsync(3));
    }

    public void CargarMenu()
    {
        StartCoroutine(CargarEscenaAsync(0));
    }


    public void Salir()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif
    }

    private IEnumerator CargarEscenaAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
