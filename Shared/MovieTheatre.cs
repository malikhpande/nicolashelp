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

// report data types

global using ConcessionsReportTuple = (string name, int sold, decimal revenue, int givenAway);

namespace Shared;

public static class MovieTheater
{
    public static List<ConcessionMenuTuple> ConcessionMenuList = new();
    public static List<ConcessionSaleTuple> ConcessionSaleList = new();


    public static readonly decimal salesTaxRate = 0.06512m;
    public static List<MovieTuple> MovieList = new();// List<(string,int,string,string)>();
    public static Dictionary<int, int> TheaterRoomCapacity = new();
    public static List<ShowingTuple> ScheduleList = new();
    public static List<SoldTicketTuple> SoldTicketList = new();
    public static List<PreferredCustomerTuple> PreferredCustomerList = new();
    public static List<AdvertisementTuple> AdvertisementList = new();
    public static List<ScheduledAdsTuple> ScheduledAdsList = new();

    public static void ReadDataInFromAllFiles()
    {
        MovieList = DataStorage.FileAccess.ReadMovies();
        TheaterRoomCapacity = DataStorage.FileAccess.ReadTheaterRoomData();
        ScheduleList = DataStorage.FileAccess.ReadScheduleData();
        PreferredCustomerList = DataStorage.FileAccess.ReadPreferredCustomerData();
        SoldTicketList = DataStorage.FileAccess.ReadSoldTicketData();
        ConcessionMenuList = DataStorage.FileAccess.ReadConcessionMenuData();
        ConcessionSaleList = DataStorage.FileAccess.ReadConcessionSalesData();
        AdvertisementList = DataStorage.FileAccess.ReadAdvertisementData();
        ScheduledAdsList = DataStorage.FileAccess.ReadAdvertisementScheduleData();
    }
    public static void WriteDataToAllFiles()
    {
        DataStorage.FileAccess.WriteMovies(MovieList);
        DataStorage.FileAccess.WriteTheaterRoomData(TheaterRoomCapacity);
        DataStorage.FileAccess.WriteScheduleData(ScheduleList);
        DataStorage.FileAccess.WritePreferredCustomerData(PreferredCustomerList);
        DataStorage.FileAccess.WriteSoldTicketData(SoldTicketList);
        DataStorage.FileAccess.WriteConcessionMenuData(ConcessionMenuList);
        DataStorage.FileAccess.WriteConcessionSalesData(ConcessionSaleList);
        DataStorage.FileAccess.WriteAdvertisementData(AdvertisementList);
        DataStorage.FileAccess.WriteAdvertisementScheduleData(ScheduledAdsList);
    }
    // Concessions
    public static void PurchaseMenuItem(string customerName, string itemWanted, int quantity, bool preferredCustomerPayWithPoints)
    {
        //global using ConcessionSaleTuple = (System.DateTime soldDateTime, string itemName, int quantitySold, decimal revenueCollected, int preferredCustomerID);
        decimal itemPrice = -1.00m;
        foreach (var item in ConcessionMenuList)
        {
            if (itemWanted == item.itemName)
            {
                itemPrice = item.price;
            }
        }
        if (itemPrice == -1.00m)
            throw new InvalidOperationException("Cant buy a product that isn't on the menu");
        //first option:
        ConcessionSaleList.Add((DateTime.Now, itemWanted, quantity, quantity * itemPrice, -1)); //-1 for non preffered customer id

        // //second option
        // ConcessionSaleTuple ct = (
        //     soldDateTime: DateTime.Now,
        //     itemName : itemWanted,
        //     quantitySold: quantity,
        //     revenueCollected: quantity*itemPrice,
        //     preferredCustomerID: -1
        // );
        // ConcessionSaleList.Add(ct);
    }

    public static string ConcessionReport3_AllReceipts()
    {
        //global using ConcessionSaleTuple = (System.DateTime soldDateTime, string itemName, int quantitySold, decimal revenueCollected, int preferredCustomerID);
        //public static List<ConcessionSaleTuple> ConcessionSaleList = new();
        string output = $"{"Time Sold:",25}{"Item Name:",20}{"Quantities Sold:",20}{"Revenue collected:",20}\n";
        foreach (var item in ConcessionSaleList)
        {
            output += $"{item.soldDateTime,25}{item.itemName,20}{item.quantitySold,20}{item.revenueCollected,20:$0.00}\n";
        }
        return output;

    }
    public static string ConcessionReport4_RevenueTotalsForAllDays()
    {
        Dictionary<DateOnly, decimal> revenuePerDay = new();
        foreach (ConcessionSaleTuple sale in ConcessionSaleList)
        {
            DateOnly saleDay = DateOnly.FromDateTime(sale.soldDateTime);
            if (revenuePerDay.ContainsKey(saleDay))
            {
                revenuePerDay[saleDay] += sale.revenueCollected; //add to existing sum
            }
            else
            {
                revenuePerDay[saleDay] = sale.revenueCollected; //new day
            }
        }

        string output = $"{"Day",20}{"Revenue",20}\n";
        foreach (KeyValuePair<DateOnly, decimal> dayRevenue in revenuePerDay)
        {
            string date = $"{dayRevenue.Key.Month}/{dayRevenue.Key.Day}/{dayRevenue.Key.Year}";
            output += $"{date,20}{dayRevenue.Value,20:$0.00}/n";
        }
        return output;
    }
    public static string ConcessionReport5_ItemTotalsPerDay(DateOnly givenDay)
    { //step 1 get sales for that day
        List<ConcessionSaleTuple> salesOnGivenDay = new();
        foreach (ConcessionSaleTuple sale in ConcessionSaleList)
        {
            DateOnly dayOfSale = DateOnly.FromDateTime(sale.soldDateTime);
            if (dayOfSale == givenDay)
            {
                salesOnGivenDay.Add(sale);
            }
        }
        //step 2, add together quantity and revenue

        Dictionary<string, int> quantityDictionary = new();
        Dictionary<string, decimal> revenueDictionary = new();
        string output = $"Item Sales for Day {givenDay.Month}/{givenDay.Day}/{givenDay.Year}\n";

        foreach (ConcessionSaleTuple sale in salesOnGivenDay)
        {

            if (quantityDictionary.ContainsKey(sale.itemName))
            {
                quantityDictionary[sale.itemName] += sale.quantitySold;
                revenueDictionary[sale.itemName] += sale.revenueCollected;
            }
            else
            {
                quantityDictionary[sale.itemName] = sale.quantitySold;
                revenueDictionary[sale.itemName] = sale.revenueCollected;
            }

            foreach (string itemName in quantityDictionary.Keys)
            {
                output += $"{itemName,20}{quantityDictionary[itemName],10}{revenueDictionary[itemName],10:$0.00}\n";
            }

        }
        return output;
    }


    //Scheduling
    public static void CreateShowingID(int ID, DateTime date, decimal ticketPrice, int theaterRoom, string movieTitle)
    {
        if (ScheduleList.Count >= 1 && ScheduleList[0].showingID == ID)
        {
            throw new ArgumentException("Cannot use the same ID on two movies", "ID");
        }
        ShowingTuple showingTuple1 = new()
        {
            showingID = ID,
            showingDateTime = date,
            ticketPrice = ticketPrice,
            theaterRoom = theaterRoom,
            movieTitle = movieTitle
        };

        ScheduleList.Add(showingTuple1);
    }

    public static void NewMovieRegistration(string title, int runLengthMinutes, string advertisingMesssage, string leads)
    {
        foreach (MovieTuple movie in MovieList)
        {
            if (movie.title == title)
            {
                throw new Exception("Cannot add duplicate movie title");
            }
        }
        MovieTuple newMovie = (
            title: title,
            runLengthMinutes: runLengthMinutes,
            advertisingMesssage: advertisingMesssage,
            leads: leads
        );
        MovieList.Add(newMovie);

    }


    //Advertisements
    public static string AdvertisingReport3_DailyShowingAndAdvertisementLength(DateOnly date)
    {
        // 	Produce a report, given a certain date, that shows the following data per showingID
        // 	- # of commercials total
        // 	- Sum of Minutes/Seconds 
        // 	- $ earned from Advertisements for that showing
        return "";
    }

    public static string AdvertisingReport4_DailyAdvertisingRevenue(DateOnly date)
    {
        // 	Produce a report, given a certain day, that shows the following data summations:
        // 	- AdvertisementTitle, # of Showings, Total$Revenue
        return "";
    }

    public static string AdvertisingReport5_MonthlyAdvertisingRevenue(DateOnly date)
    {
        // 	Produce a report, given a certain month, that shows the following data summations for each commercial shown: (each commercial should have its own line, and only be listed once)
        // 	- Title & Description
        // 	- Total # of showings
        // 	- Total $ of revenue

        // [Advertisement Title]          [Showings]     [Revenue]
        // DrWisdomTooth-17              4              $88.88
        // DrWisdomTooth-16              4              $80.00
        // Food Pantry #2                6              $0.00 
        // Bob's Fishing                 1              $0.50 
        return "";
    }

    public static void ScheduleAdForMovie(int showingID, string name)
    {
        // Schedule Advertisements for a ShowingID (schedule time & movie)
    }

    public static void RegisterNewAd(string name, string description, int lengthInSeconds, decimal chargePerPlay)
    {
        // Register NEW advertisement and their rates
    }



    //Tickets

    public static string TicketReport5_TicketSalesRevenue(DateOnly date)
    {
        //      Produce a report, for a given date, that shows the following data summations for each showingID:
        //  	- ShowTime,  MovieTitle, NumberTicketsSold, Sum$Collected, CountGivenAwayFreeToPreferredCustomers
        //  	(NumberTicketsSold should include the #s of free tickets.  Example 3 tickets paid for + 1 free ticket = 4 NumberTicketsSold)

        // example:
        // Show Time                Movie Title                   Revenue Collected             Given Away                    
        // 4/11/2024 6:35:00 PM     Dune: Part Two (2024)         $19,999.98                    0 
        return "";
    }
    public static void PreferredCustomerRegistration(int ID, string name, string email)
    {
        //-T5 - (Preferred Customer Registration)
        //    A Customer can register as a preferred customer.
        //    PreferredCustomerTuple = (int preferredCustomerID, string name, string email, int ticketPoints, int concessionPoints);

    }
    public static List<DailyShowingTuple> GetDailySchedule_Basic(System.DateOnly requestedDate)
    {
        //- T3 - (Daily Movies & Showtime Report) 
        //        When Customers select a date, they can see what movies are playing at which show times for that date.

        return new();
    }
    public static int HowManySeatsAreAvailableForShowing(int showingID)
    {
        //REQ T2 - (Seat Availability)
        //Customers(providing a schedule) can check ticket availability(is a show sold out already? How many seats are left?)

        return -1;
    }
    public static (int Sold, int Given) SumOfSoldTicketsForShowing(int showingID)
    {
        return new();
    }
    public static void TicketPurchase(int showingID, decimal revenueAmt)
    {
        // Customers can buy a ticket
    }
    public static void TicketPurchase(int showingID, decimal revenueAmt, int preferredID)
    {
        // Customers can buy a ticket
        // preferred customers get special treatment
    }

    public static decimal GetSalesTotal(int numberOfTickets, decimal ticketPrice)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) must include sales tax
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static decimal GetAdvertisementRevenue(int watchers, decimal ratePerWatcher)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) must NOT include sales tax
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static decimal GetTicketRevenueNoSalesTax(int numberOfTickets, decimal ticketPrice)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) must NOT include sales tax
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static decimal GetTotalRevenuePerShowing(int numberOfTickets, decimal ticketPrice, decimal ratePerWatcher)
    {
        // Requirements:
        // 1) expected to return the correct amount
        // 2) Per Showing:
        //      Advertisement Revenue (no sales tax)   +  TicketRevenue (no sales tax)
        // 3) must be rounded to the nearest penny  (up , down as appropriate)

        return -1m;
    }
    public static bool DoesMovieExist(string movieTitle, DateTime date, out ShowingTuple movie)
    {
        movie = new();
        return false;
    }

    public static bool CheckIfPreferredCustomer(string userName)
    {
        return false;
    }

    public static PreferredCustomerTuple GetPreferredCustomer(string userName)
    {
        return new();
    }

    public static ShowingTuple? GetMovieSchedule(int movieID, DateTime time)
    {
        return null;
    }

    public static List<string> GetScheduledMovies()
    {
        return [];
    }

    public static Dictionary<int, DateTime> AvailableShowingDatesByShowingId(string movieTitle)
    {
        return new();
    }

    public static void UpdatePreferredCustomer(int ticketNumber, PreferredCustomerTuple? prefferedCustomer, bool usePoints)
    {
    }
}