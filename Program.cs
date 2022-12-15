using Newtonsoft.Json;
using Pro6;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Xml.Serialization;

string cStr;
string cStritog;

do
{
    Console.Clear();
    Console.WriteLine("Введите путь до файла, который вы хотите открыть");
    Console.WriteLine("------------------------------------------------");
    cStr = Console.ReadLine();
    List<Student> students = new List<Student>();

    if (cStr.Contains(".txt"))
    {
        students = readtxt(cStr, students);
    }
    else if (cStr.Contains(".json"))
    {
        students = readjson(cStr, students);
    }
    else if (cStr.Contains(".xml"))
    {
        students = readxls(cStr, students);
    }
    else
    {
        Console.WriteLine("Неверный тип файла");
    }


    ConsoleKeyInfo key = Console.ReadKey();
    if (key.Key == ConsoleKey.F1)

    {
        menu2();
        cStr = Console.ReadLine();
        save(cStr, students);
    }
    else if (key.Key == ConsoleKey.Escape)
    {
        return;
    }
}
while (true);



static void save(string cStr, List<Student> stud) // сереиализуем файл
{
    string cStritog;

    if (cStr.Contains(".txt"))
    {
        // Сериализация в txt
        cStritog = "";
        for (int i = 0; i < stud.Count; i++)
        {
            cStritog = cStritog + stud[i].Name + "\n" + stud[i].Age + "\n" + stud[i].Sex + "\n";
        }
        File.WriteAllText(cStr, cStritog);
    }
    else if (cStr.Contains(".json"))
    {
        // Сериализация в json
        string json = JsonConvert.SerializeObject(stud);
        File.WriteAllText(cStr, json);
    }
    else if (cStr.Contains(".xml"))
    {
        // Сериализация в xml
        XmlSerializer xml = new XmlSerializer(typeof(List<Student>));
        using (FileStream fs = new FileStream(cStr, FileMode.OpenOrCreate))
        {
            xml.Serialize(fs, stud);
            fs.Close();
        }
    }
    else
    {
        Console.WriteLine("Неверный тип файла");
    }

}

static List<Student> readtxt(string cF, List<Student> stud) // десерелизируем текстовый файл
{
    string cSt;
    cSt = File.ReadAllText(cF);
    menu2();
    string[] aMas = cSt.Split("\n");
    for (int i = 0; i < aMas.Length - 1; i = i + 3)
    {
        Student elem = new Student();
        elem.Name = aMas[i];
        elem.Age = Convert.ToInt32(aMas[i + 1]);
        elem.Sex = aMas[i + 2];
        stud.Add(elem);
    }

    foreach (Student aSt in stud)
    {
        Console.WriteLine(aSt.Name);
        Console.WriteLine(aSt.Age);
        Console.WriteLine(aSt.Sex);
    }
    return stud;
}


static List<Student> readjson(string cF, List<Student> stud) // десерелизируем json
{
    string cSt;
    cSt = File.ReadAllText(cF);
    menu2();
    stud = JsonConvert.DeserializeObject<List<Student>>(cSt);

    foreach (Student aSt in stud)
    {
        Console.WriteLine(aSt.Name);
        Console.WriteLine(aSt.Age);
        Console.WriteLine(aSt.Sex);
    }
    return stud;
}
static List<Student> readxls(string cF, List<Student> stud) // десерелизируем xls
{
    string cSt;
    cSt = File.ReadAllText(cF);
    menu2();
    XmlSerializer xml = new XmlSerializer(typeof(List<Student>));
    using (FileStream fs = new FileStream(cF, FileMode.Open))
    {
        stud = (List<Student>)xml.Deserialize(fs);
    }

    foreach (Student aSt in stud)
    {
        Console.WriteLine(aSt.Name);
        Console.WriteLine(aSt.Age);
        Console.WriteLine(aSt.Sex);
    }
    return stud;
}
static void menu2() // рисуем второе меню
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Сохранить файл в одном из трёх форматов (txt, json, xml) - F1. Закрыть программу - Escape");
    Console.WriteLine("------------------------------------------------");
}
