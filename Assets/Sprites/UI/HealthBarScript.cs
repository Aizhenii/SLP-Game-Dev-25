using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HealthBarHandler : MonoBehaviour
{
    public PlayerControl PlayerControl;
    public UIDocument UIDoc;


    private void Start()
    {
        m_HealthLabel = UIDoc.rootVisualElement.Q<Label>("HealthLabel");

        HealthChanged();
    }


    void HealthChanged()
    {
        
    }
    
    void Update()
    {
        
    }

}


