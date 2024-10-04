using Scraper;
using Database;
using Models;
using System;
  
  
   class Program
{
    static void Main()
    {
        var url = "https://www.tbca.net.br/base-dados/composicao_estatistica.php";
        var scraper = new FoodScraper();
        var foodItems = scraper.ScrapeFoodData(url);

      
        string dbFilePath = "composicao.db";
        var database = new SqliteDatabase(dbFilePath);

        database.InitializeDatabase();

       
        database.SaveFoodData(foodItems);

        Console.WriteLine("Dados salvos com sucesso no banco de dados SQLite.");
    }
}