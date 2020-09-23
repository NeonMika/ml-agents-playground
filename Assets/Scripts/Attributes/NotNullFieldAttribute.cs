using System;
using UnityEngine;

namespace Attributes
{
    /// <summary> Cannot Be Null will red-flood the field if the reference is null. </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class NotNullFieldAttribute : PropertyAttribute { }
}