using UnityEngine;

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    public class Slot : UnityEngine.ScriptableObject
    {
        public Form form;
        public string slotName;
        
        public static Slot New(
            Form form,
            string slotName
        )
        {
            var instance = CreateInstance<Slot>();
            instance.name = "Runtime";
            instance.form = form;
            instance.slotName = slotName;
            return instance;
        }

        public Slot Clone()
        {
            var instance = CreateInstance<Slot>();
            instance.name = "Runtime";
            instance.form = form;
            instance.slotName = slotName;
            return instance;
        }
    }
}