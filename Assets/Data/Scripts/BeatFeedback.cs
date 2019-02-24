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

        private MusicController Music;
        
        void Start()
        {
            Music = FindObjectOfType<MusicController>();
        }

        private void Update()
        {
            if (BadCombo > 3)
            {
                Music.Progress = MusicController.NiceProgress.No;
            }

            if (GoodCombo > 3)
            {
                Music.Progress = MusicController.NiceProgress.Yes;
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
    }
}