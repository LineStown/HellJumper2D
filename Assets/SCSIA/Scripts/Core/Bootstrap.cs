using UnityEngine;
using UnityEngine.SceneManagement;

namespace SCSIA
{
    public class Bootstrap : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private AudioManagerConfig _audioManagerConfig;

        private static Bootstrap _instance;
        private AudioManager _audioManager;
        private GameDataManager _gameDataManager;
        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public static AudioManager AudioManager
        {
            get
            {
                if (_instance == null || _instance._audioManager == null)
                    return null;
                return _instance._audioManager;
            }
        }

        public static GameDataManager GameDataManager
        {
            get
            {
                if (_instance == null || _instance._gameDataManager == null)
                    return null;
                return _instance._gameDataManager;
            }
        }

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void Awake()
        {
            Init();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void Init()
        {
            ToSingltone();
            InitManagers();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void ToSingltone()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void InitManagers()
        {
            InitAudioManager();
            InitGameDataManager();
        }

        private void InitAudioManager()
        {
            _instance._audioManager = new GameObject("AudioManager").AddComponent<AudioManager>();
            _instance._audioManager.transform.SetParent(transform);
            _instance._audioManager.Init(_audioManagerConfig);
            DontDestroyOnLoad(_instance._audioManager.gameObject);
        }

        private void InitGameDataManager()
        {
            _instance._gameDataManager = new GameObject("GameDataManager").AddComponent<GameDataManager>();
            _instance._gameDataManager.transform.SetParent(transform);
            DontDestroyOnLoad(_instance._gameDataManager.gameObject);
        }
    }
}
