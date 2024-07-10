namespace JsonExercise;

public interface IUserInteraction
{
    void PrintMessage(string message);
    void PrintTours(List<Tour> tours);
    (DateOnly startDate, DateOnly endDate) dateReciever();
    (DateOnly startDate, DateOnly endDate) DateConverter(string start, string end);
    int Promt();   
    void SearchOperation();
    void save(List<Tour> tours);
}

