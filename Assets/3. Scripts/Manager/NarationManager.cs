using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarationManager : MonoBehaviour
{
    [SerializeField] Naration pre_naration;

    // Start is called before the first frame update
    void Start()
    {
        SchoolManager.instance.OnNoticedSomething += SetNaration;
    }

    private void OnDisable()
    {
        SchoolManager.instance.OnNoticedSomething -= SetNaration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNaration(string naration)
    {
        Naration nara = Instantiate(pre_naration, transform);
        nara.Initialize(naration);
    }
}
