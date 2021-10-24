using UnityEngine;

namespace Assets.Managers
{
    [RequireComponent(typeof(CameraManager))]
    [RequireComponent(typeof(TaskManager))]
    [RequireComponent(typeof(UIManager))]
    public class SimulatorManagers : MonoBehaviour
    {
        public static CameraManager CameraManager { get; private set; }
        public static TaskManager TaskManager { get; private set; }
        public static UIManager UIManager { get; private set; }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
