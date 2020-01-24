using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveSystem
{
    private static string path = "./Data.txt";
    public static void SaveData(gameData data)
    {
        if (data.Highscore == -1)
        {
            gameData actual = LoadData();
            if (actual != null)
                data.Highscore = actual.Highscore;
        }

        if (data.PixelEffect == -1)
        {
            gameData actual = LoadData();
            if (actual != null)
                data.PixelEffect = actual.Highscore;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static gameData LoadData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            gameData data;
            if (stream.Length != 0)
            {
                data = formatter.Deserialize(stream) as gameData;
                return data;
            }
        }

        return null;
    }

}
