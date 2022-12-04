public class PlantMenuButtonDrop : AbstractPlantMenuButton
{
    protected override void OnClick()
    {
        _plantMenu.Selected(DropPlant);
    }

    private void DropPlant(AbstractPlant plant) => Destroy(plant.gameObject);
}