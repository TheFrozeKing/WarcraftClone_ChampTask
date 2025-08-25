using System.Collections.Generic;

public static class SelectionSystem
{
    public static List<ISelectable> SelectableObjects = new();
    public static List<ISelectable> SelectedObjects = new();

    public static bool IsMouseOverUI;
    public static void ClickSelect(ISelectable objToAdd)
    {
        DeselectAll();
        SelectedObjects.Add(objToAdd);

        objToAdd.IsSelected = true;
        objToAdd.OnSelectionStatusChanged();
    }

    public static void ShiftClickSelect(ISelectable objToAdd)
    {
        if (!SelectedObjects.Contains(objToAdd))
        {
            SelectedObjects.Add(objToAdd);
            objToAdd.IsSelected = true;
        }
        else
        {
            SelectedObjects.Remove(objToAdd);
            objToAdd.IsSelected = false;
        }
        objToAdd.OnSelectionStatusChanged();
    }

    public static void DragSelect(ISelectable objToAdd)
    {
        if (!SelectedObjects.Contains(objToAdd))
        {
            SelectedObjects.Add(objToAdd);

            objToAdd.IsSelected = true;
            objToAdd.OnSelectionStatusChanged();
        }
    }

    public static void Deselect(ISelectable objToDeselect)
    {
        SelectedObjects.Remove(objToDeselect);

        objToDeselect.IsSelected = false;
        objToDeselect.OnSelectionStatusChanged();
    }

    public static void DeselectAll()
    {
        List<ISelectable> listCopy = new List<ISelectable>(SelectedObjects);
        foreach(ISelectable obj in listCopy)
        {
            Deselect(obj);
        }
    }
}
