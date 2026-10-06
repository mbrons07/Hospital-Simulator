using UnityEngine;

public enum TriageCategory
{
    Red,
    Yellow,
    Green,
    Black
}

[CreateAssetMenu(fileName = "NewPatient", menuName = "Triage/Patient Data")]
public class PatientData : ScriptableObject
{
    [SerializeField] private string patientName;
    [TextArea(3, 5)]
    [SerializeField] private string description;
    [SerializeField] private TriageCategory correctCategory;

    public string PatientName => patientName;
    public string Description => description;
    public TriageCategory CorrectCategory => correctCategory;
}