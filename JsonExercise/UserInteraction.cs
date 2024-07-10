

using System.Data;

namespace JsonExercise;

public class UserInteraction : IUserInteraction
{
    private readonly ITourRepository _tourRepository;

    public UserInteraction(ITourRepository tourRepository)
    {
        this._tourRepository = tourRepository;
    }
    public (DateOnly startDate, DateOnly endDate) dateReciever()
    {
        string start = "";
        PrintMessage("Please enter the start date: ");
        start = Console.ReadLine();

        string end = "";
        PrintMessage("Please enter the end date: ");
        end = Console.ReadLine();
                
        return DateConverter(start, end);
    }

    public (DateOnly startDate, DateOnly endDate) DateConverter(string start, string end)
    {
        (DateOnly startDate, DateOnly endDate) dates;
        if (DateOnly.TryParse(start, out dates.startDate) && DateOnly.TryParse(end, out dates.endDate))
        {
            return dates;
        }
        return (new DateOnly(), new DateOnly());
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintTours(List<Tour> tours)
    {
        PrintMessage("List of tours: ");
        foreach (var tour in tours)
        {
            PrintMessage("".PadLeft(10, '-'));
            PrintMessage($"Name: {tour.Name}");
            PrintMessage($"Start date: {tour.StartTime}");
            PrintMessage($"City name: {tour.CityName}");
        }
    }

    public int Promt()
    {
        string menu = @"1. Search through tours
2. Save a tour";
        PrintMessage(menu);
        PrintMessage("Please choose one of the options: ");
        return int.Parse(Console.ReadLine());         
    }

    private string SaveTour()
    {
        PrintMessage("Please type the name of the tour that you want: ");
        return Console.ReadLine();
    }

    public void SearchOperation()
    {
        while (true)
        {
            try
            {
                (DateOnly start, DateOnly end) dates = dateReciever();
                var results = _tourRepository.Search(dates.start, dates.end);
                PrintTours(results);
                save(results);
                break;
            }
            catch (ArgumentException ex)
            {
                PrintMessage(ex.Message);
            }
            catch (Exception ex)
            {
                PrintMessage(ex.Message);
            }
        }
        
    }

    public void save(List<Tour> tours)
    {
        Tour tour;
        while (true)
        {
            PrintMessage("Please enter a tour name: ");
            string name = Console.ReadLine();
            tour = tours.FirstOrDefault(x => x.Name == name);
            if (tour == null)
            {
                PrintMessage("Please choose a correct tour!!!");
                continue;
            }
            else
            {
                break;
            }
        }
        string fileName = SaveTour();
        while (true)
        {
            PrintMessage("Please enter a path to save the file: ");
            string path = Console.ReadLine();
            try
            {
                _tourRepository.Save(tour, path, fileName);
                PrintMessage("The tour saved.");
                break;
            }
            catch (DirectoryNotFoundException ex)
            {
                PrintMessage(ex.Message);
            }
            catch (Exception ex)
            {
                PrintMessage(ex.Message);
            }
        }
    }
}

