namespace JsonExercise;

public class TourController
{
    private readonly ITourRepository _tourRepository;
    private readonly IUserInteraction _userInteraction;
    private readonly string filePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Data", "tours.json");
    public TourController(ITourRepository tourRepository, IUserInteraction userInteraction)
    {
        this._tourRepository = tourRepository;
        this._userInteraction = userInteraction;
    }

    internal void Execute()
    {
        _userInteraction.PrintMessage("**Welcome to tour manager app**");
        var tours = _tourRepository.GetAll(filePath);
        _userInteraction.PrintTours(tours);
        int option = _userInteraction.Promt();
        switch (option)
        {
            case 1:
                _userInteraction.SearchOperation();
                break;
            case 2:
                _userInteraction.save(tours);
                break;
            default:
                _userInteraction.PrintMessage("Please select a correct option!!");
                break;
        }
    }
}

