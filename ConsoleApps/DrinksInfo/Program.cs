using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Menu.DrawMenu();

        string URI = Menu.GetCategory();

        using var client = new HttpClient();

        var response = await client.GetAsync(URI);

        try
        {
            response.EnsureSuccessStatusCode();
        }

        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
            return;
        }

        string json = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<DrinkResponse>(json);

        Menu.DrawChooseDrink();

        int i = 0;

        while (i < data.drinks.Count)
        {
            for (int j = 0; j < 10; j++)
            {  
                if (i + j >= data.drinks.Count)
                {
                    break;
                }
                Console.WriteLine($"{i + j}. {data.drinks[i + j].strDrink}");
            }

            Console.WriteLine("Press Enter to view the next page of drinks or type the index of a drink you wish to view more in detail:");

            var input = Console.ReadLine();

            if (input == "")
            {
                Console.Clear();
                Menu.DrawChooseDrink();
            }
            else
            {
                bool isNumber = int.TryParse(input, out int drinkIndex);

                if (isNumber && drinkIndex >= 0 && drinkIndex < data.drinks.Count)
                {
                    Console.WriteLine("swags"); 
                    Menu.ShowDrink(data.drinks[drinkIndex]);
                    Console.WriteLine("Returning to main menu...");
                        break;
                }

                Console.Clear();
                i -= 10;
                Console.WriteLine("Invalid input. Please enter a valid drink index or press Enter to continue.");
            }
            i += 10;
        }
        Console.Clear();
        Console.WriteLine("Returning to main menu...");
        await Main();
    }

}
    class DrinkResponse
    {
        public List<Drink> drinks { get; set; }
    }

    class Drink
    {
        public string strDrink { get; set; }
        public string strDrinkThumb { get; set; }
        public string idDrink { get; set; }
    }
static class Menu
{
    public static void ShowDrink(Drink drink)
    {
        Console.Clear();
        if (drink.idDrink != null);
        Console.WriteLine("You have selected drink number: " + drink.idDrink);
        if (drink.strDrink != null);
            Console.WriteLine("Drink Name: " + drink.strDrink);
        if (drink.strDrinkThumb != null);
        Console.WriteLine("Drink Image URL: " + drink.strDrinkThumb);

        if (drink.strDrink == null && drink.strDrinkThumb == null && drink.idDrink == null)
        {
            Console.WriteLine("No details available for this drink.");
        }

        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        Console.Clear();
    }
    public static void DrawMenu()
    {
        Console.WriteLine("****************************");
        Console.WriteLine("*        Drinks Menu       *");
        Console.WriteLine("****************************");
        Console.WriteLine("1. Alcoholic Drinks");
        Console.WriteLine("2. Non-Alcoholic Drinks");
        Console.WriteLine("3. Ordinary Drinks"); ;
        Console.WriteLine("4. Cocktails");
        Console.WriteLine("5. Cocktail Glass");
        Console.WriteLine("6. Chamnpagne Flutes");
        Console.WriteLine("Press any other key to exit.");
        Console.WriteLine("****************************");
    } 
    
    public static void DrawChooseDrink()
    {
        Console.WriteLine("*****************************************************");
        Console.WriteLine("Here are the drinks from your chosen category: ");
        Console.WriteLine("To view more information about a drink, please enter its number:");
        Console.WriteLine("To view more drinks press enter:");
        Console.WriteLine("*****************************************************");
    }


    public static string GetCategory()
    {
        var key = Console.ReadKey();

        Console.Clear();

        switch (key.KeyChar)
        {
            case '1':
                return "http://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Alcoholic";
            case '2':
                return "http://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic";
            case '3':
                return "http://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Ordinary_Drink";
            case '4':
                return "http://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Cocktail";
            case '5':
                return "http://www.thecocktaildb.com/api/json/v1/1/filter.php?g=Cocktail_glass";
            case '6':
                return "http://www.thecocktaildb.com/api/json/v1/1/filter.php?g=Champagne_flute";
            default:
                Environment.Exit(0);
                break;
        }
        return "";
    }
}
