using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util.Base;

namespace Manager
{
    public class GameUIManager : BaseSingleton<GameUIManager>
    {
        [HideInInspector]
        public int killNum;
        [HideInInspector]
        public double time;
        [HideInInspector]
        public float health;
        // [HideInInspector]
        public int skillNum = 1;
        public int curControlProperty;

        public TMP_Text textKillNum;
        public TMP_Text textTime;
        public Slider sliderHealth;
        public List<Slider> propertyControlList;
        public List<TMP_Text> propertyLabelList;

        public List<Image> skills;

        private void Start()
        {
            killNum = 1;
            time = 0;
            health = 1;
        }

        private void Update()
        {
            time += Time.deltaTime;
            time = Math.Round(time, 2);

            textKillNum.text = GetNumText();
            textTime.text = GetTimeText();
            sliderHealth.value = health;
            OnSkillChange();
            OnPropertyChange();
            OnControlProperty();
        }

        private void OnSkillChange()
        {
            var changeSkill = 0;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                changeSkill = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                changeSkill = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                changeSkill = 3;
            }

            if (changeSkill == 0 || changeSkill == skillNum) return;
            skillNum = changeSkill;
            for (var i = 0; i < skills.Count; i++)
            {
                skills[i].gameObject.SetActive(i + 1 == skillNum);
            }
        }

        private void OnPropertyChange()
        {
            if (propertyControlList.Count != propertyLabelList.Count) return;
            if (!Input.GetKeyDown(KeyCode.Tab)) return;
            var lastIndex = curControlProperty;
            curControlProperty += 1;
            if (curControlProperty >= propertyControlList.Count)
            {
                curControlProperty = 0;
            }
            propertyLabelList[lastIndex].gameObject.SetActive(false);
            propertyLabelList[curControlProperty].gameObject.SetActive(true);
            propertyControlList[lastIndex].gameObject.SetActive(false);
            propertyControlList[curControlProperty].gameObject.SetActive(true);
        }

        private void OnControlProperty()
        {
            var axis = Input.GetAxis("Mouse ScrollWheel");
            propertyControlList[curControlProperty].value -= axis;
        }

        private string GetNumText()
        {
            return killNum switch
            {
                0 => "000",
                < 10 => $"00{killNum}",
                < 100 => $"0{killNum}",
                _ => $"{killNum}"
            };
        }
        
        private string GetTimeText()
        {
            return time switch
            {
                0 => "00.00",
                < 10 => $"0{time:N2}",
                _ => $"{time:N2}"
            };
        }
    }
}