
using System.Text.Json;

namespace JsonExercise;

public class TourJsonRepository : ITourRepository
{
    private List<Tour> _tours;

    public TourJsonRepository()
    {
        _tours = new List<Tour>();
    }

    public List<Tour> GetAll(string filePath)
    {        
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"There is no file in this address: {filePath}");
        }
        string tour = "";
        using (StreamReader reader = new(filePath))
        {
            while (!reader.EndOfStream)
            {
                tour = reader.ReadLine();
                _tours.Add(JsonSerializer.Deserialize<Tour>(tour));
            }

        }
        return _tours;        
    }

    public void Save(Tour tour, string path, string fileName)
    {
        string json = JsonSerializer.Serialize(tour);
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"There is no directory in this address: {path}");
        }
        using (File.Create($"{path}\\{fileName}.json")) ;
        File.WriteAllText($"{path}\\{fileName}.json", json);
    }

    public List<Tour> Search(DateOnly startDate, DateOnly endDate)
    {
        if(startDate == endDate || startDate > endDate || endDate < startDate)
        {
            throw new ArgumentException("The dates that you entered are wrong!!!");
        }
        List<Tour> result = new();

        foreach (var tour in _tours)
        {
            if(tour.StartTime >= startDate && tour.StartTime <= endDate)
            {
                result.Add(tour);
            }
        }

        return result;
    }
}

