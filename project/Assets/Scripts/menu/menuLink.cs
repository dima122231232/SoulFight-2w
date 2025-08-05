using UnityEngine;
using UnityEngine.SceneManagement;
public class menuLink : MonoBehaviour
{
    public int sceneNumber;
    public void Tran()
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
