using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace F1Manager
{
    public static class ThemeManager
    {
        public static bool IsDarkMode { get; private set; } = true;

        // Dark mode colors
        public static Color DarkBackColor => Color.FromArgb(20, 20, 20);
        public static Color DarkContentBackColor => Color.FromArgb(27, 27, 27);
        public static Color DarkPanelBackColor => Color.FromArgb(30, 30, 30);
        public static Color DarkPanelAltBackColor => Color.FromArgb(36, 36, 36);
        public static Color DarkHeaderBackColor => Color.FromArgb(24, 24, 24);
        public static Color DarkSidebarBackColor => Color.FromArgb(18, 18, 18);
        public static Color DarkForeColor => Color.WhiteSmoke;
        public static Color DarkLightGray => Color.LightGray;
        public static Color DarkWhite => Color.White;
        public static Color DarkButtonBackColor => Color.FromArgb(32, 32, 32);
        public static Color DarkRedColor => Color.FromArgb(230, 28, 43);
        public static Color DarkLogoutColor => Color.FromArgb(200, 80, 80);

        // Light mode colors
        public static Color LightBackColor => Color.FromArgb(240, 240, 240);
        public static Color LightContentBackColor => Color.FromArgb(255, 255, 255);
        public static Color LightPanelBackColor => Color.FromArgb(250, 250, 250);
        public static Color LightPanelAltBackColor => Color.FromArgb(240, 240, 240);
        public static Color LightHeaderBackColor => Color.FromArgb(220, 220, 220);
        public static Color LightSidebarBackColor => Color.FromArgb(200, 200, 200);
        public static Color LightForeColor => Color.FromArgb(30, 30, 30);
        public static Color LightLightGray => Color.FromArgb(60, 60, 60);
        public static Color LightWhite => Color.FromArgb(20, 20, 20);
        public static Color LightButtonBackColor => Color.FromArgb(220, 220, 220);
        public static Color LightRedColor => Color.FromArgb(200, 30, 40);
        public static Color LightLogoutColor => Color.FromArgb(180, 40, 40);

        // Getters based on current mode
        public static Color BackColor => IsDarkMode ? DarkBackColor : LightBackColor;
        public static Color ContentBackColor => IsDarkMode ? DarkContentBackColor : LightContentBackColor;
        public static Color PanelBackColor => IsDarkMode ? DarkPanelBackColor : LightPanelBackColor;
        public static Color PanelAltBackColor => IsDarkMode ? DarkPanelAltBackColor : LightPanelAltBackColor;
        public static Color HeaderBackColor => IsDarkMode ? DarkHeaderBackColor : LightHeaderBackColor;
        public static Color SidebarBackColor => IsDarkMode ? DarkSidebarBackColor : LightSidebarBackColor;
        public static Color ForeColor => IsDarkMode ? DarkForeColor : LightForeColor;
        public static Color LightGray => IsDarkMode ? DarkLightGray : LightLightGray;
        public static Color White => IsDarkMode ? DarkWhite : LightWhite;
        public static Color ButtonBackColor => IsDarkMode ? DarkButtonBackColor : LightButtonBackColor;
        public static Color RedColor => IsDarkMode ? DarkRedColor : LightRedColor;
        public static Color LogoutColor => IsDarkMode ? DarkLogoutColor : LightLogoutColor;
        public static Color HeaderForeColor => IsDarkMode ? Color.White : Color.Black;
        public static Color TextBoxBackColor => IsDarkMode ? Color.WhiteSmoke : Color.White;
        public static Color LoginBackColor => IsDarkMode ? Color.FromArgb(200, 200, 200) : Color.FromArgb(220, 220, 220);

        public static void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
        }

        public static void ApplyTheme(Form form, bool isMainForm = false)
        {
            if (form == null) return;

            form.BackColor = BackColor;

            ApplyThemeToControls(form.Controls);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is Panel panel)
                {
                    ApplyThemeToPanel(panel);
                }
                else if (ctrl is Button button)
                {
                    ApplyThemeToButton(button);
                }
                else if (ctrl is Label label)
                {
                    ApplyThemeToLabel(label);
                }
                else if (ctrl is TextBox textBox)
                {
                    ApplyThemeToTextBox(textBox);
                }
                else if (ctrl is PictureBox pb)
                {
                    if (pb.BackColor != Color.Transparent)
                        pb.BackColor = IsDarkMode ? Color.Black : Color.FromArgb(220, 220, 220);
                }

                if (ctrl.HasChildren)
                {
                    ApplyThemeToControls(ctrl.Controls);
                }
            }
        }

        private static void ApplyThemeToPanel(Panel panel)
        {
            if (panel.BackColor == Color.FromArgb(18, 18, 18) ||
                panel.BackColor == Color.FromArgb(200, 200, 200))
            {
                panel.BackColor = SidebarBackColor;
            }
            else if (panel.BackColor == Color.FromArgb(24, 24, 24) ||
                     panel.BackColor == Color.FromArgb(220, 220, 220))
            {
                panel.BackColor = HeaderBackColor;
            }
            else if (panel.BackColor == Color.FromArgb(27, 27, 27) ||
                     panel.BackColor == Color.FromArgb(255, 255, 255))
            {
                panel.BackColor = ContentBackColor;
            }
            else if (panel.BackColor == Color.FromArgb(36, 36, 36) ||
                     panel.BackColor == Color.FromArgb(240, 240, 240))
            {
                panel.BackColor = PanelAltBackColor;
            }
            else if (panel.BackColor == Color.FromArgb(30, 30, 30) ||
                     panel.BackColor == Color.FromArgb(250, 250, 250))
            {
                panel.BackColor = PanelBackColor;
            }
            else if (panel.BackColor == Color.Black ||
                     panel.BackColor == Color.FromArgb(220, 220, 220))
            {
                panel.BackColor = IsDarkMode ? Color.Black : Color.FromArgb(220, 220, 220);
            }
            else
            {
                panel.BackColor = PanelBackColor;
            }
        }

        private static void ApplyThemeToButton(Button button)
        {
            Color btnColor = button.BackColor;

            // Red action buttons
            if (btnColor == Color.FromArgb(230, 28, 43) ||
                btnColor == Color.FromArgb(200, 30, 40))
            {
                button.BackColor = RedColor;
                button.ForeColor = Color.White;
                return;
            }

            // Logout button
            if (btnColor == Color.FromArgb(200, 80, 80) ||
                btnColor == Color.FromArgb(180, 40, 40))
            {
                button.ForeColor = LogoutColor;
                if (button.FlatAppearance.BorderSize == 0 &&
                    button.FlatStyle == FlatStyle.Flat &&
                    button.BackColor == Color.Transparent)
                {
                    // nav button - keep transparent bg
                }
                else
                {
                    button.BackColor = ButtonBackColor;
                }
                return;
            }

            // Dark gray buttons (secondary actions)
            if (btnColor == Color.FromArgb(32, 32, 32) ||
                btnColor == Color.FromArgb(220, 220, 220))
            {
                button.BackColor = ButtonBackColor;
                button.ForeColor = ForeColor;
                return;
            }

            // Login/Register dark buttons
            if (btnColor == Color.FromArgb(64, 64, 64) ||
                btnColor == Color.FromArgb(180, 180, 180))
            {
                button.BackColor = IsDarkMode ? Color.FromArgb(64, 64, 64) : Color.FromArgb(180, 180, 180);
                button.ForeColor = IsDarkMode ? Color.White : Color.Black;
                return;
            }

            // Nav buttons with transparent background
            if (button.BackColor == Color.Transparent ||
                button.BackColor == Color.FromArgb(200, 200, 200))
            {
                button.BackColor = Color.Transparent;
                button.ForeColor = ForeColor;
                return;
            }

            // Fix for buttons with UseVisualStyleBackColor = true (system background)
            if (button.UseVisualStyleBackColor)
            {
                button.UseVisualStyleBackColor = false;
                button.BackColor = IsDarkMode ? Color.FromArgb(30, 30, 30) : Color.FromArgb(240, 240, 240);
            }

            // Flat buttons without border (like Register link, Back to Login)
            if (button.FlatStyle == FlatStyle.Flat && button.FlatAppearance.BorderSize == 0)
            {
                button.FlatAppearance.MouseOverBackColor = Color.Transparent;

                Color fc = button.ForeColor;
                // If text is black or very dark
                if (fc.GetBrightness() < 0.3f)
                {
                    button.ForeColor = IsDarkMode ? Color.WhiteSmoke : Color.FromArgb(30, 30, 30);
                }
                // If text is white or very light
                else if (fc.GetBrightness() > 0.8f)
                {
                    button.ForeColor = IsDarkMode ? Color.WhiteSmoke : Color.FromArgb(30, 30, 30);
                }
            }
        }

        private static void ApplyThemeToLabel(Label label)
        {
            Color fc = label.ForeColor;

            // Title labels (white/black)
            if (fc == Color.White || fc == Color.Black || fc == Color.FromArgb(20, 20, 20))
            {
                label.ForeColor = White;
                return;
            }
            // LightGray labels
            if (fc == Color.LightGray || fc == Color.FromArgb(60, 60, 60) || fc == Color.FromArgb(30, 30, 30))
            {
                label.ForeColor = LightGray;
                return;
            }
            // Red accent labels
            if (fc == Color.FromArgb(230, 28, 43) || fc == Color.FromArgb(200, 30, 40))
            {
                label.ForeColor = RedColor;
                return;
            }

            // Fix for any dark text on dark background (like "Username:", "Password:" labels)
            if (IsDarkMode && (fc.R < 80 && fc.G < 80 && fc.B < 80) && fc != Color.Transparent)
            {
                label.ForeColor = Color.LightGray;
            }
            // Fix for light text on light background
            else if (!IsDarkMode && (fc.R > 180 && fc.G > 180 && fc.B > 180))
            {
                label.ForeColor = Color.FromArgb(30, 30, 30);
            }
        }

        private static void ApplyThemeToTextBox(TextBox textBox)
        {
            textBox.BackColor = TextBoxBackColor;
            textBox.ForeColor = IsDarkMode ? Color.Black : Color.Black;
        }

        public static void ApplyThemeToAllOpenForms()
        {
            var openForms = Application.OpenForms;
            for (int i = 0; i < openForms.Count; i++)
            {
                ApplyTheme(openForms[i]);
            }
        }
    }
}