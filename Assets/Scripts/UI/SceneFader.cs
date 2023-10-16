using System.Collections;
using UnityEngine;

namespace UI
{
    public class SceneFader : MonoBehaviour
    {
        private CanvasGroup _group;

        public float fadeInDur;
        public float fadeOutDur;

        private void Awake()
        {
            _group = GetComponent<CanvasGroup>();
            DontDestroyOnLoad(gameObject);
        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(fadeOutDur);
            yield return FadeIn(fadeInDur);
        }

        public IEnumerator FadeOut(float time)
        {
            while (_group.alpha < 1)
            {
                _group.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        
        public IEnumerator FadeIn(float time)
        {
            while (_group.alpha != 0)
            {
                _group.alpha -= Time.deltaTime / time;
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}
