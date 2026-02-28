using UnityEngine;

public class SetParameter : StateMachineBehaviour
{
    [SerializeField] private Type typeToSet;
    [SerializeField] private When whenToSet;
    [SerializeField] private string value;
    
    private enum Type
    {
        Bool,
        Float,
        Int,
        Trigger
    }

    private enum When
    {
        Exit,
        Enter,
        Update
    }
}
