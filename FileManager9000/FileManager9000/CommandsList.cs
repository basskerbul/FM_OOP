interface ICommandRecognition
{
    /// <summary>
    /// Распознает команды пользователя, вызывает нужный метод, вызывает окна
    /// </summary>
    /// <param name="command"></param>
    void List(string command);
}

class CommandsList: ICommandRecognition
{
    private Interaction interaction = new Interaction();
    private Functions functions = new Functions();
    private string path;
    private int page; 

    public CommandsList(string path, int page)
    {
        this.path = path;
        this.page = page;
    }

    public void List(string command)
    {
        string[] elements = command.Split(' ');

        //Следующая страница
        if (command == "next")
        {
            Console.Clear();
            interaction.OutputTree(path, page++);
            interaction.InformationOutput(path);
        }
        //Предыдущая страница
        else if (command == "back")
        {
            Console.Clear();
            interaction.OutputTree(path, page--);
            interaction.InformationOutput(path);
        }
        //Вызывает список команд
        else if (command == "help")
        {
            Console.Clear();
            interaction.CommandWindow();
        }
        //Переход по папкам
        else if (elements[0] == "cd")
        {
            if (elements[1] == "..")
            {
                //Проверка на попытку выйти на несуществующий уровень
                if (path == @"C:\")
                {
                    path = @"C:\";
                    Console.Clear();
                    interaction.OutputTree(path, page);
                    interaction.InformationOutput(path);
                    interaction.MessageOutput("Выход на уровень выше невозможен");
                }
                else
                {
                    string[] path_elements = path.Split('\\');
                    page = 0;
                    string new_path = "";
                    for (int i = 0; i < path_elements.Length - 2; i++)
                    {
                        new_path += $@"{path_elements[i]}\";
                    }
                    Console.Clear();
                    interaction.OutputTree(new_path, page);
                    interaction.InformationOutput(new_path);
                    path = new_path;
                }
            }
            else
            {
                page = 0;
                string new_path = $@"{path}{elements[1]}\";
                Console.Clear();
                interaction.OutputTree(new_path, page);
                interaction.InformationOutput(new_path);
                path = new_path;
            }
        }
        //Вызвать окно информации
        else if (elements[0] == "info")
        {
            string file = "";
            for (int i = 1; i < elements.Length; i++)
            {
                file += $"{elements[i]} ";
            }
            string new_path = $@"{path}{file}";
            Console.Clear();
            interaction.OutputTree(path, page);
            interaction.InformationOutput(new_path);
        }
        //Копирование и перенос папок/файлов
        else if (elements[0] == "copy")
        {
            string[] files = command.Split('"');
            string origin_path = path + files[1];

            //Копировать в то же мето
            if (elements[3] == "here")
            {
                string copy_path = path + files[3];

                functions.Copy(origin_path, copy_path);
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(copy_path);
            }
            //Копировать в папку по указанному пути
            else if (elements[3] == "to")
            {
                string copy_path = files[5] + files[3];
                functions.Copy(origin_path, copy_path);
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(copy_path);
            }
        }
        //Перенести
        else if (elements[0] == "transfer")
        {
            string[] files = command.Split('"');
            string origin_path = path + files[1];
            string new_path = $@"{files[3]}\{files[1]}";

            functions.Transfer(origin_path, new_path);
            Console.Clear();
            interaction.OutputTree(path, page);
            interaction.InformationOutput(new_path);
        }
        else
        {
            Console.Clear();
            interaction.OutputTree(path, page);
            interaction.InformationOutput(path);
            interaction.MessageOutput("Операция не удалась. Введите help");
        }
    }
}
