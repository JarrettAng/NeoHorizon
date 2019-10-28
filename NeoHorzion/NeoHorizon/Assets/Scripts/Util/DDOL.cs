using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : Singleton<DDOL> {
    private static DDOL instance;

    private void OnEnable() {
        HandleInstance();

        void HandleInstance() {
            if(instance == null) {
                instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }
    }
}
