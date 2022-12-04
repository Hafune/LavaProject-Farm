public class PlantMenuButtonCollect : AbstractPlantMenuButton
{
    private Storage _storage;

    protected override void OnClick() => _plantMenu.Selected(Collect);

    private void Start() => _storage = _context.Resolve<Storage>();

    private void Collect(AbstractPlant abstractPlant)
    {
        Destroy(abstractPlant.gameObject);
        _storage.AddCarrot();
    }
}