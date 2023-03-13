using System;
using System.Collections.Generic;
using System.IO;

class Dictionary
{
    private Dictionary<string, List<string>> words;
    private string language1;
    private string language2;

    public Dictionary(string language1, string language2)
    {
        this.language1 = language1;
        this.language2 = language2;
        words = new Dictionary<string, List<string>>();
    }

    public void AddWord(string word, string translation)
    {
        if (words.ContainsKey(word))
        {
            words[word].Add(translation);
        }
        else
        {
            words.Add(word, new List<string> { translation });
        }
    }

    public void ReplaceWord(string oldWord, string newWord)
    {
        if (words.ContainsKey(oldWord))
        {
            words.Add(newWord, words[oldWord]);
            words.Remove(oldWord);
        }
    }

    public void ReplaceTranslation(string word, string oldTranslation, string newTranslation)
    {
        if (words.ContainsKey(word) && words[word].Contains(oldTranslation))
        {
            words[word].Remove(oldTranslation);
            words[word].Add(newTranslation);
        }
    }

    public void RemoveWord(string word)
    {
        words.Remove(word);
    }

    public void RemoveTranslation(string word, string translation)
    {
        if (words.ContainsKey(word))
        {
            words[word].Remove(translation);
            if (words[word].Count == 0)
            {
                words.Remove(word);
            }
        }
    }

    public List<string> TranslateWord(string word)
    {
        if (words.ContainsKey(word))
        {
            return words[word];
        }
        return new List<string>();
    }

    public void ExportToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var pair in words)
            {
                writer.WriteLine(pair.Key);
                foreach (var translation in pair.Value)
                {
                    writer.WriteLine("\t" + translation);
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Словарь");

        Dictionary dict = null;
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Выберите вариант:");
            Console.WriteLine("1. Создать новый словарь");
            Console.WriteLine("2. Добавить слово и его перевод");
            Console.WriteLine("3. Заменить слово");
            Console.WriteLine("4. Заменить перевод");
            Console.WriteLine("5. Удалить слово");
            Console.WriteLine("6. Удалить перевод");
            Console.WriteLine("7. Перевести слово");
            Console.WriteLine("8. Экспорт словаря в файл");
            Console.WriteLine("9. Выход");

            int option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Введите название первого языка:");
                    string language1 = Console.ReadLine();
                    Console.WriteLine("Введите название второго языка:");
                    string language2 = Console.ReadLine();
                    dict = new Dictionary(language1, language2);
                    Console.WriteLine("Словарь создан");
                    break;
                case 2:
                    if (dict != null)
                    {
                        Console.WriteLine("Введите слово:");
                        string word = Console.ReadLine();
                        Console.WriteLine("Введите перевод:");
                        string translation = Console.ReadLine();
                        dict.AddWord(word, translation);
                        Console.WriteLine("Добавлено слово и перевод");
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 3:
                    if (dict != null)
                    {
                        Console.WriteLine("Введите старое слово:");
                        string oldWord = Console.ReadLine();
                        Console.WriteLine("Введите новое слово:");
                        string newWord = Console.ReadLine();
                        dict.ReplaceWord(oldWord, newWord);
                        Console.WriteLine("Слово заменено");
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 4:
                    if (dict != null)
                    {
                        Console.WriteLine("Введите слово:");
                        string word = Console.ReadLine();
                        Console.WriteLine("Введите старый перевод:");
                        string oldTranslation = Console.ReadLine();
                        Console.WriteLine("Введите новый перевод:");
                        string newTranslation = Console.ReadLine();
                        dict.ReplaceTranslation(word, oldTranslation, newTranslation);
                        Console.WriteLine("Перевод заменен");
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 5:
                    if (dict != null)
                    {
                        Console.Write("Введите слово: ");
                        string word = Console.ReadLine();
                        dict.RemoveWord(word);
                        Console.WriteLine("Слово удалено");
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 6:
                    if (dict != null)
                    {
                        Console.Write("Введите слово: ");
                        string word = Console.ReadLine();
                        Console.Write("Введите перевод: ");
                        string translation = Console.ReadLine();
                        dict.RemoveTranslation(word, translation);
                        Console.WriteLine("Перевод удален");
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 7:
                    if (dict != null)
                    {
                        Console.WriteLine("Введите слово:");
                        string word = Console.ReadLine();
                        List<string> translations = dict.TranslateWord(word);
                        if (translations.Count == 0)
                        {
                            Console.WriteLine("Переводы не найдены");
                        }
                        else
                        {
                            Console.Write("Переводы: ");
                            foreach (string translation in translations)
                            {
                                Console.WriteLine(translation);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 8:
                    if (dict != null)
                    {
                        Console.Write("Пожалуйста, введите имя файла: ");
                        string fileName = Console.ReadLine();
                        dict.ExportToFile(fileName);
                        Console.WriteLine("Словарь экспортирован в файл");
                    }
                    else
                    {
                        Console.WriteLine("Сначала создайте словарь");
                    }
                    break;
                case 9:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный вариант");
                    break;
            }
        }
    }
}