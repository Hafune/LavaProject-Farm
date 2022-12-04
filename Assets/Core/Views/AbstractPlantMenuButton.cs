using Lib;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class AbstractPlantMenuButton : MonoConstruct
{
    [SerializeField] protected PlantMenu _plantMenu;
    
    private Button _button;
    protected IMyContext _context;

    protected override void Construct(IMyContext context) => _context = context;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
    
    private void OnDestroy() => _button.onClick.RemoveListener(OnClick);
}