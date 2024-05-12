using UnityEngine;
using UnityEngine.EventSystems;

public class MB : MonoBehaviour {

    ///<summary>Эхлэж байгааг шалгана</summary>
    public static bool IsStarting => GameController.State == GameState.Starting;

    ///<summary>Тоглож байгааг шалгана</summary>
    public static bool IsPlaying => GameController.State == GameState.Playing;

    ///<summary>Зогсоосон байгааг шалгана</summary>
    public static bool IsPause => GameController.State == GameState.Pause;

    ///<summary>Хожсон эсэхийг шалгана</summary>
    public static bool IsLevelCompleted => GameController.State == GameState.LevelCompleted;

    ///<summary>Хожигдсон эсэхийг шалгана</summary>
    public static bool IsGameOver => GameController.State == GameState.GameOver;

    ///<summary>Тохиргоо хийж байгааг шалгана</summary>
    public static bool IsSettings => GameController.State == GameState.Settings;

    ///<summary>Шилжиж байгааг шалгана</summary>
    public static bool IsShifting => GameController.State == GameState.Shifting;

    ///<summary>Input.GetMouseButton(0)</summary>
    public static bool IsClick => Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject();

    ///<summary>Input.GetMouseButtonDown(0)</summary>
    public static bool IsDown => Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();

    ///<summary>Input.GetMouseButtonUp(0)</summary>
    public static bool IsUp => Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject();

    ///<summary>Input.mousePosition</summary>
    public static Vector3 MP => Input.mousePosition;

    [HideInInspector]
    public Vector3 mp;

    ///<summary>Time.deltaTime</summary>
    public static float DT => Time.deltaTime;
    
    ///<summary>gameObject</summary>
    public GameObject go => gameObject;
    
    ///<summary>transform</summary>
    public Transform tf => transform;
    
    ///<summary>Rigidbody</summary>
    public Rigidbody rb {
        get {
            if (!_rb)
                _rb = go.Rb();
            return _rb;
        }
    }
    Rigidbody _rb;
}