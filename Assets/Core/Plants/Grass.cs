public class Grass : AbstractPlant
{
    private PlantMenuButtonDrop _plantMenuButtonDrop;

    private void Awake() => _plantMenuButtonDrop = _context.Resolve<PlantMenuButtonDrop>();

    protected override AbstractPlantMenuButton PlantMenuButton => _plantMenuButtonDrop;
}