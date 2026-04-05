using Game.Economy;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(IndicatorsKeeper))]
public class IndicatorsProviderDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Начинаем блок для отображения PropertyDrawer
        EditorGUI.BeginProperty(position, label, property);

        // Находим поле массива _indicators
        SerializedProperty indicatorsArray = property.FindPropertyRelative("_indicators");

        // Определяем начальную позицию для рисования элементов
        Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        // Отображаем заголовок для массива индикаторов
        EditorGUI.LabelField(fieldRect, "Indicators");

        // Смещаем rect вниз для начала рисования элементов
        fieldRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // Если длина массива отличается от 4 элементов, выставляем её в 4
        if (indicatorsArray.arraySize != 4)
            indicatorsArray.arraySize = 4;

        // Отображаем индикаторы по порядку
        for (int i = 0; i < 4; i++)
        {
            // Находим индикатор (объект класса Indicator)
            SerializedProperty indicator = indicatorsArray.GetArrayElementAtIndex(i);

            // Находим поле типа индикатора
            SerializedProperty typeProp = indicator.FindPropertyRelative("Type");
            SerializedProperty valueProp = indicator.FindPropertyRelative("Value");

            if (typeProp.enumValueIndex != i)
            {
                typeProp.enumValueIndex = i; // Присваиваем соответствующий индекс для enum
            }
            
            // Рисуем поле для типа индикатора
            Rect typeRect = new Rect(fieldRect.x, fieldRect.y, 100, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

            // Рисуем кнопки "-" и "+"
            Rect minusButtonRect = new Rect(fieldRect.x + 110, fieldRect.y, 20, EditorGUIUtility.singleLineHeight);
            Rect valueRect = new Rect(fieldRect.x + 135, fieldRect.y, 50, EditorGUIUtility.singleLineHeight);
            Rect plusButtonRect = new Rect(fieldRect.x + 190, fieldRect.y, 20, EditorGUIUtility.singleLineHeight);

            if (GUI.Button(minusButtonRect, "-"))
                valueProp.intValue -= 1; // Уменьшаем значение, но не ниже 0

            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);

            if (GUI.Button(plusButtonRect, "+"))
                valueProp.intValue += 1; // Увеличиваем значение

            // Смещаемся вниз для следующего индикатора
            fieldRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        // Завершаем блок PropertyDrawer
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Высота: 1 линия для заголовка и 4 линии для индикаторов
        return EditorGUIUtility.singleLineHeight * 5 + EditorGUIUtility.standardVerticalSpacing * 4;
    }
}
