Public Class frmNodTrial
    Const KEYEVENTF_KEYUP = &H2
    Dim tmps As String
    Dim usr(500) As String
    Dim pas(500) As String
    Dim i As Integer
    Dim pos As Integer
    Dim r As Integer
    Private Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Long, ByVal dwExtraInfo As Long)
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Randomize()
        WebBrowser1.Navigate("http://www.nod32keys.com/")
        Timer1.Enabled = True
    End Sub
    Public Function RemoveTag(ByVal InpData As String) As String
        Dim intStart, intEnd As Integer
        Dim strExtract, strInput As String
        strInput = InpData
        While Instr(strInput, "<") <> 0
            intStart = Instr(strInput, "<")
            intEnd = Instr(strInput, ">")
            If intStart > 1 Then
                strExtract = Mid(strInput, 1, intStart - 1) & Mid(strInput, intEnd + 1, Len(strInput) - intEnd)
            Else
                strExtract = Mid(strInput, intEnd + 1, Len(strInput) - intEnd)
            End If
            strInput = strExtract
        End While
        Return strInput
    End Function
    Private Function getkeys()
        tmps = TextBox1.Text
        tmps = tmps.ToLower
        tmps = tmps.Replace(" ", "")
        TextBox1.Text = "u" & RemoveTag(tmps.Substring(InStr(tmps, "username:"), InStr(InStr(tmps, "username:"), tmps, vbCrLf) - InStr(tmps, "username:")) & "username:")
        i = 0
        pos = 1
        While Not pos = 0
            If InStr(pos, TextBox1.Text, "password:") = 0 Then
                Exit While
            End If
            usr(i) = TextBox1.Text.Substring(InStr(pos, TextBox1.Text, "username:") + 8, (InStr(pos, TextBox1.Text, "password:") - (InStr(pos, TextBox1.Text, "username:") + 8)) - 1)
            pos = InStr(pos, TextBox1.Text, "password:")
            pas(i) = TextBox1.Text.Substring(InStr(pos, TextBox1.Text, "password:") + 8, (InStr(pos, TextBox1.Text, "username:") - (InStr(pos, TextBox1.Text, "password:") + 8)) - 1)
            pos = InStr(pos, TextBox1.Text, "username:")
            i = i + 1
        End While
        r = Int((i + 1) * Rnd())
        Return 1
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Not WebBrowser1.Document.Window.Frames.Count = 7 Then
            Exit Sub
        Else
            TextBox1.Text = WebBrowser1.Document.Body.OuterHtml
            Timer1.Enabled = False
            getkeys()
            Dim pList() As System.Diagnostics.Process = _
                System.Diagnostics.Process.GetProcesses
            For Each proc As System.Diagnostics.Process In pList
                If proc.ProcessName = "egui" Then
                    Shell("C:\Program Files\ESET\ESET Smart Security\egui.exe")
                    Dim a As Integer
                    Do Until a = 100000
                        a = a + 1
                        Application.DoEvents()
                    Loop
                    SendKeys.SendWait("^u")
                    SendKeys.SendWait(usr(r))
                    SendKeys.SendWait("{TAB}")
                    SendKeys.SendWait(pas(r))
                    SendKeys.SendWait("{ENTER}")
                    SendKeys.SendWait("%{F4}")
                    Do Until a = 100000
                        a = a + 1
                        Application.DoEvents()
                    Loop
                    NodTrial.Visible = False
                    Me.Dispose()
                End If
            Next
        End If
    End Sub
End Class
