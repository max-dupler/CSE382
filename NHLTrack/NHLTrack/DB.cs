using System.Reflection;
using SQLite;

namespace NHLTrack;

public class DB : ContentPage
{
    public static SQLiteConnection conn;

    private static string GetFullNameOfEmbeddedResource(string baseName)
    {
        string namespaceName = "NHLTrack";
        string fullFileName = namespaceName + "." + baseName;
        return fullFileName;
    }

    private static Stream GetInputStreamForEmbeddedResource(string baseName)
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Rosters)).Assembly;
        string fullFileName = GetFullNameOfEmbeddedResource(baseName);
        return assembly.GetManifestResourceStream(fullFileName);
    }

    public static void InitDB()
    {
        string libFolder = FileSystem.Current.AppDataDirectory;
        string fullFileName = Path.Combine(libFolder, "Teams.db");

        //File.Delete(fullFileName);
        //Environment.Exit(0);

        if (File.Exists(fullFileName))
        {
            conn = new SQLiteConnection(fullFileName);
            return;
        }

        conn = new SQLiteConnection(fullFileName);
        conn.CreateTable<Team>();

        Stream stream = GetInputStreamForEmbeddedResource("Teams.csv");

        using (StreamReader input = new StreamReader(stream))
        {
            string? header = input.ReadLine();
            while (!input.EndOfStream)
            {
                string? line = input.ReadLine();
                if (line == null)
                {
                    continue;
                }
                Team t = new Team(line);
                conn.Insert(t);
            }
        }
    }
}
