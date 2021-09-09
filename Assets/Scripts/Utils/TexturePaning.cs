using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeldingMadness.Utils {
    public class TexturePaning : MonoBehaviour {

        public Vector2 paningSpeed;

        private Material mat;

        private void Start() {
            mat = GetComponent<Renderer>().material;
        }

        private void Update() {
            mat.mainTextureOffset += paningSpeed * Time.deltaTime;
        }
    }
}
