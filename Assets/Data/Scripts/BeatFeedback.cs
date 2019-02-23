using UnityEngine;
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

        public void OnTooFast()
        {
            Target.sprite = TooFast;
            Anim.SetTrigger(DoTrigger);
        }
        
        public void OnTooEarly()
        {
            Target.sprite = TooEarly;
            Anim.SetTrigger(DoTrigger);
        }
        
        public void OnGood()
        {
            Target.sprite = Good;
            Anim.SetTrigger(DoTrigger);
        }
        
        public void OnMissed()
        {
            Target.sprite = Miss;
            Anim.SetTrigger(DoTrigger);
        }
    }
}