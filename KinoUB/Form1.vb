Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'MainDataSet.Table' table. You can move, or remove it, as needed.
        Me.TableTableAdapter.Fill(Me.MainDataSet.Table)

    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        'For i = 2000 To 4000
        MsgBox(stahni(3056)) 'i)
        'Next
    End Sub

    Private Function stahni(i As Integer) As String
        Dim sr As New StreamReader(Download("http://kino.ub.cz/detailfilmu.php?id=" & i), System.Text.Encoding.GetEncoding(1250))
        While Not sr.ReadLine = "<td VALIGN=TOP WIDTH=""400"" BGCOLOR=""#000000"" class=btxt>"
        End While

        Return StripTags(sr.ReadToEnd)
    End Function

    Private Function Download(p1 As String) As String
        Dim cesta As String = IO.Path.GetTempFileName()
        My.Computer.Network.DownloadFile(p1, cesta, "", "", True, 1000, True, FileIO.UICancelOption.ThrowException)
        Return cesta
    End Function

    Function StripTags(ByVal html As String) As String
        ' Remove HTML tags.
        Return Regex.Replace(html, "<.*?>", "")
    End Function
    'remove empty lines
    'add to database
End Class
