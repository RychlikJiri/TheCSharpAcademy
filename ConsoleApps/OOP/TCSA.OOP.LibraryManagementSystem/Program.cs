//See https://aka.ms/new-console-template for more informationvar 
using Spectre.Console;

var books = new List<string>()
{
    "The Great Gatsby", "To Kill a Mockingbird", "1984", "Pride and Prejudice", "The Catcher in the Rye", "The Hobbit", "Moby-Dick", "War and Peace", "The Odyssey", "The Lord of the Rings", "Jane Eyre", "Animal Farm", "Brave New World", "The Chronicles of Narnia", "The Diary of a Young Girl", "The Alchemist", "Wuthering Heights", "Fahrenheit 451", "Catch-22", "The Hitchhiker's Guide to the Galaxy"
};
while(true){
var menuChoices = new string[3] { "View Books", "Add Book", "Delete Book" };
var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("What do you want to do next?")
        .AddChoices(menuChoices));

if(choice == "View Books")
{
    AnsiConsole.MarkupLine("[underline yellow]Books in Library:[/]");
    foreach(var book in books)
    {
        AnsiConsole.MarkupLine($"[green]{book}[/]");
    }
}
else if(choice == "Add Book")
{
    var newBook = AnsiConsole.Ask<string>("Enter the name of the book to add:");
    books.Add(newBook);
    AnsiConsole.MarkupLine($"[green]{newBook} has been added to the library.[/]");
}
else if(choice == "Delete Book")
{
    var bookToDelete = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Select a book to delete:")
        .AddChoices(books));

    books.Remove(bookToDelete);
    AnsiConsole.MarkupLine($"[red]{bookToDelete} has been removed from the library.[/]");
}}
