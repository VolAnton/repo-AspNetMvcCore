using System.Text;

List<string> Paths = new List<string>() { @"D:\File1.txt", @"D:\File2.txt", @"D:\File3.txt", @"D:\File4.txt" };

Console.WriteLine(await FileReader(Paths));

Console.ReadKey();

async Task<string> FileReader(List<string> paths)
{
    StringBuilder sb = new StringBuilder();

    try
    {
        foreach (string path in paths)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string? line;

                while ((line = await sr.ReadLineAsync()) != null)
                {
                    sb.AppendLine(line);
                }

            }

            Console.WriteLine($"Чтение исходного файла:{path} прошло успешно.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    return sb.ToString();
}
