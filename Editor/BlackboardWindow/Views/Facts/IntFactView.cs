using Blackboard.Facts;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public class IntFactView : FactView
    {
        private IntFactSO intFact;
    
        private IntegerField intField;
    
        protected override string ValueViewUxmlPath => "UXML/IntFactValue.uxml";
    
        public IntFactView(bool showValue = false) : base(showValue)
        {
            factDropdown.SetFactType(FactType.Int);
        }
        public override void SetFact(FactSO factSelected)
        {
            if (factSelected is IntFactSO iFactSelected)
            {
                intFact = iFactSelected;
            
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

            var so = new SerializedObject(intFact);
            intField.BindProperty(so.FindProperty("_value"));
        }
    
        protected override void AddValueField()
        {
            base.AddValueField();
        
            intField = factDropdown.Q<IntegerField>();
        }
    }
}