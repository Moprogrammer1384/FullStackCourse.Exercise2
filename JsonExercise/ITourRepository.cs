namespace JsonExercise;

public interface ITourRepository
{
    List<Tour> GetAll(string filePath);
    List<Tour> Search(DateOnly startDate, DateOnly endDate);
    void Save(Tour tour, string path, string fileName);
}
