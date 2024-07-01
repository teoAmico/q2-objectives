using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Match3
{
    public class Gem : MonoBehaviour
    {
        private TextMeshPro letter;
        private SpriteRenderer background;
        [SerializeField] Sprite image;

        public void setLetter(string letter, Vector3 position)
        {
            if(this.letter == null)
            {
                // Attempt to get the TextMeshPro component
                this.letter = gameObject.AddComponent<TextMeshPro>();

                // If the component is still null, log an error and return
                if (this.letter == null)
                {
                    Debug.LogError("TextMeshPro component is not found.");
                    return;
                }
            }
            this.letter.text = letter;
            this.letter.fontSize = 6.5f;
            // Set text alignment to center both horizontally and vertically
            this.letter.alignment = TMPro.TextAlignmentOptions.Center;
            this.letter.verticalAlignment = TMPro.VerticalAlignmentOptions.Middle;

            if (background == null) {
                GameObject childObject = new GameObject("background");
                background = childObject.AddComponent<SpriteRenderer>();
                background.color = Color.red;
                background.sprite = image;
                Vector3 currentScale = childObject.transform.localScale;
                currentScale.x = 6.5f; // Change the X component to modify the width
                currentScale.y = 6.5f; // Change the Y component to modify the height
                background.transform.localScale = currentScale;
                background.transform.localPosition = position;

                childObject.transform.SetParent(transform);
            }
            

        }

        public string GetLetter() => this.letter.text;

    }
}
