﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30128.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection

Namespace Data
	
	<Global.System.Data.Linq.Mapping.DatabaseAttribute(Name:="Metanol")>  _
	Partial Public Class DatabaseAccessDataContext
		Inherits System.Data.Linq.DataContext
		
		Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource()
		
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub InsertExif(instance As Exif)
    End Sub
    Partial Private Sub UpdateExif(instance As Exif)
    End Sub
    Partial Private Sub DeleteExif(instance As Exif)
    End Sub
    Partial Private Sub InsertIPTC(instance As IPTC)
    End Sub
    Partial Private Sub UpdateIPTC(instance As IPTC)
    End Sub
    Partial Private Sub DeleteIPTC(instance As IPTC)
    End Sub
    Partial Private Sub InsertPicture(instance As Picture)
    End Sub
    Partial Private Sub UpdatePicture(instance As Picture)
    End Sub
    Partial Private Sub DeletePicture(instance As Picture)
    End Sub
    Partial Private Sub InsertPictureMetadata(instance As PictureMetadata)
    End Sub
    Partial Private Sub UpdatePictureMetadata(instance As PictureMetadata)
    End Sub
    Partial Private Sub DeletePictureMetadata(instance As PictureMetadata)
    End Sub
    #End Region
		
		Public Sub New()
			MyBase.New(Global.Tools.Metanol.My.MySettings.Default.MetanolConnectionString, mappingSource)
			OnCreated
		End Sub
		
		Public Sub New(ByVal connection As String)
			MyBase.New(connection, mappingSource)
			OnCreated
		End Sub
		
		Public Sub New(ByVal connection As System.Data.IDbConnection)
			MyBase.New(connection, mappingSource)
			OnCreated
		End Sub
		
		Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
			MyBase.New(connection, mappingSource)
			OnCreated
		End Sub
		
		Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
			MyBase.New(connection, mappingSource)
			OnCreated
		End Sub
		
		Public ReadOnly Property Exifs() As System.Data.Linq.Table(Of Exif)
			Get
				Return Me.GetTable(Of Exif)
			End Get
		End Property
		
		Public ReadOnly Property IPTCs() As System.Data.Linq.Table(Of IPTC)
			Get
				Return Me.GetTable(Of IPTC)
			End Get
		End Property
		
		Public ReadOnly Property Pictures() As System.Data.Linq.Table(Of Picture)
			Get
				Return Me.GetTable(Of Picture)
			End Get
		End Property
		
		Public ReadOnly Property PictureMetadatas() As System.Data.Linq.Table(Of PictureMetadata)
			Get
				Return Me.GetTable(Of PictureMetadata)
			End Get
		End Property
	End Class
	
	<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Exif")>  _
	Partial Public Class Exif
		Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
		
		Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
		
		Private _PictureID As Integer
		
		Private _Model As String
		
		Private _Digitized As System.Nullable(Of Date)
		
		Private _Picture As EntityRef(Of Picture)
		
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnPictureIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnPictureIDChanged()
    End Sub
    Partial Private Sub OnModelChanging(value As String)
    End Sub
    Partial Private Sub OnModelChanged()
    End Sub
    Partial Private Sub OnDigitizedChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnDigitizedChanged()
    End Sub
    #End Region
		
		Public Sub New()
			MyBase.New
			Me._Picture = CType(Nothing, EntityRef(Of Picture))
			OnCreated
		End Sub
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PictureID", DbType:="Int NOT NULL", IsPrimaryKey:=true)>  _
		Public Property PictureID() As Integer
			Get
				Return Me._PictureID
			End Get
			Set
				If ((Me._PictureID = value)  _
							= false) Then
					If Me._Picture.HasLoadedOrAssignedValue Then
						Throw New System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException()
					End If
					Me.OnPictureIDChanging(value)
					Me.SendPropertyChanging
					Me._PictureID = value
					Me.SendPropertyChanged("PictureID")
					Me.OnPictureIDChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Model", DbType:="NVarChar(1024)")>  _
		Public Property Model() As String
			Get
				Return Me._Model
			End Get
			Set
				If (String.Equals(Me._Model, value) = false) Then
					Me.OnModelChanging(value)
					Me.SendPropertyChanging
					Me._Model = value
					Me.SendPropertyChanged("Model")
					Me.OnModelChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Digitized", DbType:="DateTime")>  _
		Public Property Digitized() As System.Nullable(Of Date)
			Get
				Return Me._Digitized
			End Get
			Set
				If (Me._Digitized.Equals(value) = false) Then
					Me.OnDigitizedChanging(value)
					Me.SendPropertyChanging
					Me._Digitized = value
					Me.SendPropertyChanged("Digitized")
					Me.OnDigitizedChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Picture_Exif", Storage:="_Picture", ThisKey:="PictureID", OtherKey:="ID", IsForeignKey:=true, DeleteOnNull:=true, DeleteRule:="CASCADE")>  _
		Public Property Picture() As Picture
			Get
				Return Me._Picture.Entity
			End Get
			Set
				Dim previousValue As Picture = Me._Picture.Entity
				If ((Object.Equals(previousValue, value) = false)  _
							OrElse (Me._Picture.HasLoadedOrAssignedValue = false)) Then
					Me.SendPropertyChanging
					If ((previousValue Is Nothing)  _
								= false) Then
						Me._Picture.Entity = Nothing
						previousValue.Exif = Nothing
					End If
					Me._Picture.Entity = value
					If ((value Is Nothing)  _
								= false) Then
						value.Exif = Me
						Me._PictureID = value.ID
					Else
						Me._PictureID = CType(Nothing, Integer)
					End If
					Me.SendPropertyChanged("Picture")
				End If
			End Set
		End Property
		
		Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
		
		Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		
		Protected Overridable Sub SendPropertyChanging()
			If ((Me.PropertyChangingEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
			End If
		End Sub
		
		Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
			If ((Me.PropertyChangedEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
			End If
		End Sub
	End Class
	
	<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.IPTC")>  _
	Partial Public Class IPTC
		Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
		
		Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
		
		Private _PictureID As Integer
		
		Private _ObjectName As String
		
		Private _Text As String
		
		Private _City As String
		
		Private _CountryCode As String
		
		Private _Country As String
		
		Private _Province As String
		
		Private _Sublocation As String
		
		Private _Keywords As String
		
		Private _Copyright As String
		
		Private _Credit As String
		
		Private _EditStatus As String
		
		Private _Urgence As System.Nullable(Of Decimal)
		
		Private _Picture As EntityRef(Of Picture)
		
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnPictureIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnPictureIDChanged()
    End Sub
    Partial Private Sub OnObjectNameChanging(value As String)
    End Sub
    Partial Private Sub OnObjectNameChanged()
    End Sub
    Partial Private Sub OnTextChanging(value As String)
    End Sub
    Partial Private Sub OnTextChanged()
    End Sub
    Partial Private Sub OnCityChanging(value As String)
    End Sub
    Partial Private Sub OnCityChanged()
    End Sub
    Partial Private Sub OnCountryCodeChanging(value As String)
    End Sub
    Partial Private Sub OnCountryCodeChanged()
    End Sub
    Partial Private Sub OnCountryChanging(value As String)
    End Sub
    Partial Private Sub OnCountryChanged()
    End Sub
    Partial Private Sub OnProvinceChanging(value As String)
    End Sub
    Partial Private Sub OnProvinceChanged()
    End Sub
    Partial Private Sub OnSublocationChanging(value As String)
    End Sub
    Partial Private Sub OnSublocationChanged()
    End Sub
    Partial Private Sub OnKeywordsChanging(value As String)
    End Sub
    Partial Private Sub OnKeywordsChanged()
    End Sub
    Partial Private Sub OnCopyrightChanging(value As String)
    End Sub
    Partial Private Sub OnCopyrightChanged()
    End Sub
    Partial Private Sub OnCreditChanging(value As String)
    End Sub
    Partial Private Sub OnCreditChanged()
    End Sub
    Partial Private Sub OnEditStatusChanging(value As String)
    End Sub
    Partial Private Sub OnEditStatusChanged()
    End Sub
    Partial Private Sub OnUrgenceChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnUrgenceChanged()
    End Sub
    #End Region
		
		Public Sub New()
			MyBase.New
			Me._Picture = CType(Nothing, EntityRef(Of Picture))
			OnCreated
		End Sub
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_PictureID", DbType:="Int NOT NULL", IsPrimaryKey:=true)>  _
		Public Property PictureID() As Integer
			Get
				Return Me._PictureID
			End Get
			Set
				If ((Me._PictureID = value)  _
							= false) Then
					If Me._Picture.HasLoadedOrAssignedValue Then
						Throw New System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException()
					End If
					Me.OnPictureIDChanging(value)
					Me.SendPropertyChanging
					Me._PictureID = value
					Me.SendPropertyChanged("PictureID")
					Me.OnPictureIDChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ObjectName", DbType:="VarChar(1024)")>  _
		Public Property ObjectName() As String
			Get
				Return Me._ObjectName
			End Get
			Set
				If (String.Equals(Me._ObjectName, value) = false) Then
					Me.OnObjectNameChanging(value)
					Me.SendPropertyChanging
					Me._ObjectName = value
					Me.SendPropertyChanged("ObjectName")
					Me.OnObjectNameChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Text", DbType:="VarChar(MAX)")>  _
		Public Property Text() As String
			Get
				Return Me._Text
			End Get
			Set
				If (String.Equals(Me._Text, value) = false) Then
					Me.OnTextChanging(value)
					Me.SendPropertyChanging
					Me._Text = value
					Me.SendPropertyChanged("Text")
					Me.OnTextChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_City", DbType:="VarChar(1024)")>  _
		Public Property City() As String
			Get
				Return Me._City
			End Get
			Set
				If (String.Equals(Me._City, value) = false) Then
					Me.OnCityChanging(value)
					Me.SendPropertyChanging
					Me._City = value
					Me.SendPropertyChanged("City")
					Me.OnCityChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CountryCode", DbType:="Char(3)")>  _
		Public Property CountryCode() As String
			Get
				Return Me._CountryCode
			End Get
			Set
				If (String.Equals(Me._CountryCode, value) = false) Then
					Me.OnCountryCodeChanging(value)
					Me.SendPropertyChanging
					Me._CountryCode = value
					Me.SendPropertyChanged("CountryCode")
					Me.OnCountryCodeChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Country", DbType:="VarChar(1024)")>  _
		Public Property Country() As String
			Get
				Return Me._Country
			End Get
			Set
				If (String.Equals(Me._Country, value) = false) Then
					Me.OnCountryChanging(value)
					Me.SendPropertyChanging
					Me._Country = value
					Me.SendPropertyChanged("Country")
					Me.OnCountryChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Province", DbType:="VarChar(1024)")>  _
		Public Property Province() As String
			Get
				Return Me._Province
			End Get
			Set
				If (String.Equals(Me._Province, value) = false) Then
					Me.OnProvinceChanging(value)
					Me.SendPropertyChanging
					Me._Province = value
					Me.SendPropertyChanged("Province")
					Me.OnProvinceChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Sublocation", DbType:="VarChar(1024)")>  _
		Public Property Sublocation() As String
			Get
				Return Me._Sublocation
			End Get
			Set
				If (String.Equals(Me._Sublocation, value) = false) Then
					Me.OnSublocationChanging(value)
					Me.SendPropertyChanging
					Me._Sublocation = value
					Me.SendPropertyChanged("Sublocation")
					Me.OnSublocationChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Keywords", DbType:="VarChar(MAX)")>  _
		Public Property Keywords() As String
			Get
				Return Me._Keywords
			End Get
			Set
				If (String.Equals(Me._Keywords, value) = false) Then
					Me.OnKeywordsChanging(value)
					Me.SendPropertyChanging
					Me._Keywords = value
					Me.SendPropertyChanged("Keywords")
					Me.OnKeywordsChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Copyright", DbType:="VarChar(1024)")>  _
		Public Property Copyright() As String
			Get
				Return Me._Copyright
			End Get
			Set
				If (String.Equals(Me._Copyright, value) = false) Then
					Me.OnCopyrightChanging(value)
					Me.SendPropertyChanging
					Me._Copyright = value
					Me.SendPropertyChanged("Copyright")
					Me.OnCopyrightChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Credit", DbType:="VarChar(1024)")>  _
		Public Property Credit() As String
			Get
				Return Me._Credit
			End Get
			Set
				If (String.Equals(Me._Credit, value) = false) Then
					Me.OnCreditChanging(value)
					Me.SendPropertyChanging
					Me._Credit = value
					Me.SendPropertyChanged("Credit")
					Me.OnCreditChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EditStatus", DbType:="VarChar(1024)")>  _
		Public Property EditStatus() As String
			Get
				Return Me._EditStatus
			End Get
			Set
				If (String.Equals(Me._EditStatus, value) = false) Then
					Me.OnEditStatusChanging(value)
					Me.SendPropertyChanging
					Me._EditStatus = value
					Me.SendPropertyChanged("EditStatus")
					Me.OnEditStatusChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Urgence", DbType:="Decimal(1,0)")>  _
		Public Property Urgence() As System.Nullable(Of Decimal)
			Get
				Return Me._Urgence
			End Get
			Set
				If (Me._Urgence.Equals(value) = false) Then
					Me.OnUrgenceChanging(value)
					Me.SendPropertyChanging
					Me._Urgence = value
					Me.SendPropertyChanged("Urgence")
					Me.OnUrgenceChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Picture_IPTC", Storage:="_Picture", ThisKey:="PictureID", OtherKey:="ID", IsForeignKey:=true, DeleteOnNull:=true, DeleteRule:="CASCADE")>  _
		Public Property Picture() As Picture
			Get
				Return Me._Picture.Entity
			End Get
			Set
				Dim previousValue As Picture = Me._Picture.Entity
				If ((Object.Equals(previousValue, value) = false)  _
							OrElse (Me._Picture.HasLoadedOrAssignedValue = false)) Then
					Me.SendPropertyChanging
					If ((previousValue Is Nothing)  _
								= false) Then
						Me._Picture.Entity = Nothing
						previousValue.IPTC = Nothing
					End If
					Me._Picture.Entity = value
					If ((value Is Nothing)  _
								= false) Then
						value.IPTC = Me
						Me._PictureID = value.ID
					Else
						Me._PictureID = CType(Nothing, Integer)
					End If
					Me.SendPropertyChanged("Picture")
				End If
			End Set
		End Property
		
		Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
		
		Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		
		Protected Overridable Sub SendPropertyChanging()
			If ((Me.PropertyChangingEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
			End If
		End Sub
		
		Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
			If ((Me.PropertyChangedEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
			End If
		End Sub
	End Class
	
	<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.Picture")>  _
	Partial Public Class Picture
		Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
		
		Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
		
		Private _ID As Integer
		
		Private _FileName As String
		
		Private _Folder As String
		
		Private _x As Integer
		
		Private _y As Integer
		
		Private _LastSync As Date
		
		Private _Exif As EntityRef(Of Exif)
		
		Private _IPTC As EntityRef(Of IPTC)
		
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnIDChanged()
    End Sub
    Partial Private Sub OnFileNameChanging(value As String)
    End Sub
    Partial Private Sub OnFileNameChanged()
    End Sub
    Partial Private Sub OnFolderChanging(value As String)
    End Sub
    Partial Private Sub OnFolderChanged()
    End Sub
    Partial Private Sub OnxChanging(value As Integer)
    End Sub
    Partial Private Sub OnxChanged()
    End Sub
    Partial Private Sub OnyChanging(value As Integer)
    End Sub
    Partial Private Sub OnyChanged()
    End Sub
    Partial Private Sub OnLastSyncChanging(value As Date)
    End Sub
    Partial Private Sub OnLastSyncChanged()
    End Sub
    #End Region
		
		Public Sub New()
			MyBase.New
			Me._Exif = CType(Nothing, EntityRef(Of Exif))
			Me._IPTC = CType(Nothing, EntityRef(Of IPTC))
			OnCreated
		End Sub
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ID", AutoSync:=AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true)>  _
		Public Property ID() As Integer
			Get
				Return Me._ID
			End Get
			Set
				If ((Me._ID = value)  _
							= false) Then
					Me.OnIDChanging(value)
					Me.SendPropertyChanging
					Me._ID = value
					Me.SendPropertyChanged("ID")
					Me.OnIDChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_FileName", DbType:="VarChar(256) NOT NULL", CanBeNull:=false)>  _
		Public Property FileName() As String
			Get
				Return Me._FileName
			End Get
			Set
				If (String.Equals(Me._FileName, value) = false) Then
					Me.OnFileNameChanging(value)
					Me.SendPropertyChanging
					Me._FileName = value
					Me.SendPropertyChanged("FileName")
					Me.OnFileNameChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Folder", DbType:="VarChar(512) NOT NULL", CanBeNull:=false)>  _
		Public Property Folder() As String
			Get
				Return Me._Folder
			End Get
			Set
				If (String.Equals(Me._Folder, value) = false) Then
					Me.OnFolderChanging(value)
					Me.SendPropertyChanging
					Me._Folder = value
					Me.SendPropertyChanged("Folder")
					Me.OnFolderChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_x", DbType:="Int NOT NULL")>  _
		Public Property x() As Integer
			Get
				Return Me._x
			End Get
			Set
				If ((Me._x = value)  _
							= false) Then
					Me.OnxChanging(value)
					Me.SendPropertyChanging
					Me._x = value
					Me.SendPropertyChanged("x")
					Me.OnxChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_y", DbType:="Int NOT NULL")>  _
		Public Property y() As Integer
			Get
				Return Me._y
			End Get
			Set
				If ((Me._y = value)  _
							= false) Then
					Me.OnyChanging(value)
					Me.SendPropertyChanging
					Me._y = value
					Me.SendPropertyChanged("y")
					Me.OnyChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_LastSync", DbType:="DateTime NOT NULL")>  _
		Public Property LastSync() As Date
			Get
				Return Me._LastSync
			End Get
			Set
				If ((Me._LastSync = value)  _
							= false) Then
					Me.OnLastSyncChanging(value)
					Me.SendPropertyChanging
					Me._LastSync = value
					Me.SendPropertyChanged("LastSync")
					Me.OnLastSyncChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Picture_Exif", Storage:="_Exif", ThisKey:="ID", OtherKey:="PictureID", IsUnique:=true, IsForeignKey:=false)>  _
		Public Property Exif() As Exif
			Get
				Return Me._Exif.Entity
			End Get
			Set
				Dim previousValue As Exif = Me._Exif.Entity
				If ((Object.Equals(previousValue, value) = false)  _
							OrElse (Me._Exif.HasLoadedOrAssignedValue = false)) Then
					Me.SendPropertyChanging
					If ((previousValue Is Nothing)  _
								= false) Then
						Me._Exif.Entity = Nothing
						previousValue.Picture = Nothing
					End If
					Me._Exif.Entity = value
					If (Object.Equals(value, Nothing) = false) Then
						value.Picture = Me
					End If
					Me.SendPropertyChanged("Exif")
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.AssociationAttribute(Name:="Picture_IPTC", Storage:="_IPTC", ThisKey:="ID", OtherKey:="PictureID", IsUnique:=true, IsForeignKey:=false)>  _
		Public Property IPTC() As IPTC
			Get
				Return Me._IPTC.Entity
			End Get
			Set
				Dim previousValue As IPTC = Me._IPTC.Entity
				If ((Object.Equals(previousValue, value) = false)  _
							OrElse (Me._IPTC.HasLoadedOrAssignedValue = false)) Then
					Me.SendPropertyChanging
					If ((previousValue Is Nothing)  _
								= false) Then
						Me._IPTC.Entity = Nothing
						previousValue.Picture = Nothing
					End If
					Me._IPTC.Entity = value
					If (Object.Equals(value, Nothing) = false) Then
						value.Picture = Me
					End If
					Me.SendPropertyChanged("IPTC")
				End If
			End Set
		End Property
		
		Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
		
		Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		
		Protected Overridable Sub SendPropertyChanging()
			If ((Me.PropertyChangingEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
			End If
		End Sub
		
		Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
			If ((Me.PropertyChangedEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
			End If
		End Sub
	End Class
	
	<Global.System.Data.Linq.Mapping.TableAttribute(Name:="dbo.PictureMetadata")>  _
	Partial Public Class PictureMetadata
		Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
		
		Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
		
		Private _ID As Integer
		
		Private _FileName As String
		
		Private _Folder As String
		
		Private _x As Integer
		
		Private _y As Integer
		
		Private _LastSync As Date
		
		Private _Model As String
		
		Private _Digitized As System.Nullable(Of Date)
		
		Private _ObjectName As String
		
		Private _Text As String
		
		Private _City As String
		
		Private _CountryCode As String
		
		Private _Country As String
		
		Private _Province As String
		
		Private _Sublocation As String
		
		Private _Keywords As String
		
		Private _Copyright As String
		
		Private _Credit As String
		
		Private _EditStatus As String
		
		Private _Urgence As System.Nullable(Of Decimal)
		
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnIDChanging(value As Integer)
    End Sub
    Partial Private Sub OnIDChanged()
    End Sub
    Partial Private Sub OnFileNameChanging(value As String)
    End Sub
    Partial Private Sub OnFileNameChanged()
    End Sub
    Partial Private Sub OnFolderChanging(value As String)
    End Sub
    Partial Private Sub OnFolderChanged()
    End Sub
    Partial Private Sub OnxChanging(value As Integer)
    End Sub
    Partial Private Sub OnxChanged()
    End Sub
    Partial Private Sub OnyChanging(value As Integer)
    End Sub
    Partial Private Sub OnyChanged()
    End Sub
    Partial Private Sub OnLastSyncChanging(value As Date)
    End Sub
    Partial Private Sub OnLastSyncChanged()
    End Sub
    Partial Private Sub OnModelChanging(value As String)
    End Sub
    Partial Private Sub OnModelChanged()
    End Sub
    Partial Private Sub OnDigitizedChanging(value As System.Nullable(Of Date))
    End Sub
    Partial Private Sub OnDigitizedChanged()
    End Sub
    Partial Private Sub OnObjectNameChanging(value As String)
    End Sub
    Partial Private Sub OnObjectNameChanged()
    End Sub
    Partial Private Sub OnTextChanging(value As String)
    End Sub
    Partial Private Sub OnTextChanged()
    End Sub
    Partial Private Sub OnCityChanging(value As String)
    End Sub
    Partial Private Sub OnCityChanged()
    End Sub
    Partial Private Sub OnCountryCodeChanging(value As String)
    End Sub
    Partial Private Sub OnCountryCodeChanged()
    End Sub
    Partial Private Sub OnCountryChanging(value As String)
    End Sub
    Partial Private Sub OnCountryChanged()
    End Sub
    Partial Private Sub OnProvinceChanging(value As String)
    End Sub
    Partial Private Sub OnProvinceChanged()
    End Sub
    Partial Private Sub OnSublocationChanging(value As String)
    End Sub
    Partial Private Sub OnSublocationChanged()
    End Sub
    Partial Private Sub OnKeywordsChanging(value As String)
    End Sub
    Partial Private Sub OnKeywordsChanged()
    End Sub
    Partial Private Sub OnCopyrightChanging(value As String)
    End Sub
    Partial Private Sub OnCopyrightChanged()
    End Sub
    Partial Private Sub OnCreditChanging(value As String)
    End Sub
    Partial Private Sub OnCreditChanged()
    End Sub
    Partial Private Sub OnEditStatusChanging(value As String)
    End Sub
    Partial Private Sub OnEditStatusChanged()
    End Sub
    Partial Private Sub OnUrgenceChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnUrgenceChanged()
    End Sub
    #End Region
		
		Public Sub New()
			MyBase.New
			OnCreated
		End Sub
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ID", AutoSync:=AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true)>  _
		Public Property ID() As Integer
			Get
				Return Me._ID
			End Get
			Set
				If ((Me._ID = value)  _
							= false) Then
					Me.OnIDChanging(value)
					Me.SendPropertyChanging
					Me._ID = value
					Me.SendPropertyChanged("ID")
					Me.OnIDChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_FileName", DbType:="VarChar(256) NOT NULL", CanBeNull:=false)>  _
		Public Property FileName() As String
			Get
				Return Me._FileName
			End Get
			Set
				If (String.Equals(Me._FileName, value) = false) Then
					Me.OnFileNameChanging(value)
					Me.SendPropertyChanging
					Me._FileName = value
					Me.SendPropertyChanged("FileName")
					Me.OnFileNameChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Folder", DbType:="VarChar(512) NOT NULL", CanBeNull:=false)>  _
		Public Property Folder() As String
			Get
				Return Me._Folder
			End Get
			Set
				If (String.Equals(Me._Folder, value) = false) Then
					Me.OnFolderChanging(value)
					Me.SendPropertyChanging
					Me._Folder = value
					Me.SendPropertyChanged("Folder")
					Me.OnFolderChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_x", DbType:="Int NOT NULL")>  _
		Public Property x() As Integer
			Get
				Return Me._x
			End Get
			Set
				If ((Me._x = value)  _
							= false) Then
					Me.OnxChanging(value)
					Me.SendPropertyChanging
					Me._x = value
					Me.SendPropertyChanged("x")
					Me.OnxChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_y", DbType:="Int NOT NULL")>  _
		Public Property y() As Integer
			Get
				Return Me._y
			End Get
			Set
				If ((Me._y = value)  _
							= false) Then
					Me.OnyChanging(value)
					Me.SendPropertyChanging
					Me._y = value
					Me.SendPropertyChanged("y")
					Me.OnyChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_LastSync", DbType:="DateTime NOT NULL")>  _
		Public Property LastSync() As Date
			Get
				Return Me._LastSync
			End Get
			Set
				If ((Me._LastSync = value)  _
							= false) Then
					Me.OnLastSyncChanging(value)
					Me.SendPropertyChanging
					Me._LastSync = value
					Me.SendPropertyChanged("LastSync")
					Me.OnLastSyncChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Model", DbType:="NVarChar(1024)")>  _
		Public Property Model() As String
			Get
				Return Me._Model
			End Get
			Set
				If (String.Equals(Me._Model, value) = false) Then
					Me.OnModelChanging(value)
					Me.SendPropertyChanging
					Me._Model = value
					Me.SendPropertyChanged("Model")
					Me.OnModelChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Digitized", DbType:="DateTime")>  _
		Public Property Digitized() As System.Nullable(Of Date)
			Get
				Return Me._Digitized
			End Get
			Set
				If (Me._Digitized.Equals(value) = false) Then
					Me.OnDigitizedChanging(value)
					Me.SendPropertyChanging
					Me._Digitized = value
					Me.SendPropertyChanged("Digitized")
					Me.OnDigitizedChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_ObjectName", DbType:="VarChar(1024)")>  _
		Public Property ObjectName() As String
			Get
				Return Me._ObjectName
			End Get
			Set
				If (String.Equals(Me._ObjectName, value) = false) Then
					Me.OnObjectNameChanging(value)
					Me.SendPropertyChanging
					Me._ObjectName = value
					Me.SendPropertyChanged("ObjectName")
					Me.OnObjectNameChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Text", DbType:="VarChar(MAX)")>  _
		Public Property Text() As String
			Get
				Return Me._Text
			End Get
			Set
				If (String.Equals(Me._Text, value) = false) Then
					Me.OnTextChanging(value)
					Me.SendPropertyChanging
					Me._Text = value
					Me.SendPropertyChanged("Text")
					Me.OnTextChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_City", DbType:="VarChar(1024)")>  _
		Public Property City() As String
			Get
				Return Me._City
			End Get
			Set
				If (String.Equals(Me._City, value) = false) Then
					Me.OnCityChanging(value)
					Me.SendPropertyChanging
					Me._City = value
					Me.SendPropertyChanged("City")
					Me.OnCityChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_CountryCode", DbType:="Char(3)")>  _
		Public Property CountryCode() As String
			Get
				Return Me._CountryCode
			End Get
			Set
				If (String.Equals(Me._CountryCode, value) = false) Then
					Me.OnCountryCodeChanging(value)
					Me.SendPropertyChanging
					Me._CountryCode = value
					Me.SendPropertyChanged("CountryCode")
					Me.OnCountryCodeChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Country", DbType:="VarChar(1024)")>  _
		Public Property Country() As String
			Get
				Return Me._Country
			End Get
			Set
				If (String.Equals(Me._Country, value) = false) Then
					Me.OnCountryChanging(value)
					Me.SendPropertyChanging
					Me._Country = value
					Me.SendPropertyChanged("Country")
					Me.OnCountryChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Province", DbType:="VarChar(1024)")>  _
		Public Property Province() As String
			Get
				Return Me._Province
			End Get
			Set
				If (String.Equals(Me._Province, value) = false) Then
					Me.OnProvinceChanging(value)
					Me.SendPropertyChanging
					Me._Province = value
					Me.SendPropertyChanged("Province")
					Me.OnProvinceChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Sublocation", DbType:="VarChar(1024)")>  _
		Public Property Sublocation() As String
			Get
				Return Me._Sublocation
			End Get
			Set
				If (String.Equals(Me._Sublocation, value) = false) Then
					Me.OnSublocationChanging(value)
					Me.SendPropertyChanging
					Me._Sublocation = value
					Me.SendPropertyChanged("Sublocation")
					Me.OnSublocationChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Keywords", DbType:="VarChar(MAX)")>  _
		Public Property Keywords() As String
			Get
				Return Me._Keywords
			End Get
			Set
				If (String.Equals(Me._Keywords, value) = false) Then
					Me.OnKeywordsChanging(value)
					Me.SendPropertyChanging
					Me._Keywords = value
					Me.SendPropertyChanged("Keywords")
					Me.OnKeywordsChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Copyright", DbType:="VarChar(1024)")>  _
		Public Property Copyright() As String
			Get
				Return Me._Copyright
			End Get
			Set
				If (String.Equals(Me._Copyright, value) = false) Then
					Me.OnCopyrightChanging(value)
					Me.SendPropertyChanging
					Me._Copyright = value
					Me.SendPropertyChanged("Copyright")
					Me.OnCopyrightChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Credit", DbType:="VarChar(1024)")>  _
		Public Property Credit() As String
			Get
				Return Me._Credit
			End Get
			Set
				If (String.Equals(Me._Credit, value) = false) Then
					Me.OnCreditChanging(value)
					Me.SendPropertyChanging
					Me._Credit = value
					Me.SendPropertyChanged("Credit")
					Me.OnCreditChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_EditStatus", DbType:="VarChar(1024)")>  _
		Public Property EditStatus() As String
			Get
				Return Me._EditStatus
			End Get
			Set
				If (String.Equals(Me._EditStatus, value) = false) Then
					Me.OnEditStatusChanging(value)
					Me.SendPropertyChanging
					Me._EditStatus = value
					Me.SendPropertyChanged("EditStatus")
					Me.OnEditStatusChanged
				End If
			End Set
		End Property
		
		<Global.System.Data.Linq.Mapping.ColumnAttribute(Storage:="_Urgence", DbType:="Decimal(1,0)")>  _
		Public Property Urgence() As System.Nullable(Of Decimal)
			Get
				Return Me._Urgence
			End Get
			Set
				If (Me._Urgence.Equals(value) = false) Then
					Me.OnUrgenceChanging(value)
					Me.SendPropertyChanging
					Me._Urgence = value
					Me.SendPropertyChanged("Urgence")
					Me.OnUrgenceChanged
				End If
			End Set
		End Property
		
		Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
		
		Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		
		Protected Overridable Sub SendPropertyChanging()
			If ((Me.PropertyChangingEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
			End If
		End Sub
		
		Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
			If ((Me.PropertyChangedEvent Is Nothing)  _
						= false) Then
				RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
			End If
		End Sub
	End Class
End Namespace
