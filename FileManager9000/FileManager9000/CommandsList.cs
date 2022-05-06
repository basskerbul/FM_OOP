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
    private Search search = new Search();
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
        string[] file_name = command.Split('"');

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
            string new_path = $@"{path}{file_name[1]}";
            Console.Clear();
            interaction.OutputTree(path, page);
            interaction.InformationOutput(new_path);
        }
        //Копирование и перенос папок/файлов
        else if (elements[0] == "copy")
        {
            string origin_path = path + file_name[1];

            //Копировать в то же мето
            if (elements[3] == "here")
            {
                string copy_path = path + file_name[3];

                functions.Copy(origin_path, copy_path);
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(copy_path);
            }
            //Копировать в папку по указанному пути
            else if (elements[3] == "to")
            {
                string copy_path = file_name[5] + file_name[3];
                functions.Copy(origin_path, copy_path);
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(copy_path);
            }
        }
        //Перенести
        else if (elements[0] == "transfer")
        {
            string origin_path = path + file_name[1];
            string new_path = $@"{file_name[3]}\{file_name[1]}";

            functions.Transfer(origin_path, new_path);
            Console.Clear();
            interaction.OutputTree(path, page);
            interaction.InformationOutput(new_path);
        }
        //Поиск
        else if(elements[0] == "find")
        {
            search.Area = path;
            string? result = search.TrySearch(file_name[1], path);
            if (result != null)
            {
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(result);
                interaction.SearchWindow(file_name[1], result);
            }
            else
            {
                Console.Clear();
                interaction.OutputTree(path, page);
                interaction.InformationOutput(path);
                interaction.MessageOutput("По запросу ничего не найдено");
            }
            
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
