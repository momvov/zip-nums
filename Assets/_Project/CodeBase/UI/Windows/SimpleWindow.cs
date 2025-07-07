namespace CodeBase.UI.Windows
{
  public class SimpleWindow : Window
  {
    public override void Show() => 
      gameObject.SetActive(true);

    public override void Hide() => 
      gameObject.SetActive(false);
  }
}