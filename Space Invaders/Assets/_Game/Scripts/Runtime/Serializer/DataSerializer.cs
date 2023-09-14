using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Runtime.GameState;
using UnityEngine;

namespace Runtime.Serializer
{
    public class DataSerializer
    {
        private const string FILE_NAME = "/game.data";

        public void SaveHighScores(HighScores highScores)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string saveFile = Application.persistentDataPath + FILE_NAME;

            using FileStream dataStream = new FileStream(saveFile, FileMode.Create);
            binaryFormatter.Serialize(dataStream, highScores);

            dataStream.Close();
        }
        
        public HighScores LoadHighScores()
        {
            string savedFile = Application.persistentDataPath + FILE_NAME;

            if (!File.Exists(savedFile))
            {
                return new HighScores();
            }

            try
            {
                using FileStream dataStream = new FileStream(savedFile, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                HighScores gridData = (HighScores)binaryFormatter.Deserialize(dataStream);
                dataStream.Close();
                return gridData;
            }
            catch (Exception)
            {
                return new HighScores();
            }
        }
    }
}