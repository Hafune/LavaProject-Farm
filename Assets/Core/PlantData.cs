using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(PlantData))]
public class PlantData : ScriptableObject
{
    [SerializeField] private AbstractPlant _plantPrefab;
    [SerializeField] private float _growTime;

    public AbstractPlant PlantPrefab => _plantPrefab;
    public float GrowTime => _growTime;
}