﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Metrobit.Shell.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/FirstFloor.ModernUI;component/Assets/ModernUI.Dark.xaml")]
        public global::System.Uri ThemeSource {
            get {
                return ((global::System.Uri)(this["ThemeSource"]));
            }
            set {
                this["ThemeSource"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FF647687")]
        public global::System.Windows.Media.Color AccentColor {
            get {
                return ((global::System.Windows.Media.Color)(this["AccentColor"]));
            }
            set {
                this["AccentColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<WINDOWPLACEMENT xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <length>0</length>
  <flags>0</flags>
  <showCmd>0</showCmd>
  <minPosition>
    <X>0</X>
    <Y>0</Y>
  </minPosition>
  <maxPosition>
    <X>0</X>
    <Y>0</Y>
  </maxPosition>
  <normalPosition>
    <Left>0</Left>
    <Top>0</Top>
    <Right>0</Right>
    <Bottom>0</Bottom>
  </normalPosition>
</WINDOWPLACEMENT>")]
        public global::Metrobit.Shell.Utils.WINDOWPLACEMENT WindowPlacement {
            get {
                return ((global::Metrobit.Shell.Utils.WINDOWPLACEMENT)(this["WindowPlacement"]));
            }
            set {
                this["WindowPlacement"] = value;
            }
        }
    }
}
