﻿//### Файловый менеджер должен иметь возможность:
//*показывать содержимое дисков; +
//*создавать папки / файлы; +
//*удалять папки / файлы; +
//*переименовывать папки / файлы; +
//*копировать / переносить папки / файлы; +
//*вычислять размер папки/файла; +
//*производить поиск по маске (с поиском по подпапкам);
//*для текстовых файлов готовить статические данные (кол-во слов, 
//кол-во строк, кол-во абзацев, кол-во символов с пробелами, кол-во слов без пробелов).

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

interface IReturnSize
{
    /// <summary>
    /// Вычисляет размер файла/папки
    /// </summary>
    /// <returns></returns>
    long Size(string path);
}
