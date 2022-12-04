using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SeedMenuButton : MonoBehaviour
{
    [SerializeField] private SeedMenu _seedMenu;
    [SerializeField] private PlantData _plantData;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(Clicked);
    }

    private void Clicked() => _seedMenu.Selected(_plantData);

    private void OnDestroy() => _button.onClick.RemoveListener(Clicked);
}