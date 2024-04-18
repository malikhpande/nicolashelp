# MovieTheater_1405Final

Skill Areas:

1. Data Written To Files
2. Data Read-in from Files
3. Segregation of Layers:  "Data" vs "Library" vs "Presentation" vs "Unit Tests"
4. Console Display
5. User Data Entry Validation & Control
6. Looping
7. Exception Handling
8. Test Cases - appropriate coverage
9. Bug-Free Code!


Application Features:

150% possible
Grading: 150% possible
10% - File Access  (read in & write out correctly/consistently)
	(provided with tuple definitions & method signatures, perhaps 2 working read/write methods?)
15% - UserInterface
	- Data Entry:  Menu selections protected from errors (10%)
	- Useable UserInterface:  (5%)
		Multiple Menu Screens work seamlessly
		(ranked between: Well-designed 5% - Poorly Designed 0%)
20% - Test Cases
		17% - Instructor Provided Tests 
			- - All Pass
		03% - Student Created Tests:
			- - commented to explain what/why they intend to test, and how this test proves the requirements are met
			- - appropriately created/designed (they test the 'right' things)
			- - pass
20% - C# Programming Fundamentals:  (student provides line# for grading sample)
	- FOR loop
	- FOREACH loop
	- While / do-while loop
	- List
	- Dictionary
	- Console Layout Control
	- Method ReUse (example: daily reports re-used for monthly reporting)
	- Exception Handling
	- Comments for explanation (quality)

85% - Functionality
	TICKET WINDOW
	- T1 - (Purchase Ticket)
			Customers can buy a ticket
			Customers can buy a ticket and update their preferred customer bonus points
	- T2 - (Seat Availability)
			Customers (providing a schedule) can check ticket availability (is a show sold out already?  How many seats are left?)
	- T3 - (Daily Movies & Showtime Report) 
			When Customers select a date, they can see what movies are playing at which show times for that date.
	- T4 - (Daily Movies & Showtime Report -> Movie Details)
			After a Customer sees what movies are playing (for that date), then the Customer can select to see the details on any movie in that list.
	- T5 - (Preferred Customer Registration)
			A Customer can register as a preferred customer.
	- T6 - (Daily Ticket Sales Revenue Report)
			Produce a report, for a given date, that shows the following data summations for each showingID:
			- ShowTime,  MovieTitle, NumberTicketsSold, Sum$Collected, CountGivenAwayFreeToPreferredCustomers
			(NumberTicketsSold should include the #s of free tickets.  Example 3 tickets paid for + 1 free ticket = 4 NumberTicketsSold)
	CONCESSION STAND
	 - C1 - (Menu Inquiry)
	 - C2 - (Purchase Menu Item)
			Customer can buy X quantity of an item and pay for it
			Preferred Customer can buy X quantity of an item and pay for it
			Preferred Customer can buy an item with Reward points (if balance is sufficent)
	 - C3 - (Daily Report)
			Creates a report sumarizing a day's concession activity, one line for each item that was sold:
			- Item Name, Qty Sold, SumRevenueCollected, CountGivenAwayFreeToPreferredCustomers
	ADVERTISEMENT CONTROLS
	- A1 - (Register NEW advertisement(s) and their rates)
	- A2 - (Schedule Advertisements for a ShowingID (schedule time & movie))
	- A3 - (Daily Showing & Advertisement Length Report)
			Produce a report, given a certain date, that shows the following data per showingID
			- # of commercials total
			- Sum of Minutes/Seconds 
			- $ earned from Advertisements for that showing
	- A4 - (Daily Advertising Revenue Report)
			Produce a report, given a certain day, that shows the following data summations:
			- AdvertisementTitle, # of Showings, Total$Revenue
	- A5 - (Monthly Advertising Revenue Report)
			Produce a report, given a certain month, that shows the following data summations for each commercial shown: (each commercial should have its own line, and only be listed once)
			- Title & Description
			- Total # of showings
			- Total $ of revenue
	SCHEDULING CONTROLS
	- S1 - (New Movie)
			Administrator can add a new movie into the system
  - S2 - (Create ShowingIDs)
			Administrator can create a new ShowingID
				- links up a showing DateTime, theaterRoomID, TicketPrice and MovieTitle
	THEATERWIDE CONTROLS
	- T1 - (Daily Revenue Report)
			Produce a report, given a certain day, that shows the following data summations:
			- Date, 
			- - Daily Ticket Sales Revenue Report
			- - Daily Advertising Revenue Report
			- - Daily Concession Revenue Report
			- Grand Total Revenue for the day (tickets+ads+concessions)
	- T2 - (Monthly Revenue Report)
			Produce a report, given a certain month, that shows the following data summations:
			- Month
			- - Monthly Ticket Revenue
			- - Monthly Advertising Revenue
			- - Monthly Concession Revenue
			- Grand Total Revenue for the month
			


menu tree:

- Main Menu
  - TicketWindowMenu
    - PurchaseTicket
    - Seat Availability
    - Movie and Showtime Report
    - Prefered Customer Registration
    - Daily Ticket Sales Revenue

  - ConcessionMenu
    - View Menu
    - Purchase Concession Item
      - item, amount, prefered customer id
    - Concession report
      - all sales report
      - per day report (do all days?)

  - AdvertismentMenu
    - create new advertisement
    - schedule advertisement
      - name, description, length, charge
    - daily advertizing report
    - monthly advertizing report
    
  - SchedulingControlsMenu
    - new movie
    - new showing (when, ticket price, theater, movie title)
    
  - TheaterWideMenu
    - Daily Revenue
    - Monthy Revenue