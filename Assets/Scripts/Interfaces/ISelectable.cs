public interface ISelectable
{
    public bool IsSelected { get; set; }
    public void OnSelectionStatusChanged();
}
