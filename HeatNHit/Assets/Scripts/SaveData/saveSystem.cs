using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveSystem
{
    private static string path = Application.dataPath + "/gameData.txt";
    private static float PixelEffect = -1f;
    private static int Highscore = -1;

    public static void SaveData(gameData data)
    {
        if (data.Highscore == -1)
            data.Highscore = Highscore;
        else
            Highscore = data.Highscore;


        if (data.PixelEffect == -1)
            data.PixelEffect = PixelEffect;
        else
            PixelEffect = data.PixelEffect;
       

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
                PixelEffect = data.PixelEffect;
                Highscore = data.Highscore;
                return data;
            }

            stream.Close();
        }
        return null;
    }

}
