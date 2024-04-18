namespace Test;
using Shared;

public class ConcessionTests
{
  //START TESTS - FinalProject 1
  [Fact]
  public void CanLoadConcessionMenuDataFromFile()
  {
    MovieTheater.ReadDataInFromAllFiles();
    Assert.NotEmpty(MovieTheater.ConcessionMenuList); //IF file file was read in OK, then List will not be empty
  }


  [Fact]
  public void CanLoadConcessionSalesDataFromFile()
  {
    MovieTheater.ReadDataInFromAllFiles();
    Assert.NotEmpty(MovieTheater.ConcessionSaleList); //IF file file was read in OK, then List will not be empty
  }

  //PurchaseMenuItem
  [Fact]
  public void ConcessionPurchaseItem_NormalCustomer()
  {
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.ConcessionSaleList = new();
    MovieTheater.PurchaseMenuItem("Bob", "Large Soda", 5, false);
    Assert.Single(MovieTheater.ConcessionSaleList);
  }

  //PurchaseMenuItem
  [Fact]
  public void ConcessionPurchaseItemDoesNotExist_NormalCustomer()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.ConcessionSaleList = new();
    MovieTheater.ConcessionMenuList = new(); //Large Soda will not exist any more
    try
    {
      //act
      MovieTheater.PurchaseMenuItem("Bob", "Large Soda", 5, false);
    }
    catch
    {
      //assert
      Assert.True(true); //We expect an exception to be caught
      return;
    }
    Assert.Fail("Expected an exception thrown since menuItem does not exist");
  }
  //PurchaseMenuItem
  [Fact]
  public void ConcessionPurchaseItemQuantityPrice_Check()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    MovieTheater.ConcessionSaleList = new();
    //ACT
    MovieTheater.PurchaseMenuItem("Bob", "Large Soda", 4, false);
    //ASSERT    Assert.Equal(4, MovieTheater.ConcessionSaleList[0].quantitySold);
    Assert.Equal(4 * 5.00M, MovieTheater.ConcessionSaleList[0].revenueCollected);
  }

  [Fact]
  public void ConcessionDailyReportOption3_CanDisplaySales()
  {
    // arrange
    MovieTheater.ReadDataInFromAllFiles();
    DateTime date = new DateTime(2024, 4, 10);
    ConcessionSaleTuple firstSale = (
      soldDateTime: date, 
      itemName: "Large Soda", 
      quantitySold: 5, 
      revenueCollected: 50m, 
      preferredCustomerID: -1
    );
    MovieTheater.ConcessionSaleList = [firstSale];

    // act
    string dailyReport = MovieTheater.ConcessionReport3_AllReceipts();

    // assert
    Assert.Contains("Large Soda", dailyReport); // should have a line about the large soda
    Assert.Contains("$50.00", dailyReport); // should have the revenue printed with proper formatting
  }
  //END TESTS - FinalProject 1


  // START TESTS - Final Project 2
  [Fact]
  public void ConcessionDailyReportOption4_CanDisplaySales()
  {
    //arrange
    MovieTheater.ReadDataInFromAllFiles();
    DateTime date = new DateTime(2024, 4, 10);
    ConcessionSaleTuple firstSale = (date, "Large Soda", 5, 50m, -1);
    ConcessionSaleTuple secondSale = (date, "Large Soda", 2, 20m, -1);
    MovieTheater.ConcessionSaleList = [firstSale, secondSale];
    //act
    string dailyReport = MovieTheater.ConcessionReport4_RevenueTotalsForAllDays();

    //assert
    Assert.Contains("4/10/2024", dailyReport);
    Assert.Contains("$70.00", dailyReport); //both sales should be added together for this day
  }

  [Fact]
  public void ConcessionReport5_CanDisplayItemsPerDay()
  {
    //arrange
    MovieTheater.ReadDataInFromAllFiles();
    DateTime date = new DateTime(2024, 4, 10);
    ConcessionSaleTuple firstSale = (date, "Large Soda", 5, 50m, -1);
    ConcessionSaleTuple secondSale = (date, "Large Soda", 2, 20m, -1);
    MovieTheater.ConcessionSaleList = [firstSale, secondSale];

    //act
    string dailyReport = MovieTheater.ConcessionReport5_ItemTotalsPerDay(DateOnly.FromDateTime(date));

    //assert
    Assert.Contains("4/10/2024", dailyReport); // daily report should report which day was selected in the first row (not every row)
    Assert.Contains("Large Soda", dailyReport); // daily report should state that large sodas were purchased
    Assert.Contains("7", dailyReport); // 7 large sodas were sold in total
    Assert.Contains("$70.00", dailyReport); // 70 dollars were made from large sodas
  }
  // End TESTS - Final Project 2

}