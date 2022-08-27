using UnityEngine;

namespace FPSController{
    public class FPSController : MonoBehaviour{
        [SerializeField] private int FPS = 60;

        private void Start(){
            Application.targetFrameRate = FPS;
        }
    }
}