using Blackboard.Facts;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public class BoolFactView : FactView
    {
        private BoolFactSO boolFact;
    
        private Toggle toggle;
    
        protected override string ValueViewUxmlPath => "UXML/BoolFactValue.uxml";
    
        public BoolFactView(bool showValue = false) : base(showValue)
        {
            factDropdown.SetFactType(FactType.Bool);
        }
        public override void SetFact(FactSO factSelected)
        {
            if (factSelected is BoolFactSO bFactSelected)
            {
                boolFact = bFactSelected;
            
                factDropdown.SetFact(factSelected);
            
                if(showValue)
                    BindValue();
            
                onFactSelected?.Invoke(factSelected);
            }
        }
    
        private void BindValue()
        {
            if(!valueContainer.visible)
                ShowValue();

            var so = new SerializedObject(boolFact);
            toggle.BindProperty(so.FindProperty("_value"));
        }
    
        protected override void AddValueField()
        {
            base.AddValueField();
        
            toggle = factDropdown.Q<Toggle>();
        }
    }
}