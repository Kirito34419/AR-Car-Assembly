using UnityEngine;
using UnityEngine.UI;

public class AssemblyManager : MonoBehaviour
{
    [System.Serializable]
    public class AssemblyStep
    {
        public string stepName;              // e.g. "Attach Front Bumper"
        public SnapZoneDistance snapPart;    // reference to that part’s snap script
    }

    [Header("Assembly Steps (in order)")]
    public AssemblyStep[] steps;

    [Header("UI")]
    public Text stepText;   // drag a UI Text element here

    private int currentStep = 0;

    void Start()
    {
        // Disable all parts except the first one
        for (int i = 0; i < steps.Length; i++)
        {
            bool active = (i == 0);
            steps[i].snapPart.gameObject.SetActive(active);
            if (steps[i].snapPart.targetTransform != null)
                steps[i].snapPart.targetTransform.gameObject.SetActive(active);
        }

        if (steps.Length > 0) UpdateStepUI();
    }

    void Update()
    {
        if (currentStep < steps.Length && steps[currentStep].snapPart != null)
        {
            if (steps[currentStep].snapPart.IsSnapped())
            {
                // move to next
                currentStep++;
                if (currentStep < steps.Length)
                {
                    ActivateStep(currentStep);
                    UpdateStepUI();
                }
                else
                {
                    stepText.text = "✅ Assembly Complete!";
                }
            }
        }
    }

    void ActivateStep(int index)
    {
        steps[index].snapPart.gameObject.SetActive(true);
        if (steps[index].snapPart.targetTransform != null)
            steps[index].snapPart.targetTransform.gameObject.SetActive(true);
    }

    void UpdateStepUI()
    {
        stepText.text = $"Step {currentStep + 1}/{steps.Length}: {steps[currentStep].stepName}";
    }
}
