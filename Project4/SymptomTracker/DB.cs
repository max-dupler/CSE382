//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;
//using SQLite;
//using System.Reflection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace SymptomTracker; 
public class DB {
    private static string DBName = "log.db";
    public static SQLiteConnection conn;


    private static string GetFullNameOfEmbeddedResource(string baseName)
    {
        string namespaceName = "SymptomTracker";
        string fullFileName = namespaceName + "." + baseName;
        return fullFileName;
    }
    private static Stream GetInputStreamForEmbeddedResource(string baseName)
    {
        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ModifyPage)).Assembly;
        string fullFileName = GetFullNameOfEmbeddedResource(baseName);
        return assembly.GetManifestResourceStream(fullFileName);
    }

    public static void InitDB()
    {
        string libFolder = FileSystem.Current.AppDataDirectory;
        string fullFileName = Path.Combine(libFolder, DBName);

        //File.Delete(fullFileName);
        //Environment.Exit(0);

        conn = new SQLiteConnection(fullFileName);

        conn.CreateTable<Symptom>();
  
    }

    public static void DeleteTableContents(string tableName)
    {
        int v = conn.Execute("DELETE FROM " + tableName);
    }

}
