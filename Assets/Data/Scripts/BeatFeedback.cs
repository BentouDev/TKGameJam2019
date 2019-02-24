using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UnityTemplateProjects
{
    public class BeatFeedback : MonoBehaviour
    {
        public Animator Anim;
        public string DoTrigger;

        public Image Target;
        public Sprite Good;
        public Sprite Miss;
        public Sprite TooFast;
        public Sprite TooEarly;

        private int GoodCombo;
        private int BadCombo;

        public float AnimDuration = 0.5f;
        public float AnimDelayed = 0.5f;
        
        private MusicController Music;
        private GameManager Game;
        
        void Start()
        {
            Music = FindObjectOfType<MusicController>();
            Game = FindObjectOfType<GameManager>();
            CanAnimate = true;
        }

        private void Update()
        {
            if (BadCombo > 1)
            {
                if (Game.CurrentMode == GameManager.GameMode.Easy && CanAnimate)
                {
                    CanAnimate = false;
                    Game.ToggleMode();
                    StartCoroutine(CoAnimateModeChange());
                }
                // Music.Progress = MusicController.NiceProgress.No;
            }

            if (GoodCombo > 14)
            {
                if (Game.CurrentMode == GameManager.GameMode.Hard && CanAnimate)
                {
                    CanAnimate = false;
                    Game.ToggleMode();
                    StartCoroutine(CoAnimateModeChange());
                }
                // Music.Progress = MusicController.NiceProgress.Yes;
            }
        }

        public void OnTooFast()
        {
            GoodCombo = 0;
            BadCombo++;
            Target.sprite = TooFast;
            Anim.SetTrigger(DoTrigger);
        }
        
        public void OnTooEarly()
        {
            GoodCombo = 0;
            BadCombo++;
            Target.sprite = TooEarly;
            Anim.SetTrigger(DoTrigger);
        }
        
        public void OnGood()
        {
            if (GoodCombo > 3 && GoodCombo % 4 == 0)
                Game.OnGoodBeat();

            GoodCombo++;
            BadCombo = 0;
            Target.sprite = Good;
            Anim.SetTrigger(DoTrigger);
        }
        
        public void OnMissed()
        {
            GoodCombo = 0;
            BadCombo++;
            Target.sprite = Miss;
            Anim.SetTrigger(DoTrigger);
        }

        private static bool CanAnimate;
        
        IEnumerator CoAnimateModeChange()
        {
            yield return new WaitForSeconds(AnimDelayed);
        
            float source = Music.Coefficient;
            float target = 1 - source;
        
            float elapsed = 0;
            while (elapsed < AnimDuration)
            {
                elapsed += Time.deltaTime;

                Music.Coefficient = Mathf.Lerp(source, target, elapsed / AnimDuration);

                yield return null;
            }

            Music.Coefficient = target;
        
            yield return new WaitForSeconds(AnimDuration);

            CanAnimate = true;
        }
    }
}