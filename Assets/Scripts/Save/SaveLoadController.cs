using System.IO;
using UnityEngine;

namespace Save{
    public class SaveLoadController{
        private string _filePath = Application.persistentDataPath + @"\SaveFile.txt";

        public void Save(DataContainer dataContainer){
            string json = JsonUtility.ToJson(dataContainer);
            File.WriteAllText(_filePath, json);
        }

        public DataContainer Load(){
            if (File.Exists(_filePath)){
                string json = File.ReadAllText(_filePath);
                Debug.Log(json);
                return JsonUtility.FromJson<DataContainer>(json);
            }
            else{
                return new DataContainer();
            }
        }
    }
}