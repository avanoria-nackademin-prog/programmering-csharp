List<string> names = [];

while (true)
{
    Console.Write("Ange namn: ");
    string? nameInput = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(nameInput))
        names.Add(nameInput.Trim());

    Console.Write("Ange en till medlem? (y/n): ");
    string? option = Console.ReadLine();

    Console.Clear();

    if (option?.Trim().Equals("n", StringComparison.OrdinalIgnoreCase) == true)
        break;
}

Console.WriteLine("Gruppens medlemmar:");

foreach (string name in names)
    Console.WriteLine($"- {name}");


