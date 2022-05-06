Console.ForegroundColor = ConsoleColor.Cyan;
Console.SetWindowSize(59, 30);
string path = @"C:\";
int page = 0;

CommandsList cl = new CommandsList(path, page);
Interaction inter = new Interaction();
inter.OutputTree(path, page);
inter.InformationOutput(path);

while (true)
{
    cl.List(inter.Input());
}
