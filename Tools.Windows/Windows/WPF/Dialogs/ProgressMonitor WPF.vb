﻿Imports System.Windows, System.Windows.Controls
Imports System.Windows.Input
Imports Tools.WindowsT.IndependentT
Imports Tools.ComponentModelT

#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_MainInfo, Type:=GetType(TextBlock))> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_ProgressBar, Type:=GetType(ProgressBar))> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_Info, Type:=GetType(TextBlock))> _
    <TemplatePart(Name:=ProgressMonitorImplementationControl.PART_Cancel, Type:=GetType(Button))> _
    Public Class ProgressMonitorImplementationControl
        Inherits Windows.Controls.Control
        ''' <summary>Identifies placeholder for main information text block</summary>
        Public Const PART_MainInfo As String = "PART_MainInfo"
        ''' <summary>Identifies placeholder for progress bar</summary>
        Public Const PART_ProgressBar As String = "PART_ProgressBar"
        ''' <summary>Identifies placeholder for secondary information text block</summary>
        Public Const PART_Info As String = "PART_Info"
        ''' <summary>Identifies placeholder for cancel button</summary>
        Public Const PART_Cancel As String = "PART_Cancel"
        ''' <summary>Initializer</summary>
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(ProgressMonitorImplementationControl), New FrameworkPropertyMetadata(GetType(ProgressMonitorImplementationControl)))
            InitializeCommands()
        End Sub
#Region "Commands"
        ''' <summary>Gets command to be executed when button is clicked.</summary>
        ''' <returns>Command to be execued when button is clicked</returns>
        Public Shared ReadOnly Property CancelCommand() As RoutedCommand
            Get
                Return _CancelCommand
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="CancelCommand"/> property</summary>
        Private Shared _CancelCommand As RoutedCommand
        ''' <summary>Initializes comands</summary>
        Private Shared Sub InitializeCommands()
            _CancelCommand = New RoutedCommand("CancelCommand", GetType(ProgressMonitorImplementationControl))
            CommandManager.RegisterClassCommandBinding(GetType(ProgressMonitorImplementationControl), New CommandBinding(_CancelCommand, AddressOf OnCancelButtonClick))
        End Sub
        ''' <summary>Called when <see cref="CancelCommand"/> is invoked</summary>
        ''' <param name="sender">Source of event (<see cref="ProgressMonitorImplementationControl"/>)</param>
        ''' <param name="e">Event arguments</param>
        Private Shared Sub OnCancelButtonClick(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            Dim control As ProgressMonitorImplementationControl = TryCast(sender, ProgressMonitorImplementationControl)
            If control IsNot Nothing Then
                control.OnCancelButtonClick(e)
            End If
        End Sub
        ''' <summary>Called whne button is clicked</summary>
        ''' <param name="e">Event arguments.</param>
        Protected Overridable Sub OnCancelButtonClick(ByVal e As ExecutedRoutedEventArgs)
            'TODO:
        End Sub
#End Region

    End Class

    ''' <summary>This class provides predefined progress monitor with <see cref="ProgressBar"/> for <see cref="BackgroundWorker"/></summary>
    ''' <remarks>See documentation of the <see cref="ProgressMonitor.OnProgressChanged"/> method in order to see rich options for reporting progress.</remarks>
    Public Class ProgressMonitor
        Implements IProgressMonitorUI, INotifyPropertyChanged

#Region "CTors"
        ''' <summary>Default CTor</summary>
        ''' <remarks>Value of the <see cref="BackgroundWorker"/> property is populated with new instance of <see cref="System.ComponentModel.BackgroundWorker"/></remarks>
        ''' <filterpriority>3</filterpriority>
        Public Sub New()
            Me.new(New BackgroundWorker With {.WorkerReportsProgress = True, .WorkerSupportsCancellation = True})
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="bgw"><see cref="BackgroundWorker"/> this form will report progress for</param>
        ''' <exception cref="ArgumentNullException" ><paramref name="bgw"/> is null</exception>
        ''' <filterpriority>1</filterpriority>
        Public Sub New(ByVal bgw As BackgroundWorker)
            If bgw Is Nothing Then Throw (New ArgumentNullException("bgw"))
            Me.BackgroundWorker = bgw
        End Sub
        ''' <summary>CTor with title text and prompt</summary>
        ''' <param name="bgw"><see cref="BackgroundWorker"/> this form will report progress for</param>
        ''' <exception cref="ArgumentNullException" ><paramref name="bgw"/> is null</exception>
        ''' <param name="title">Title text of window (see <see cref="Text"/>)</param>
        ''' <param name="prompt">Prompt text (see <see cref="Prompt"/>)</param>
        ''' <filterpriority>2</filterpriority>
        Public Sub New(ByVal bgw As BackgroundWorker, ByVal title$, Optional ByVal prompt$ = Nothing)
            Me.new(bgw)
            Me.Title = Title
            Me.Prompt = Prompt
        End Sub
#End Region
#Region "Show"
        ''' <summary>Shows progress form and runs worker</summary>
        ''' <param name="bgw">Worker to run</param>
        ''' <param name="Text">Title text of window (see <see  cref="Text"/>)</param>
        ''' <param name="Prompt">Text prompt (see <see cref="Prompt"/>)</param>
        ''' <param name="Owner">Any object that implements <see cref="System.Windows.Forms.IWin32Window"/> or <see cref="Windows.Interop.IWin32Window"/>, or <see cref="Windows.Window"/> that represents the top-level window that will own the modal dialog box.</param>
        ''' <param name="WorkerArgument">Optional parameter for background worker</param>
        ''' <returns>Result of work of <paramref name="bgw"/></returns>
        Public Overloads Shared Function Show(ByVal bgw As BackgroundWorker, ByVal text As String, ByVal prompt As String, Optional ByVal owner As Object = Nothing, Optional ByVal workerArgument As Object = Nothing) As RunWorkerCompletedEventArgs
            Dim mon As New ProgressMonitor(bgw, Text, Prompt)
            mon.WorkerArgument = WorkerArgument
            mon.ShowDialog(Owner)
            Return mon.WorkerResult
        End Function

        ''' <summary>Window being currently shown</summary>
        Private WithEvents window As ProgressMonitorWindow = New ProgressMonitorWindow(Me)

        ''' <summary>Shows window modally</summary>
        ''' <param name="owner">Owner object of dialog. It can be either <see cref="System.Windows.Forms.IWin32Window"/> (e.g. <see cref="Windows.Forms.Form"/>), <see cref="System.Windows.Interop.IWin32Window"/> or <see cref="Windows.Window"/>. When owner is not of recognized type (or is null), it's ignored.</param>
        ''' <returns>True when dialog was closed normally, false if it was closed because of user has cancelled the operation</returns>
        Public Overloads Function ShowDialog(Optional ByVal owner As Object = Nothing) As Boolean Implements IProgressMonitorUI.ShowDialog
            If TypeOf owner Is Windows.Window Then
                Return window.ShowDialog(DirectCast(owner, Window))
            ElseIf TypeOf owner Is Windows.Forms.IWin32Window Then
                Return WindowsT.InteropT.InteropExtensions.ShowDialog(window, DirectCast(owner, Forms.IWin32Window))
            ElseIf TypeOf owner Is Windows.Interop.IWin32Window Then
                Return WindowsT.InteropT.InteropExtensions.ShowDialog(window, DirectCast(owner, Interop.IWin32Window))
            Else
                Return window.ShowDialog
            End If
        End Function
        Private Sub window_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles window.Loaded
            If DoWorkOnShow Then BackgroundWorker.RunWorkerAsync(WorkerArgument)
        End Sub
#End Region
#Region "Properties"
        ''' <summary>Contains value of the <see cref="BackgroundWorker"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private WithEvents bgw As BackgroundWorker
        ''' <summary>Gets <see cref="BackgroundWorker"/> this form repports progress of</summary>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property BackgroundWorker() As BackgroundWorker Implements IProgressMonitorUI.BackgroundWorker
            <DebuggerStepThrough()> Get
                Return bgw
            End Get
            Private Set(ByVal value As BackgroundWorker)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If value IsNot bgw Then
                    bgw = value
                    CanCancel = bgw.WorkerSupportsCancellation
                    ProgressBarShowsProgress = bgw.WorkerReportsProgress
                    OnPropertyChanged("BackgroundWorker")
                End If
            End Set
        End Property

        Private _ProgressBarShowsProgress As Boolean = True
        ''' <summary>Gets or sets current style of progress bar</summary>
        ''' <value>True to show progressbar which indicates percentage progress of operation, false to show progressbar which only indicates that something is going on but does not indicate actual progress</value>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        Public Property ProgressBarShowsProgress() As Boolean Implements IProgressMonitorUI.ProgressBarShowsProgress
            <DebuggerStepThrough()> Get
                Return _ProgressBarShowsProgress
            End Get
            Set(ByVal value As Boolean)
                If ProgressBarShowsProgress <> value Then
                    _ProgressBarShowsProgress = value
                    OnPropertyChanged("ProgressBarShowsProgress")
                End If
            End Set
        End Property
        Private _progress As Boolean = 0
        ''' <summary>Gets or sets current value of <see cref="ProgressBar"/> that reports progress</summary>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is smaller than 0 or greater than 100.</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Progress() As Integer Implements IProgressMonitorUI.Progress
            <DebuggerStepThrough()> Get
                Return _progress
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As Integer)
                If value <> Progress Then
                    If value < 0 OrElse value > 100 Then Throw New ArgumentOutOfRangeException("value")
                    _progress = value
                    OnPropertyChanged("Progress")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CloseOnFinish"/> property</summary>
        Private _CloseOnFinish As Boolean = True
        ''' <summary>Gets or sets value indicating if form automatically closes when <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerCompleted">RunWorkerCompleted</see> event occures.</summary>
        ''' <value>New behavoir. Defalt value is true.</value>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property CloseOnFinish() As Boolean Implements IProgressMonitorUI.CloseOnFinish
            Get
                Return _CloseOnFinish
            End Get
            Set(ByVal value As Boolean)
                If CloseOnFinish <> value Then
                    _CloseOnFinish = value
                    OnPropertyChanged("CloseOnFinish")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="WorkerResult"/> property</summary>
        Private _WorkerResult As RunWorkerCompletedEventArgs
        ''' <summary>Gets or sets result of <see cref="BackgroundWorker"/> work</summary>
        ''' <returns>Null until <see cref="BackgroundWorker.RunWorkerCompleted"/> event occures. That returns its e parameter.</returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property WorkerResult() As RunWorkerCompletedEventArgs Implements IProgressMonitorUI.WorkerResult
            <DebuggerStepThrough()> Get
                Return _WorkerResult
            End Get
            Protected Set(ByVal value As RunWorkerCompletedEventArgs)
                If WorkerResult IsNot value Then
                    _WorkerResult = value
                    OnPropertyChanged("WorkerResult")
                End If
            End Set
        End Property
        Private _Prompt$
        ''' <summary>Gets or sets prompt diaplyed in upper part of form</summary>
        <DefaultValue(GetType(String), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        Public Property Prompt$() Implements IProgressMonitorUI.Prompt
            <DebuggerStepThrough()> Get
                Return _Prompt
            End Get
            Set(ByVal value$)
                If value <> Prompt Then
                    _Prompt = value
                    OnPropertyChanged("Prompt")
                End If
            End Set
        End Property
        Private _Information$
        ''' <summary>Gets or sets informative text displayed below the progress bar</summary>
        <DefaultValue(GetType(String), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        Public Property Information$() Implements IProgressMonitorUI.Information
            <DebuggerStepThrough()> Get
                Return _Information
            End Get
            Set(ByVal value$)
                If value <> _Information$ Then
                    _Information$ = value
                    OnPropertyChanged("Information")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CanCancel"/> property</summary>
        Private _CanCancel As Boolean
        ''' <summary>Gets or sets value indicationg if dialog supports cancelation</summary>
        ''' <value>Default value depends on <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.WorkerSupportsCancellation">WorkerSupportsCancellation</see> in time when it is passed to CTor.</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property CanCancel() As Boolean Implements IProgressMonitorUI.CanCancel
            <DebuggerStepThrough()> Get
                Return _CanCancel
            End Get
            Set(ByVal value As Boolean)
                If value <> CanCancel Then
                    _CanCancel = value
                    OnPropertyChanged("CanCancel")
                    OnPropertyChanged("CancelEnabled")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="DoWorkOnShow"/> property</summary>
        Private _DoWorkOnShow As Boolean = True
        ''' <summary>Gets or sets value indicationg if form will call <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerAsync">RunWorkerAsync</see> when <see cref="Shown"/> event occures.</summary>
        ''' <seelaso cref="WorkerArgument"/>
        ''' <value>Default value is true</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(True)> _
        Public Property DoWorkOnShow() As Boolean Implements IProgressMonitorUI.DoWorkOnShow
            <DebuggerStepThrough()> Get
                Return _DoWorkOnShow
            End Get
            Set(ByVal value As Boolean)
                If value <> DoWorkOnShow Then
                    _DoWorkOnShow = value
                    OnPropertyChanged("DoWorkOnShow")
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="WorkerArgument"/> property</summary>
        Private _WorkerArgument As Object
        ''' <summary>Gets or sets value of argument passed to <see cref="BackgroundWorker.DoWork"/> when <see cref="DoWorkOnShow"/> is true</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
        <DefaultValue(GetType(Object), Nothing)> _
        Public Property WorkerArgument() As Object Implements IProgressMonitorUI.WorkerArgument
            <DebuggerStepThrough()> Get
                Return _WorkerArgument
            End Get
            Set(ByVal value As Object)
                If WorkerArgument IsNot value Then
                    _WorkerArgument = value
                    OnPropertyChanged("WorkerArgument")
                End If
            End Set
        End Property
        Private _Title$ = Tools.WindowsT.WPF.Dialogs.Progress
        ''' <summary>Gets or sets dialog title</summary>
        ''' <value>Title of window showing the progress. Default value is localized word "Progress"</value>
        ''' <returns>Current title of window showing progress</returns>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDefaultValue(GetType(Tools.WindowsT.WPF.Dialogs), "Progress")> _
        Public Property Title As String Implements IProgressMonitorUI.Title
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                If value <> Title Then
                    _Title = value
                    OnPropertyChanged("Title")
                End If
            End Set
        End Property

        ''' <summary>Gets value indicating if Cancle button is enabled</summary>
        ''' <returns>True when both - <see cref="CanCancel"/> and <see cref="CancelPending"/> are enabled</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property CancelEnanled As Boolean
            Get
                Return CanCancel AndAlso Not CancelPending
            End Get
        End Property
        Private _CancelPending As Boolean = False
        ''' <summary>Gets or sets value indicating if user has pressed the Cancel button and dialog is now waiting for process to cancel itself</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Property CancelPending As Boolean
            Get
                Return _CancelPending
            End Get
            Protected Set(ByVal value As Boolean)
                If CancelPending <> value Then
                    _CancelPending = value
                    OnPropertyChanged("CancelPending")
                    OnPropertyChanged("CancelEnabled")
                End If
            End Set
        End Property

#End Region
#Region "Worker events"
        ''' <summary>Handles <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.ProgressChanged">ProgressChanged</see> event</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/></param>
        ''' <param name="e">Event erguments</param>
        ''' <remarks>Default implementation works in following way:
        ''' <list type="bullet">
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than or equal to zero then sets this value to the <see cref="Progress"/> property. Values smaller than zero are ignored.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="Windows.Forms.ProgressBarStyle"/> sets <see cref="ProgressBarShowsProgress"/> to true or false</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="String"/> passes that value to the <see cref="Information"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="Boolean"/> passes that value to the <see cref="CanCancel"/> property.</item>
        ''' <item>If <paramref name="e"/>.<see cref="ProgressChangedEventArgs.UserState">UserState</see> is <see cref="BackgroundWorker"/> (same instance) than <see cref="Reset"/> method is called.</item>
        ''' </list>
        ''' </remarks>
        ''' <exception cref="ArgumentException"><paramref name="e"/>.<see cref="ProgressChangedEventArgs.ProgressPercentage">ProgressPercentage</see> is greater than 100.</exception>
        Protected Overridable Sub OnProgressChanged(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bgw.ProgressChanged
            If e.ProgressPercentage >= 0 Then Progress = e.ProgressPercentage
            If TypeOf e.UserState Is Forms.ProgressBarStyle Then
                ProgressBarShowsProgress = DirectCast(e.UserState, Forms.ProgressBarStyle) = Forms.ProgressBarStyle.Blocks OrElse DirectCast(e.UserState, Forms.ProgressBarStyle) = Forms.ProgressBarStyle.Continuous
            ElseIf TypeOf e.UserState Is String Then
                Information = e.UserState
            ElseIf TypeOf e.UserState Is Boolean Then
                CanCancel = e.UserState
            ElseIf TypeOf e.UserState Is BackgroundWorker AndAlso BackgroundWorker Is e.UserState Then
                Reset()
            End If
        End Sub
        ''' <summary>Handles <see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.RunWorkerCompleted">RunWorkerCompleted</see> event.</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/></param>
        ''' <param name="e">event arguments</param>
        ''' <remarks>This implementation sets <see cref="DialogResult"/> to <see cref="DialogResult.Cancel"/> when <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.Cancelled">Cancelled</see> is true; to <see cref="DialogResult.Abort"/> when <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.[Error]"/> isnot nothing and to <see cref="DialogResult.OK"/> in all other cases. Then sets <see cref="WorkerResult"/>. If <see cref="CloseOnFinish"/> is true, closes the form.</remarks>
        Protected Overridable Sub OnRunWorkerCompleted(ByVal sender As BackgroundWorker, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
            If e.Cancelled Then : window.DialogResult = False
            ElseIf e.Error IsNot Nothing Then : window.DialogResult = False
            Else : window.DialogResult = True : End If
            WorkerResult = e
            If CloseOnFinish Then
                window.Close()
            End If
        End Sub
#End Region

        ''' <summary>Resets the dialog</summary>
        ''' <remarks>In case you want to use the dialog from multiple runs of <see cref="BackgroundWorker"/>, you should call this method before each (excluding first, but you can to) runs of <see cref="BackgroundWorker"/>. Alternativly you can report new run using <see cref="BackgroundWorker.ReportProgress"/> - see <see cref="OnProgressChanged"/>.</remarks>
        Public Overridable Sub Reset() Implements IProgressMonitorUI.Reset
            CancelPending = False
            Progress = 0
            WorkerResult = Nothing
        End Sub

#Region "INotifyPropertyChanged"
        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        ''' <param name="propertyName">The name of the property that changed</param>
        Protected Sub OnPropertyChanged(ByVal propertyName$)
            OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
        End Sub

        ''' <summary>Occurs when a property value changes.</summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
#End Region
    End Class
End Namespace
#End If