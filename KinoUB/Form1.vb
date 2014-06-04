Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Text

Public Class Form1


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TableTableAdapter.Fill(Me.MainDataSet.Movies)
    End Sub

    Private Sub LoadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        Dim por As Integer = downloadmovie(0).Length
        Parallel.For(2500, 4000, Sub(i As Integer)
                                     Dim film As String = downloadmovie(i)
                                     If Not film.Length = por Then
                                         Add(remove(film), i)
                                     End If
                                     SyncLock Me.Text
                                         Me.Text = FormatPercent((i - 2500) / 1500)
                                     End SyncLock
                                 End Sub)
        MainDataSet.AcceptChanges()
    End Sub

    Private Function downloadmovie(i As Integer) As String
        Dim sr As New StreamReader(Downloadfile("http://kino.ub.cz/detailfilmu.php?id=" & i), System.Text.Encoding.GetEncoding(1250))
        While Not sr.ReadLine = "<td VALIGN=TOP WIDTH=""400"" BGCOLOR=""#000000"" class=btxt>"
        End While
        Return StripTags(sr.ReadToEnd)
    End Function

    Private Function Downloadfile(p1 As String) As String
        Dim cesta As String = IO.Path.GetTempFileName()
        My.Computer.Network.DownloadFile(p1, cesta, "", "", False, 10000, True)
        Return cesta
    End Function

    Function StripTags(ByVal html As String) As String
        ' Remove HTML tags.
        Return Regex.Replace(html, "<.*?>", "")
    End Function

    Private Function remove(p1 As String) As Object
        Dim sb As New StringBuilder
        For Each s As String In p1.Split(vbCr, vbCrLf, vbLf, Environment.NewLine)
            If Not s = "" Then
                sb.AppendLine(s)
            End If
        Next
        Return sb.ToString
    End Function

    Private Sub Add(p1 As String, i As Integer)
        'MsgBox(p1)
        Try
            Dim s() As String = p1.Split(vbCr, vbCrLf, vbLf, Environment.NewLine)
            Dim newrow As mainDataSet.MoviesRow
            newrow = MainDataSet.Movies.NewMoviesRow()
            newrow.BeginEdit()
            newrow.Id = i
            newrow.Name = s(0)
            If Not p1.Contains("REZERVACE") Then Throw New Exception("Neni film (Chybí rezervace)")
            newrow.Description = p1.Remove(0, p1.LastIndexOf("REZERVACE") + 9)
            newrow.EndEdit()
            SyncLock MainDataSet
                MainDataSet.Movies.Rows.Add(newrow)
            End SyncLock
        Catch ex As Exception
            Debug.WriteLine(i & " - " & ex.ToString)
        End Try
    End Sub



End Class
