using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    //メニュー画面のスタートをクリック
    public void OnGameStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    //タイトル画面へ
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
