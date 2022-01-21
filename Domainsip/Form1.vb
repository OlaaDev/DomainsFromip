Imports System.IO, Leaf.Net
Imports System.Threading
Imports System.Text.RegularExpressions

Public Class Form1
    Dim XLIST As String()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim open As New OpenFileDialog() With {.Title = "Load List Ip", .Multiselect = False}
        If open.ShowDialog = DialogResult.OK Then
            XLIST = File.ReadAllLines(open.FileName)
        End If
    End Sub
    Sub Checker(ByVal ip As String)
        Dim https As New HttpRequest
        https.UserAgent = Http.RandomUserAgent
        Dim response As String = https.Get("https://sonar.omnisint.io/reverse/" + ip).ToString
        Dim match As MatchCollection = Regex.Matches(response, """(.*?)""")
        Dim x As String
        For Each olaa As Object In match
            Dim math As Match = CType(olaa, Match)
            Dim domain = math.Groups(1).Value.ToString
            TextBox1.AppendText(domain + vbCrLf)
            x += domain + vbCrLf
        Next
        File.AppendAllText(ip + ".txt", x)
    End Sub
    Sub geter(ByVal domain As String)
        Try
            Dim https As New HttpRequest
            https.UserAgent = Http.RandomUserAgent
            Dim response As String = https.Post("https://check-host.net/ip-info?host=" + domain).ToString
            Dim ip As String = Regex.Match(response, "<td><strong>(.*?)</strong></td>").Groups(1).Value
            TextBox1.AppendText(ip + vbCrLf)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Thread As New Thread(Sub()
                                     Try
                                         If RadioButton2.Checked Then
                                             For Each ip In XLIST
                                                 Checker(ip)
                                             Next
                                         End If
                                         If RadioButton1.Checked Then
                                             For Each domain In XLIST
                                                 geter(domain)
                                             Next
                                         End If
                                     Catch ex As Exception

                                     End Try

                                 End Sub)
        Thread.IsBackground = True
        Thread.Start()
    End Sub
End Class
