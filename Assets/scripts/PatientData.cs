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
    
    [TextArea(2, 4)]
    [SerializeField] private string description;

    [TextArea(3, 6)]
    [SerializeField] private string medicalHistory;

    [SerializeField] private TriageCategory correctCategory;

    public string PatientName => patientName;
    public string Description => description;
    public string MedicalHistory => medicalHistory;
    public TriageCategory CorrectCategory => correctCategory;
}