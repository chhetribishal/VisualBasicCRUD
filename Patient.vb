Imports System.Data.SqlClient  'Import SQL capabilities

Public Class Patient
    Private strConn As String = "Data Source=DESKTOP-BPNBOGT;Initial Catalog=master;Integrated Security=True"
    Private sqlCon As SqlConnection
    Private Sub Patient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        View()

    End Sub

    Private Sub View()
        Dim strQuery As String
        strQuery = "Select * From PatientTbl"
        sqlCon = New SqlConnection(strConn)

        Using (sqlCon)
            Dim SqlCmd As SqlCommand = New SqlCommand(strQuery, sqlCon)
            sqlCon.Open()
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            da = New SqlDataAdapter(SqlCmd)
            da.Fill(ds, "dd")
            DataGridView1.DataSource = ds.Tables("dd")
            sqlCon.Close()


        End Using
    End Sub

    'CreateTable procedure Used only when there is not exist same table object in DB
    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    sqlCon = New SqlConnection(strConn)
    '    Using (sqlCon)
    '        Dim SqlCmd As New SqlCommand
    '        SqlCmd.Connection = sqlCon
    '        SqlCmd.CommandText = "sp_CreateTable"
    '        SqlCmd.CommandType = CommandType.StoredProcedure

    '        sqlCon.Open()
    '        SqlCmd.ExecuteNonQuery()
    '    End Using
    'End Sub

    Private Sub Insert_Click(sender As Object, e As EventArgs) Handles Insert.Click
        sqlCon = New SqlConnection(strConn)
        Using (sqlCon)
            Dim sqlcmd As New SqlCommand
            sqlcmd.Connection = sqlCon
            sqlcmd.CommandText = "sp_InsertPatient"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Parameters.AddWithValue("Name", txtName.Text)
            sqlcmd.Parameters.AddWithValue("Surname", txtSurname.Text)
            sqlcmd.Parameters.AddWithValue("Age", Integer.Parse(txtAge.Text))
            sqlCon.Open()
            sqlcmd.ExecuteNonQuery()
        End Using
        View()
        Clear()
    End Sub

    Private Sub Update_Click(sender As Object, e As EventArgs) Handles Update.Click
        sqlCon = New SqlConnection(strConn)
        Using (sqlCon)
            Dim Sqlcmd As New SqlCommand
            Sqlcmd.Connection = sqlCon
            Sqlcmd.CommandText = "sp_UpdatePatient"
            Sqlcmd.CommandType = CommandType.StoredProcedure
            Sqlcmd.Parameters.AddWithValue("Name", txtName.Text)
            Sqlcmd.Parameters.AddWithValue("Surname", txtSurname.Text)
            Sqlcmd.Parameters.AddWithValue("Age", txtAge.Text)
            Sqlcmd.Parameters.AddWithValue("PatientID", Convert.ToInt32(txtSearch.Text))
            sqlCon.Open()
            Sqlcmd.ExecuteNonQuery()
        End Using
        View()
        Clear()
    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click
        sqlCon = New SqlConnection(strConn)
        Using (sqlCon)
            Dim Sqlcmd As New SqlCommand
            Sqlcmd.Connection = sqlCon
            Sqlcmd.CommandText = "sp_DeletePatient"
            Sqlcmd.CommandType = CommandType.StoredProcedure
            Sqlcmd.Parameters.AddWithValue("PatientID", Convert.ToInt32(txtSearch.Text))
            sqlCon.Open()
            Sqlcmd.ExecuteNonQuery()
        End Using
        View()
        Clear()
    End Sub

    Private Sub Drop_Click(sender As Object, e As EventArgs) Handles Drop.Click
        sqlCon = New SqlConnection(strConn)
        Using (sqlCon)
            Dim Sqlcmd As New SqlCommand
            Sqlcmd.Connection = sqlCon
            Sqlcmd.CommandText = "sp_DropTable"
            Sqlcmd.CommandType = CommandType.StoredProcedure
            sqlCon.Open()
            Sqlcmd.ExecuteNonQuery()
        End Using
        View()
    End Sub

    Private Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click
        Dim strQuery As String
        strQuery = "Select * From PatientTbl Where PatientID=" + txtSearch.Text + " "
        sqlCon = New SqlConnection(strConn)

        Using (sqlCon)
            Dim SqlCmd As SqlCommand = New SqlCommand(strQuery, sqlCon)
            SqlCmd.Connection = sqlCon
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

    Private Sub Clear()
        txtName.Text = " "
        txtSurname.Text = " "
        txtAge.Text = " "
        txtSearch.Text = " "

    End Sub
End Class
