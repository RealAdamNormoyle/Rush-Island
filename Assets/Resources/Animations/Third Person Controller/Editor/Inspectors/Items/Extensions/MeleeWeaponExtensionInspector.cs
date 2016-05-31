using UnityEngine;
using UnityEditor;

namespace Opsive.ThirdPersonController.Editor
{
    /// <summary>
    /// Shows a custom inspector for MeleeWeaponExtension.
    /// </summary>
    [CustomEditor(typeof(ThirdPersonController.MeleeWeaponExtension))]
    public class MeleeWeaponExtensionInspector : ItemExtensionInspector
    {
        // MeleeWeaponExtension
        [SerializeField] private static bool m_AttackFoldout = true;
        [SerializeField] private static bool m_ImpactFoldout = true;

        /// <summary>
        /// Draws the custom inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            var meleeWeapon = target as MeleeWeaponExtension;
            if (meleeWeapon == null || serializedObject == null)
                return; // How'd this happen?

            base.OnInspectorGUI();

            // Show all of the fields.
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            if ((m_AttackFoldout = EditorGUILayout.Foldout(m_AttackFoldout, "Attack Options", InspectorUtility.BoldFoldout))) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_CanUseInAir"));
                var attackType = PropertyFromName(serializedObject, "m_HitType");
                EditorGUILayout.PropertyField(attackType);
                if (attackType.enumValueIndex == (int)MeleeWeapon.HitType.Hitscan) {
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_AttackPoint"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_AttackDistance"));
                    EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_AttackRadius"));
                } else {
                    // A collider must exist.
                    var collider = meleeWeapon.GetComponent<Collider>();
                    if (collider == null) {
                        EditorGUILayout.HelpBox("A collider must exist on the weapon extension if using the Collision attack type.", MessageType.Error);
                    }
                    // As does a Rigidbody.
                    var rigidbody = meleeWeapon.GetComponent<Rigidbody>();
                    if (rigidbody == null || rigidbody.isKinematic == false) {
                        EditorGUILayout.HelpBox("A kinematic Rigidbody must exist on the weapon extension if using the Collision attack type.", MessageType.Error);
                    }
                }
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_AttackLayer"));
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_AttackSound"), true);
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_AttackSoundDelay"));
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_WaitForEndUseEvent"));
                EditorGUI.indentLevel--;
            }

            if ((m_ImpactFoldout = EditorGUILayout.Foldout(m_ImpactFoldout, "Impact Options", InspectorUtility.BoldFoldout))) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DamageEvent"));
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DamageAmount"));
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_ImpactForce"));
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DefaultDust"));
                EditorGUILayout.PropertyField(PropertyFromName(serializedObject, "m_DefaultImpactSound"));
                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(meleeWeapon, "Inspector");
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(meleeWeapon);
            }
        }
    }
}