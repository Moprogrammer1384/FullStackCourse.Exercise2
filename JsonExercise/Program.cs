using JsonExercise;

TourJsonRepository tourJsonRepository = new();
UserInteraction userInteraction = new(tourJsonRepository);
TourController tourController = new(tourJsonRepository, userInteraction);

tourController.Execute();
Console.ReadLine();