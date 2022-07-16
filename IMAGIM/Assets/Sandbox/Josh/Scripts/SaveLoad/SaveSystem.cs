using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public static class SaveSystem
    {
        private const string FOLDER_DATA_PATH = "/Saves/";
        private static void ensureFoldersExist()
        {
            if (!Directory.Exists(Path(FOLDER_DATA_PATH)))
            {
                Directory.CreateDirectory(Path(FOLDER_DATA_PATH));
            }
        }

        /// <summary>
        /// Saves the IData object into a file
        /// </summary>
        /// <param name="data">Any serializable object</param>
        /// <returns>true if success</returns>
        public static bool SaveGameData(IData data)
        {
            ensureFoldersExist();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Path(FOLDER_DATA_PATH + data.DataFileName), FileMode.Create);

            bf.Serialize(stream, data);
            stream.Close();
            return true;
        }

        /// <summary>
        /// Loads the file from path into a casted object
        /// </summary>
        /// <param name="dataFileName">Filename located in Saves folder</param>
        /// <returns>Data object or null</returns>
        public static T LoadGameData<T>(string dataFileName)
        {
            ensureFoldersExist();
            string fpath = Path(FOLDER_DATA_PATH + dataFileName);
            if (File.Exists(fpath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(fpath, FileMode.Open);

                T data = (T)bf.Deserialize(stream);
                stream.Close();
                return data;
            }
            else
            {
                return default(T);
            }
        }
        private static string Path(string path)
        {
            return Application.persistentDataPath + path;
        }
    }
}