using System.Collections.Generic;

namespace Save{
    public class SaveablesObjects{
        public List<ISaveable> _saveables = new List<ISaveable>();

        public SaveablesObjects(params ISaveable[] saveable){
            foreach (var iSaveable in saveable){
                _saveables.Add(iSaveable);
            }
        }

        public void WriteDataToContainer(){
            foreach (var saveable in _saveables){
                saveable.SaveData();
            }
        }

        public void GetDataFromContainer(){
            foreach (var saveable in _saveables){
                saveable.LoadData();
            }
        }
    }
}