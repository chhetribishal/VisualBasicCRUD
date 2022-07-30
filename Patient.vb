Imports System.Data.SqlClient  'Import SQL capabilities

Public Class Patient
    Private strConn As String = "Data Source=DESKTOP-BPNBOGT;Initial Catalog=master;Integrated Security=True"
    Private sqlCon As SqlConnection
    Private Sub Patient_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim strQuery As String
        strQuery = "Select * From PatientTbl"
        sqlCon = New SqlConnection(strConn)

        Using (sqlCon)
            Dim SqlCmd As SqlCommand = New SqlCommand(strQuery, sqlCon)
            sqlCon.Open()

            Dim SqlReader As SqlDataReader = SqlCmd.ExecuteReader()

            If SqlReader.HasRows Then
                While (SqlReader.Read())
                    txtName.Text = SqlReader.GetString(1)
                    txtSurname.Text = SqlReader.GetString(2)
                    txtAge.Text = SqlReader.GetValue(3)
                End While
            End If
            SqlReader.Close()
        End Using
    End Sub


    'CreateTable
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sqlCon = New SqlConnection(strConn)
        Using (sqlCon)
            Dim SqlCmd As New SqlCommand
            SqlCmd.Connection = sqlCon
            SqlCmd.CommandText = "sp_CreateTable"
            SqlCmd.CommandType = CommandType.StoredProcedure

            sqlCon.Open()
            SqlCmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub Insert_Click(sender As Object, e As EventArgs) Handles Insert.Click

    End Sub
End Class
