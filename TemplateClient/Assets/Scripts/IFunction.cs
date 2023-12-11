using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;

public interface IFunction
{
    void Execute(Message message);
}
