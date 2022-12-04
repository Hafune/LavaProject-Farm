using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    [RequireComponent(typeof(Image))]
    public class ImageProgressBar : MonoBehaviour
    {
        private Image _image;
        
        private void Awake() => _image = GetComponent<Image>();

        public void SetPercent(float percent) => _image.fillAmount = percent;
    }
}