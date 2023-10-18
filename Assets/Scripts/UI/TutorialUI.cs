using UnityEngine;

public class TutorialUI : MonoBehaviour
{


    private void Start()
    {
        Show();
        KitchenGameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
    }

    private void Instance_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
            Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
