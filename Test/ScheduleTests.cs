namespace Test;
using Shared;
public class ScheduleTests
{
  //NewMovieRegistration
  [Fact]
  public void RegisterNewMovie()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.MovieList = new();
    try
    {
      MovieTheater.NewMovieRegistration("Clifford", 30, "The Big Red Dog", "Clifford- George Clooney");
    }
    catch
    {
      Assert.Fail("Shouldn't get here");
    }
    Assert.Single(MovieTheater.MovieList);
  }

  [Fact]
  public void RegisterNewMovieThatsAlreadyRegistered()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.MovieList = new();
    try
    {
      MovieTheater.NewMovieRegistration("Clifford", 30, "The Big Red Dog", "Clifford- George Clooney");
      MovieTheater.NewMovieRegistration("Clifford", 30, "The Big Red Dog", "Clifford- George Clooney");
    }
    catch
    {
      Assert.Single(MovieTheater.MovieList); // should throw an exception to prevent duplicate movies
      return;
    }
    Assert.Fail("Shouldn't get here");
  }

  //NewShowingID
  [Fact]
  public void CreateOneNewID()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.ScheduleList = new();
    try
    {
      MovieTheater.CreateShowingID(1, DateTime.Parse("1/1/1001 5:45"), 5, 1, "Clifford");
    }
    catch (ArgumentException)
    {
      Assert.Fail("Shouldn't get here");
    }
    Assert.Single(MovieTheater.ScheduleList);
  }

  [Fact]
  public void CreateSameIDTwice()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.ScheduleList = new();
    try
    {
      MovieTheater.CreateShowingID(1, DateTime.Parse("1/1/1001 5:45"), 5, 1, "Clifford");
      MovieTheater.CreateShowingID(1, DateTime.Parse("1/1/2001 6:30"), 8, 2, "Clifford");
    }
    catch (ArgumentException)
    {
      Assert.True(true);
      return;
    }

    Assert.Fail("Shouldn't get here");
  }
}