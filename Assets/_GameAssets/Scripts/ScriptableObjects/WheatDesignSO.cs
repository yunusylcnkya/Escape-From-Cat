using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "ScriptableObject/WheatDesignSO")]

public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _resetBoosDuration;



    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoosDuration;

}
