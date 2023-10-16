using System.Collections;
using UnityEngine.SceneManagement;
using Util.Base;
using Util.Observer;

namespace UI
{
    public class SceneController : BaseSingleton<SceneController>, IEndGameObserver
    {
        
        public SceneFader sceneFader;
        private bool _fadeFinished;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }
        
        void Start()
        {
            _fadeFinished = true;
        }
        
        public void TransitionFirstLevel()
        {
            StartCoroutine(LoadLevel("Demo"));
        }

        private IEnumerator LoadLevel(string scene)
        {
            
            SceneFader fade = Instantiate(sceneFader);
            if (scene != "null")
            {
                yield return StartCoroutine(fade.FadeOut(2.5f));
                yield return SceneManager.LoadSceneAsync(scene);
                yield return StartCoroutine(fade.FadeIn(2.5f));
                yield break;
            }
        }

        private IEnumerator LoadMain()
        {
            SceneFader fade = Instantiate(sceneFader);
            yield return StartCoroutine(fade.FadeOut(2f));
            yield return SceneManager.LoadSceneAsync("Start");
            yield return StartCoroutine(fade.FadeIn(2f));
            yield break;
        }

        public void EndNotify()
        {
            if (_fadeFinished)
            {
                _fadeFinished = false;
                StartCoroutine(LoadMain());
            }
        }
    }
}
