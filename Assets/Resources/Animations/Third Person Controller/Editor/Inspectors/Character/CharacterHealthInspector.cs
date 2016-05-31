using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

namespace Opsive.ThirdPersonController.Editor
{
    /// <summary>
    /// Shows a custom inspector for CharacterHealth.
    /// </summary>
    [CustomEditor(typeof(CharacterHealth))]
    public class CharacterHealthInspector : HealthInspector
    {
        [SerializeField] private static bool m_FallDamageFoldout = true;
        [SerializeField] private static bool m_DamageMultiplierFoldout = true;

        private ReorderableList m_DamageMultiplierList;

        /// <summary>
        /// Draws the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var characterHealth = target as CharacterHealth;
            if (characterHealth == null || serializedObject == null)
                return; // How'd this happen?

            base.OnInspectorGUI();
            
            // Show all of the fields.
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            if ((m_FallDamageFoldout = EditorGUILayout.Foldout(m_FallDamageFoldout, "Fall Damage Options", InspectorUtility.BoldFoldout))) {
                EditorGUI.indentLevel++;
                var useFallDamage = PropertyFromName(serializedObject, "m_UseFallDamage");
                EditorGUILayout.PropertyField(useFallDamage);
                if (useFallDamage.boolValue) {
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_MinFallDamageHeight"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DeathHeight"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_MinFallDamage"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_MaxFallDamage"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DamageCurve"));
                }
                EditorGUI.indentLevel--;
            }

            if ((m_FallDamageFoldout = EditorGUILayout.Foldout(m_FallDamageFoldout, "Fall Damage Options", InspectorUtility.BoldFoldout))) {
                EditorGUI.indentLevel++;
                var useFallDamage = PropertyFromName(serializedObject, "m_UseFallDamage");
                EditorGUILayout.PropertyField(useFallDamage);
                if (useFallDamage.boolValue) {
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_MinFallDamageHeight"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DeathHeight"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_MinFallDamage"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_MaxFallDamage"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DamageCurve"));
                }
                EditorGUI.indentLevel--;
            }

            if ((m_DamageMultiplierFoldout = EditorGUILayout.Foldout(m_DamageMultiplierFoldout, "Damage Multiplier Options", InspectorUtility.BoldFoldout))) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DamageDistanceThreshold"));
                if (m_DamageMultiplierList == null) {
                    var damageMultiplierProperty = PropertyFromName(serializedObject, "m_DamageMultipliers");
                    m_DamageMultiplierList = new ReorderableList(serializedObject, damageMultiplierProperty, true, true, true, true);
                    m_DamageMultiplierList.drawHeaderCallback = OnDamageMultiplierHeaderDraw;
                    m_DamageMultiplierList.drawElementCallback = OnDamageMultiplierElementDraw;
                }
                // Indent the list so it lines up with the rest of the content.
                var rect = GUILayoutUtility.GetRect(0, m_DamageMultiplierList.GetHeight());
                rect.x += EditorGUI.indentLevel * 15;
                rect.xMax -= EditorGUI.indentLevel * 15;
                m_DamageMultiplierList.DoList(rect);

                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(characterHealth, "Inspector");
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(characterHealth);
            }
        }

        /// <summary>
        /// Draws the DamageMultiplier ReordableList header.
        /// </summary>
        private void OnDamageMultiplierHeaderDraw(Rect rect)
        {
            EditorGUI.LabelField(new Rect(rect.x + 12, rect.y, rect.width - 90, EditorGUIUtility.singleLineHeight), "Bone");
            EditorGUI.LabelField(new Rect(rect.x + (rect.width - 90), rect.y, 90, EditorGUIUtility.singleLineHeight), "Multiplier");
        }

        /// <summary>
        /// Draws the DamageMultiplier ReordableList element.
        /// </summary>
        private void OnDamageMultiplierElementDraw(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.BeginChangeCheck();

            var damageMultiplier = m_DamageMultiplierList.serializedProperty.GetArrayElementAtIndex(index);
            var bone = damageMultiplier.FindPropertyRelative("m_Bone");
            var multiplier = damageMultiplier.FindPropertyRelative("m_Multiplier");
            bone.enumValueIndex = Convert.ToInt32(EditorGUI.EnumPopup(new Rect(rect.x, rect.y, rect.width - 90, EditorGUIUtility.singleLineHeight), (HumanBodyBones)bone.enumValueIndex));
            multiplier.floatValue = EditorGUI.FloatField(new Rect(rect.x + (rect.width - 90), rect.y, 90, EditorGUIUtility.singleLineHeight), multiplier.floatValue);

            if (EditorGUI.EndChangeCheck()) {
                var serializedObject = m_DamageMultiplierList.serializedProperty.serializedObject;
                Undo.RecordObject(serializedObject.targetObject, "Inspector");
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(serializedObject.targetObject);
            }
        }
    }
}