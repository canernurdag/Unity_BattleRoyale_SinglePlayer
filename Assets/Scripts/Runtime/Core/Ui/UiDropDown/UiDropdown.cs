using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui.UiDropDown
{
    public abstract class UiDropdown : MonoBehaviour
    {
        [SerializeField] protected TMP_Dropdown _dropDown;

        protected virtual void Start()
        {

            SetEnumValuesToDropdownUi();
        }

        protected abstract void SetEnumValuesToDropdownUi();
    }
}