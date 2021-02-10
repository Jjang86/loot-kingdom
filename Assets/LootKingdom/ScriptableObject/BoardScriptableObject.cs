using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "BoardScriptableObject", menuName = "BoardScriptableObject")]
public class BoardScriptableObject : ScriptableObject {
    public List<Board> boards;
}
