using Blackboard.Facts;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Facts
{
    public class StringFactView : FactView
    {
        private StringFactSO stringFact;
    
        private TextField textField;
    
        protected override string ValueViewUxmlPath => "UXML/StringFactValue.uxml";
    
        public StringFactView(bool showValue = false) : base(showValue)
        {
            factDropdown.SetFactType(FactType.String);
        }
        public override void SetFact(FactSO factSelected)
        {
            if (factSelected is StringFactSO sFactSelected)
            {
                stringFact = sFactSelected;
            
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

            var so = new SerializedObject(stringFact);
            textField.BindProperty(so.FindProperty("_value"));
        }
    
        protected override void AddValueField()
        {
            base.AddValueField();
        
            textField = factDropdown.Q<TextField>();
        }
    }
}