using System;
using System.Data;
using System.Globalization;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Shared;

internal class Program
{
  private static void Main()
  {
    Shared.MovieTheater.ReadDataInFromAllFiles();
    MainMenu();
    Shared.MovieTheater.WriteDataToAllFiles();

  }
  public static void MainMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Main Menu***");
      string menu = "1-Ticket Window\n" +
                      "2-Concession Stand\n" +
                      "3-Advertisement Controls\n" +
                      "4-Scheduling Controls\n" +
                      "5-Theaterwide Controls\n" +
                      "6-Save and Exit\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 6);
      if (choice == 1)//Ticket Window
      {
        // TicketWindowMenu();
      }
      else if (choice == 2)//Concession Stand
      {
        ConcessionMenu();
      }
      else if (choice == 3)//Advertisment Controls
      {
        // AdvertisementMenu();
      }
      else if (choice == 4)//Scheduling Controls
      {
        SchedulingControlsMenu();
      }
      else if (choice == 5)//Theaterwide Controls
      {
        Console.Clear();
        DateOnly selectedDay = getDateOnlyWillLoop("Which day would you like to see reported");
        Console.WriteLine(MovieTheater.ConcessionReport5_ItemTotalsPerDay(selectedDay));
        PressKeyToContinue("Press any key to continue");
      }
      else if (choice == 6)// Save and Exit
      {
        return;
      }
    }
  }
  public static void ConcessionMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Concession Window Menu***");
      string menu = "1-View Menu Items\n" +
                      "2-Purchase a Concession\n" +
                      "3-All Sales Report\n" +
                      "4-Revenue Per Day Report\n" +
                      "5-Item Sold for a Given Day\n" +
                      "6-Return to Main Menu\n" +
                      "What would you like to do?";
      int choice = getIntWillLoop(menu, 1, 6);
      if (choice == 1)//Print Menu
      {
        Console.Clear();
        Console.WriteLine("****Concessions Menu****");
        Console.WriteLine($"{"NAME",-30} {"DESCRIPTION",-30} {"PRICE",-10:C2}");
        foreach (var mi in MovieTheater.ConcessionMenuList)
        {
          Console.WriteLine($"{mi.itemName,-30} {mi.itemDescription,-30} {mi.price,-10:C2}");
        }
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey(true);
      }
      if (choice == 2)//Purchase Concession Item
      {
        Console.Clear();
        Console.WriteLine("***Purchase Concessions***");
        // display items and walk user through purchase
        System.Console.WriteLine("Available concessions menu");
        Console.WriteLine($"{"#",-3}{"NAME",-30} {"DESCRIPTION",-30} {"PRICE",-10:C2}");
        for (int i = 0; i < MovieTheater.ConcessionMenuList.Count; i++)
        {
          System.Console.WriteLine($"{i + 1,-2} {MovieTheater.ConcessionMenuList[i].itemName,-30} {MovieTheater.ConcessionMenuList[i].itemDescription,-31}{MovieTheater.ConcessionMenuList[i].price,-10:C2}");

        }


        int toBuyID = getIntWillLoop("Enter the item # you wish to buy", 1, MovieTheater.ConcessionMenuList.Count);

        int toQuantitytoBuy = getIntWillLoop("How many do you want to buy?", 1, 999);
        //Calculate the bill 
        decimal billamt = MovieTheater.ConcessionMenuList[toBuyID - 1].price * toQuantitytoBuy;

        //Ask for name
        System.Console.WriteLine("What is the customer's name?");
        string custname = Console.ReadLine();

        //Do the purchase

        MovieTheater.PurchaseMenuItem(custname,
           MovieTheater.ConcessionMenuList[toBuyID - 1].itemName, toQuantitytoBuy, false);

        System.Console.WriteLine($"Thank you for buying {toQuantitytoBuy} of {MovieTheater.ConcessionMenuList[toBuyID - 1].itemName} for {billamt} ");

        PressKeyToContinue("\nTransaction Complete.\nPress any key to continue.");
      }
      if (choice == 3)//Receipts from All Sales
      {
        Console.Clear();
        System.Console.WriteLine("***All Sales Report***");
        Console.WriteLine(MovieTheater.ConcessionReport3_AllReceipts());
        PressKeyToContinue("Hit any key to move on");
      }
      if (choice == 4)//Revenue Totals For All Days
      {
        Console.Clear();
        System.Console.WriteLine("***Revenute Report Per Day***");
        Console.WriteLine(MovieTheater.ConcessionReport4_RevenueTotalsForAllDays());
        PressKeyToContinue("Hit any key to move on");
      }
      if (choice == 5)//Display Item Revenue For A Given Day
      {
        Console.Clear();
        // Have the user input a date
        DateOnly selectedDay = getDateOnlyWillLoop("Which day would you like to see reported?");

        PressKeyToContinue("Hit any key to move on");
      }
      if (choice == 6)//return to main menu
      {
        return;
      }
    }
  }

  public static DateOnly getDateOnlyWillLoop(string prompt)
  {
    System.Console.WriteLine(prompt);
    System.Console.WriteLine("Format like MM/DD/YYYY");
    try
    {
      string input = Console.ReadLine();
      return DateOnly.Parse(input);
    }
    catch
    {
      System.Console.WriteLine("That was invalid");
      return getDateOnlyWillLoop(prompt);
    }
    return new DateOnly();
  }
  public static DateOnly getDateOnlyWillLoop(string prompt, List<DateOnly> dayOnlyOptions)
  {
    for (int i = 0; i < dayOnlyOptions.Count; i++)
    {
      System.Console.WriteLine($"{i + 1}; {dayOnlyOptions[i]}");
    }
    int choice = getIntWillLoop(prompt, 1, dayOnlyOptions.Count + 1);
    return dayOnlyOptions[choice - 1];
  }

  public static int getIntWillLoop(string prompt, int min, int max)
  {
    while (true)
    {
      Console.WriteLine(prompt);
      int number;
      if (int.TryParse(Console.ReadLine(), out number))
      {
        if (number >= min && number <= max)
        {
          return number;
        }
      }
      Console.Write("Invalid. Please enter valid number: ");
    }
  }

  public static decimal getDecimalWillLoop(string prompt, int min, int max)
  {
    while (true)
    {
     System.Console.WriteLine(prompt); 
     decimal number;
     if (decimal.TryParse(Console.ReadLine(), out number))
     {
      if(number >= min && number <= max)
      {
        return number;
      }
     }
    }
    //todo
    return -1m;
  }

  public static bool GetBoolWillLoop(string prompt)
  {
    while (true)
    {
      Console.WriteLine(prompt);
      string input = Console.ReadLine().ToUpper();
      if (input == "YES" || input == "Y" || input.ToLower() == "true" || input.ToLower() == "t") return true;
      else if (input == "NO" || input == "N" || input.ToLower() == "false" || input.ToLower() == "f") return false;
      Console.Write("Invalid.  Please enter a valid True/False/Yes/No answer");
    }
  }


  public static void PressKeyToContinue(string prompt)
  {
    while (Console.KeyAvailable)
    {
      Console.ReadKey(true);
    }
    Console.WriteLine(prompt);
    Console.ReadKey(true);
  }

  public static void SchedulingControlsMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("***Scheduling Controls***");
      string menu = "1-New Movie\n" +
                      "2-Create ShowingID\n" +
                      "3-Return to Main Menu\n" +
                      "What would you like to do? ";
      int choice = getIntWillLoop(menu, 1, 3);
      if (choice == 1) //New Movie
      {
        Console.Clear();
        Console.WriteLine("***Register New Movie***");
        System.Console.WriteLine("input movie title");
        string title = Console.ReadLine();

        int runLengthMinutes = getIntWillLoop("Input run length in minutes", 1, int.MaxValue);
        Console.WriteLine("Input advertising message");
        string advertisingMesssage = Console.ReadLine();
        System.Console.WriteLine("Input lead actors");
        string leads = Console.ReadLine();

        try
        {
          MovieTheater.NewMovieRegistration(title, runLengthMinutes, advertisingMesssage, leads);
        }
        catch (Exception e)
        {
          System.Console.WriteLine(e.Message);
          System.Console.WriteLine("Error while adding movie");
        }

        PressKeyToContinue("\nPress any key to continue.");
      }

      if (choice == 2) //Create ShowingID
      {
        Console.Clear();
        Console.WriteLine("***Register New Showing ID***");
        // get a new showing ID from the user
        System.Console.WriteLine("Insert a number of the showing ID.");
        int movieNumber = int.Parse(Console.ReadLine());
        // have the user input the date and time of the showing
        System.Console.WriteLine("Insert the date and time of the showing in the format MM/DD/YYYY HH:MM:SS ");
        string date = Console.ReadLine();
        DateTime dateTime = DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        // use getDecimalWillLoop to have the user input a ticket price
        decimal ticketPrice = getDecimalWillLoop("Insert the ticket price.", 0, int.MaxValue);
        // have the user select a theater room (implement getValidTheaterRoomWillLoop)
        int theatreid = getValidTheaterRoomWillLoop("Please enter the theatre ID.");
        System.Console.WriteLine("What is the name of the movie?");
        string movieTitle = Console.ReadLine();

        // use MovieTheater.CreateShowingID to create the showing
        //    - if MovieTheater.CreateShowingID throws an exception, let the user know something went wrong and keep going.
        try
        {
          System.Console.WriteLine("Attempting to create showing ID.");
          MovieTheater.CreateShowingID(movieNumber, dateTime, ticketPrice, theatreid, movieTitle);
        }
        catch (Exception e)
        {
          System.Console.WriteLine("Something went wrong, please try again.");

        }
      }
      if (choice == 3)//return to main menu
      {
        return;
      }
    }


  }
  public static int getValidTheaterRoomWillLoop(string prompt)
  {
    System.Console.WriteLine(prompt);
    foreach (KeyValuePair<int, int> room in MovieTheater.TheaterRoomCapacity)
    {
      System.Console.WriteLine($"{room.Key,4}: capacity {room.Value}");

    }
    System.Console.WriteLine("Input a room number:");
    string userInputNumber = Console.ReadLine();
    try
    {
      int roomNumber = int.Parse(userInputNumber);

      if (MovieTheater.TheaterRoomCapacity.ContainsKey(roomNumber))
      {
        return roomNumber;
      }
      else
      {
        System.Console.WriteLine("That is not a valid room number");
        return getValidTheaterRoomWillLoop(prompt);
      }
    }
    catch
    {
      System.Console.WriteLine("Invalid number, try again");
    return getValidTheaterRoomWillLoop(prompt);
    }
  }
  // show the user a list of theater rooms
  // allow them to input a number to select one
  // return the ID of the theater room they selected
}
