﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlarmaBomberosChimbarongo.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool FondoOscuro {
            get {
                return ((bool)(this["FondoOscuro"]));
            }
            set {
                this["FondoOscuro"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Data.DataTable Cuarteles {
            get {
                return ((global::System.Data.DataTable)(this["Cuarteles"]));
            }
            set {
                this["Cuarteles"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Data.DataTable Grifos {
            get {
                return ((global::System.Data.DataTable)(this["Grifos"]));
            }
            set {
                this["Grifos"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Data.DataTable MaterialesPeligrosos {
            get {
                return ((global::System.Data.DataTable)(this["MaterialesPeligrosos"]));
            }
            set {
                this["MaterialesPeligrosos"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Data.DataTable Voluntarios {
            get {
                return ((global::System.Data.DataTable)(this["Voluntarios"]));
            }
            set {
                this["Voluntarios"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Data.DataTable GuiaTelefonica {
            get {
                return ((global::System.Data.DataTable)(this["GuiaTelefonica"]));
            }
            set {
                this["GuiaTelefonica"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Data.DataTable ClavesRadiales {
            get {
                return ((global::System.Data.DataTable)(this["ClavesRadiales"]));
            }
            set {
                this["ClavesRadiales"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("42c5ba79f0761abc25be49d6c39a10180d82c8a1d2f6dfac9f10cf99b5fe36e8")]
        public string ClaveMaestra {
            get {
                return ((string)(this["ClaveMaestra"]));
            }
            set {
                this["ClaveMaestra"] = value;
            }
        }
    }
}
