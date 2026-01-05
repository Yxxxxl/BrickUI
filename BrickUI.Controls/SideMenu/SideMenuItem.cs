using System.Windows.Input;

namespace BrickUI.Controls.SideMenu
{
    public class SideMenuItem
    {
        public SideMenuItem()
        {
        }

        public SideMenuItem(string text, object icon, ICommand command, object commandParameter = null)
        {
            Text = text;
            Icon = icon;
            Command = command;
            CommandParameter = commandParameter;
        }

        public string Text { get; set; }

        public object Icon { get; set; }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public bool IsEnabled { get; set; } = true;
    }
}
