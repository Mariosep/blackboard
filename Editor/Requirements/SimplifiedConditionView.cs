using Blackboard.Requirement;
using UnityEngine.UIElements;

namespace Blackboard.Editor.Requirement
{
    public abstract class SimplifiedConditionView : VisualElement
    {
        protected SimplifiedRequirementsListView requirementsListView;

        protected Toggle fulfilledToggle;
        
        public abstract void BindCondition(SimplifiedRequirementsListView requirementsListView, ConditionSO condition);
        protected abstract void UpdateContent();

        public void EnableDebugMode(Condition condition)
        {
            fulfilledToggle.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
            fulfilledToggle.value = condition.IsFulfilled;
            condition.onFulfilled += e => OnFulfilled();
            condition.onUnfulfilled += e => OnUnfulfilled();
        }

        public void DisableDebugMode()
        {
            fulfilledToggle.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
        
        private void OnFulfilled()
        {
            fulfilledToggle.value = true;
        }

        private void OnUnfulfilled()
        {
            fulfilledToggle.value = false;
        }
    }
}