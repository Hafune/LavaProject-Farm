using JetBrains.Annotations;
using Lib;
using UnityEngine.EventSystems;

public abstract class AbstractPlant : MonoConstruct, IPointerClickHandler
{
    private PlantMenu _plantActionMenu;
    private bool _isActive;
    protected IMyContext _context;
    private Cell _cell;

    [CanBeNull] protected abstract AbstractPlantMenuButton PlantMenuButton { get; }

    protected override void Construct(IMyContext context)
    {
        _plantActionMenu = context.Resolve<PlantMenu>();
        _context = context;
    }

    public void Activate() => _isActive = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isActive)
            return;

        _plantActionMenu.Open(PlantMenuButton, this);
    }

    public void SetCell(Cell cell) => _cell = cell;

    private void OnDestroy() => _cell.Unlock();
}