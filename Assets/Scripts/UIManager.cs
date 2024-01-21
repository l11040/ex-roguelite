using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Image hpFill;

    public void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    void Update()
    {
        hpFill.fillAmount = Player.Instance.hp / Player.Instance.MAX_HP;
    }
}
