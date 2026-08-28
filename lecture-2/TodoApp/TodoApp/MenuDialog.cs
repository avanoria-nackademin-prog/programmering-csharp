namespace TodoApp;

internal class MenuDialog
{
    private static readonly List<string> _todos = [];


    public static void WelcomeMessage()
    {
        Console.WriteLine("Välkommen till Todo-Appen!");
        Console.WriteLine("");
    }

    public static void MainMenu()
    {
        bool running = true;

        do
        {
            Console.WriteLine("##### HUVUDMENY #####");
            Console.WriteLine("1. Lägg till aktivitet");
            Console.WriteLine("2. Visa aktiviteter");
            Console.WriteLine("0. Avsluta Applikationen");
            Console.WriteLine("");
            Console.Write("Ange ett val: ");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    AddTodoItem();
                    break;

                case "2":
                    ViewTodos();
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    InvalidOption();
                    break;
            }

            Console.Clear();

        } while (running);
    }


    public static void AddTodoItem()
    {
        Console.Clear();
        Console.WriteLine("Ange aktivitet:");
        string? activity = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(activity))
            _todos.Add(activity);
    }

    public static void ViewTodos()
    {
        foreach (var activity in _todos)
        {
            Console.WriteLine(activity);
        }

        Console.ReadKey();
    }

    public static void InvalidOption()
    {
        Console.WriteLine("Felaktigt val, försök igen.");
        Console.ReadKey();
    }
}

