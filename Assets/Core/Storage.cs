using Core.Views;

public class Storage
{
    private CarrotsView _carrotsView;
    private ExperienceView _experienceView;
    private int _carrots;
    private float _experience;

    public Storage(CarrotsView carrotsView, ExperienceView experienceView)
    {
        _carrotsView = carrotsView;
        _experienceView = experienceView;
        _carrotsView.Label.FormatText(_carrots);
        _experienceView.Label.FormatText(_experience);
    }

    public void AddCarrot()
    {
        _carrots++;
        _carrotsView.Label.FormatText(_carrots);
    }

    public void AddExperience(float experience)
    {
        _experience += experience;
        _experienceView.Label.FormatText(_experience);
    }
}