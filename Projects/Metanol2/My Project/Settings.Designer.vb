﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1434
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property Folder() As String
            Get
                Return CType(Me("Folder"),String)
            End Get
            Set
                Me("Folder") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("")>  _
        Public Property LastVersion() As String
            Get
                Return CType(Me("LastVersion"),String)
            End Get
            Set
                Me("LastVersion") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("64, 64"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property ThumbSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("ThumbSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("ThumbSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("736, 532")>  _
        Public Property FormSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("FormSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("FormSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property FormLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("FormLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("FormLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Normal")>  _
        Public Property FormState() As Global.System.Windows.Forms.FormWindowState
            Get
                Return CType(Me("FormState"),Global.System.Windows.Forms.FormWindowState)
            End Get
            Set
                Me("FormState") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("143")>  _
        Public Property BrowserSplitter() As Integer
            Get
                Return CType(Me("BrowserSplitter"),Integer)
            End Get
            Set
                Me("BrowserSplitter") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("231")>  _
        Public Property MainSplitter() As Integer
            Get
                Return CType(Me("MainSplitter"),Integer)
            End Get
            Set
                Me("MainSplitter") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("300, 300")>  _
        Public Property LargeSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("LargeSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("LargeSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property LargePosition() As Global.System.Drawing.Point
            Get
                Return CType(Me("LargePosition"),Global.System.Drawing.Point)
            End Get
            Set
                Me("LargePosition") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Normal")>  _
        Public Property LargeState() As Global.System.Windows.Forms.FormWindowState
            Get
                Return CType(Me("LargeState"),Global.System.Windows.Forms.FormWindowState)
            End Get
            Set
                Me("LargeState") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property LargeShown() As Boolean
            Get
                Return CType(Me("LargeShown"),Boolean)
            End Get
            Set
                Me("LargeShown") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property LargeFloating() As Boolean
            Get
                Return CType(Me("LargeFloating"),Boolean)
            End Get
            Set
                Me("LargeFloating") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("150")>  _
        Public Property PreviewHeight() As UInteger
            Get
                Return CType(Me("PreviewHeight"),UInteger)
            End Get
            Set
                Me("PreviewHeight") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("200")>  _
        Public Property KeywordsHeight() As UInteger
            Get
                Return CType(Me("KeywordsHeight"),UInteger)
            End Get
            Set
                Me("KeywordsHeight") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("179")>  _
        Public Property TextHeight() As UInteger
            Get
                Return CType(Me("TextHeight"),UInteger)
            End Get
            Set
                Me("TextHeight") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("<?xml version=""1.0"" encoding=""utf-16""?>"&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"<Keywords xmlns=""http://dzonny.cz/xml/To"& _ 
            "ols.WindowsT.FormsT.KeyWordsEditor"" />"),  _
         Global.System.Configuration.SettingsManageabilityAttribute(Global.System.Configuration.SettingsManageability.Roaming)>  _
        Public Property Keywords() As Global.System.Xml.XmlDocument
            Get
                Return CType(Me("Keywords"),Global.System.Xml.XmlDocument)
            End Get
            Set
                Me("Keywords") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property LargeFullScreen() As Boolean
            Get
                Return CType(Me("LargeFullScreen"),Boolean)
            End Get
            Set
                Me("LargeFullScreen") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property TCBehavior() As Boolean
            Get
                Return CType(Me("TCBehavior"),Boolean)
            End Get
            Set
                Me("TCBehavior") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.ConnectionString),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=.\SQLEXPRESS2008;AttachDbFilename=|DataDirectory|\Database\Metanol.md"& _ 
            "f;Integrated Security=True;Connect Timeout=30;User Instance=True")>  _
        Public Property MetanolConnectionString() As String
            Get
                Return CType(Me("MetanolConnectionString"),String)
            End Get
            Set
                Me("MetanolConnectionString") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Tools.Metanol.My.MySettings
            Get
                Return Global.Tools.Metanol.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
