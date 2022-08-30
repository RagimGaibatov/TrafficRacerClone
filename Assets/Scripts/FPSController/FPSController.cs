using UnityEngine;

namespace FPSController{
    public class FPSController : MonoBehaviour{
        [SerializeField] private int fps = 60;

        private void Start(){
            Application.targetFrameRate = fps;
        }
    }
}