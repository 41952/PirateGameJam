using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RewardMenuUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject rewardPanel; // корневой объект UI-меню
    [SerializeField] private Transform rewardsParent; // куда будем инстансить префабы
    [SerializeField] private GameObject rewardOptionPrefab; // префаб UIRewardOptionBlock
    [SerializeField] private Button skipButton;

    private List<GameObject> spawnedOptions = new();

    private void Awake()
    {
        rewardPanel.SetActive(false);
        skipButton.onClick.AddListener(CloseMenu);
    }

    [ContextMenu("OpenUpgradesMenu")]
    public void OpenMenu()
    {
        MusicManager.Instance.PlayIntermission();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        rewardPanel.SetActive(true);

        var rewardDataList = GetComponent<RewardGenerator>().GenerateRewardOptions();

        foreach (var data in rewardDataList)
        {
            GameObject go = Instantiate(rewardOptionPrefab, rewardsParent);
            var uiBlock = go.GetComponent<UIRewardOptionBlock>();
            uiBlock.Initialize(data, () =>
            {
                CloseMenu();
            });

            spawnedOptions.Add(go);
        }
    }

    public void CloseMenu()
    {
        foreach (var go in spawnedOptions)
        {
            Destroy(go);
        }
        spawnedOptions.Clear();
        MusicManager.Instance.PlayBattle();

        rewardPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameEvents.RaiseUpgradeMenuClosed(); // <-- добавлено

    }

}
