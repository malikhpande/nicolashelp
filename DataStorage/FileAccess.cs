global using MovieTuple = (string title, int runLengthMinutes, string advertisingMesssage, string leads);
global using ShowingTuple = (int showingID, System.DateTime showingDateTime, decimal ticketPrice, int theaterRoom, string movieTitle);
global using DailyShowingTuple = (string MovieTitle, System.DateTime showTime);

global using PreferredCustomerTuple = (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints);
global using SoldTicketTuple = (System.DateTime soldDateTime, int showingID, decimal revenueCharged, int preferredCustomerNum);

//concessions
global using ConcessionMenuTuple = (string itemName, string itemDescription, decimal price);
global using ConcessionSaleTuple = (System.DateTime soldDateTime, string itemName, int quantitySold, decimal revenueCollected, int preferredCustomerID);

// advertisements
global using AdvertisementTuple = (string name, string description, int lengthInSeconds, decimal chargePerPlay);
global using ScheduledAdsTuple = (int scheduleShowingID, string advertisementName);

using System.Globalization;
namespace DataStorage;

public class FileAccess
{

  public static List<ConcessionMenuTuple> ReadConcessionMenuData()
  {
    string filePath = GetBasePath() + "ConcessionMenuData.txt";
    List<ConcessionMenuTuple> menuList = new();

    foreach (var line in File.ReadAllLines(filePath))
    {
      var x = line.Split(";");
      ConcessionMenuTuple item = (
        itemName: x[0],
        itemDescription: x[1],
        price: decimal.Parse(x[2], NumberStyles.Currency)
      );
      menuList.Add(item);
    }
    return menuList;
  }
  public static List<ConcessionSaleTuple> ReadConcessionSalesData()
  {
    string filePath = GetBasePath() + "ConcessionSalesData.txt";
    List<ConcessionSaleTuple> saleList = new();
    // TODO 
    foreach (var line in File.ReadAllLines(filePath))
    {
      var x = line.Split(";");
      ConcessionSaleTuple item = (
        soldDateTime: DateTime.Parse(x[0]),
        itemName: x[1],
        quantitySold: int.Parse(x[2]),
        revenueCollected: decimal.Parse(x[3], NumberStyles.Currency),
        preferredCustomerID: int.Parse(x[4])
      );
      saleList.Add(item);
    }
    return saleList;
  }

  public static void WriteConcessionMenuData(List<ConcessionMenuTuple> menuList)
  {
    string filePath = GetBasePath() + "ConcessionMenuData.txt";
    List<string> fileLines = new List<string>();
    foreach (var x in menuList)
    {
      string menuLineForfile =
        x.itemName + ";" +
        x.itemDescription + ";" +
        x.price.ToString("C2", CultureInfo.CurrentCulture); // properly handle the '$'

      fileLines.Add(menuLineForfile);
    }
    File.WriteAllLines(filePath, fileLines);
  }

  public static void WriteConcessionSalesData(List<ConcessionSaleTuple> soldConcessions)
  {
    string filePath = GetBasePath() + "ConcessionSalesData.txt";
    List<string> fileLines = new List<string>();
    foreach (var x in soldConcessions)
    {
      string saleLineForfile =
        x.soldDateTime.ToString() + ";" +
        x.itemName + ";" +
        x.quantitySold + ";" +
        x.revenueCollected.ToString("C2", CultureInfo.CurrentCulture) // properly handle the '$'
      + ";" + x.preferredCustomerID;
      fileLines.Add(saleLineForfile);
    }
    File.WriteAllLines(filePath, fileLines);
  }


  public static List<MovieTuple> ReadMovies()
  {
    string filePath = GetBasePath() + "MovieData.txt";
    List<MovieTuple> movies = new();
    // TODO 
    string[] lines = File.ReadAllLines(filePath);
    foreach (string line in lines)
    {
      string[] attributes = line.Split(";");
      MovieTuple newMovie = (
        title: attributes[0],
        runLengthMinutes: int.Parse(attributes[1]),
        advertisingMesssage: attributes[2],
        leads: attributes[3]
      );
    }


    return movies;
  }

  public static List<PreferredCustomerTuple> ReadPreferredCustomerData()
  {
    string filePath = GetBasePath() + "PreferredCustomerData.txt";
    List<PreferredCustomerTuple> customers = new();
    // TODO 
    return customers;
  }
  public static Dictionary<int, int> ReadTheaterRoomData()
  {
    string filePath = GetBasePath() + "TheaterRoomData.txt";
    Dictionary<int, int> theaterRooms = new();
    // TODO 
    string[] lines = File.ReadAllLines(filePath);

    foreach (string line in lines)
    {
      string[] attributes = line.Split(';');
      theaterRooms[int.Parse(attributes[0])] = int.Parse(attributes[1]);
    }
    return theaterRooms;
  }
  public static List<ShowingTuple> ReadScheduleData()
  {
    string filePath = GetBasePath() + "ScheduleData.txt";
    List<ShowingTuple> schedule = new();
    // TODO ]
    string[] lines = File.ReadAllLines(filePath);
    foreach (string line in lines)
    {
      string[] showingAttributes = line.Split(";");
      ShowingTuple newShowing = (
        showingID: int.Parse(showingAttributes[0]),
        showingDateTime: DateTime.Parse(showingAttributes[1]),
        ticketPrice: decimal.Parse(showingAttributes[2], NumberStyles.Currency),
        theaterRoom: int.Parse(showingAttributes[3]),
        movieTitle: showingAttributes[4]
      );

      schedule.Add(newShowing);
    }
    return schedule;
  }
  public static List<SoldTicketTuple> ReadSoldTicketData()
  {
    string filePath = GetBasePath() + "SoldTicketData.txt";
    List<SoldTicketTuple> ticketList = new();
    // TODO 
    return ticketList;
  }
  public static List<AdvertisementTuple> ReadAdvertisementData()
  {
    string filePath = GetBasePath() + "AdvertisementData.txt";
    List<AdvertisementTuple> adList = new();
    // TODO 
    return adList;
  }
  public static List<ScheduledAdsTuple> ReadAdvertisementScheduleData()
  {
    string filePath = GetBasePath() + "AdvertisementScheduleData.txt";
    List<ScheduledAdsTuple> adList = new();
    // TODO 
    return adList;
  }


  public static void WriteMovies(List<MovieTuple> movies)
  {
    string filePath = GetBasePath() + "MovieData.txt";
    // Turn each movie into a string and then write a list of movie strings to a file
    List<string> movieAsStrings = new();
    foreach (MovieTuple movie in movies)
    {
      string movieAsString = $"{movie.title};{movie.runLengthMinutes};{movie.advertisingMesssage};{movie.leads}";
      movieAsStrings.Add(movieAsString);
    }
    File.WriteAllLines(filePath, movieAsStrings);
  }
  public static void WritePreferredCustomerData(List<PreferredCustomerTuple> customers)
  {
    string filePath = GetBasePath() + "PreferredCustomerData.txt";
    // TODO 
  }
  public static void WriteTheaterRoomData(Dictionary<int, int> rooms)
  {
    string filePath = GetBasePath() + "TheaterRoomData.txt";
    List<string> roomAsStrings = new();
    foreach (KeyValuePair<int, int> room in rooms)
    {
      roomAsStrings.Add($"{room.Key};{room.Value}");

    }
    File.WriteAllLines(filePath, roomAsStrings);
  }
  public static void WriteScheduleData(List<ShowingTuple> schedule)
  {
    string filePath = GetBasePath() + "ScheduleData.txt";
    // TODO 
    List<string> lines = new();

    foreach (ShowingTuple showing in schedule)
    {
      string line = $"{showing.showingID};{showing.showingDateTime};{showing.ticketPrice:$0.00};{showing.theaterRoom};{showing.movieTitle}";
      lines.Add(line);
    }
    File.WriteAllLines(filePath, lines);
  }
public static void WriteSoldTicketData(List<SoldTicketTuple> soldTickets)
{
  string filePath = GetBasePath() + "SoldTicketData.txt";
  // TODO 
}
public static void WriteAdvertisementData(List<AdvertisementTuple> advertisements)
{
  string filePath = GetBasePath() + "AdvertisementData.txt";
  // TODO 
}
public static void WriteAdvertisementScheduleData(List<ScheduledAdsTuple> scheduleAds)
{
  string filePath = GetBasePath() + "AdvertisementScheduleData.txt";
  // TODO 
}

// do not change, makes datafiles discoverable between tests and user interface
public static string GetBasePath()
{
  if (Directory.GetCurrentDirectory().Contains("UserInterface"))
    return "../";
  if (Directory.GetCurrentDirectory().Contains("Test"))
    return "../../../../";

  return "./";
}
}
