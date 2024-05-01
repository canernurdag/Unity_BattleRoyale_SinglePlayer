using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui.UiDropDown
{
    public class UiDropdown_MapType : UiDropdown
    {
        protected override void SetEnumValuesToDropdownUi()
        {
            _dropDown.options.Clear();

            var enumValues = Enum.GetValues(typeof(GameSession.MapType)).Cast<GameSession.MapType>().ToList();

            for (int i = 0; i < enumValues.Count; i++)
            {
                var enumValue = enumValues[i];
                var option = new TMP_Dropdown.OptionData(enumValue.ToString(), null);

                _dropDown.options.Add(option);
            }

            _dropDown.RefreshShownValue();
        }

        public GameSession.MapType GetCurrentMatchTypeFromDropdown()
        {
            int index = _dropDown.value;
            var selectedDropDownValue = _dropDown.options[index].text;

            var enumValues = Enum.GetValues(typeof(GameSession.MapType)).Cast<GameSession.MapType>().ToList();

            for (int i = 0; i < enumValues.Count; i++)
            {
                var enumValue = enumValues[i];

                if (enumValue.ToString().Equals(selectedDropDownValue))
                {
                    return enumValue;
                }

            }

            throw new ArgumentOutOfRangeException();
        }
    }
}