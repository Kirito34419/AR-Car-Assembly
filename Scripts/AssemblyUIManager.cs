using UnityEngine;
using TMPro;

public class AssemblyUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI instructionText;

    // List of parts in assembly order
    public string[] assemblyOrder = {
        "amdb11_bumper_F.001",
        "amdb11_bumper_R.001",
        "amdb11_radiator.001",
        "amdb11_engine_v8.001",
        "amdb11_hood.001",
        "amdb11_trunk.001",
        "amdb11_seat_FL.001",
        "amdb11_seat_FR.001",
        "amdb11_steer.001",
        "amdb11_door_L.001",
        "amdb11_door_R.001",
        "amdb11_wheel_03.004",
        "amdb11_wheel_03.005",
        "amdb11_wheel_03.006",
        "amdb11_wheel_03.007"
    };

    private int currentIndex = 0;

    void Start()
    {
        UpdateInstruction();
    }

    public void OnPartPlaced(string partName)
    {
        if (currentIndex < assemblyOrder.Length && partName == assemblyOrder[currentIndex])
        {
            currentIndex++;
            UpdateInstruction();
        }
    }

    private void UpdateInstruction()
    {
        if (currentIndex < assemblyOrder.Length)
        {
            instructionText.text = $"Next Part: {assemblyOrder[currentIndex]}";
        }
        else
        {
            instructionText.text = "Assembly Complete! 🎉";
        }
    }
}
