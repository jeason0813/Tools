﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.20506.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Tools.CommentsMerge.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Primary file: {0}: {1}.
        '''</summary>
        Friend ReadOnly Property PrimaryFile01() As String
            Get
                Return ResourceManager.GetString("PrimaryFile01", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Save: {0}: {1}.
        '''</summary>
        Friend ReadOnly Property Save01() As String
            Get
                Return ResourceManager.GetString("Save01", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Secondary file: {0}: {1}.
        '''</summary>
        Friend ReadOnly Property SecondaryFile01() As String
            Get
                Return ResourceManager.GetString("SecondaryFile01", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Transformation: {0}: {1}.
        '''</summary>
        Friend ReadOnly Property Transformation01() As String
            Get
                Return ResourceManager.GetString("Transformation01", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to CommentsMerge: Merges XML comment files
        '''Usage: CommenstMerge InFile1 InFile2 ... InFileN OutputFile
        '''    At least 2 input files must must be specified
        '''Files should be namespace-less or have http://dzonny.cz/xml/schemas/intellisense namespace.
        '''</summary>
        Friend ReadOnly Property Usage() As String
            Get
                Return ResourceManager.GetString("Usage", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
