using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public static class CharacterEvents
{
    // Initialize the events with empty UnityAction
    public static UnityAction<GameObject, int> characterDamaged = delegate { };
    public static UnityAction<GameObject, int> characterHealed = delegate { };
}