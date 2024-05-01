using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiLoadingCanvas : MonoBehaviour
    {
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private TMP_Text _loadingText;

        public void SetSliderValue(float value)
        {
            value = Mathf.Clamp01(value);
            _loadingSlider.value = value;
            string stringValue = (value * 100).ToString("F0") + "%";
            _loadingText.text = stringValue;
        }

    }
}


