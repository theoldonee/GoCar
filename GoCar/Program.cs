using GoCar;

Console.WriteLine("Would you like to enter a path?\nEnter 1 for yes and 2 for no. ");
string choice = Console.ReadLine();

if(choice == "2")
{
    FileManager.LoadFile(true);
}
else
{
    Console.WriteLine("Enter path: ");
    FileManager.alternatePath = Console.ReadLine();
    FileManager.LoadFile(false);

}