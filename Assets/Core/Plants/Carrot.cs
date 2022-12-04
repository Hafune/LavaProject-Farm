public class Carrot : AbstractPlant
{
    private PlantMenuButtonCollect _plantMenuButtonCollect;

    private void Awake() => _plantMenuButtonCollect = _context.Resolve<PlantMenuButtonCollect>();

    protected override AbstractPlantMenuButton PlantMenuButton => _plantMenuButtonCollect;
}